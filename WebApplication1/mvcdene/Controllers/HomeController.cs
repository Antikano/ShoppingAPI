using dege;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvcdene.Models;
using System.Diagnostics;

namespace mvcdene.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly private DenemContext context;

        public HomeController(ILogger<HomeController> logger, DenemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Privacy(ProductDto productDto)
        {
            if (productDto.Photo != null && productDto.Photo.Length > 0)
            {
                byte[] photoBytes;

                using (var memoryStream = new MemoryStream())
                {
                    await productDto.Photo.CopyToAsync(memoryStream);
                    photoBytes = memoryStream.ToArray();
                }

                // Yeni bir Product oluşturun
                var product = new Product
                {
                    Name = productDto.Name,
                    Photo = photoBytes,
                    // Diğer özellikleri ayarlayın
                };

                // Product'u veritabanına kaydedin
                context.Products.Add(product);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(productDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}