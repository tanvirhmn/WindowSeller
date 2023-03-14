using Microsoft.Graph;
using UserModule.API.Models;

namespace UserModule.API.Helpers
{
    public static class GraphHelper
    {
        public static async Task<CachedUser> GetCurrentUserDetailAsync(string accessToken)
        {
            GraphServiceClient graphServiceClient = new GraphServiceClient("https://graph.microsoft.com/v1.0",
                new DelegateAuthenticationProvider(
                  request =>
                  {
                      request.Headers.Authorization = new System.Net.Http.Headers
                      .AuthenticationHeaderValue("bearer", accessToken);
                      return Task.CompletedTask;
                  }
                ));

            var user = await graphServiceClient.Me.Request()
                .Select(user => new {
                    user.DisplayName,
                    user.UserType,
                    user.MemberOf,
                    user.Photo,
                    user.Id
                }).GetAsync();

            var groups = new List<string>();
            groups.AddRange(user.MemberOf
                    .OfType<Group>().Select(grp => grp.DisplayName));

            return new CachedUser
            {
                DisplayName = user.DisplayName,
                UserType = user.UserType,
                UserPhoto = user.Photo,
                AzureUserID = user.Id,
                CommasparatedGroups = String.Join(",", groups)
            };
        }

        public static IGraphServiceUsersCollectionPage GetAllAzureADUsers(string accessToken)
        {
            GraphServiceClient graphServiceClient = new GraphServiceClient("https://graph.microsoft.com/v1.0",
                new DelegateAuthenticationProvider(
                  request =>
                  {
                      request.Headers.Authorization = new System.Net.Http.Headers
                      .AuthenticationHeaderValue("bearer", accessToken);
                      return Task.CompletedTask;
                  }
                ));

            var userList = graphServiceClient.Users.Request().Select(user => user.DisplayName).GetAsync().Result;
            return userList;
        }
    }
}
