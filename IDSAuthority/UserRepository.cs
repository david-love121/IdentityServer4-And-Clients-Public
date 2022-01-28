using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Test;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;
using IDSEmpty.sakila;
namespace IDSEmpty
{ 
    public interface IDBInterface
    {
        public Task<IDSEmpty.sakila.Idstable> Finder(params Object[] args);
    }
    public class DBInterface
    {
        public readonly CSATMContext context;
        public DBInterface(CSATMContext context2)
        {
            context = context2;
        }
        
        public async Task<IDSEmpty.sakila.Idstable> Finder(params Object[] args)
        {
            var user = await context.Idstable.FindAsync(args);
            return user;
        }
    }
}
