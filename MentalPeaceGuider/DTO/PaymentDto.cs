using System;

namespace MentalPeaceGuider.Dtos
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class CreatePaymentDto
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class UpdatePaymentDto
    {
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
