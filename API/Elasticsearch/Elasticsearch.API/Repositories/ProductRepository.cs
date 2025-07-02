using Elastic.Clients.Elasticsearch;
using Elasticsearch.API.DTOs;
using Elasticsearch.API.Models;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repositories
{
    public class ProductRepository
    {

        private readonly ElasticsearchClient _client;
        private const string indexName = "products";

        public ProductRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.Now;

            var response = await _client.IndexAsync(product, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));

            if(!response.IsValidResponse) return null;

            product.Id = response.Id;
            
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            var res = await _client.SearchAsync<Product>(
                s => s.Index(indices: indexName)
                .Query(q => q.MatchAll())) ;

            foreach(var hit in res.Hits) hit.Source.Id = hit.Id;

            return res.Documents.ToImmutableList();

        }
        
        public async Task<Product?> GetByIdAsync(string id)
        {
            var res = await _client.GetAsync<Product>(id, x => x.Index(indexName));

            if (!res.IsValidResponse)
            {
                return null;
            }

            res.Source.Id = res.Id;
            return res.Source;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto updateDto)
        {
            var res = await _client.UpdateAsync<Product, ProductUpdateDto>(indexName, updateDto.Id, x => x.Doc(updateDto));

            return res.IsValidResponse;
        }     

        public async Task<DeleteResponse> DeleteAsync(string id)
        {
            var res = await _client.DeleteAsync<Product>(id, x => x.Index(indexName));

            return res;
        }
    }
}
