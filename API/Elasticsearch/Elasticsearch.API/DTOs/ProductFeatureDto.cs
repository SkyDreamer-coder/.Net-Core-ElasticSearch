using Elasticsearch.API.Models;

namespace Elasticsearch.API.DTOs
{
    public record ProductFeatureDto(int Width, int Height, Colors Color)
    {
    }
}
