using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] SaltPassword { get; set; }
    }
}