using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Profile Picture is required")]
        public string ProfilePictureURL { get; set; }

        [Required(ErrorMessage ="Full Name is required.")]
        //[StringLength(50, MinimumLength = 3,ErrorMessage ="Full name must be between 3 and 50 chars.")]
        public string FullName { get; set;}

        [Required(ErrorMessage ="Bio is required.")]
        public string Bio { get; set;}

        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
