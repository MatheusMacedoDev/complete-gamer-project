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
		public Team Team { get; private set; }
		
		public Player(string name, string email, string password, int teamId) 
		{
			this.Name = name;
			this.Email = email;
			this.Password = password;
			this.TeamId = teamId;
		}
		
		public Player(int id, string name, string email, string password, int teamId) 
		{
			this.Id = id;
			this.Name = name;
			this.Email = email;
			this.Password = password;
			this.TeamId = teamId;
		}
	}
}