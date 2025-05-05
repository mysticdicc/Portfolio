using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioClassLibrary.Classes.Abstract;
using PortfolioClassLibrary.Classes.Images;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace PortfolioClassLibrary.Classes.Blog
{
    public class BlogPost : BaseWebsitePost, IWebsitePost
    {
        public BlogPost(string title, string body, List<Image> images) : base(title, body, images) { }
        public BlogPost(string title, string body) : base(title, body) { }

        [JsonConstructor] public BlogPost(Guid ID, string Title, string Body, DateTime LastSubmit, List<Image> Images) : base(ID, Title, Body, LastSubmit, Images) { }
    }
}
