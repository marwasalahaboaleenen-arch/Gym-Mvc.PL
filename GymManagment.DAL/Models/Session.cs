using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMmangment.DAL.Models
{
  public class Session:BaseEntity
    {
        public string Descriptio  { get; set; }
        public string Capacity { get; set; }

        public DateTime SatartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
