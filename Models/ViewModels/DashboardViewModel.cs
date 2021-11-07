using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CamperPlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamperPlanner.Models.ViewModels
{
    public class DashboardViewModel
    {
        public ApplicationUser userVm { get; set; }
        public Voertuigen voertuigVM { get; set; }
    }
}
