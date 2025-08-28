using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class ImmediateCall
    {
        [Key]
        public int CallID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int CounselorID { get; set; }

        [Required]
        public DateTime CallRequestedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }  //  "Pending", "Ongoing", "Completed"

        // Navigation properties 
        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }

        [ForeignKey("CounselorID")]
        public virtual Counselor Counselor { get; set; }
    }
}
