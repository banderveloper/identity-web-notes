using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityWebNotes.Identity;

public class Configuration
{
    // Scope(region) - part of api that client can use (id for resource)
    public static IEnumerable<ApiScope> ApiScopes => 
        new List<ApiScope>
        {
            new ApiScope("notesapi", "notes web api")    
        };

    // In Identity server regions are introduced as Resources (it can be identity and webapi resources)

    // indentity resources created a region that allows client to check user claims
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(), // open id
            new IdentityResources.Profile() // user data (for example name or birthdate)
        };

    // Api resource to create an access to whole protected resource (for example API with a few level of security)
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            // will path user NAME claim, for generating user id at api server
            new ApiResource("notesapi", "notes web api", new[] { JwtClaimTypes.Name }){
                Scopes = {"notesapi"}
            }
        };

    // Clients (applications that works with our identity server)
    // Each client app configs - it can use only given part of our api
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "notes-api-id",
                ClientName = "notes-api",

                // Grand types (protocol / flow) - type of interaction of client with identity server, grand types:
                // 1. Authorization code - exchange of auth code for access token (to public and private clients)...
                //      ...after user return to client using return url, application will get auth code... 
                //      ...and will user it for taking access token
                // 2. Client credentials - for access for api out of user context
                // 3. Device code - user for devices without browser or some physical limits
                // 4. Refresh token - exchange of refresh token for access token, when access token is out of time
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,

                // needed confirm key for auth code
                RequirePkce = true,

                // Allowed URIs to redirect after client auth 
                RedirectUris =
                {
                    "https://.../signin-oidc"
                },

                // List of URIs who allowed to use identity server
                AllowedCorsOrigins = 
                {   
                    "https://..."
                },

                // Allowed URIs to redirect after client logout
                PostLogoutRedirectUris = 
                {
                    "https://.../signout-oidc"
                },

                // Scopes, allowed for client
                AllowedScopes = 
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "notesapi"
                },

                // Controls transfer access token via browser
                AllowAccessTokensViaBrowser = true
            }
        };


}