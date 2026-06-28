using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Booking : BaseEntity
    {

        public member Member { get; set; }
        public member MemberId { get; set; }

        public Session Session { get; set; }
        public Session SessionId { get; set; }
         //Booking Date == CreatedAt
         public bool IsAttened { get; set; }

    }
}
