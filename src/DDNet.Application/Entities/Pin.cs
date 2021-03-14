using NodaTime;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DDNet.Application.Entities
{
    public class Pin
    {
        private readonly Duration PinValidlity = Duration.FromHours(12);
        
        public string Lookup { get; set; }
        public string Salt { get; }
        public string SecretPinSalt { get; }
        public string SecretEmailSalt { get; }
        public Instant Instantiated { get; }

        private Instant Expiry => Instantiated.Plus(PinValidlity);

        public Pin(string lookup, string salt, string secretPinSalt, string secretEmailSalt, Instant instantiated)
        {
            Lookup = lookup;
            Salt = salt;
            SecretPinSalt = secretPinSalt;
            SecretEmailSalt = secretEmailSalt;
            Instantiated = instantiated;
        }

        public static Pin From(string emailAddress, string secretPinSalt, string secretEmailSalt, Instant instantiated)
        {
            var lookup = GeneratePinLookup(emailAddress, secretEmailSalt);
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return new Pin(lookup , salt, secretPinSalt, secretEmailSalt, instantiated);
        }

        public static Pin Hypdrate(string lookup, string salt, string secretPinSalt, string secretEmailSalt, Instant instantiated)
        {
            return new Pin(lookup, salt, secretPinSalt, secretEmailSalt, instantiated);
        }

        public bool Verify(string emailAddress, Guid code, Instant currentTime)
        {
            var internalCode = GeneratePinCode(emailAddress);
            var equalCodes = internalCode == code;
            var notExpired = Expiry >= currentTime;
            
            return equalCodes && notExpired;
        }

        public static string GeneratePinLookup(string emailAddress, string secretEmailSalt)
        {
            return BCrypt.Net.BCrypt.HashPassword(emailAddress, secretEmailSalt);
        }

        public Guid GeneratePinCode(string emailAddress)
        {
            using (MD5 sha = MD5.Create())
            {
                var hiddenEmail = $"{emailAddress}{Salt}{SecretPinSalt}";
                var hash = sha.ComputeHash(Encoding.Default.GetBytes(hiddenEmail));

                return new Guid(hash);
            }
        }
    }
}
