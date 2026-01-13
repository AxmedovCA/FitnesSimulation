using System.ComponentModel.DataAnnotations;

namespace FitnessSimulation.ViewModels.UserViewModels
{
	public class RegisterVM
	{
		[Required,EmailAddress]
		public string Email { get; set; }=string.Empty;
		[Required,MinLength(3)]

		public string Fullname{ get; set; }=string.Empty;
		[Required, MinLength(3)]
		public string Username { get; set; }=string.Empty;
		[Required, DataType(DataType.Password)]
		public	string Password { get; set; }=string.Empty;
		[Required, DataType(DataType.Password),Compare(nameof(Password))]
		public string ConfirmPassword { get; set; } =string.Empty;
	}
}
