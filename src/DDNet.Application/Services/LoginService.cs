using DDNet.Application.Interfaces;
using System.Threading.Tasks;
using DDNet.Application.Entities;
using System;
using NodaTime;

namespace DDNet.Application.Services
{
    public class LoginService
    {
        private readonly IAuthDispatcher authDispatcher;
        private readonly IPinRepository pinRepo;
        private readonly IUserRepository userRepo;
        private readonly IClock systemClock;

        private Instant CurrentTime => systemClock.GetCurrentInstant();

        public LoginService(IAuthDispatcher authDispatcher, IPinRepository pinRepo, IUserRepository userRepo, IClock systemClock)
        {
            this.authDispatcher = authDispatcher;
            this.pinRepo = pinRepo;
            this.userRepo = userRepo;
            this.systemClock = systemClock;
        }
        
        public async Task SendPin(string emailAddress, Pin pin)
        {
            await pinRepo.Add(pin);

            var pinEmailCode = pin.GeneratePinCode(emailAddress);
            await authDispatcher.DispatchPin(emailAddress, pinEmailCode);
        }

        public async Task<bool> VerifyPin(string emailAddress, string pinLookup, Guid pinCode)
        {
            var pinIsInvalid = !await pinRepo.Exists(pinLookup);
            if (pinIsInvalid)
            {
                return false;
            }

            var existingPin = await pinRepo.Find(pinLookup);
            return existingPin.Verify(emailAddress, pinCode, CurrentTime);
        }

        public async Task<Guid> UpsertUser(string name, string emailAddress, string countryCode)
        {
            var emailHash = User.GenerateEmailHash(emailAddress);
            var emailExists = await userRepo.Exists(emailHash);
            if (emailExists)
            {
                var fetchedUser = await userRepo.Find(emailHash);
                fetchedUser.Update(name, emailHash, countryCode);
                await userRepo.Update(fetchedUser);

                return fetchedUser.AccessToken;
            }
            else
            {
                var newUser = User.From(name, emailHash, countryCode, CurrentTime);
                await userRepo.Add(newUser);

                return newUser.AccessToken;
            }
        }
    }
}
