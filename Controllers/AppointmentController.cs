using CamperPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CamperPlanner.Models;

namespace CamperPlanner.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AppointmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserId(HttpContext.User);
            

            ViewBag.voertuigenList = GetVoertuigen();
            var Voertuigen = _context.Voertuigen.Include(m => m.ApplicationUser).Where(i => i.ApplicationUser.Id == user);
            return View();
        }

        public dynamic GetVoertuigen()
        {
            throw new NotImplementedException();
        }
    }
}
