using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The Profile Picture Is Required.")]
        public string ProfilePictureURL { get; set; }
        [Required(ErrorMessage = "The Full Name Is Required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "The Bio Is Required.")]
        public string Bio { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
