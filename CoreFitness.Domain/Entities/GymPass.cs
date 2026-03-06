using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Entities
{
    public class GymPass
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty; // Ex: Body Pump,Yoga....
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Instructor { get; set; } = string.Empty;

        // För att kunna Kategoriera passen ,ex: Styrka, Condition
        public string Category { get; set; } = string.Empty;
    }
}
