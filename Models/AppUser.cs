using Microsoft.AspNetCore.Identity;

namespace FitnessSimulation.Models
{
	public class AppUser:IdentityUser
	{
		public string Fullname { get; set; } = null!;
	}
}
