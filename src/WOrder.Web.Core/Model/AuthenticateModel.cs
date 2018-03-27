using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WOrder.Web.Core.Model
{
    public class AuthenticateModel
    {
        [Required]
        [StringLength(10)]
        public string Account { get; set; }

        [Required]
        [StringLength(15)]
        public string Password { get; set; }

        public bool RememberClient { get; set; }
    }
}
