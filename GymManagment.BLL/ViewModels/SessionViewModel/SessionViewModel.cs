using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.ViewModels.SessionViewModel
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = default;
        public int capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TrainerName { get; set; } = default;
        public String CategoryName { get; set; } = default;
        public int AvailableSlots { get; set; }

        //Computed properties
        public String DateDisplay => $"{StartDate:MMM dd , yyyy}";
        public String TimeRangeDisplay => $"{StartDate:hh:mm:tt} - {EndDate:hh:mm:tt}";
        public TimeSpan Duration => EndDate - StartDate;
        public String Status
        {   get
            {
                if (StartDate > DateTime.Now)
                    return "Upcomin";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
                    return "Ongoing";
                else
                    return "Completed";
            }
        }












    }
}
