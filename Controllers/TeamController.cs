using complete_gamer_project.Infra;
using complete_gamer_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace complete_gamer_project.Controllers
{
	[Route("[controller]")]
	public class TeamController : Controller
	{
		private readonly ILogger<TeamController> _logger;
		
		private Context context = new Context();

		public TeamController(ILogger<TeamController> logger)
		{
			_logger = logger;
		}

		[Route("List")]
		public IActionResult Index()
		{
			ViewBag.Teams = context.Teams.ToList();
			return View();
		}
		
		[Route("Register")]
		public IActionResult Register(IFormCollection form) 
		{
			string name = form["Name"].ToString();
			string imageUrl = form["Image"].ToString();
			Team insertingTeam = new Team(name, imageUrl);
			
			context.Add(insertingTeam);
			context.SaveChanges();
			
			return LocalRedirect("~/Team/List");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}