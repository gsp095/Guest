using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Guesty.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
       
        private readonly RoleManager<IdentityRole> _RoleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
           
            _RoleManager = roleManager;

        }

        public async Task<IActionResult> Index()
        {

            string[] roleNames = {
                 "Owner", "Manager", "Team", "Partner","Guest"
            };

            IdentityResult roleResult;

            foreach (var roleName in roleNames)

            {
                //creating the roles and seeding them to the database

                var roleExist = await _RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await _RoleManager.CreateAsync(new IdentityRole(roleName));

                }
            }

            return null;
        }
    }
}