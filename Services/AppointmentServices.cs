using CamperPlanner.Data;
using CamperPlanner.Models;
using CamperPlanner.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CamperPlanner.Services
{
    public class AppointmentServices : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AppointmentServices(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<Voertuigen> GetVoertuigenList()
        {
            //var user = _userManager.GetUserId(HttpContext.User);

            //var voertuigen = _context.Voertuigen.Include(m => m.ApplicationUser).Where(i => i.ApplicationUser.Id == user).ToList();

            return new List<Voertuigen>();
        }

        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            var startDate = DateTime.Parse(model.StartDate, CultureInfo.CreateSpecificCulture("en-US"));
            if (model != null && model.Appointment.AfspraakId > 0)
            {
                return 1;
            }
            else
            {
                Appointment appointment = new Appointment()
                {
                    BeginDatum = startDate,
                    VoertuigID = model.Voertuig.VoertuigID
                };
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return 2;
            }
        }

        
    }
}
