using WebAppLab.Services.EmailServices;
using WebLabApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebLabApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ISender _emailSender;
		public HomeController(ILogger<HomeController> logger, ISender emailSender)
		{
			_logger = logger;
			_emailSender=emailSender;
		}
        public IActionResult Index()
        {
            _logger.LogInformation("This is an information log from HomeController.");
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> SendEmail([FromBody] EmailModel vm)
		{
			var _vm = vm;
			if (_vm == null)
			{
				return BadRequest();
			}
			await _emailSender.SendEmailAsync(_vm.Email, "Test", _vm.MessageText);
			return Ok();
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}