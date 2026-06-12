using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYMmangment.DAL.Models
{
    public class BaseEntity

    {

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpadatedAt { get; set; }
    }
}
