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
			string imageUrl;
			
			IFormFileCollection files = form.Files;
			if (files.Count > 0) 
			{
				IFormFile file = files[0];
				
				string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Teams");
				
				if (!Directory.Exists(folderPath)) 
				{
					Directory.CreateDirectory(folderPath);
				}
				
				string filePath = Path.Combine(folderPath, file.FileName);				
				using (FileStream stream = new FileStream(filePath, FileMode.Create)) 
				{
					file.CopyTo(stream);
				}
				
				imageUrl = file.FileName;
			}
			else 
			{
				imageUrl = "standard.png";
			}
			
			
			Team insertingTeam = new Team(name, imageUrl);
			
			context.Add(insertingTeam);
			context.SaveChanges();
			
			return LocalRedirect("~/Team/List");
		}
		
		[Route("Delete")]
		public IActionResult Delete(int id) 
		{
			Team findedTeam = context.Teams.First(team => team.Id == id);
			context.Remove(findedTeam);
			context.SaveChanges();
			return LocalRedirect("~/Team/Delete");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}