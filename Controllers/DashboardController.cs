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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CamperPlanner.Controllers
{
    [Authorize(Roles = "User")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        
        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (userId == null)
            {
                return NotFound();
            }
            else
            {
                DashboardViewModel dashboardViewModel = new DashboardViewModel();

                dashboardViewModel.Voertuigens = await _context.Voertuigen.Where(i => i.ApplicationUser.Id == userId).ToListAsync();

                dashboardViewModel.contracten = await _context.Contracten
                    .Include(m => m.Voertuig)
                    .Where(i => i.Voertuig.UserId == userId)
                    .ToListAsync();

                dashboardViewModel.user = await _userManager.FindByIdAsync(userId);

                ViewBag.Role = await _userManager.GetRolesAsync(user);
                ViewBag.activeUserId = userId;
                ViewBag.user = await _userManager.FindByIdAsync(userId);

                /*
                return View(await _context.Voertuigen
                    .Include(m => m.ApplicationUser)
                    .Include(m => m.Contract)
                    .Where(i => i.ApplicationUser.Id == userId)
                    .ToListAsync());
                */

                return View(dashboardViewModel);
            }
        }
    }
}
