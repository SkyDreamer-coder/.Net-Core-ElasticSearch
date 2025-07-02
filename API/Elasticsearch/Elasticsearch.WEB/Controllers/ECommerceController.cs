using Elasticsearch.WEB.Services;
using Elasticsearch.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.WEB.Controllers
{
    public class ECommerceController : Controller
    {

        private readonly EcommerceService _service;

        public ECommerceController(EcommerceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Search([FromQuery] SearchPageViewModel viewModel)
        {

            var res = await _service.SearchAsync(viewModel.SearchViewModel, viewModel.Page, viewModel.PageSize);

            viewModel.ECommerceViewList = res.list.ToList();
            viewModel.TotalCount = res.totalCount;
            viewModel.PageLinkCount = res.pageLinkCount;

            return View(viewModel);
        }
    }
}
