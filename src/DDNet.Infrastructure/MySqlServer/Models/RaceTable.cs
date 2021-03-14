using System;
using System.ComponentModel.DataAnnotations;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class RaceTable
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Time { get; set; }
        public bool Team { get; set; }
        [StringLength(36)]
        public string RaceCode { get; set; }

        public MapTable Map { get; set; }
        public UserTable User { get; set; }
        public ServerTable Server { get; set; }
    }
}
