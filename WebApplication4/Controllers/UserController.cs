using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication4.ApiModels;
using WebApplication4.Data;
using WebApplication4.Identity;

namespace WebApplication4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IIdentityService identity;
        private readonly UserManager<AplicationUser> userManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext db;
        private readonly AppSettings appSettings;

        public UserController(IIdentityService identity,
            UserManager<AplicationUser> userManager,
            SignInManager<AplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
            IOptions<AppSettings> appSettings
            )
        {
            this.identity = identity;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
            this.appSettings = appSettings.Value;
        }
        [Authorize(Roles = "Admin")]
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult> Create(AplicationUser user)
        {
            var name = User.Identity.Name;
           //var result =  await userManager.CreateAsync(user, "123456");
            var user1 = db.Users.FirstOrDefault();
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                //var user1 = await userManager.GetUserAsync(User);
                var result1 = await userManager.AddToRoleAsync(user1, "Admin");
            }

            return Content("200");
        }
        [Route("login")]
        [HttpGet]
        public  ActionResult Login()
        {
             var signInStatus = signInManager.PasswordSignInAsync("Pesho@abv.bg", "123456", true, true);

            bool t = signInStatus.IsCompletedSuccessfully;
           
            string user = User.Identity.Name;

           // var user = db.Users.Where(x => x.Email == userModel.Email).Select(x => new {Id = x.Id,UserName = x.UserName}).FirstOrDefault();
            /*var roleId = db.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.RoleId).FirstOrDefault();
            var applicationRole = await this.roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            var jwt = identity.GenerateJwt(appSettings.Secret, user.Id, user.UserName, applicationRole.Name);
           
            return jwt;*/
           // return new ResultModel(new { JWT = jwt });
            return Ok(200);
        }
    }
}
