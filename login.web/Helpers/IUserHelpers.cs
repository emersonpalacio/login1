using login.web.Data.Entity;
using login.web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.web
{
     public interface IUserHelpers
    {
        Task AddUserAddToRoleAsync(UserEntity user, string roleName);
        Task<IdentityResult> AddUserCreateAsync(UserEntity user, string password);
        Task CheckRolesExistAsync(string roleName);
        Task<UserEntity> GetUserByFindByEmailAsync(string email);
        Task<bool> IsUserIsInRoleAsync(UserEntity user, string roleNme);

        Task<SignInResult> LoginPasswordSAsync(LoginViewModel model);
        Task LogoutSigOutAsync();

    }
}
