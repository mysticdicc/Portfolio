using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PortfolioClassLibrary.Classes.Abstract;
using PortfolioClassLibrary.Classes.Images;
using System.Text.Json.Serialization;

namespace PortfolioClassLibrary.Classes.DevProjects
{
    public class DevProjectPost : BaseWebsitePost, IWebsitePost
    {
        public DevProjectPost(string title, string body, List<Image> images) : base(title, body, images) { }
        public DevProjectPost(string title, string body) : base(title, body) { }

        [JsonConstructor] public DevProjectPost(Guid ID, string Title, string Body, DateTime LastSubmit, List<Image> Images) : base(ID, Title, Body, LastSubmit, Images) { }
    }
}
