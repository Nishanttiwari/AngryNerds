namespace APIHelper
{
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class JSONProvider : IJSONProvider
    {
        public string GetJSON(string URL)
        {
            return MakeCall2(URL);
        }

        private void MakeCall()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://nielsen.api.tibco.com:443/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "Shred");
                client.DefaultRequestHeaders.Add("Apikey", "8831-7f5c6de0-d144-4043-b700-e425ed2d1331");

                ////using (HttpResponseMessage response = await client.GetAsync("AdView/Product/v1?productcategory=TOOTHBRUSH"));

                client.GetAsync("").ContinueWith(
                    (requestTask) =>
                    {
                        HttpResponseMessage response = requestTask.Result;

                        response.EnsureSuccessStatusCode();

                         ///response.Content.read


                    });

                ////return await response.Content.ReadAsStringAsync();

                // 
            }
        }

        private string MakeCall2(string URL)
        {
            WebClient client = new WebClient();
            client.BaseAddress = "https://nielsen.api.tibco.com:443/";
            client.Headers.Set("User-Agent", "Shred");
            client.Headers.Set("Accept", "application/json");
            client.Headers.Add("Apikey", "8831-7f5c6de0-d144-4043-b700-e425ed2d1331");

            string reply = client.DownloadString(URL);

            return reply;
        }


        public string GetJSONFromThirdParty(string URL)
        {
            WebClient client = new WebClient();
            client.Headers.Set("User-Agent", "Shred");
            client.Headers.Set("Accept", "application/json");
            client.Headers.Add("Apikey", "8831-7f5c6de0-d144-4043-b700-e425ed2d1331");

            string reply = client.DownloadString(URL);

            return reply;
        }
    }
}
