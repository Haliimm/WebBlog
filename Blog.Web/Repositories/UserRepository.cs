using Blog.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await authDbContext.Users.ToListAsync();

            var seperAdminUser = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@gmail.com");

            if (seperAdminUser is not null)
            {
                users.Remove(seperAdminUser);
            }

            return users;
        }
    }
}
