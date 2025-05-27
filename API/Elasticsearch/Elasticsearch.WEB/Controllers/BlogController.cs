using Elasticsearch.WEB.Services;
using Elasticsearch.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Elasticsearch.WEB.Models;

namespace Elasticsearch.WEB.Controllers
{
    public class BlogController : Controller
    {

        private readonly BlogService _service;

        public BlogController(BlogService service)
        {
            _service = service;
        }

        public IActionResult Save()
        {
            return View();
        }

        public async Task<IActionResult> Search()
        {
            return View(await _service.SearchAsync(string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel model)
        {
            var res = await _service.SaveAsync(model);
            if (!res)
            {
                TempData["result"] = "kayıt başarısız";
                return RedirectToAction(nameof(BlogController.Save));
            }
            TempData["result"] = "kayıt başarılı";
            return RedirectToAction(nameof(BlogController.Save));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            ViewBag.searchText = searchText;
            return View(await _service.SearchAsync(searchText));
        }
    }
}
