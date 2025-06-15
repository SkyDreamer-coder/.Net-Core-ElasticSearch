using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.WEB.ViewModels
{
    public class ECommerceSearchViewModel
    {
        [Display(Name ="Category")]
        public string? Category { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Order start date")]
        [DataType(DataType.Date)]
        public DateTime? OrderDateStart { get; set; }

        [Display(Name = "Order end date")]
        [DataType(DataType.Date)]
        public DateTime? OrderDateEnd { get; set; }

        [Display(Name = "Customer full name")]
        public string? CustomerFullName { get; set;}
    }
}
