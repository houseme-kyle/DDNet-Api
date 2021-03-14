using DDNet.Application.Entities;
using DDNet.Application.Interfaces;
using DDNet.Infrastructure.SqlServer;
using DDNet.Infrastructure.SqlServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDNet.Infrastructure.MySqlServer.Repositories
{
    public class UserManager : IUserRepository
    {
        private readonly DdNetDbContext context;

        public UserManager(DdNetDbContext context)
        {
            this.context = context;
        }

        public async Task Add(User entity)
        {
            var addedEntry = UserTable.New(entity);
            await context.Users.AddAsync(addedEntry);
            await context.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            var entry = UserTable.Existing(entity);
            context.Users.Remove(entry);

            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(string emailHash)
        {
            return await context.Users.AnyAsync(x => x.EmailHash == emailHash);
        }

        public async Task<User> Find(string emailHash)
        {
            var entry = await context.Users.SingleAsync(x => x.EmailHash == emailHash);

            return entry.ToUser();
        }

        public async Task<User> Get(int id)
        {
            var entry = await context.Users.SingleAsync(x => x.Id == id);

            return entry.ToUser();
        }

        public async Task Update(User entity)
        {
            var updatedUser = UserTable.Existing(entity);

            var entry = await context.Users.SingleAsync(x => x.Id == updatedUser.Id);
            entry.Name = updatedUser.Name;
            entry.AccessKey = updatedUser.AccessKey;
            entry.CountryCode = updatedUser.CountryCode;

            context.Update(updatedUser);
            await context.SaveChangesAsync();
        }
    }
}
