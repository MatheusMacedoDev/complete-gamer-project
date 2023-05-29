using System.ComponentModel.DataAnnotations;

namespace complete_gamer_project.Models
{
	public class Team
	{
		[Key]
		public int Id { get; private set; }
		
		[Required]
		public string Name { get; private set; }

		[Required]
		public string Image { get; private set; }
		
		public ICollection<Player> Players { get; private set; }
	}
}