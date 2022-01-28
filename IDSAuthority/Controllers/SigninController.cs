using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IDSEmpty.sakila;
using IDSEmpty.Crypto;
namespace IDSEmpty.Controllers
{
    public class Functions
    {
        public bool checkForMatch(Microsoft.AspNetCore.Http.HttpContext HttpContext)
        {
            CSATMContext context = new CSATMContext();
            var u1 = context.Idstable.FirstOrDefault(x => x.Id == HttpContext.Request.Form["LF.Username"]);
            if (u1 == null)
            {
                var u2 = context.Idstable.FirstOrDefault(x => x.Email == HttpContext.Request.Form["LF.Email"]);
                if (u2 == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
    [ApiController]
    public class SigninController : Controller
    {
        //Reserach if automatically signing in users creates user consent or security issues.
        
        [Route("/Signup")]
        [HttpPost]
        public IActionResult SigninForm()
        {
            Functions F = new Functions();
            CSATMContext context = new CSATMContext();
            SaltandHash SH = new SaltandHash();
            var NewUser = new Idstable();
            if (!F.checkForMatch(HttpContext))
            {
                NewUser.Id = HttpContext.Request.Form["LF.Username"];
                NewUser.Email = HttpContext.Request.Form["LF.Email"];
                NewUser.Name = HttpContext.Request.Form["LF.Name"];
                NewUser.Balance = 0;
                Tuple<string, string> SaltandH = SH.ComputeSH(HttpContext.Request.Form["LF.Password"]);
                NewUser.Pword = SaltandH.Item1;
                NewUser.Salt = SaltandH.Item2;
                context.Idstable.Add(NewUser);
                context.SaveChanges();
                return StatusCode(200);
            }
            return StatusCode(300);
        }
        
       
    }
}
