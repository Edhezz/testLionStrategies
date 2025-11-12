using System.ComponentModel.DataAnnotations;

namespace LionStrategiesTest.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; } 

        [Required]
        public string Password { get; set; }

        //Relacion un usuario puede cero o muchas operaciones
        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }
}