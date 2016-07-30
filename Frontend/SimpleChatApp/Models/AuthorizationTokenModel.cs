using RestSharp.Deserializers;

namespace SimpleChatApp.Models
{
    public class AuthorizationTokenModel
    {
        [DeserializeAs(Name = "access_token")]
        public string Token { get; set; }

        [DeserializeAs(Name = "token_type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "expires_in")]
        public int Expires { get; set; }
    }
}
