using DDNet.Application.Entities;
using DDNet.Application.Services;
using DDNet.Client.Api.Controllers.Models;
using DDNet.Infrastructure.MySqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NodaTime;
using System.Threading.Tasks;

namespace DDNet.Client.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly LoginService loginService;
        private readonly IClock systemClock;
        private readonly SecurityConfig config;

        private Instant CurrentTime => systemClock.GetCurrentInstant();

        public LoginController(ILogger<LoginController> logger, LoginService loginService, IOptions<SecurityConfig> config, IClock systemClock)
        {
            this.logger = logger;
            this.loginService = loginService;
            this.systemClock = systemClock;
            this.config = config.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Login([FromQuery] string emailAddress)
        {
            var pin = Pin.From(emailAddress, config.SecretPinSalt, config.SecretEmailSalt, CurrentTime);
            await loginService.SendPin(emailAddress, pin);

            var tempCodeForTesting = pin.GeneratePinCode(emailAddress);
            return Ok(tempCodeForTesting);
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateApiModel model)
        {
            var pinLookup = Pin.GeneratePinLookup(model.EmailAddress, config.SecretEmailSalt);
            var validPin = await loginService.VerifyPin(model.EmailAddress, pinLookup, model.PinCode);
            if (!validPin)
            {
                return BadRequest("Pin provided is invalid");
            }

            var accessToken = await loginService.UpsertUser(model.Name, model.EmailAddress, model.CountryCode);
            return Ok(accessToken);
        }
    }
}
