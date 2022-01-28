// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.EntityFramework;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using IDSEmpty.sakila;
using IdentityServer4.Stores;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace IDSEmpty
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
            
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // uncomment, if you want to add an MVC-based UI
            string connectionString = "Redacted";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddMvc();
            services.AddRazorPages();
            
          
            services.AddScoped(_ => new CSATMContext());
            var builder = services.AddIdentityServer(options =>
            {
                options.Endpoints.EnableDeviceAuthorizationEndpoint = false;
            }
            )
        .AddInMemoryApiScopes(Config.ApiScopes).AddOperationalStore(options =>
        {
            options.ConfigureDbContext = builder =>
                builder.UseMySQL(connectionString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));

         
            options.EnableTokenCleanup = true;
            
        })
        //.AddInMemoryClients(Config.Clients);
        .AddConfigurationStore(options => options.ConfigureDbContext = builder =>
     builder.UseLazyLoadingProxies().UseMySQL(connectionString));
            //.AddInMemoryIdentityResources(Config.IdentityResources)
            //.AddInMemoryApiScopes(Config.ApiScopes)
            //.AddInMemoryClients(Config.Clients);
            
            //services.AddScoped<IDBInterface, DBInterface>;
           
            builder.AddDeveloperSigningCredential();
            
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            //services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            builder.AddInMemoryIdentityResources(Config.IdentityResources);
            builder.AddInMemoryApiScopes(Config.ApiScopes);
            builder.AddInMemoryClients(Config.Clients);
            
            
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseIdentityServer();

           
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
