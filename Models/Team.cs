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
		
		public Team(string name, string image) 
		{
			this.Name = name;
			this.Image = image;
		}
		
		public Team(int id, string name, string image) 
		{
			this.Id = id;
			this.Name = name;
			this.Image = image;
		}
	}
}