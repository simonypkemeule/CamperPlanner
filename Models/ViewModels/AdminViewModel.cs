using System.Collections.Generic;

namespace CamperPlanner.Models.ViewModels
{
    public class AdminViewModel
    {
        public ApplicationUser user { get; set; }
        public List<ApplicationUser> userList { get; set; }
        public List<Voertuigen> Voertuigens { get; set; }
        public List<Contracten> contracten { get; set; }

        //public Appointments appointments { get; set; }
    }
}
