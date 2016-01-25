using LightenDark.Api.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Models
{
    public class RequestLogin : RequestModelBase
    {
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public RequestLogin(string login, string password)
        {
            Type = RequestTypes.Login;
            Login = login;
            Password = password;
        }
    }
}
