using MentalPeaceGuider.Models; // Ensure this points to your User class
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class MentalHealthProgress
    {
        [Key]
        [Column("ProgressId")]
        public int ProgressID { get; set; }
        public int UserID { get; set; }
        public DateTime DateRecorded { get; set; } = DateTime.Now;
        public int MoodLevel { get; set; }
        public string Notes { get; set; }

        public Users Users { get; set; }
    }
}
