using PortfolioClassLibrary.Classes.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PortfolioClassLibrary.Classes.Abstract
{
    public class BaseWebsitePost
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime LastSubmit { get; set; }
        public List<Image> Images { get; set; }
        public BaseWebsitePost(string title, string body, List<Image> images) 
        {
            ID = Guid.NewGuid();
            Title = title;
            Body = body;
            LastSubmit = DateTime.Now;
            Images = images;
        }

        public BaseWebsitePost(string title, string body)
        {
            ID = Guid.NewGuid();
            Title = title;
            Body = body;
            LastSubmit = DateTime.Now;
            Images = [];
        }

        [JsonConstructor]
        public BaseWebsitePost(Guid ID, string Title, string Body, DateTime LastSubmit, List<Image>Images) 
        {
            this.ID = ID;
            this.Title = Title;
            this.Body = Body;
            this.LastSubmit = LastSubmit;
            this.Images = Images;
        }
    }
}
