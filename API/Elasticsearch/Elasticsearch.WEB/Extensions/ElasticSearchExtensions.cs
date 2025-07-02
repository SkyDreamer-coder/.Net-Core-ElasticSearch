
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;


namespace Elasticsearch.WEB.Extensions
{
    public static class ElasticSearchExtensions
    {

        public static void AddElasticServices(this IServiceCollection services, IConfiguration configuration)
        {            
            var userName = configuration.GetSection("Elastic")["Username"];
            var passWord = configuration.GetSection("Elastic")["Password"];

            var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!));
            settings.Authentication(new BasicAuthentication(userName!, passWord!));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);
        }
    }
}
