using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserManager<AplicationUser> userManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            UserManager<AplicationUser> userManager,
            SignInManager<AplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        [Route("Login")]
        public  void Login()
        {
             signInManager.PasswordSignInAsync("Pesho@abv.bg", "123456", true, true);
            //return Redirect("Post");
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post(AplicationUser user)
        {
             var result = await userManager.CreateAsync(user, "123456");
            /*
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole {Name = "Admin"});
                var user1 = await userManager.GetUserAsync(User);
                var result1 = await userManager.AddToRoleAsync(user1, "Admin");
            }
            var user2 = User;
            if (User.IsInRole("Admin"))
            {
                return NotFound();
            }
          */
            

            return Ok(200);
        } 

        [HttpGet]
        public IEnumerable<WeatherForecast> Get1()
        {
            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
