using Elasticsearch.API.Models;
using Nest;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repositories
{
    public class ProductRepository
    {

        private readonly ElasticClient _client;
        private const string indexName = "products";

        public ProductRepository(ElasticClient client)
        {
            _client = client;
        }

        public async Task<Product?> SaveAsync(Product product)
        {
            product.Created = DateTime.Now;

            var response = await _client.IndexAsync(product, x => x.Index(indexName).Id(Guid.NewGuid().ToString()));

            if(!response.IsValid) return null;

            product.Id = response.Id;
            
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {

            var res = await _client.SearchAsync<Product>(
                s => s.Index(indexName)
                .Query(q => q.MatchAll()));

            foreach(var hit in res.Hits) hit.Source.Id = hit.Id;

            return res.Documents.ToImmutableList();

        }
        
        public async Task<Product?> GetByIdAsync(string id)
        {
            var res = await _client.GetAsync<Product>(id, x => x.Index(indexName));

            if (!res.IsValid)
            {
                return null;
            }

            res.Source.Id = res.Id;
            return res.Source;
        }
    }
}
