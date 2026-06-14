
using GymManagment.DAL.Models;



namespace GymManagment.DAL.Models
{
    public class  Plan: BaseEntity
    {
        

        public string Name { get; set; } = default!;

        public  string  Describtion { get; set; }=default!;

        public  decimal Price { get; set; }

        public  int DurationDay { get; set; }

        public  bool IsActive { get; set; }

        #region RelationShips
        public ICollection<Membership> Members { get; set; }
        #endregion

    }
}
