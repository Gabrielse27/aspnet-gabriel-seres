using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty; // Koppling till Identity User
        public int GymPassId { get; set; } // Koppling till GymPass
        public DateTime BookedAt { get; set; }

        public GymSession? GymPass { get; set; }


    }
}
