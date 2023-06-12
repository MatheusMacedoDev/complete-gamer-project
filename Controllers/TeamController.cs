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
			if (invalidLogin()) {
				return LocalRedirect("~/Login/LoginArea");
			}
			
			ViewBag.Teams = context.Teams.ToList();
			return View();
		}
		
		[Route("Register")]
		public IActionResult Register(IFormCollection form) 
		{
			if (invalidLogin()) {
				return LocalRedirect("~/Login/LoginArea");
			}
			
			string name = form["Name"].ToString();
			string imageUrl;
			
			IFormFileCollection files = form.Files;
			imageUrl = uploadImage(files, "standard.png");
			
			Team insertingTeam = new Team(name, imageUrl);
			
			context.Add(insertingTeam);
			context.SaveChanges();
			
			return LocalRedirect("~/Team/List");
		}
		
		[Route("Delete/{id}")]
		public IActionResult Delete(int id) 
		{
			if (invalidLogin()) {
				return LocalRedirect("~/Login/LoginArea");
			}
			
			Team findedTeam = context.Teams.First(team => team.Id == id);
			context.Remove(findedTeam);
			context.SaveChanges();
			return LocalRedirect("~/Team/List");
		}
		
		[Route("Update")]
		public IActionResult Update(IFormCollection form)  
		{
			if (invalidLogin()) {
				return LocalRedirect("~/Login/LoginArea");
			}
			
			int id = int.Parse(form["Id"].ToString());
			string name = form["Name"].ToString();
			string imageUrl = form["ImageUrl"].ToString(); 
			
			IFormFileCollection files = form.Files;
			imageUrl = uploadImage(files, imageUrl);
			
			Team editedTeam = new Team(id, name, imageUrl);
			
			context.Teams.Update(editedTeam);
			context.SaveChanges();
			
			return LocalRedirect("~/Team/List");
		}
		
		[Route("Edit/{id}")]
		public IActionResult Edit(int id) 
		{
			if (invalidLogin()) {
				return LocalRedirect("~/Login/LoginArea");
			}
			
			Team team = context.Teams.First(team => team.Id == id);
			
			ViewBag.Team = team;
			
			return View("Edit");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
		
		private string uploadImage(IFormFileCollection files, string standardImageUrl) 
		{
			string imageUrl = standardImageUrl;
			
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
			
			return imageUrl;
		}
		
		private bool invalidLogin() 
		{
			return HttpContext.Session.GetString("UserName") == null; 
		}
	}
}