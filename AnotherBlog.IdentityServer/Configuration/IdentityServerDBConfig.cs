using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AnotherBlog.IdentityServer.Configuration
{
    public class IdentityServerDBConfig
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("GatewayAPI"),
            };
        }


        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("GatewayAPI", "网关API", new List<string>{ "UserNo", "RoleId"})
                {
                    Scopes = new List<string>()
                    {
                        "GatewayAPI"
                    }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "AdminSite",
                    ClientName = "Admin Site",
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenLifetime = 3600 * 2,
                    AccessTokenType = AccessTokenType.Jwt,
                    RequireConsent = false,
                    RequireClientSecret = false,
                    RedirectUris = { "http://localhost:8081/callback" },
                    PostLogoutRedirectUris = { "http://localhost:8081" },
                    AllowedCorsOrigins = { "http://localhost:8081" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "GatewayAPI"
                    }
                }
            };
        }
    }
}
