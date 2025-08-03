using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PetAppServer.Model
{
    public class Pet
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public int PetAge { get; set; }
        public string PetGender { get; set; }
        public string PetBreed { get; set; }
        public string VaccinationStatus { get; set; }
        public string Location { get; set; }

        public string? HealthIssues { get; set; }
        public string? PetBehaviour { get; set; }
        public string? PetDescription { get; set; }
        public string? PetVideo { get; set; }

        // Navigation property for images
        public ICollection<Image>? PetImages { get; set; }

        public User? User { get; set; }
    }
    public class PetDto
    {
        public Guid Id { get; set; }
        public string PetName { get; set; }
        public int PetAge { get; set; }
        public string PetGender { get; set; }
        public string PetDescription { get; set; }

        // List of image URLs associated with the pet
        public List<string> PetImageUrls { get; set; }
    }
    public class PetDetailDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public int PetAge { get; set; }
        public string PetGender { get; set; }
        public string PetBreed { get; set; }
        public string VaccinationStatus { get; set; }
        public string Location { get; set; }
        public string? HealthIssues { get; set; }
        public string? PetBehaviour { get; set; }
        public string? PetDescription { get; set; }
        public string? PetVideo { get; set; }

        public List<string> PetImageUrls { get; set; }
    }
    public class EmailDto
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
    }


}
