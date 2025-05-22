using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text.Json;

namespace NetSuiteOauthM2mDemo.Core
{
    public class NetSuiteApiClient
    {
        private const string AccountId = "4197339-sb1";

        private const string ClientCredentialsCertificateId = "1ie_ovX3A4H-et36j2gQYM1Ns7jCN4AbOz5bX73SlbU";
        private string ApiConsumerKey = "96dd0d197b26ed148b8fa02d6cb0b4e85a0c4f22624451d87022be9287b64d6e";

        private const string PrivateKeyPem = @"-----BEGIN PRIVATE KEY-----
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
/U/UiM8akiY4RG5cssru4g==
-----END PRIVATE KEY-----";


        private static string RestApiRoot = $"https://{AccountId}.suitetalk.api.netsuite.com/services/rest";

        private static string Oauth2ApiRoot = $"{RestApiRoot}/auth/oauth2/v1";
        private static string RecordApiRoot = $"{RestApiRoot}/record/v1";

        private static string TokenEndPointUrl = $"{Oauth2ApiRoot}/token";

        private static readonly HttpClient _httpClient = new HttpClient();

        private static string _accessToken;

        public async Task<string> GetAccessToken()
        {
            var url = Oauth2ApiRoot + "/token/";

            string clientAssertion = GetJwtToken();

            var requestParams = new List<KeyValuePair<string, string>>();
            requestParams.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            requestParams.Add(new KeyValuePair<string, string>("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer"));
            requestParams.Add(new KeyValuePair<string, string>("client_assertion", clientAssertion));

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Content = new FormUrlEncodedContent(requestParams);

            var httpResponse = await _httpClient.SendAsync(httpRequest);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();


            var response = JsonSerializer.Deserialize<NsToken>(responseJson);

            return response.access_token;

        }

        private string GetJwtToken()
        {
            string privateKeyPem = PrivateKeyPem;

            // keep only the payload of the key. 
            //privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
            //privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

            // Create the RSA key.
            //byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);
            PemReader pemRdr = new PemReader(new StringReader(privateKeyPem));

            RSAParameters  rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)pemRdr.ReadObject());
            var provider = RSA.Create(4096);
            provider.ImportParameters(rsaParams);

            RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);

            // Create signature and add to it the certificate ID provided by NetSuite.
            var signingCreds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSsaPssSha256);
            signingCreds.Key.KeyId = ClientCredentialsCertificateId;

            // Get issuing timestamp.
            var now = DateTime.UtcNow;

            // Create token.
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = ApiConsumerKey,
                Audience = TokenEndPointUrl,
                Expires = now.AddMinutes(5),
                IssuedAt = now,
                Claims = new Dictionary<string, object> { 
                                                            { "scope", new[] { "rest_webservices", "restlets" } } 
                                                        },
                SigningCredentials = signingCreds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenText =  tokenHandler.WriteToken(token);

            return tokenText;
        }

        public async Task<List<string>> FindCustomerIds(int limit)
        {
            var url = RecordApiRoot + "/customer?limit=" + limit;

            if (_accessToken == null)
                _accessToken = await GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            var httpResponse = await _httpClient.SendAsync(httpRequest);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();

            var response =
                JsonSerializer.Deserialize<NsFindIdsResponse>(responseJson);

            return response.items.Select(i => i.id).ToList();
        }

        public async Task<NsCustomer> GetCustomer(int customerId)
        {
            var url = RecordApiRoot + "/inventoryItem/" + customerId;

            if (_accessToken == null)
                _accessToken = await GetAccessToken();

            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            
             var httpResponse = await _httpClient.SendAsync(httpRequest);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();

            var customer =
                 JsonSerializer.Deserialize<NsCustomer>(responseJson);

            return customer;
        }

    }
}