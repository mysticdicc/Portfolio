using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClassLibrary
{
    public abstract class WebsitePost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set;  }
        required public string Title { get; set; }
        required public string Body { get; set; }
        required public DateTime LastSubmit {  get; set; }
        required public List<string> Base64Images { get; set; }
    }
}
