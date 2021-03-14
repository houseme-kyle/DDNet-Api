using NodaTime;
using System;

namespace DDNet.Application.Entities
{
    public class User
    {
        public int? Id { get; set; }
        public Guid AccessToken { get; private set; }
        public string Name { get; private set; }
        public string EmailHash { get; private set; }
        public string CountryCode { get; private set; }
        public Instant CreatedDate { get; }

        public User(int? id, Guid accessToken, string name, string emailHash, string countryCode, Instant createdDate)
        {
            Id = id;
            AccessToken = accessToken;
            Name = name;
            EmailHash = emailHash;
            CountryCode = countryCode;
            CreatedDate = createdDate;
        }

        public static User From(string name, string emailHash, string countryCode, Instant createdDate)
        {
            return new User(null, Guid.NewGuid(), name, emailHash, countryCode, createdDate);
        }

        public static User Hypdrate(int id, Guid accessToken, string name, string emailHash, string countryCode, Instant createdDate)
        {
            return new User(id, accessToken, name, emailHash, countryCode, createdDate);
        }

        public void Update(string name, string emailHash, string countryCode)
        {
            AccessToken = Guid.NewGuid();
            Name = name;
            EmailHash = emailHash;
            CountryCode = countryCode;
        }

        public static string GenerateEmailHash(string emailAddress)
        {
            return BCrypt.Net.BCrypt.HashPassword(emailAddress);
        }
    }
}
