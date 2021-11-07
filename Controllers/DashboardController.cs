using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamperPlanner.Models;
using CamperPlanner.Models.ViewModels;
using CamperPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CamperPlanner.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        //weergave va voertuigen bij de ingelogde user
        public IActionResult Calendar()
        {

            var user = _userManager.GetUserId(HttpContext.User);

            List<SelectListItem> items = new List<SelectListItem>();
            
            var Voertuigen = _context.Voertuigen.Include(m => m.ApplicationUser).Where(i => i.ApplicationUser.Id == user);
            foreach (var item in Voertuigen)
            {
                items.Add(new SelectListItem { Value = item.VoertuigID.ToString(), Text = item.Kenteken });
            }
            ViewBag.voertuigenList = items;

            return View();
        }
    }
}
