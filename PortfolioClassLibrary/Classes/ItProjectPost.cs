using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioClassLibrary
{
    public class ItProjectPost : WebsitePost
    {

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
