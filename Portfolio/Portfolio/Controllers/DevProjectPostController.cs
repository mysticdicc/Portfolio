

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using PortfolioClassLibrary.Classes.DevProjects;
using Portfolio.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioClassLibrary.Classes.Images;

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
            return JsonConvert.SerializeObject(db.DevProjects.Include(x => x.Images).ToList(), 
                Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpGet]
        [Route("[controller]/get/byid")]
        public string GetById(string id)
        {
            using var db = _PortfolioFactory.CreateDbContext();
            var realGuid = new Guid(id);
            return JsonConvert.SerializeObject(db.DevProjects.Where(x => x.ID == realGuid).Include(x => x.Images).FirstOrDefault(),
                Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]/post/new")]
        public async Task<Results<BadRequest<string>, Created<DevProjectPost>>> NewPost(DevProjectPost post)
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

                db.DevProjects.Add(post);
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
        public async Task<Results<BadRequest<string>, Ok<DevProjectPost>>> UpdatePost(DevProjectPost post)
        {
            using var db = _PortfolioFactory.CreateDbContext();

            bool validObject = true;

            if (validObject)
            {
                var existingPost = await db.DevProjects.Include(x => x.Images).Where(x => x.ID == post.ID).FirstOrDefaultAsync();

                if (null != existingPost)
                {
                    existingPost.Title = post.Title;
                    existingPost.Body = post.Body;
                    existingPost.LastSubmit = DateTime.Now;

                    List<Guid> imageIds = post.Images.Select(x => x.Id).ToList();

                    try
                    {
                        foreach (Image image in existingPost.Images)
                        {
                            if (!imageIds.Contains(image.Id))
                            {
                                await Image.DeleteFile(image.LocalPath);
                                db.Images.Remove(image);
                            }
                        }

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
                var item = await db.DevProjects.Include(x => x.Images).Where(x => x.ID == guid).FirstOrDefaultAsync();

                if (item != null)
                {
                    try
                    {
                        foreach (Image image in item.Images)
                        {
                            await Image.DeleteFile(image.LocalPath);
                            var dbImage = db.Images.Find(image.Id);

                            if (null != dbImage)
                            {
                                db.Images.Remove(dbImage);
                            }
                        }

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
