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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}