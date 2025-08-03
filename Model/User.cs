using System.ComponentModel.DataAnnotations;

namespace PetAppServer.Model
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }

        public ICollection<Pet>? Pets { get; set; }
        public ICollection<SuccessStory>? SuccessStories { get; set; }
        public ICollection<LostPet>? LostPets { get; set; }
    }

    public class RegisterResDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class ChangePasswordDto
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
