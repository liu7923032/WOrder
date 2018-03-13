using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WOrder.Authorization
{
    public class LoginModel
    {
        [Required]
        public string Account { get; set; }

        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}
