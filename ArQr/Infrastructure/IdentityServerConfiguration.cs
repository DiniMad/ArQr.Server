using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace ArQr.Infrastructure
{
    public static class IdentityServerConfiguration
    {
        public static IReadOnlyList<Client> GetClients() =>
            new[]
            {
                new Client
                {
                    ClientId            = "ArQr",
                    AllowedGrantTypes   = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ArQrAPI",
                    },
                },
            };
    }
}