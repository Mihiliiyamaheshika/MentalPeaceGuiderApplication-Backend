using MentalPeaceGuider.Models; // ensures Counselor is recognized
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentalPeaceGuider.Models
{
    public class AvailableSlot
    {
        [Key]
        [Column("SlotId")]
        public int SlotID { get; set; }
        public int CounselorID { get; set; }
        public DateTime SlotDateTime { get; set; }
        public bool IsBooked { get; set; }

        public Counselor Counselor { get; set; }
    }
}
