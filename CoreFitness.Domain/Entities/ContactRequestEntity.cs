using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities
{
    public class ContactRequestEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Message { get; set; } = null!;
        public DateTime CreatedDate { get; set; }   




    }
}
