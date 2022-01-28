using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using IDSEmpty.sakila;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IDSEmpty
{
    //Turns out this file is only needed for Password grant type. Oopsie, gonna keep it just in case I need it later
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public CSATMContext context;
        public ResourceOwnerPasswordValidator(CSATMContext context2)
        {
            context = context2;
        }
        
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext Vcontext)
        {
            
            var user = await context.Idstable.FindAsync(Vcontext.UserName);
            Log.Information("VContext is " + Vcontext.Password + "user context is " + user.Pword);
            if (user != null)
            {
                if (user.Pword == Vcontext.Password)
                {
                    /*Vcontext.Result = new GrantValidationResult(
                        subject: user.Userid,
                        authenticationMethod: "code",
                        claims: GetUserClaims(user));*/
                    Vcontext.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                //context.Dispose();
                Vcontext.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "Incorrect password");
                
            }
           // context.Dispose();
            Vcontext.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidGrant, "User doesn't exist");
            
            return;
        }
        public static Claim[] GetUserClaims(IDSEmpty.sakila.Idstable user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.Id),//user.Userid.ToString() ?? ""),
            //TODO: Implement all of these claims. (This will take forever.)
            //new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.Firstname) && !string.IsNullOrEmpty(user.Lastname)) ? (user.Firstname + " " + user.Lastname) : ""),
            //new Claim(JwtClaimTypes.GivenName, user.Firstname  ?? ""),
            //new Claim(JwtClaimTypes.FamilyName, user.Lastname  ?? ""),
            new Claim(JwtClaimTypes.Email, "david.love.wii@gmail.com")
            //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

            //roles
            //new Claim(JwtClaimTypes.Role, user.Role)
            };
        }
    }
}
