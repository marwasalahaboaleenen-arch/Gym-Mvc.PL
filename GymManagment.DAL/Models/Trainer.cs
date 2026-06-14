using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Trainer:GymUser
    {
        public  Spacialty specialty { get; set; }



        #region RelationShips

        public ICollection<Session> Sessions { get; set; }


        #endregion

    }
}
