using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioClassLibrary.Classes.Images;

namespace PortfolioClassLibrary.Classes.Abstract
{
    public interface IWebsitePost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime LastSubmit { get; set; }
        public List<Image> Images { get; set; }
    }
}
