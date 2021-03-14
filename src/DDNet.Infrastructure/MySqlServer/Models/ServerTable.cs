using DDNet.Application.Types;
using System.ComponentModel.DataAnnotations;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class ServerTable
    {
        public int Id { get; set; }
        [StringLength(16)]
        public string Name { get; set; }
        [StringLength(3)]
        public string Region { get; set; }
        [StringLength(16)]
        public DifficultyType Difficulty { get; set; }
    }
}
