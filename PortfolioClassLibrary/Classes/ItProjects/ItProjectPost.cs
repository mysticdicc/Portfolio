﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioClassLibrary.Classes.Abstract;
using PortfolioClassLibrary.Classes.Images;
using System.Text.Json.Serialization;

namespace PortfolioClassLibrary.Classes.ItProjects
{
    public class ItProjectPost : BaseWebsitePost
    {
        public ItProjectPost(string title, string body, List<Image> images) : base(title, body, images) { }
        public ItProjectPost(string title, string body) : base(title, body) { }
        public ItProjectPost() : base() { }
        [JsonConstructor] public ItProjectPost(Guid ID, string Title, string Body, DateTime LastSubmit, List<Image> Images) : base(ID, Title, Body, LastSubmit, Images) { }
        public ItProjectPost(IWebsitePost post)
        {
            Title = post.Title;
            Body = post.Body;
            ID = post.ID;
            LastSubmit = post.LastSubmit;
            Images = post.Images;
        }
    }
}
