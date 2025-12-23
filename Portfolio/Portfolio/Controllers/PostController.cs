using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using PortfolioClassLibrary.Classes.Abstract;

namespace Portfolio.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IDbContextFactory<PortfolioDatabase> _PortfolioFactory;

        public PostController(IDbContextFactory<PortfolioDatabase> PortfolioFactory)
        {
            _PortfolioFactory = PortfolioFactory;
        }

        [HttpGet]
        [Route("[controller]/get/byid")]
        public string GetById(string id)
        {
            using var db = _PortfolioFactory.CreateDbContext();
            var realGuid = new Guid(id);

            IWebsitePost? post = db.BlogPosts.Find(realGuid);

            if (post == null)
            {
                post = db.ItProjects.Find(realGuid);

                if (post == null)
                {
                    post = db.DevProjects.Find(realGuid);

                    if (post == null)
                    {
                        return "{}";
                    }
                }
            }

            var json = JsonConvert.SerializeObject(post, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return json;
        }
    }

}
