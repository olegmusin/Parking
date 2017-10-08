using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ParkingApp.Data.Domain.Identity
{
    public class ParkingIdentityInitializer
    {

        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<AppUser> _userMgr;

        public ParkingIdentityInitializer(UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task Seed()
        {
            var user = await _userMgr.FindByNameAsync("omusin");
            var user1 = await _userMgr.FindByNameAsync("customer1");
            // Add Users
            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                  
                    await _roleMgr.CreateAsync(role);
                }
              

                user = new AppUser()
                {
                    UserName = "omusin",
                    FirstName = "Oleg",
                    LastName = "Musin",
                    Email = "o.musin@outlook.com"
                };
                

                var userResult = await _userMgr.CreateAsync(user, "P@ssw0rd");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }
            if (user1 == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Customer")))
                {
                    var role = new IdentityRole("Customer");
                   
                    await _roleMgr.CreateAsync(role);
                }
                user1 = new AppUser()
                {
                    UserName = "customer1",
                    FirstName = "New",
                    LastName = "Customer",
                    Email = "customer@customercompany.com"
                };
                var userResult = await _userMgr.CreateAsync(user1, "P@ssw0rd");
                var roleResult = await _userMgr.AddToRoleAsync(user1, "Customer");
                var claimResult = await _userMgr.AddClaimAsync(user1, new Claim("User", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user1 and roles");
                }
            }
        }
    }


}

