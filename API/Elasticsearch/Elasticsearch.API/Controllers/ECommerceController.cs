using Elasticsearch.API.Services;
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

        [HttpGet]
        public async Task<IActionResult> PrefixQuery(string input)
        {
            return CreateActionResult(await _service.PrefixQuery(input));
        }

        [HttpGet]
        public async Task<IActionResult> RangeQuery(double beginPrice, double endPrice)
        {
            return CreateActionResult(await _service.RangeQueryAsync(beginPrice, endPrice));
        }

        [HttpGet]
        public async Task<IActionResult> MactchAllQuery()
        {
            return CreateActionResult(await _service.MactchAllQueryAsync());
        }

        [HttpGet]
        public async Task<IActionResult> WildCardQuery(string customerFullName)
        {
            return CreateActionResult(await _service.WildCardQueryAsync(customerFullName));
        }

        [HttpGet]
        public async Task<IActionResult> FuzzyQuery(string customerFirstName)
        {
            return CreateActionResult(await _service.FuzzyQueryAsync(customerFirstName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchQueryFullText(string categoryName)
        {
            return CreateActionResult(await _service.MatchQueryFullTextAsync(categoryName));
        }

        [HttpGet]
        public async Task<IActionResult> MatchBoolPrefix(string customerFullName)
        {
            return CreateActionResult(await _service.MatchBoolPrefixAsync(customerFullName));
        }
    }
}
