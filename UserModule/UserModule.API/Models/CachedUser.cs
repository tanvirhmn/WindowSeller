using Microsoft.Graph;

namespace UserModule.API.Models
{
    public class CachedUser
    {
        public string AzureUserID { get; set; } 
        public string CommasparatedGroups { get; set; }
        public string DisplayName { get; set; }
        public string UserType { get; set; }
        public ProfilePhoto UserPhoto { get; set; } 
    }
}
