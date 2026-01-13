using System.ComponentModel.DataAnnotations;

namespace FitnessSimulation.ViewModels.TrainerViewModel
{
    public class TrainerUpdaetVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string FullName { get; set; } = string.Empty;
        [Required, Range(4, int.MaxValue)]
        public int Experience { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public IFormFile? Image { get; set; } 
    }
}
