using Microsoft.AspNetCore.Mvc;

namespace TestUploadFiles.Controllers
{
    public class RemoteController : Controller
    {
        public IActionResult Index()
        {
        //hello
            return View();
        }
        public IActionResult Get()
        {
            return View();
        }
    }
}
