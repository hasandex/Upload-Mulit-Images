using Microsoft.AspNetCore.Mvc;

namespace TestUploadFiles.Controllers
{
    public class RemoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
