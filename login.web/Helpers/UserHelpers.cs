using login.web.Data;
using login.web.Data.Entity;
using login.web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.web.Helpers
{
    public class UserHelpers : IUserHelpers
    {



        private readonly DataContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public UserHelpers(DataContext context,
                           UserManager<UserEntity> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<UserEntity> GetUserByFindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> AddUserCreateAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task CheckRolesExistAsync(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName,
                });
            }
        }

        public async Task AddUserAddToRoleAsync(UserEntity user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> IsUserIsInRoleAsync(UserEntity user, string roleNme)
        {
            return await _userManager.IsInRoleAsync(user, roleNme);
        }

        public async Task<SignInResult> LoginPasswordSAsync(LoginViewModel model)
        {

            return await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RemerberMe,
                false );            
        }

        public async Task LogoutSigOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
