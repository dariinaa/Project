using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Review
    {
        public string Review_Id { get; set; }
        [Key]
        public string Review_Message { get; set; }
        public double Review_Rating { get; set; }
        public DateTime Review_Date { get; set; }
        public string Recipe_Id { get; set; }
        public string User_Id { get; set; }
    }
}
