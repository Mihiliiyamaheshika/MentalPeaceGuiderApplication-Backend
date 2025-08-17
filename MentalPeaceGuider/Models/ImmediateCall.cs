using MentalPeaceGuider.Models; // ensures User and Counselor are recognized
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class ImmediateCall
    {
        [Key]
        [Column("CallId")]
        public int CallID { get; set; }
        public int UserID { get; set; }
        public int CounselorID { get; set; }
        public DateTime CallRequestedAt { get; set; } = DateTime.Now;
        public string Status { get; set; }

        public Users Users { get; set; }
        public Counselor Counselor { get; set; }
    }
}
