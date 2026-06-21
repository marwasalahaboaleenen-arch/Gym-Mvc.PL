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

        public Address  address { get; set; }
    }
    [Owned]
    public class Address
    {
        public int BuildingNumb { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;



    }
}
