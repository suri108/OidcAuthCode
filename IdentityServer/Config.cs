// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace QuickstartIdentityServer
{
    using IdentityServer4;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;

    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
               // new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ocelotGateway")
                {
                    ApiSecrets =
                    {
                        new Secret("ocelotGatewaySecret".Sha256())
                    }
                    ,
                    Scopes ={ "ocelotGateway", "accountdetails", "accountdetailsro", "accountdetailsauthcode" }
                },

                new ApiResource("accountDetailsApi")
                {
                  Scopes = { "accountdetails", "accountdetailsro", "accountdetailsauthcode" }
                }
                 ,new ApiResource("demographicsApi")
                 {
                  Scopes = { "demographicsApiRead", "demographicsApiWrite" }
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ocelotGateway", "ocelotGateway"),
                new ApiScope("accountdetails", "accountdetails"),
                new ApiScope("accountdetailsro", "accountdetailsro"),
                new ApiScope("accountdetailsauthcode", "accountdetailsauthcode"),
                new ApiScope("demographicsApiRead", "demographicsApiRead"),
                new ApiScope("demographicsApiWrite", "demographicsApiWrite")
            };


        public static IEnumerable<Client> GetClients()
        {
            // Client credential type -- 
            List<Client> clients = new List<Client>();

            // Client credentials granttype
            var clientCredsClient = new Client
            {
                ClientId = "clientcredentialsclient",
                ClientSecrets = { new Secret("clientcredentialssecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "accountdetails", "ocelotGateway" }
            };
            clients.Add(clientCredsClient);

            // Resource owner password granttype

            
            // Auth code type --
            var authCodeClient = new Client
            {
                ClientId = "mvcclient",
                ClientSecrets = { new Secret("authcodesecret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "accountdetailsauthcode"
                    }
            };

            clients.Add(authCodeClient);

            return clients;
        }
    }
}