using DDNet.Application.Types;

namespace DDNet.Infrastructure.SqlServer.Models
{
    public class UserRoleTable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserRoleType Role { get; set; }
    }
}
