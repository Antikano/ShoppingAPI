﻿using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.Abstraction.Services;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Productt;
using ShoppingAPI.Application.ViewModel.ClosedXML;
using ShoppingAPI.Domain.DTOs;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Persistence.Contexts;
using System.Linq.Expressions;


namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        readonly private ShoppingAPIDbContext context;
        readonly private ICreWorksheet _creWorksheet;
        //readonly private ICacheService _cacheService;
        public EfProductRepository(ShoppingAPIDbContext _context, ICreWorksheet creWorksheet/*, ICacheService cacheService*/) : base(_context)
        {
            context = _context;
            _creWorksheet = creWorksheet;
            //_cacheService = cacheService;
        }

        public IQueryable<ProductWithCategoryNamesDTO> GetProductsWithCategory(Expression<Func<Product, bool>> filter = null)
        {
            //var products = _cacheService.GetOrAdd<List<ProductWithCategoryNamesDTO>>("Products", () =>
            //{
                var productss = context.Products
               .Select(p => new ProductWithCategoryNamesDTO
               {
                   Id = p.Id,
                   Name = p.Name,
                   Stock = p.Stock,
                   Price = p.Price,
                   ImageData = p.ImageData,
                   Description = p.Description,
                   CategoryNames = p.Categories.Select(c => c.Name).ToList(),
                   CreatedDate = p.CreatedDate,
                   UpdatedDate = p.UpdatedDate
               });

                //return productss.ToList();
            //});

            return productss;
        }

        public async Task AddProductWithCategories(CreatedProductDto p)
        {
            var product = new Product()
            {
                Name = p.Name,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                ImageData = p.ImageData,
                Categories = new List<Category>() { }
            };

            foreach (var item in p.categoriesName)
            {
                var canc = context.Categories.FirstOrDefault(c => c.Name == item);

                if (canc is not null)
                    product.Categories.Add(canc);
            }

            await context.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public void ExportToDocument()
        {
            var updatedProducts = context.Products.Where(p => p.UpdatedDate != new DateTime(0001, 01, 01))
                .OrderByDescending(p => p.UpdatedDate)
                .ToList();

            string filePath = "UpdatedProducts.xlsx";
            string wsName = "Güncellenmiş Ürünler";

            var worksheetAndWorkbook = _creWorksheet.CreateWorksheet(filePath, wsName);

            var worksheet = worksheetAndWorkbook.Worksheet;
            var workbook = worksheetAndWorkbook.Workbook;

            if (worksheet != null)
            {
                worksheet.Rows().Delete();
            }
            else
            {
                worksheet = workbook.Worksheets.Add(wsName);
            }

            int rowIndex = 2;
            foreach (var product in updatedProducts)
            {
                if (rowIndex == 2)
                {
                    worksheet.Cell(1, 1).Value = "Ürün Adı";
                    worksheet.Cell(1, 2).Value = "Ürün Stoğu";
                    worksheet.Cell(1, 3).Value = "Ürün Fiyatı";
                    worksheet.Cell(1, 4).Value = "Güncellenme Tarihi";
                }
                worksheet.Cell(rowIndex, 1).Value = product.Name;
                worksheet.Cell(rowIndex, 2).Value = product.Stock.ToString();
                worksheet.Cell(rowIndex, 3).Value = product.Price.ToString();
                worksheet.Cell(rowIndex, 4).Value = product.UpdatedDate.ToString();
                rowIndex++;
            }

            workbook.SaveAs(filePath);
        }

        public async Task<bool> UpdateProduct(int id, UpdatedProductDto updatedProductDto)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id); 

            if (existingProduct == null)
            {
                throw new ArgumentNullException("categories", "Category list is null.");
            }

            existingProduct.Name = updatedProductDto.Name;
            existingProduct.Description = updatedProductDto.Description;
            existingProduct.Stock = updatedProductDto.Stock;
            existingProduct.Price = updatedProductDto.Price;
            existingProduct.ImageData = updatedProductDto.imageData;

            await context.SaveChangesAsync();
            ExportToDocument();

            return true;
        }
    }
}
