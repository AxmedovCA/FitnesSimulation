using System.ComponentModel.DataAnnotations;

namespace FitnessSimulation.ViewModels.UserViewModels
{
	public class LoginVM
	{
		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty;
		[Required, DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
	}
}
