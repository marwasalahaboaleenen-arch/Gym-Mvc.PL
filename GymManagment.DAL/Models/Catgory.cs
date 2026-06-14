using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Catgory:BaseEntity
    {
        public string  CatgoryName { get; set; }


        #region RelationShips

        public ICollection<Session> Sessions { get; set; }
        #endregion
    }
}
