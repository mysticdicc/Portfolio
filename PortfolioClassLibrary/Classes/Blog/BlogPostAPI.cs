using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using PortfolioClassLibrary.Classes.ItProjects;

namespace PortfolioClassLibrary.Classes.Blog
{
    public class BlogPostAPI(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<BlogPost>> GetAll()
        {
            List<BlogPost>? items = await _httpClient.GetFromJsonAsync<List<BlogPost>>("/blogpost/get/all");

            if (null != items)
            {
                return items;
            }
            else
            {
                throw new Exception("No list");
            }
        }

        public async Task<BlogPost> GetById(string guid)
        {
            var post = await _httpClient.GetFromJsonAsync<BlogPost>($"/blogpost/get/byid?id={guid}");

            if (null != post)
            {
                return post;
            }
            else
            {
                throw new Exception("No post");
            }
        }

        public async Task AddPost(BlogPost post)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<BlogPost>("/blogpost/post/new", post);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdatePost(BlogPost post)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<BlogPost>("/blogpost/post/update", post);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeletePost(Guid id)
        {
            try
            {
                await _httpClient.PostAsync($"/blogpost/delete/post?id={id}", null);
            }
            catch
            {
                throw;
            }
        }
    }
}
