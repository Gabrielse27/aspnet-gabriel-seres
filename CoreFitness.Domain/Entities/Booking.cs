using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFitness.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty; // Koppling till Identity User
        public int GymPassId { get; set; } // Koppling till GymPass

        [Column("BookedAt")]
        public DateTime BookedAt { get; set; }

        
        public string SessionName { get; set; } = string.Empty;

        
        public DateTime StartTime { get; set; }

        
        public DateTime EndTime { get; set; }

        public GymSession? GymPass { get; set; }


    }
}
