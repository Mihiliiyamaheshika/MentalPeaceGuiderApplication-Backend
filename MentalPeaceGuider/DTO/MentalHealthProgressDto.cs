using System;

namespace MentalPeaceGuider.Dtos
{
    public class MentalHealthProgressDto
    {
        public int ProgressID { get; set; }
        public int UserID { get; set; }
        public DateTime DateRecorded { get; set; }
        public int MoodLevel { get; set; }
        public string Notes { get; set; }
    }

    public class CreateMentalHealthProgressDto
    {
        public int UserID { get; set; }
        public int MoodLevel { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateMentalHealthProgressDto
    {
        public int MoodLevel { get; set; }
        public string Notes { get; set; }
    }
}
