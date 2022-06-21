using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace FabrikamFiber.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string GetDisplayName(this HtmlHelper helper, IPrincipal user)
        {
            if (helper == null || user == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(user.Identity.Name))
            {
                return user.Identity.Name;
            }

            var claimsPrincipal = user as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                var displayNameClaim = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "name");
                if (displayNameClaim != null)
                {
                    return displayNameClaim.Value;
                }    
            }

            return null;
        }
    }
}