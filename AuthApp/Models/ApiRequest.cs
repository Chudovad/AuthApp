using static AuthApp.StaticData;

namespace AuthApp.Models
{
    public class ApiRequest
    {
        public ApiTypes ApiType { get; set; } = ApiTypes.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
