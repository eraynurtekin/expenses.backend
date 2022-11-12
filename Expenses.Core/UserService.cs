using Expenses.Core.CustomExceptions;
using Expenses.Core.DTO;
using Expenses.Core.Utilities;
using Expenses.DB;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Expenses.Core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;
        private readonly IPasswordHasher passwordHasher;

        public UserService(AppDbContext context,IPasswordHasher passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
            if (dbUser == null || passwordHasher.VerifyHashedPassword(dbUser.Password,user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid Username or Password");
            }
            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await context.Users.FirstOrDefaultAsync(x=>x.Username == user.Username);
            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }
            user.Password = passwordHasher.HashPassword(user.Password);
            await context.AddAsync(user);
            await context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateUserToken(user.Username)
            };
        }
    }
}
