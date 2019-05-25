using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace TripWMe.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "fec0a4d6-5830-4eb8-8024-272bd5d6d2bb",
                    Username = "John",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "John"),
                        new Claim("family_name", "Smith"),
                        new Claim("role", "Administrator"),
                    }
                },
                new TestUser
                {
                    SubjectId = "c3b7f625-c07f-4d7d-9be1-ddff8ff93b4d",
                    Username = "Mark",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Mark"),
                        new Claim("family_name", "Smith"),
                        new Claim("role", "User"),
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResource("roles", "Your role(s)", new []{"role"}),
            };
        }

        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new[] {
                new ApiResource("tripwithmeapi", "Trip With Me API", new[] { "role" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Trip With Me",
                    ClientId="tripwithmeclient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =new List<string>
                    {
                        "https://localhost:44339/#/signin-oidc#"
                    },
                    AccessTokenLifetime = 180,
                    PostLogoutRedirectUris = new[]{
                        "https://localhost:44339/#/" },
                    AllowedScopes = new []
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "tripwithmeapi",
                    }
                }
            };
        }
    }
}
