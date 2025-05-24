using Elastic.Clients.Elasticsearch;
using System.Reflection;

namespace Elasticsearch.API.Repositories.Extensions
{
    public static class MetaIdExtension
    {

        /*public static IEnumerable<string> AddMetaId(this SearchResponse<T> response)
        {
            foreach (var item in response.Hits) yield return item.Id;
        }*/

        public static void ApplyMetaIds<T>(this SearchResponse<T> response, string propertyName = "Id") where T : class
        {
            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null || !propertyInfo.CanWrite)
                throw new InvalidOperationException($"'{typeof(T).Name}' tipinde '{propertyName}' adında yazılabilir bir property bulunamadı.");

            foreach (var hit in response.Hits)
            {
                var document = hit.Source;
                propertyInfo.SetValue(document, hit.Id);
            }
        }
    }
}
