using System.ComponentModel.DataAnnotations;

namespace Project.Data.DataModels
{
    public class Role
    {
        public string Role_Id { get; set; }
        [Key]
        public string Role_Type { get; set; }
    }
}
