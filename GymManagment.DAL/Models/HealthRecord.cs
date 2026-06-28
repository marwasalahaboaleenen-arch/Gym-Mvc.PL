using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class HealthRecord:BaseEntity
    {
        public decimal height  { get; set; }
        public decimal weight { get; set; }
        public string bloodType { get; set; }
        public string note { get; set; }


        #region RelationShips


        public member Member { get; set; } = default;
        public int MemberId { get; set; }
        #endregion
    }
}
