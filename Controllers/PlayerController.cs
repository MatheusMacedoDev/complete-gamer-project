using complete_gamer_project.Infra;
using complete_gamer_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace complete_gamer_project.Controllers
{
	[Route("[controller]")]
	public class PlayerController : Controller
	{
		private Context context = new Context();
		
		private readonly ILogger<PlayerController> _logger;

		public PlayerController(ILogger<PlayerController> logger)
		{
			_logger = logger;
		}

		[Route("List")]
		public IActionResult Index()
		{
			ViewBag.Players = context.Players.ToList();
			ViewBag.Teams = context.Teams.ToList();
			
			return View();
		}
		
		[Route("Register")]
		public IActionResult Register(IFormCollection form) 
		{
			string name = form["Name"].ToString();
			string email = form["Email"].ToString();
			string password = form["Password"].ToString();
			int teamId = int.Parse(form["Team"].ToString());

			Player player = new Player(name, email, password, teamId);

			context.Add(player);
			context.SaveChanges();

			return LocalRedirect("~/Player/List");
		}
		
		[Route("Delete/{id}")]
		public IActionResult Delete(int id) 
		{
			Player findedPlayer = context.Players.First(player => player.Id == id);
			context.Remove(findedPlayer);
			context.SaveChanges();
			return LocalRedirect("~/Player/List");
		}
		
		[Route("Edit/{Id}")]
		public IActionResult Edit(int id) 
		{
			Player findedPlayer = context.Players.First(player => player.Id == id);
			ViewBag.Player = findedPlayer;

			ViewBag.Teams = context.Teams.ToList();

			return View();
		}
		
		[Route("Update")]
		public IActionResult Update(IFormCollection form) 
		{
			int id = int.Parse(form["Id"].ToString());
			string name = form["Name"].ToString();
			string email = form["Email"].ToString();
			string password = form["Password"].ToString();
			int teamId = int.Parse(form["Team"].ToString());
			
			Player editedPlayer = new Player(id, name, email, password, teamId);
			
			context.Players.Update(editedPlayer);
			context.SaveChanges();
			
			return LocalRedirect("~/Player/List");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}