using System.Text.Json.Serialization;

namespace Elasticsearch.WEB.ViewModels
{
    public class EcommerceViewModel
    {      
        public string Id { get; set; } = null!;

        public string CustomerFirstName { get; set; } = null!;

        public string CustomerLastName { get; set; } = null!;

        public string CustomerFullName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public double TaxFulTotalPrice { get; set; }

        public string Category { get; set; } = null!;

        public int OrderId { get; set; }

        public string OrderDate { get; set; }
    }
}
