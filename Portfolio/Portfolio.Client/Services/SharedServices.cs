using PortfolioClassLibrary.Classes.Blog;
using PortfolioClassLibrary.Classes.DevProjects;
using PortfolioClassLibrary.Classes.Images;
using PortfolioClassLibrary.Classes.ItProjects;

namespace Portfolio.Client.Services
{
    public class SharedServices
    {
        public static void Register(IServiceCollection services, Uri baseAddress)
        {
            services.AddTransient<ImageAPI>();
            services.AddHttpClient<ImageAPI>(client =>
            {
                client.BaseAddress = baseAddress;
            });
            services.AddTransient<BlogPostAPI>();
            services.AddHttpClient<BlogPostAPI>(client =>
            {
                client.BaseAddress = baseAddress;
            });
            services.AddTransient<DevProjectAPI>();
            services.AddHttpClient<DevProjectAPI>(client =>
            {
                client.BaseAddress = baseAddress;
            });
            services.AddTransient<ItProjectAPI>();
            services.AddHttpClient<ItProjectAPI>(client =>
            {
                client.BaseAddress = baseAddress;
            });
        }
    }
}
