using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
        public string Email { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
