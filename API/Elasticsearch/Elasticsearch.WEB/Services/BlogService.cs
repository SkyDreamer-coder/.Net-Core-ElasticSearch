using Elasticsearch.WEB.Models;
using Elasticsearch.WEB.Repositories;
using Elasticsearch.WEB.ViewModels;

namespace Elasticsearch.WEB.Services
{
    public class BlogService
    {
        private readonly BlogRepository _repository;

        public BlogService(BlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> SaveAsync(BlogCreateViewModel blogCreate)
        {

            var newBlog = new Blog()
            {
                Title = blogCreate.Title,
                UserId = Guid.NewGuid(),
                Content = blogCreate.Content,
                Tags = blogCreate.Tags.Split(",")
            };     

            var res = await _repository.SaveAsync(newBlog); 

            if (res is null) return false;

            return true;
        }

    }
}
