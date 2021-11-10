using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public Appointment Appointment { get; set; }
        public ApplicationUser User { get; set; }
        public Voertuigen Voertuig { get; set; }
        public string StartDate { get; set; }
    }
}
