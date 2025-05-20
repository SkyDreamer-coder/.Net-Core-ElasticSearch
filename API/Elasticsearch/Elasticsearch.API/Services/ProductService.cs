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

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
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
                (x.Feature!.Width, x.Feature.Height, x.Feature.Color)));
                }
                
            }
            
            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
        }


    }
}
