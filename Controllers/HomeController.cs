
using FitnessSimulation.Context;
using FitnessSimulation.Migrations;
using FitnessSimulation.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FitnessSimulation.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x => new TrainerGetVM()
            {
                Id = x.Id,
                FullName = x.FullName,
                Experience = x.Experience,
                ImagePath = x.ImagePath,
                CategoryName = x.Category.Name
            }).ToListAsync();
            return View(trainers);
        }

        
    }
}
