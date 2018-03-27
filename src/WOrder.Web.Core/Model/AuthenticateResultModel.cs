using System;
using System.Collections.Generic;
using System.Text;
using WOrder.UserApp;

namespace WOrder.Web.Core.Model
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public UserDto UserDto { get; set; }
    }
}
