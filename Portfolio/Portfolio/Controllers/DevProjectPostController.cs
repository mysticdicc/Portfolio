

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PortfolioClassLibrary;
using Portfolio.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Controllers
{
    [ApiController]
    public class DevProjectPostController : ControllerBase
    {
        private readonly IDbContextFactory<PortfolioDatabase> _PortfolioFactory;

        public DevProjectPostController(IDbContextFactory<PortfolioDatabase> PortfolioFactory)
        {
            _PortfolioFactory = PortfolioFactory;
        }

        [HttpGet]
        [Route("[controller]/get/all")]
        public string GetAll()
        {
            using var db = _PortfolioFactory.CreateDbContext();
            return JsonConvert.SerializeObject(db.DevProjects.ToList());
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/post/new")]
        public async Task<Results<BadRequest<string>, Created<DevProjectPost>>> NewPost(DevProjectPost post)
        {
            using var db = _PortfolioFactory.CreateDbContext();

            bool validObject = true;

            if (validObject)
            {
                try
                {
                    db.DevProjects.Add(post);
                    await db.SaveChangesAsync();

                    return TypedResults.Created(post.ID.ToString(), post);
                } 
                catch(Exception ex)
                {
                    return TypedResults.BadRequest(ex.Message);
                }
            } 
            else
            {
                return TypedResults.BadRequest("Object invalid");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/post/update")]
        public async Task<Results<BadRequest<string>, Ok<DevProjectPost>>> UpdatePost(DevProjectPost post)
        {
            using var db = _PortfolioFactory.CreateDbContext();

            bool validObject = true;

            if (validObject)
            {
                var existingPost = db.DevProjects.Find(post.ID);

                if (null != existingPost)
                {
                    existingPost.Title = post.Title;
                    existingPost.Body = post.Body;
                    existingPost.LastSubmit = DateTime.Now;
                    existingPost.Base64Images = post.Base64Images;

                    try
                    {
                        await db.SaveChangesAsync();

                        return TypedResults.Ok(existingPost);
                    }
                    catch(Exception ex)
                    {
                        return TypedResults.BadRequest(ex.Message);
                    }
                }
                else
                {
                    return TypedResults.BadRequest($"Cannot find post with {post.ID}");
                }
            } 
            else
            {
                return TypedResults.BadRequest("Object invalid");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/delete/post")]
        public async Task<Results<BadRequest<string>, Ok<string>>> DeletePost(string id)
        {
            using var db = _PortfolioFactory.CreateDbContext();
            Guid? guid = Guid.Parse(id);

            if (guid != null)
            {
                var item = db.DevProjects.FirstOrDefault(x => x.ID == guid);

                if (item != null)
                {
                    try
                    {
                        db.DevProjects.Remove(item);
                        await db.SaveChangesAsync();

                        return TypedResults.Ok(id);
                    }
                    catch (Exception ex)
                    {
                        return TypedResults.BadRequest(ex.Message);
                    }
                } 
                else
                {
                    return TypedResults.BadRequest("No item with this GUID in db context");
                }
            }
            else
            {
                return TypedResults.BadRequest("No GUID provided");
            }
        }
    }
}
