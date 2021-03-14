using System.ComponentModel.DataAnnotations;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class MapTable
    {
        public int Id { get; set; }
        public int DifficultyTierId { get; set; }
        public int TierId { get; set; }
        [StringLength(16)]
        public string Name { get; set; }

        public DifficultyTierTable DifficultyTier { get; set; }
    }
}
