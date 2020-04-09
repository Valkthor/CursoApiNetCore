using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo7.Models
{
    public class MyUserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
