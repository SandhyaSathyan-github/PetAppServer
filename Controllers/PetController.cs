using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetAppServer.Model;

namespace PetAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new pet
        [HttpPost("create")]
        public async Task<IActionResult> CreatePet([FromBody] Pet pet)
        {
            pet.Id = Guid.NewGuid();
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPetDetails), new { id = pet.Id }, pet);
        }

        // Add an image to a pet
        [HttpPost("add-image")]
        public async Task<IActionResult> InsertImg([FromBody] Image img)
        {
            img.Id = Guid.NewGuid();
            _context.Images.Add(img);
            await _context.SaveChangesAsync();
            return Ok(img);
        }

        // Get all pets with images
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllPets()
        {
            var pets = await _context.Pets
             .Include(p => p.PetImages)
              .ToListAsync();

            var petDtos = pets.Select(p => new PetDto
            {
                Id = p.Id,
                PetName = p.PetName,
                PetAge = p.PetAge,
                PetGender = p.PetGender,
                PetDescription = p.PetDescription,
                PetImageUrls = p.PetImages?.Select(img => img.Url).ToList()
            }).ToList();

            return Ok(petDtos);
        }

        // Get pets by user
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllPetsByUser(Guid userId)
        {
            var pets = await _context.Pets
                .Where(p => p.UserId == userId)
                .Include(p => p.PetImages)
                .ToListAsync();

            return Ok(pets);
        }

        // Get pet details by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPetDetails(Guid id)
        {
            var pet = await _context.Pets
                .Include(p => p.PetImages)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
                return NotFound();
            var dto = new PetDetailDto
            {
                Id = pet.Id,
                UserId = pet.UserId,
                PetName = pet.PetName,
                PetType = pet.PetType,
                PetAge = pet.PetAge,
                PetGender = pet.PetGender,
                PetBreed = pet.PetBreed,
                VaccinationStatus = pet.VaccinationStatus,
                Location = pet.Location,
                HealthIssues = pet.HealthIssues,
                PetBehaviour = pet.PetBehaviour,
                PetDescription = pet.PetDescription,
                PetVideo = pet.PetVideo,
                PetImageUrls = pet.PetImages?.Select(i => i.Url).ToList() ?? new List<string>()
            };

            return Ok(dto);
        }

        // Update pet
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePet(Guid id, [FromBody] Pet updatedPet)
        {
            var existingPet = await _context.Pets.FindAsync(id);
            if (existingPet == null)
                return NotFound();

            // Update fields
            existingPet.PetName = updatedPet.PetName;
            existingPet.PetType = updatedPet.PetType;
            existingPet.PetAge = updatedPet.PetAge;
            existingPet.PetGender = updatedPet.PetGender;
            existingPet.PetBreed = updatedPet.PetBreed;
            existingPet.VaccinationStatus = updatedPet.VaccinationStatus;
            existingPet.Location = updatedPet.Location;
            existingPet.HealthIssues = updatedPet.HealthIssues;
            existingPet.PetBehaviour = updatedPet.PetBehaviour;
            existingPet.PetDescription = updatedPet.PetDescription;
            existingPet.PetVideo = updatedPet.PetVideo;

            await _context.SaveChangesAsync();
            return Ok(existingPet);
        }

        // Delete pet
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> AdoptPetDelete(Guid id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
                return NotFound();

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
