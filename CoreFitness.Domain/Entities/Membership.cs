using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty; // Ex: Standard,Premium,VIP
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;


    }
}
