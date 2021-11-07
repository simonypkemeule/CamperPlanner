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

namespace CamperPlanner.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calendar()
        {

            var user = _userManager.GetUserId(HttpContext.User);


            
            var Voertuigen = _context.Voertuigen.Include(m => m.ApplicationUser).Where(i => i.ApplicationUser.Id == user);
            ViewBag.voertuigenList = Voertuigen;

            return View();
        }
    }
}
