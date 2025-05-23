using Elasticsearch.API.DTOs;
using Elasticsearch.API.Models.ECommerceModel;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;


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
            var res = await _repository.TermLevelQueryAsync(customerFirstName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> TermsQuery(List<string> customerFirstNameList)
        {
            var res = await _repository.TermsQueryAsync(customerFirstNameList);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> PrefixQuery(string input)
        {
            var res = await _repository.PrefixQueryAsync(input);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> RangeQueryAsync(double beginPrice, double endPrice)
        {
            var res = await _repository.RangeQueryAsync(beginPrice, endPrice);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }
    }
}
