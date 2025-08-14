using System;
using MentalPeaceGuider.Models; // Ensure this points to your User class

namespace MentalPeaceGuider.Models
{
    public class MentalHealthProgress
    {
        public int ProgressID { get; set; }
        public int UserID { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
        public int MoodLevel { get; set; }
        public string Notes { get; set; }

        public User User { get; set; }
    }
}
