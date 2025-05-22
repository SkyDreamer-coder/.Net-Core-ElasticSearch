using Elasticsearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ECommerceController : BaseController
    {
        private readonly ECommerceService _service;

        public ECommerceController(ECommerceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> TermLevelQuery(string customerFirstName)
        {
            return CreateActionResult(await _service.TermLevelQuery(customerFirstName));
        }

        [HttpPost]
        public async Task<IActionResult> TermsQuery(List<string> customerFirstNameList)
        {
            return CreateActionResult(await _service.TermsQuery(customerFirstNameList));
        }
    }
}
