using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portfolio.Data;
using PortfolioClassLibrary.Classes.Images;
using PortfolioClassLibrary.Classes.ItProjects;

namespace Portfolio.Controllers
{
    [ApiController]
    public class ItProjectPostController : ControllerBase
    {
        private readonly IDbContextFactory<PortfolioDatabase> _PortfolioFactory;

        public ItProjectPostController(IDbContextFactory<PortfolioDatabase> PortfolioFactory)
        {
            _PortfolioFactory = PortfolioFactory;
        }

        [HttpGet]
        [Route("[controller]/get/all")]
        public string GetAll()
        {
            using var db = _PortfolioFactory.CreateDbContext();
            return JsonConvert.SerializeObject(db.ItProjects.Include(x => x.Images).ToList(),
                Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpGet]
        [Route("[controller]/get/byid")]
        public string GetById(string id)
        {
            using var db = _PortfolioFactory.CreateDbContext();
            var realGuid = new Guid(id);
            return JsonConvert.SerializeObject(db.ItProjects.Where(x => x.ID == realGuid).Include(x => x.Images).FirstOrDefault(),
                Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/post/new")]
        public async Task<Results<BadRequest<string>, Created<ItProjectPost>>> NewPost(ItProjectPost post)
        {
            using var db = _PortfolioFactory.CreateDbContext();

            try
            {
                foreach (Image image in post.Images)
                {
                    if (null != image.Base64String)
                    {
                        await Image.SaveToFile(image.Base64String, image.LocalPath);
                        image.PostId = post.ID;
                        db.Images.Add(image);
                    }
                }

                db.ItProjects.Add(post);
                await db.SaveChangesAsync();

                return TypedResults.Created(post.ID.ToString(), post);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/post/update")]
        public async Task<Results<BadRequest<string>, Ok<ItProjectPost>>> UpdatePost(ItProjectPost post)
        {
            using var db = _PortfolioFactory.CreateDbContext();

            bool validObject = true;

            if (validObject)
            {
                var existingPost = db.ItProjects.Find(post.ID);

                if (null != existingPost)
                {
                    existingPost.Title = post.Title;
                    existingPost.Body = post.Body;
                    existingPost.LastSubmit = DateTime.Now;
                    existingPost.Images = post.Images;

                    try
                    {
                        foreach (Image image in post.Images)
                        {
                            if (null != image.Base64String)
                            {
                                await Image.SaveToFile(image.Base64String, image.LocalPath);
                                image.PostId = post.ID;
                                db.Images.Add(image);
                            }
                        }

                        await db.SaveChangesAsync();

                        return TypedResults.Ok(existingPost);
                    }
                    catch (Exception ex)
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
                var item = db.ItProjects.FirstOrDefault(x => x.ID == guid);

                if (item != null)
                {
                    try
                    {
                        db.ItProjects.Remove(item);
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
