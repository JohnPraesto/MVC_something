using static WebApplication1.StaticDetails;

namespace WebApplication1.Models
{
    public class ApiRequest
    {
        public ApiType apiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
