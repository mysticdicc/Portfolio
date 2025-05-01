using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioClassLibrary.Classes.Images;
using Portfolio.Data;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Controllers
{
    [Authorize]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IDbContextFactory<PortfolioDatabase> _PortfolioFactory;

        public ImageController(IDbContextFactory<PortfolioDatabase> portfolioFactory)
        {
            _PortfolioFactory = portfolioFactory;
        }

        [HttpGet]
        [Route("[controller]/get/all")]
        public string GetAll()
        {
            using var db = _PortfolioFactory.CreateDbContext();
            return JsonConvert.SerializeObject(db.Images.ToList());
        }

        [HttpGet]
        [Route("[controller]/get/byid")]
        public string GetById(Guid guid)
        {
            using var db = _PortfolioFactory.CreateDbContext();
            var image = db.Images.Where(x => x.Id == guid).FirstOrDefault();
            return JsonConvert.SerializeObject(image);
        }

        [HttpPost]
        [Route("[controller]/post/new")]
        public async Task<Results<Ok<Image>, BadRequest<string>>> PostNew(Image image)
        {
            if (null != image.Base64String)
            {
                using var db = _PortfolioFactory.CreateDbContext();
                db.Images.Add(image);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex) 
                {
                    return TypedResults.BadRequest(ex.Message);
                }

                try
                {
                    await Image.SaveToFile(image.Base64String, image.LocalPath);
                    return TypedResults.Ok(image);
                }
                catch (Exception ex)
                {
                    return TypedResults.BadRequest(ex.Message);
                }
            } 
            else
            {
                return TypedResults.BadRequest("Base64 image string not provided");
            }
        }

        [HttpPost]
        [Route("[controller]/post/update")]
        public async void UpdateExisting(Image uploadImage) 
        {
            using var db = _PortfolioFactory.CreateDbContext();
            var image = db.Images.Find(uploadImage.Id);

            if (null != image) 
            {
                image.Name = uploadImage.Name;
                image.RemotePath = uploadImage.RemotePath;
            }

            await db.SaveChangesAsync();
        }
    }
}
