using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestUploadFiles.Models;
using TestUploadFiles.Repo;
using TestUploadFiles.ViewModel;

namespace TestUploadFiles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUploadFiles _uploadFiles;

        public HomeController(ILogger<HomeController> logger, IUploadFiles uploadFiles)
        {
            _logger = logger;
            _uploadFiles = uploadFiles;
        }

        public IActionResult Index()
        {
            return View(_uploadFiles.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(FormProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            _uploadFiles.Create(viewModel);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _uploadFiles.GetById(id);
            if (product != null)
            {
                var productViewModel = new FormProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    //Images = product.ProductImages.Select(image => image.Path).ToList(),
                };
                return View(productViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FormProductViewModel viewModel)
        {
            var item = _uploadFiles.GetById(viewModel.Id);
            if (item == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var isUpdated = _uploadFiles.Update(viewModel);
            if (isUpdated == 0)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateImages(int id)
        {
            var product = _uploadFiles.GetById(id);
            if (product != null)
            {
                var productViewModel = new ImagesViewModel()
                {
                    Id = product.Id,
                    Images = product.ProductImages.Select(image => image.Path).ToList(),
                };
                return View(productViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateImages(ImagesViewModel viewModel)
        {
            var product = _uploadFiles.GetById(viewModel.Id);
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var isUpdated = _uploadFiles.UpdateImages(viewModel);
            if (isUpdated > 0)
                return RedirectToAction("Index");
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteImage(int productId ,string image)
        {
            var isDeleted = _uploadFiles.Delete(productId,image);
            if(isDeleted > 0)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
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
    }
}
