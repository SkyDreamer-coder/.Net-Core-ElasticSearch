namespace Elasticsearch.WEB.ViewModels
{
    public class SearchPageViewModel
    {
        public long TotalCount { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public long PageLinkCount { get; set; }
        public List<EcommerceViewModel> ECommerceViewList { get; set; }
        public ECommerceSearchViewModel SearchViewModel { get; set; }

        public int StartPage()
        {
            return Page - 6 <= 0 ? 1 : Page - 6;
        }

        public long EndPage()
        {
            return Page + 6 > PageLinkCount ? PageLinkCount : Page + 6;
        }

        public string CreatePageUrl(HttpRequest req, long page, int pageSize)
        {
            var currentUrl = new Uri($"{req.Scheme}://{req.Host}{req.Path}{req.QueryString}").AbsoluteUri;

            if (currentUrl.Contains("page", StringComparison.OrdinalIgnoreCase))
            {
                currentUrl = currentUrl.Replace($"Page={Page}", $"Page={page}",StringComparison.OrdinalIgnoreCase);

                currentUrl = currentUrl.Replace($"PageSize={PageSize}", $"Page={pageSize}",StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                currentUrl = $"{currentUrl}?Page={page}";
                currentUrl = $"{currentUrl}&PageSize={pageSize}";
            }

            return currentUrl;
        }
    }
}
