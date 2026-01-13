using System.ComponentModel.DataAnnotations;

namespace FitnessSimulation.ViewModels.CategoryViewModel
{
	public class CategoryUpdateVM
	{
		public int Id { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(256)]
		public string Name { get; set; } = string.Empty;
	}
}
