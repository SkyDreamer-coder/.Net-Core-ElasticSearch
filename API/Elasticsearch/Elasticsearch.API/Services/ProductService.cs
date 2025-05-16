using Elasticsearch.API.DTOs;
using Elasticsearch.API.Repositories;
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



    }
}
