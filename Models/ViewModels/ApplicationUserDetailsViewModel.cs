using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Models.ViewModels
{
    public class ApplicationUserDetailsViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public Voertuigen Voertuigen { get; set; }


    }
}
