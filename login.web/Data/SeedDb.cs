using login.commom.Enum;
using login.web.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelpers _userHelpers;

        public SeedDb(DataContext context, IUserHelpers userHelpers)
        {
            this._context = context;
            this._userHelpers = userHelpers;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            var admin = await CheckUserAsync("1010", "Emerson", "Palacio", "emersonpalaciootalvaro@gmail.com", "350 634 2747", "Cll34#56-6", UserType.Admin);
        }

        private async Task<UserEntity> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _userHelpers.GetUserByFindByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity {
                  FirstName =firstName,
                  LastName = lastName,
                  Email = email,
                  UserName = email,
                  PhoneNumber = phone,
                  Address = address,
                  Document = document,
                  UserType = userType
                };
                await _userHelpers.AddUserCreateAsync(user, "123456");
                await _userHelpers.AddUserAddToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelpers.CheckRolesExistAsync(UserType.Admin.ToString());
            await _userHelpers.CheckRolesExistAsync(UserType.Driver.ToString());
            await _userHelpers.CheckRolesExistAsync(UserType.User.ToString());
        }

    }
}
