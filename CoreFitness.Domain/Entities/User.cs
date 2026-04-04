using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoreFitness.Domain.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
         public int Age { get; set; }
    }
}




