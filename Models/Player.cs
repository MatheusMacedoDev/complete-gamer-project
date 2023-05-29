using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace complete_gamer_project.Models
{
	public class Player
	{
		[Key]
		public int Id { get; private set; }
		
		[Required]
		public string Name { get; private set; }
		
		[Required]
		public string Email { get; private set; }   

		[Required]
		public string Password { get; private set; }   
		
		[Required]
		[ForeignKey("Team")]
		public int TeamId { get; private set; }	
	}
}