using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetAppServer.Model
{
    public class LostPet
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string PetName { get; set; }
        public string PetType { get; set; }
        public string? PetBreed { get; set; }
        public string LastSeenLocation { get; set; }
        public string LastSeenDate { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string? AdditionalInfo { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
