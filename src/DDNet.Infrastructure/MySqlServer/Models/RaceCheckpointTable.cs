namespace DDNet.Infrastructure.SqlServer.Models
{
    public class RaceCheckpointTable
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public int Checkpoint { get; set; }
        public decimal Time { get; set; }

        public RaceTable Race { get; set; }
    }
}
