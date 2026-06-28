using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public class Membership : BaseEntity
    {
        public member Member { get; set; }
        public member MemberId { get; set; }
        public Plan Plan { get; set; }
        public Plan PlanId { get; set; }

        //StartDate == CreatedAt == BaseEntity

        public DateTime EndDate { get; set; }

        //Read Only Property
        //Read Only Property Doesnot Transfer INTO Table in Database
        public String Status => EndDate > DateTime.Now ? "Active" : "Expired";
        public bool IsActive { get; set; }
       

    }
}
