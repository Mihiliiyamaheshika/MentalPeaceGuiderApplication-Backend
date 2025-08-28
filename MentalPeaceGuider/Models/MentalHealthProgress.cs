using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class MentalHealthProgress
    {
        [Key]
        public int ProgressID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime DateRecorded { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 10)] 
        public int MoodLevel { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        // Navigation property
        [ForeignKey("UserID")]
        public Users User { get; set; }
    }
}
