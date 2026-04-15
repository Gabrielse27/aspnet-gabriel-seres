using CoreFitness.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities
{
    public class GymSession
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;

        public string? BookedByUserId { get; set; }
        public virtual User? BookedByUser { get; set; }


    }
}
