using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elasticsearch.API.DTOs;
using Elasticsearch.API.Models;
using Elasticsearch.API.Repositories;
using System.Collections.Immutable;
using System.Net;

namespace Elasticsearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto req)
        {

            var res = await _repository.SaveAsync(req.CreateProduct());

            if (res == null) {
                return ResponseDto<ProductDto>.Fail(new List<string> { "bir hata meydana geldi." },HttpStatusCode.InternalServerError);
            }

            return ResponseDto<ProductDto>.Success(res.CreateDto(), HttpStatusCode.Created);

        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {

            var products = await _repository.GetAllAsync();
            var productListDto = new List<ProductDto>();

            /*var productListDto = products.Select(x => new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto
                (x.Feature.Width, x.Feature.Height, x.Feature.Color))).ToList();*/

            foreach(var x in products)
            {
                if(x is null)
                {
                    productListDto.Add(new ProductDto(x!.Id, x.Name, x.Price, x.Stock, null));
                }
                else
                {
                    productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto
                (x.Feature!.Width, x.Feature.Height, x.Feature.Color.ToString())));
                }
                
            }
            
            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);

            if(hasProduct == null)
            {
                return ResponseDto<ProductDto>.Fail("veri bulunamadı", HttpStatusCode.NotFound);
            }
            return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateDto)
        {            
            var hasCompleted = await _repository.UpdateAsync(updateDto);

            if (!hasCompleted)
            {
                return ResponseDto<bool>.Fail(new List<string> { "güncelleme esnasında hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(hasCompleted, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var deleteResponse = await _repository.DeleteAsync(id);

            if (!deleteResponse.IsValidResponse && deleteResponse.Result == Result.NotFound)
            {
                return ResponseDto<bool>.Fail(new List<string> { "silinmek istenen item bulunamadı." }, HttpStatusCode.NotFound);
            }
            if (!deleteResponse.IsValidResponse)
            {
                deleteResponse.TryGetOriginalException(out var originalException);
                _logger.LogError(originalException, deleteResponse.ElasticsearchServerError?.Error.ToString());
                return ResponseDto<bool>.Fail(new List<string> { "silme esnasında hata meydana geldi." }, HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }
    }
}
