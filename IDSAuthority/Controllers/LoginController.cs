using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSEmpty.sakila;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDSEmpty.sakila;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using IdentityServerHost.Quickstart.UI;
using IDSEmpty.Crypto;
using Microsoft.AspNetCore.Authorization;

namespace IDSEmpty.Controllers
{
    //TODO: Set up login controller to check users against database, then call HttpContext.SigninAsync(issuer, props) to raise them assuming they pass the check
    [Route("LoginForm")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        CSATMContext context = new CSATMContext();
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public LoginController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            TestUserStore users = null)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }
        //CSATMContext context = new CSATMContext();
        [Authorize]
        [HttpGet]
        public IActionResult OnGet()
        {
            // var Users = context.Idstable;
            //var DEFUSER = Users.FirstOrDefault(x => x.Id == "_Default");
            //  return Content(DEFUSER.Balance.ToString());
            return Content("Okay");
        }
        [HttpPost]
        public async Task<IActionResult> FormReciever()
        {
            var Uname = HttpContext.Request.Form["LF.Username"];
            
            
            var user = context.Idstable.FirstOrDefault(x => x.Id == Uname);
            AuthenticationProperties props = null;
           
            if (user != null)
            {
                SaltandHash SH = new SaltandHash();
                string Hashed = SH.Hash(HttpContext.Request.Form["LF.Password"], user.Salt);
                
                if (user.Pword == Hashed)
                {
                    var isuser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.Id
                    };
                    await HttpContext.SignInAsync(isuser, props);
                    return StatusCode(200);
                    //return Redirect("https://localhost:7001/Privacy");
                }
            }
            return StatusCode(401);
            //TODO: return a view for "User not found."
        }
    }
}