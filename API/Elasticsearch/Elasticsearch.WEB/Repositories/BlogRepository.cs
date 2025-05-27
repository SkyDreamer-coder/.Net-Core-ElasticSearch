using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.API.Repositories.Extensions;
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

        public async Task<Blog?> SaveAsync(Blog newBlog)
        {
            newBlog.Created = DateTime.Now;

            var response = await _client.IndexAsync(newBlog, x => x.Index(_IndexName).Id(Guid.NewGuid().ToString()));

            if (!response.IsValidResponse) return null;

            newBlog.Id = response.Id;

            return newBlog;

        }

        public async Task<List<Blog>> SearchAsync(string searchText)
        {

            List<Action<QueryDescriptor<Blog>>> ListQuery = new();


            Action<QueryDescriptor<Blog>> matchAll = (q) => q.MatchAll();

            Action<QueryDescriptor<Blog>> matchContentt = (q) => q.Match(m => m.Field(f => f.Content).Query(searchText));

            Action<QueryDescriptor<Blog>> titleMatchBoolPrefix = (q) => q.MatchBoolPrefix(m => m.Field(f => f.Content).Query(searchText));

            Action<QueryDescriptor<Blog>> tagTerm = (q) => q.Term(t => t.Field(f => f.Tags).Value(searchText));

            if (string.IsNullOrEmpty(searchText))
            {
                ListQuery.Add(matchAll);
            }
            else
            {
                ListQuery.Add(matchContentt);
                ListQuery.Add(titleMatchBoolPrefix);
                ListQuery.Add(tagTerm);
            }

            var res = await _client.SearchAsync<Blog>(s => s.Index(_IndexName)
                        .Size(100)
                            .Query(q => q
                                .Bool(b => b
                                    .Should(ListQuery.ToArray()))));

            res.ApplyMetaIds();
            return res.Documents.ToList();
        }
    }
}
