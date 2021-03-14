using DDNet.Application.Types;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class DifficultyTierTable
    {
        public int Id { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int Tier { get; set; }
        public int Points { get; set; }
    }
}
