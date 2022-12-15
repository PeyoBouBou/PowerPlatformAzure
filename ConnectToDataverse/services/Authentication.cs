using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectToDataverse.services
{
    internal class Authentication
    {
        public static async Task<string> GetInteractiveToken(string resource, string clientId, string redirectUri)
        {
            var authBuilder = PublicClientApplicationBuilder.Create(clientId)
                  .WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs)
                  .WithRedirectUri(redirectUri)
                  .Build();
            var scope = resource + "/.default";
            string[] scopes = { scope };
            AuthenticationResult? result = null;            
            result = await authBuilder.AcquireTokenInteractive(scopes).ExecuteAsync();
            return result.AccessToken;

        }
        public static async Task<string> GetSilentToken(string resource, 
                                                        string tenantId,
                                                        string clientid, 
                                                        string secret)
        {
            var authBuilder = ConfidentialClientApplicationBuilder.Create(clientid)
                  .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                  .WithTenantId(tenantId)
                  .WithClientSecret(secret)
                  .Build();
            var scope = resource + "/.default";
            string[] scopes = { scope };
            AuthenticationResult result = null;            
            result = await authBuilder.AcquireTokenForClient(scopes).ExecuteAsync();
            return result.AccessToken;
        }
    }
 
}
