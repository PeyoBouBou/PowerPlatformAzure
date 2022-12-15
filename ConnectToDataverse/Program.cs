// See https://aka.ms/new-console-template for more information
using ConnectToDataverse.services;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Security.Authentication;

Console.WriteLine("Hello, Dataverse");

// TODO Specify the Dataverse environment name to connect with.
string resource = "https://[YOUR DATAVERSE NAME].api.crm.dynamics.com";

// Azure Active Directory app registration shared by all Power App samples.
// For your custom apps, you will need to register them with Azure AD yourself.
// See https://docs.microsoft.com/powerapps/developer/data-platform/walkthrough-register-app-azure-active-directory
var clientId = "[CLIENT ID]";
var redirectUri = "http://localhost:8080";
// Optional use it only if you want to login silently i.e without user interaction.
string secret = "[CLIENT SECRET]";
string tenantId = "[TENANT ID]";


#region Authentication

string token = await Authentication.GetInteractiveToken(resource, 
                                                        clientId, 
                                                        redirectUri);
//string token = await Authentication.GetSilentToken(resource,tenantId, clientId, secret);
#endregion Authentication

#region Client configuration
DataverseClient dataverse = new DataverseClient(resource,token);
#endregion Client configuration

#region Web API call

string statement = "accounts?$select=name,address1_city&$expand=primarycontactid($select=contactid,fullname)";
var response=await dataverse.Client.GetAsync (statement);


if (response.IsSuccessStatusCode)
{
    var json=    await response.Content.ReadAsStringAsync();    
    Console.WriteLine(json);
}
else
{
    Console.WriteLine("Web API call failed");
    Console.WriteLine("Reason: " + response.ReasonPhrase);
}
#endregion Web API call

// Pause program execution by waiting for a key press.
Console.ReadKey();