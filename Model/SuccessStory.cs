using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetAppServer.Model
{
    public class SuccessStory
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PetName { get; set; }
        public string Description { get; set; }
        public string PetPhotoUrl { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
