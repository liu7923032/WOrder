using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WOrder.Web.Models.TokenAuth
{
    public class AuthenticateModel
    {
        [Required]
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        public bool RememberClient { get; set; }
    }
}
