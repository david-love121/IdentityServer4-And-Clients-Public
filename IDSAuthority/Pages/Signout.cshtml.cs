using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4;
namespace IDSEmpty.Pages
{
    public class SignoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.SignOutAsync();
        }
    }
}