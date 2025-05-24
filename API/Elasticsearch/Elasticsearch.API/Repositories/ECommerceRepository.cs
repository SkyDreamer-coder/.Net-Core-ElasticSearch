using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.API.Models.ECommerceModel;
using Elasticsearch.API.Repositories.Extensions;
using System.Collections.Immutable;

namespace Elasticsearch.API.Repositories
{
    public class ECommerceRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "kibana_sample_data_ecommerce";

        public ECommerceRepository(ElasticsearchClient client)
        {
            _client = client;
        }

        public async Task<IImmutableList<ECommerce>> TermLevelQueryAsync(string customerFirstName)
        {
            #region Unsecured Way
            // unsecured way
            /*var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(q => q.Term(t => t.Field
            ("customer_first_name.keyword").Value(customerFirstName))));*/
            #endregion

            #region Type Secured Way
            // type secured way
            /*var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q.Term(t => t.CustomerFirstName.Suffix("keyword"), customerFirstName)));*/
            #endregion

            #region Advanced Way
            // advanced way
            var termQuery = new TermQuery("customer_first_name.keyword") { Value = customerFirstName,
            CaseInsensitive=true};

            var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termQuery));
            #endregion

            res.ApplyMetaIds();
            //foreach (var item in res.Hits) item.Source.Id = item.Id;

            return res.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> TermsQueryAsync(List<string> customerFirstNameList)
        {
            List<FieldValue> terms = new List<FieldValue>();
            customerFirstNameList.ForEach(x =>
            {
                terms.Add(x);
            });

            #region Legacy Way
            // legacy way
            /*var termsQuery = new TermsQuery()
            {
                Field = "customer_first_name.keyword",
                Terms = new TermsQueryField(terms.AsReadOnly())
            };

            var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termsQuery));*/
            #endregion

            #region Advanced Way
            // advanced way

            var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Size(100)
            .Query(q => q
            .Terms(t => t
            .Field(f => f.CustomerFirstName
            .Suffix("keyword"))
            .Terms(new TermsQueryField(terms.AsReadOnly())))));
            #endregion

            res.ApplyMetaIds();
            //foreach (var item in res.Hits) item.Source.Id = item.Id;

            return res.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> PrefixQueryAsync(string input)
        {
            var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => 
            q.Prefix(p => 
            p.Field(f => 
            f.CustomerFullName.Suffix("keyword"))
            .Value(input))));

            res.ApplyMetaIds();
            //foreach (var item in res.Hits) item.Source.Id = item.Id;

            return res.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> RangeQueryAsync(double beginPrice, double endPrice)
        {
            var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q => q
            .Range(r => r.NumberRange(n => n
            .Field(f => f.TaxFulTotalPrice)
            .Gte(beginPrice).Lte(endPrice)))));

            res.ApplyMetaIds();
            //foreach (var item in res.Hits) item.Source.Id = item.Id;

            return res.Documents.ToImmutableList();
        }

        public async Task<IImmutableList<ECommerce>> MactchAllQueryAsync()
        {
            var res = await _client.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Size(100)
            .Query(q => q.MatchAll()));
            res.ApplyMetaIds();

            return res.Documents.ToImmutableList();
        }
    }
}
