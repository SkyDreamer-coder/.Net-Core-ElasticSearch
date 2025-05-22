using Elasticsearch.API.Models.ECommerceModel;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;
using Elasticsearch.API.DTOs;


namespace Elasticsearch.API.Services
{
    public class ECommerceService
    {
        private readonly ECommerceRepository _repository;
        private readonly ILogger<ECommerceService> _logger;

        public ECommerceService(ECommerceRepository repository, ILogger<ECommerceService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> TermLevelQuery(string customerFirstName)
        {
            var res = await _repository.TermLevelQuery(customerFirstName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> TermsQuery(List<string> customerFirstNameList)
        {
            var res = await _repository.TermsQuery(customerFirstNameList);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }
    }
}
