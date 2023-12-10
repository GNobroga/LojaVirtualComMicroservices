using System.Security.Cryptography.X509Certificates;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer.Configurations;

public class IdentityConfiguration
{
    public const string ADMIN = "ADMIN";

    public const string CLIENT = "CLIENT";

    public static IEnumerable<IdentityResource> IdentityResources => 
        new List<IdentityResource>() 
        {
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResources.OpenId()
        };
    
    public static IEnumerable<ApiScope> ApiScopes => 
        new List<ApiScope>() 
        {
           new(name: "api", displayName: "Web API"),
           new(name: "read", displayName: "Read Data"),
           new(name: "write", displayName: "Write Data"),
           new(name: "delete", displayName: "Delete Data"),
        };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new()
            {
                ClientId = "api",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("123456".Sha256())
                },
                AllowedScopes = { 
                    "api",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                }
            }
        };
}



