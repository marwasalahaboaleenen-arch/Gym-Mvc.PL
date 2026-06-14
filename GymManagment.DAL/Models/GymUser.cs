using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Models
{
    public  abstract class GymUser:BaseEntity
    {
        public string  Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public  DateOnly  DateOfBirth { get; set; }

        public enumGender Gender { get; set; }

        public address  address { get; set; }
    }
    [Owned]
    public class address
    {
        public int BuildingNum { get; set; }
        public string Street { get; set; }
        public string city { get; set; }



    }
}
