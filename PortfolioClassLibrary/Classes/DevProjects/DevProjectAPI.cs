using PortfolioClassLibrary.Classes.ItProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClassLibrary.Classes.DevProjects
{
    public class DevProjectAPI(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<DevProjectPost>> GetAll()
        {
            List<DevProjectPost>? items = await _httpClient.GetFromJsonAsync<List<DevProjectPost>>("/devprojectpost/get/all");
            
            if (null != items)
            {
                return items;
            } 
            else
            {
                throw new Exception("No list");
            }
        }

        public async Task<DevProjectPost> GetById(string guid)
        {
            var post = await _httpClient.GetFromJsonAsync<DevProjectPost>($"/devprojectpost/get/byid?id={guid}");

            if (null != post)
            {
                return post;
            }
            else
            {
                throw new Exception("No post");
            }
        }

        public async Task AddPost(DevProjectPost post)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<DevProjectPost>("/devprojectpost/post/new", post);
            } 
            catch
            {
                throw;
            }
        }

        public async Task UpdatePost(DevProjectPost post)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<DevProjectPost>("/devprojectpost/post/update", post);
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
                await _httpClient.PostAsync($"/devprojectpost/delete/post?id={id}", null);
            } 
            catch
            {
                throw;
            }
        }
    }
}
