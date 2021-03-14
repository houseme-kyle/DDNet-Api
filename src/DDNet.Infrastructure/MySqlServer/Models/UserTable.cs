using DDNet.Application.Entities;
using NodaTime;
using System;
using System.ComponentModel.DataAnnotations;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class UserTable
    {
        public int Id { get; set; }
        public int? ClanId { get; set; }
        public Guid AccessKey { get; set; }
        [StringLength(16)]
        public string Name { get; set; }
        [StringLength(80)]
        public string EmailHash { get; set; }
        [StringLength(3)]
        public string CountryCode { get; set; }
        public DateTime Timestamp { get; set; }

        public ClanTable Clan { get; set; }

        public static UserTable New(User user)
        {
            return new UserTable()
            {
                AccessKey = user.AccessToken,
                Name = user.Name,
                EmailHash = user.EmailHash,
                CountryCode = user.CountryCode,
                Timestamp = user.CreatedDate.ToDateTimeUtc()
            };
        }

        public static UserTable Existing(User user)
        {
            return new UserTable()
            {
                Id = user.Id.Value,
                AccessKey = user.AccessToken,
                Name = user.Name,
                EmailHash = user.EmailHash,
                CountryCode = user.CountryCode
            };
        }

        public User ToUser()
        {
            var utcDate = DateTime.SpecifyKind(Timestamp, DateTimeKind.Utc);

            return User.Hypdrate(Id, AccessKey, Name, EmailHash, CountryCode, Instant.FromDateTimeUtc(utcDate));
        }
    }
}
