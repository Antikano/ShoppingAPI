using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Application.DTOs;
using ShoppingAPI.Application.Repositories.Baskett;
using ShoppingAPI.Application.ViewModel.ClosedXML;
using ShoppingAPI.Domain.Entities;
using ShoppingAPI.Domain.Entities.Identity;
using ShoppingAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingAPI.Persistence.Repositories.EntityFramework
{
    public class EfBasketRepository : EfEntityRepositoryBase<Basket>, IBasketRepository
    {
        readonly private ShoppingAPIDbContext context;
        readonly private ICreWorksheet _creWorksheet;
        private readonly UserManager<AppUser> _userManager;
        public EfBasketRepository(ShoppingAPIDbContext _context,
                                  ICreWorksheet creWorksheet,
                                  UserManager<AppUser> userManager) : base(_context)
        {
            context = _context;
            _creWorksheet = creWorksheet;
            _userManager = userManager;
        }

        public async Task<Basket> BasketWithProducts(int id)
        {
            var basket = await context.Baskets
                .Include(c => c.Products)
                .Where(p => p.Id == id)
                .Select(b => new Basket
                {
                    Id = b.Id,
                    Products = b.Products.Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageData = p.ImageData,
                        Price = p.Price,
                        Stock = p.Stock
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return basket;
        }



        public void ExportToDocument(OrderDto order)
        {

            var userId = context.Baskets.FirstOrDefault(c => c.Id == order.basketId).UserId;
            var user = _userManager.Users.FirstOrDefault(c=>c.Id==userId);

            string filePath = "Orders.xlsx";
            string wsName = "Siparişler";

            var worksheetAndWorkbook = _creWorksheet.CreateWorksheet(filePath, wsName);

            var worksheet = worksheetAndWorkbook.Worksheet;
            var workbook = worksheetAndWorkbook.Workbook;

            if (worksheet == null)
            {
                worksheet = workbook.Worksheets.Add(wsName);
                
                worksheet.Cell(1, 1).Value = "Kullanıcı Adı";
                worksheet.Cell(1, 2).Value = "Ürün Adı";
                worksheet.Cell(1, 3).Value = "Sipariş Fiyatı";
                worksheet.Cell(1, 4).Value = "Oluşturma Tarihi";
            }

           
            int rowIndex = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 2;
            var sortedProductNames = order.productNames.ToArray();
            for (int i = 0; i < sortedProductNames.Length; i++)
            {
                worksheet.Cell(rowIndex + i, 1).Value = user.UserName;
                worksheet.Cell(rowIndex + i, 2).Value = sortedProductNames[i];
                worksheet.Cell(rowIndex + i, 3).Value = order.totalPrice.ToString();
                worksheet.Cell(rowIndex + i, 4).Value = order.createdDate.ToString();
            }

            workbook.SaveAs(filePath);
        }


        public async Task updateBasket(int id, UpdatedBasketDto basket)
        {
            var baskett = context.Baskets.Include(c => c.Products).FirstOrDefault(p => p.Id == id);

            baskett.Products = null;
            baskett.Products = new List<Product>();

            foreach (var item in basket.products)
            {
                var product = context.Products.FirstOrDefault(c => c.Id == item);
                if (product != null)
                {
                    baskett.Products.Add(product);
                }
            }

            context.SaveChanges();
        }

    }
}
