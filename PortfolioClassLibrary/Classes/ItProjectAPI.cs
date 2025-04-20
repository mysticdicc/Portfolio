using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClassLibrary
{
    public class ItProjectAPI(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<ItProjectPost>> GetAll()
        {
            List<ItProjectPost>? items = await _httpClient.GetFromJsonAsync<List<ItProjectPost>>("/itprojectpost/get/all");

            if (null != items)
            {
                return items;
            }
            else
            {
                throw new Exception("No list");
            }
        }

        public async Task AddPost(ItProjectPost post)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<ItProjectPost>("/itprojectpost/post/new", post);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdatePost(ItProjectPost post)
        {
            try
            {
                await _httpClient.PostAsJsonAsync<ItProjectPost>("/itprojectpost/post/update", post);
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
                await _httpClient.PostAsync($"/itprojectpost/delete/post?id={id}", null);
            }
            catch
            {
                throw;
            }
        }
    }
}
