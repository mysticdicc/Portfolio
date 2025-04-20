using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClassLibrary
{
    public class ItProjectPost : IWebsitePost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        required public string Title { get; set; }
        required public string Body { get; set; }
        required public DateTime LastSubmit { get; set; }
        required public List<string> Base64Images { get; set; }

        bool IsValidObject(ItProjectPost post)
        {
            bool valid;

            if (null != Title)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            if (null != Body)
            {
                valid = true;
            } 
            else
            {
                valid = false;
            }

            if (null != Base64Images)
            {
                valid = true;
            } 
            else
            {
                valid = false;
            }

            if (valid)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
    }
}
