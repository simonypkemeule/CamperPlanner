using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
