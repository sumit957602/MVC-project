using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadVendorFile(IFormFile? vendorFile)
        {
            if (vendorFile is null || vendorFile.Length == 0)
            {
                return BadRequest(new { message = "A valid Excel file is required." });
            }

            var fileExtension = Path.GetExtension(vendorFile.FileName);
            var allowedExtensions = new[] { ".xls", ".xlsx" };

            if (string.IsNullOrWhiteSpace(fileExtension) || !allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
            {
                return BadRequest(new { message = "Only .xls and .xlsx files are allowed." });
            }

            var webRoot = _environment.WebRootPath ?? string.Empty;
            var filesDirectory = Path.Combine(webRoot, "files");

            Directory.CreateDirectory(filesDirectory);

            var safeFileName = Path.GetFileNameWithoutExtension(vendorFile.FileName);
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
            var finalFileName = $"{safeFileName}_{timestamp}{fileExtension}";
            var savePath = Path.Combine(filesDirectory, finalFileName);

            await using (var stream = System.IO.File.Create(savePath))
            {
                await vendorFile.CopyToAsync(stream);
            }

            return Ok(new { message = $"File '{vendorFile.FileName}' uploaded successfully." });
        }
    }
}
