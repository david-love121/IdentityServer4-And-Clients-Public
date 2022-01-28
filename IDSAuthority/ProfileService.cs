using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IDSEmpty.sakila;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Extensions;

namespace IDSEmpty
{
    public static class PersistedGrantMapper
    {
        public static PersistantGrantItem ToItem(this PersistedGrant persistedGrant)
        {
            var persistedGrantItem = new PersistantGrantItem
            {
                ID = persistedGrant.Key,
                TYPEI = persistedGrant.Type,
                SUBJECT_ID = persistedGrant.SubjectId,
                CLIENT_ID = persistedGrant.ClientId,
                CREATION_TIME = persistedGrant.CreationTime,
                EXPIRATION = persistedGrant.Expiration,
                DATAI = persistedGrant.Data
            };
            return persistedGrantItem;
        }

        public static PersistedGrant ToModel(this PersistantGrantItem persistedGrantItem)
        {
            var persistedGrant = new PersistedGrant
            {
                Key = persistedGrantItem.ID,
                Type = persistedGrantItem.TYPEI,
                SubjectId = persistedGrantItem.SUBJECT_ID,
                ClientId = persistedGrantItem.CLIENT_ID,
                CreationTime = persistedGrantItem.CREATION_TIME,
                Expiration = persistedGrantItem.EXPIRATION,
                Data = persistedGrantItem.DATAI
            };
            return persistedGrant;
        }
    }
    public class ProfileService : IProfileService
    {
        public CSATMContext context;
        public ProfileService(CSATMContext context2)
        {
            context = context2;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext Pcontext)
        {
            try
            {
                /*var user = await DBContext.Idstable.FindAsync(context.Subject.Identity.Name);
                if (user != null)
                {
                    var claims = GetUserClaims(user);


                    context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                }*/
            }
            //Strange thing here, exception gets thrown inside the try but it still crashes.
            catch (Exception ex)
            {

            }

            //CSATMContext DBcontext = new CSATMContext();

            var sub = Pcontext.Subject.Identity.GetSubjectId();


            if (!string.IsNullOrEmpty(sub))
            {
                var name = sub;
                var idstable = context.Idstable;
                var user = context.Idstable.FirstOrDefault(x => x.Id == sub);


                if (user != null)
                {
                    var claims = GetUserClaims(user);

                    Pcontext.IssuedClaims = claims.Where(x => Pcontext.RequestedClaimTypes.Contains(x.Type)).ToList();

                }
            }
        }
        
        public async Task IsActiveAsync(IsActiveContext context)
        {
            CSATMContext DBContext = new CSATMContext();
            var userId = context.Subject.Claims.FirstOrDefault(y => y.Type == "user_id");
            if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
            {
                var user = await DBContext.Idstable.FindAsync(long.Parse(userId.Value));
                if (user != null)
                {
                    context.IsActive = true;
                    //TODO: Fully implement this
                }
            }
        }
        public static Claim[] GetUserClaims(IDSEmpty.sakila.Idstable user)
        {
            return new Claim[]
            {

            new Claim(JwtClaimTypes.Subject, user.Id.ToString() ?? ""),
            //TODO: Implement all of these claims. (This will take forever.)
            //new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(user.Firstname) && !string.IsNullOrEmpty(user.Lastname)) ? (user.Firstname + " " + user.Lastname) : ""),
            new Claim(JwtClaimTypes.GivenName, user.Name.ToString()  ?? ""),
            //new Claim(JwtClaimTypes.FamilyName, user.Lastname  ?? ""),
            new Claim(JwtClaimTypes.Email, user.Email.ToString())
            //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

            //roles
            //new Claim(JwtClaimTypes.Role, user.Role)
            };
        }
    }
    /**public class PersistedGrantStore : IPersistedGrantService
    {
        CSATMContext _container = new CSATMContext();
        public Task StoreAsync(PersistedGrant grant)
        {
            
            var persistedGrantItem = grant.ToItem();
            return _container.PGI.Add(persistedGrantItem);
        }

        public async Task GetAllGrantsAsync()
        {

        }
        public async Task RemoveAllGrantsAsync()
        {

        }
    }**/
}
