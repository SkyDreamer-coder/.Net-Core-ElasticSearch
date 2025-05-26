using Elastic.Clients.Elasticsearch;
using Elasticsearch.WEB.Models;

namespace Elasticsearch.WEB.Repositories
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _client;
        private const string _IndexName = "blog";

        public BlogRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<Blog?> Save(Blog newBlog)
        {
            newBlog.Created = DateTime.Now;

            var response = await _client.IndexAsync(newBlog, x => x.Index(_IndexName).Id(Guid.NewGuid().ToString()));

            if (!response.IsValidResponse) return null;

            newBlog.Id = response.Id;

            return newBlog;

        }
    }
}
