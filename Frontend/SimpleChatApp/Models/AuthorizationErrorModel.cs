using RestSharp.Deserializers;

namespace SimpleChatApp.Models
{
    public class AuthorizationErrorModel
    {
        public string Error { get; set; }

        [DeserializeAs(Name ="error_description")]
        public string ErrorDescription { get; set; }
    }
}
