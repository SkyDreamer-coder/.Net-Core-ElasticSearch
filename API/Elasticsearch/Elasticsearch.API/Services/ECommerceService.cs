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

        public async Task<ResponseDto<IImmutableList<ECommerce>>> MactchAllQueryAsync()
        {
            var res = await _repository.MactchAllQueryAsync();
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> MultiMatchQueryFullTextAsync(string name)
        {
            var res = await _repository.MultiMatchQueryFullTextAsync(name);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> WildCardQueryAsync(string customerFullName) 
        {
            var res = await _repository.WildCardQueryAsync(customerFullName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> FuzzyQueryAsync(string customerFirstName)
        {
            var res = await _repository.FuzzyQueryAsync(customerFirstName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }


        public async Task<ResponseDto<IImmutableList<ECommerce>>> MatchQueryFullTextAsync(string categoryName)
        {
            var res = await _repository.MatchQueryFullTextAsync(categoryName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> MatchBoolPrefixAsync(string customerFullName)
        {
            var res = await _repository.MatchBoolPrefixAsync(customerFullName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> MatchPhraseAsync(string customerFullName)
        {
            var res = await _repository.MatchPhraseAsync(customerFullName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> CompoundQueryV1Async(string cityName, string categoryName, double totalTaxfullRange, string manufacturer)
        {
            var res = await _repository.CompoundQueryV1Async(cityName, categoryName, totalTaxfullRange, manufacturer);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IImmutableList<ECommerce>>> CompoundQueryV2Async(string customerFullName)
        {
            var res = await _repository.CompoundQueryV2Async(customerFullName);
            return ResponseDto<IImmutableList<ECommerce>>.Success(res, System.Net.HttpStatusCode.OK);
        }


    }
}
