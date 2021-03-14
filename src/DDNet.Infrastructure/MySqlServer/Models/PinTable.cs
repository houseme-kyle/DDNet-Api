using DDNet.Application.Entities;
using NodaTime;
using System;
using System.ComponentModel.DataAnnotations;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class PinTable
    {
        public int Id { get; set; }
        [StringLength(80)]
        public string Lookup { get; set; }
        [StringLength(30)]
        public string Salt { get; set; }
        public DateTime Instantiated { get; set; }

        public static PinTable New(Pin pin)
        {
            return new PinTable()
            {
                Lookup = pin.Lookup,
                Salt = pin.Salt,
                Instantiated = pin.Instantiated.ToDateTimeUtc()
            };
        }

        public Pin ToPin(string pinSalt, string emailSalt)
        {
            var utcDate = DateTime.SpecifyKind(Instantiated, DateTimeKind.Utc);

            return Pin.Hypdrate(Lookup, Salt, pinSalt, emailSalt, Instant.FromDateTimeUtc(utcDate));
        }
    }
}
