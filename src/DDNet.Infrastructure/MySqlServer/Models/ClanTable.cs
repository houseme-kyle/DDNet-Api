using System.ComponentModel.DataAnnotations;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class ClanTable
    {
        public int Id { get; set; }
        [StringLength(16)]
        public string Name { get; set; }
        [StringLength(3)]
        public string CountryCode { get; set; }
    }
}
