using Elasticsearch.WEB.Repositories;
using Elasticsearch.WEB.ViewModels;

namespace Elasticsearch.WEB.Services
{
    public class EcommerceService
    {
        private readonly ECommerceRepository _repository;

        public EcommerceService(ECommerceRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<EcommerceViewModel> list, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchViewModel viewModel, int page, int pageSize)
        {
            var (eCommerceList,totalCount) =await _repository.SearchAsync(viewModel, page, pageSize);

            long pageLinkCount = 0;

            if ((totalCount % pageSize) == 0)
                pageLinkCount = totalCount / pageSize;
            else
                pageLinkCount = (totalCount / pageSize) + 1;

            var eCommerceListViewModel = eCommerceList.Select(x => new EcommerceViewModel()
            {
                Category = string.Join(",", x.Category),
                CustomerFullName = x.CustomerFullName,
                CustomerFirstName = x.CustomerFirstName,
                CustomerLastName = x.CustomerLastName,
                OrderDate = x.OrderDate.ToShortDateString(),
                Gender = x.Gender.ToLower(),
                Id = x.Id,
                OrderId = x.OrderId,
                TaxFulTotalPrice = x.TaxFulTotalPrice,

            });

            return (list: eCommerceListViewModel, totalCount: totalCount, pageLinkCount: pageLinkCount);
        }
    }
}
