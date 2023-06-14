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

                return "https://" + host + ":"+ port + "/counter";
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
