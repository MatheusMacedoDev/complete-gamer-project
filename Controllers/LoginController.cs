using complete_gamer_project.Infra;
using complete_gamer_project.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace complete_gamer_project.Controllers
{
	[Route("[controller]")]
	public class LoginController : Controller
	{
		private Context context = new Context();

		[TempData]
		public string Message { get; set; }
		
		private readonly ILogger<LoginController> _logger;

		public LoginController(ILogger<LoginController> logger)
		{
			_logger = logger;
		}

		[Route("LoginArea")]
		public IActionResult Index()
		{
			return View();
		}
		
		[Route("Login")]
		public IActionResult Login(IFormCollection form) 
		{
			string email = form["Email"].ToString();
			string password = form["Password"].ToString();
			
			Player? findedPlayer = context.Players.FirstOrDefault(player => player.Email == email);
			
			if (findedPlayer != null) 
			{	
				if (findedPlayer.Password == password) 
				{
					HttpContext.Session.SetString("UserName", findedPlayer.Name);
					Message = $"Bem-vindo, {findedPlayer.Name}";
				}
				else 
				{
					Message = "Senha incorreta!";
					return LocalRedirect("~/Login/LoginArea");
				}
				
				return LocalRedirect("~/");
			}
			else 
			{
				Message = "E-mail inválido!";
				return LocalRedirect("~/Login/LoginArea");
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}