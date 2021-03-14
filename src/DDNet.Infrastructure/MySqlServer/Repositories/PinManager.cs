using DDNet.Application.Entities;
using DDNet.Application.Interfaces;
using DDNet.Infrastructure.SqlServer;
using DDNet.Infrastructure.SqlServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DDNet.Infrastructure.MySqlServer.Repositories
{
    public class PinManager : IPinRepository
    {
        private readonly DdNetDbContext context;
        private readonly SecurityConfig config;

        public PinManager(DdNetDbContext context, IOptions<SecurityConfig> config)
        {
            this.context = context;
            this.config = config.Value;
        }

        public async Task<bool> Exists(string pinLookup)
        {
            return await context.Pins.AnyAsync(x => x.Lookup == pinLookup);
        }

        public async Task<Pin> Find(string pinLookup)
        {
            var entry = await context.Pins.SingleAsync(x => x.Lookup == pinLookup);
            return entry.ToPin(config.SecretPinSalt, config.SecretEmailSalt);
        }

        public async Task<Pin> Get(int id)
        {
            var entry = await context.Pins.SingleAsync(x => x.Id == id);
            return entry.ToPin(config.SecretPinSalt, config.SecretEmailSalt);
        }

        public async Task Add(Pin entity)
        {
            var addedEntry = PinTable.New(entity);
            await context.Pins.AddAsync(addedEntry);

            await context.SaveChangesAsync();
        }

        public Task Update(Pin entity)
        {//Refactor to have better defined interfaces to avoid this
            throw new InvalidOperationException("A pin value can never be updated");
        }

        public Task Delete(Pin entity)
        {
            throw new InvalidOperationException("A pin value can never be deleted but is expired");
        }
    }
}
