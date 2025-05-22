
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;


namespace Elasticsearch.API.Extensions
{
    public static class ElasticSearchExtensions
    {

        public static void AddElasticServices(this IServiceCollection services, IConfiguration configuration)
        {
            /*var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));

            var settings = new ConnectionSettings(pool);

            var client = new ElasticClient(settings);*/

            var userName = configuration.GetSection("Elastic")["Username"];
            var passWord = configuration.GetSection("Elastic")["Password"];

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!));
            settings.Authentication(new BasicAuthentication(userName!, passWord!));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
