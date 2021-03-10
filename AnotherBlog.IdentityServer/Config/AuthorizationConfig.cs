using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AnotherBlog.IdentityServer.Config
{
    public class AuthorizationConfig
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("GatewayAPI", "Another Blog 网关API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "WebClientImplicit",
                    ClientSecrets = new [] { new Secret("SecretKey".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:40011/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:40011/signout-callback-oidc" },


                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ProductApi",
                        IdentityServerConstants.ClaimValueTypes.Json
                    },
                    AlwaysIncludeUserClaimsInIdToken=true
                }
            };
        }
    }
}
