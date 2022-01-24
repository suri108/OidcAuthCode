// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static async Task Main()
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:51310");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token accountDetails api read write
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "clientcredentialsclient",
                ClientSecret = "clientcredentialssecret",
                
                Scope = "ocelotGateway accountdetails"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var response = await apiClient.PostAsync("http://localhost:51306/api/gateway/accountDetails", new StringContent("", Encoding.UTF8, "application/json"));

            var response = await apiClient.GetAsync("http://localhost:51306/api/gateway/accountdetails");

            //var response = await apiClient.GetAsync("http://localhost:51306/api/gateway/demographicsapi");

            apiClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //var response = await apiClient.PostAsync("http://localhost:51306/api/gateway/demographicsapi/postdata?input=1", new StringContent("",Encoding.UTF8,"application/json"));

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            Console.ReadKey();
        }
    }
}