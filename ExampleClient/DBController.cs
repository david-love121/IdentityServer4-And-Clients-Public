using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using MySql.Data.MySqlClient;
using MySql;
using System.Net.Http;
using IdentityModel.Client;
using System.Text;

namespace ExampleClient
{


    public class DBController : Controller
    {
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = "https://localhost:5001/Logout"
            };
            await HttpContext.SignOutAsync("Cookies");
            //await HttpContext.SignOutAsync("oidc", props);
            return Redirect("https://localhost:5001/Signout");
        }
        [HttpPost]
        public async Task<IActionResult> Deposit()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return StatusCode(405);
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return StatusCode(405);
            }

            Console.WriteLine(tokenResponse.Json);
            var apiClient = new HttpClient();
            string data = "";
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            foreach (string s in Request.Form.Keys)
            {
                data = data + s + "=" + Request.Form[s] + "&";
            }

            var requestContent = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await apiClient.PostAsync("https://localhost:6001/Deposit", requestContent);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return StatusCode(200);
            }
            return StatusCode(401);
        }
    [HttpPost]
        public async Task<IActionResult> Withdrawl()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return StatusCode(405);
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return StatusCode(405);
            }

            Console.WriteLine(tokenResponse.Json);
            var apiClient = new HttpClient();
            string data = "";
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            foreach (string s in Request.Form.Keys)
            {
                data = data + s + "="+ Request.Form[s] + "&";
            }

            var requestContent = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await apiClient.PostAsync("https://localhost:6001/Withdrawl", requestContent);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return StatusCode(200);
            }
            return StatusCode(401);
        }
    }
    
}
