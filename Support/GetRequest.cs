using System.Net;

namespace Support
{
    public class GetRequest
    {
        HttpClient client= new HttpClient();
        HttpResponseMessage _request= new HttpResponseMessage();
        Uri _adress;

        public Dictionary<string, string> Headers { get; set; }
        public string Response { get; set; } = "";
        public string Accept { get; set; } = "";
        public string Host { get; set; } = "";
        public string Referer { get; set; } = "";
        public string Useragent { get; set; } = "";

        //public WebProxy Proxy { get; set; }

        public GetRequest(string address)
        {
            _adress = new Uri(address);
            Headers = new Dictionary<string, string>();
        }
        public void Run(CookieContainer cookieContainer)
        {
            var handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer,
                UseCookies = true
            };

            client = new HttpClient(handler) { BaseAddress = _adress };

            client.DefaultRequestHeaders.Add("Accept", Accept);
            client.DefaultRequestHeaders.Add("Host", Host);
            client.DefaultRequestHeaders.Add("User-Agent", Useragent);
            client.DefaultRequestHeaders.Add("Referer", Referer);
            //_request.Proxy = Proxy;

            foreach (var pair in Headers)
            {
                client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }
            try
            {
                _request = client.GetAsync(_adress).Result;
                if (_request.IsSuccessStatusCode)
                {
                    var result = _request.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        Response = result;
                    }
                }
                else
                {
                    Console.WriteLine("GET_RESPONSE_RESPONSE_IS_NULL");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("GET_RESPONSE_RESPONSE_IS_NULL_CATCH" + ex.Message);
            }
        }
    }
}
