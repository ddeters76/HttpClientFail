using System;
using System.Diagnostics;
using System.Threading;
using System.Device.Wifi;
using nanoFramework.Networking;
using System.Net.Http;
using System.Text;

namespace HttpClientFail
{
    public class Program
    {
        const string TheSsid = "itsthessid";
        const string Password = "mysupersecurepassword";
        static HttpClient _httpClient = new HttpClient();

        public static void Main()
        {
            try
            {



                CancellationTokenSource cs = new(60000);

                Debug.WriteLine("Network Status: " + WifiNetworkHelper.Status.ToString());

                var success = WifiNetworkHelper.ConnectDhcp(TheSsid, Password, requiresDateTime: true, token: cs.Token);
                Debug.WriteLine(success.ToString());
                if (!success)
                {
                    Debug.WriteLine($"Can't connect to the network, status: {WifiNetworkHelper.Status}");
                    if (WifiNetworkHelper.HelperException != null)
                    {
                        Debug.WriteLine($"ex: {WifiNetworkHelper.HelperException.Message}");
                    }
                }
                else
                {
                    Thread.Sleep(15000);
                }
                Debug.WriteLine("Wifi status:" + WifiNetworkHelper.Status);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Message:" + ex.Message);
            }
            Debug.WriteLine("Hello from nanoFramework!");

            var content = new StringContent("{\"someProperty\":\"someValue\"}", Encoding.UTF8, "application/json");
            var result = _httpClient.Post("http://httpbin.org/anything", content);
            result.EnsureSuccessStatusCode();

            var content2 = new StringContent("{\"someProperty\":\"someValue\"}", Encoding.UTF8, "application/json");
            var result2 = _httpClient.Post("http://httpbin.org/anything", content);
            result.EnsureSuccessStatusCode();

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
