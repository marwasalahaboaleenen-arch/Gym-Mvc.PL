
namespace GymManagment.DAL.Models
{
   public class member:GymUser
    {
        public string? photo   { get; set; }
        public Address Address { get; set; } = null!;
        #region RelationShips
        public HealthRecord HealthRecord { get; set; } = default;

        public ICollection<Membership> plans { get; set; }

     public ICollection<Booking> MemberSession { get; set; }

        public static implicit operator member(member v)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
