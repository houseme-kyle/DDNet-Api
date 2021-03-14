using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDNet.Client.Api.Controllers.Models
{
    public class AuthenticateApiModel
    {
        public string EmailAddress { get; set; }
        public Guid PinCode { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }
}
