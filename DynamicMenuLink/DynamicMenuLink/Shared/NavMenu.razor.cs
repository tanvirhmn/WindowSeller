using Microsoft.AspNetCore.Components;

namespace DynamicMenuLink.Shared
{
    public partial class NavMenu
    {
        [Inject] public NavigationManager NavMan { get; set; }

        string counterURL
        {
            get
            {
                    string url = NavMan.BaseUri;

                    Uri myUri = new Uri(url);

                    string host = myUri.Host;
                    string port = myUri.Port.ToString();
                if (port != "80")
                {
                    return "https://" + host + ":" + port + "/counter";
                }
                else
                {
                    return "https://" + host + "/DynamicMenuTestWeb" + "/counter";
                }
            }
        }

        string fetchdataURL
        {
            get
            {

                    return NavMan.BaseUri + @"fetchdata";

            }
        }

        string helloURL
        {
            get
            {
                    return NavMan.ToAbsoluteUri("hello").ToString();
            }
        }
    }
}
