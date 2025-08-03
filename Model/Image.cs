using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PetAppServer.Model
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PetId { get; set; }

        public string Url { get; set; }

        [ForeignKey("PetId")]
        [JsonIgnore]
        public Pet? Pet { get; set; }
    }
}
