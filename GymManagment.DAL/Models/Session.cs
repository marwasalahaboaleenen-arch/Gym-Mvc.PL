using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
  public class Session:BaseEntity
    {
        public string Descriptio  { get; set; }
        public string Capacity { get; set; }

        public DateTime SatartDate { get; set; }
        public DateTime EndDate { get; set; }

        

        #region RelationShips
        public Trainer Trainer { get; set; }

        public int TrainerId { get; set; } 
        
        public Catgory Catgory { get; set; }
        public int CatgoryId { get; set; }

        public ICollection<Booking> SessionMember { get; set; }
        #endregion
    }
}
