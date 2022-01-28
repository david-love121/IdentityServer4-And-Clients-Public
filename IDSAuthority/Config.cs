// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace IDSEmpty
{
    public static class Config
    {

        
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email() 
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
        {
            new ApiScope("api1", "api1")
        };

        public static IEnumerable<Client> Clients =>
    
            new List<Client>
    {
        new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
            
    
    new Client
        {
            ClientId = "m2m",
            RedirectUris = {
            "https://www.google.com"
            },
            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.Code,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())

            },

            // scopes that client has access to
            AllowedScopes = { "api3" }
        },
        new Client
        {
            ClientId = "interactive",

    AllowedGrantTypes = GrantTypes.Code, 
    AllowOfflineAccess = true,
    ClientSecrets = { new Secret("secret".Sha256()) },

    RedirectUris =           { "https://localhost:7001/signin-oidc" },
    PostLogoutRedirectUris = { "https://localhost:7001/" },
    FrontChannelLogoutUri =    "https://localhost:7001/signout-oidc",

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,
        "given_name",

        "api1"
    },
        },
         new Client
        {
            ClientId = "interactive2",

    AllowedGrantTypes = GrantTypes.Code,
    AllowOfflineAccess = true,
    ClientSecrets = { new Secret("secret".Sha256()) },

    RedirectUris =           { "http://localhost:3002/cb" },
    //PostLogoutRedirectUris = { "https://localhost:3002/cb" },
   // FrontChannelLogoutUri =   { "https://localhost:3002/cb" },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        IdentityServerConstants.StandardScopes.Email,
        "given_name",

        "api1"
    },
        }
    };

    }
}