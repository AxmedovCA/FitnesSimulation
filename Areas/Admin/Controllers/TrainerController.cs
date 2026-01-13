using FitnessSimulation.Context;
using FitnessSimulation.Helpers;
using FitnessSimulation.Models;
using FitnessSimulation.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessSimulation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainerController : Controller
    {
        private readonly string folderPath;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public TrainerController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x => new TrainerGetVM()
            {
                Id = x.Id,
                FullName=x.FullName,
                Experience=x.Experience,
                ImagePath= x.ImagePath,
                CategoryName = x.Category.Name
            }).ToListAsync();
            
           
            return View(trainers);
        }
        public async Task<IActionResult> Create()
        {
            await SendCategoryWithViewBag();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TrainerCreateVM vm)
        {
            await SendCategoryWithViewBag();
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var isExistCategory = await _context.Categories.AnyAsync(x=>x.Id == vm.CategoryId);

            if(!isExistCategory) 
            {
                ModelState.AddModelError("CategoryId", "This Category not found");
                return View(vm);
            }
            if(vm.Image.Length > 2*1024*1024) 
            {
                ModelState.AddModelError("Image","Max size image 2 mb");
            }
            if (!vm.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image","Ancaq sekil formatinda");
                return View(vm);
            }
            string uniqieFileName = await vm.Image.FileUploadAsync(folderPath); 
            Trainer trainer = new()
            {
                FullName = vm.FullName,
                Experience = vm.Experience,
                CategoryId = vm.CategoryId,
                ImagePath = uniqieFileName
            };
          await  _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer is null) 
            {
            return NotFound();
            }
            _context.Trainers.Remove(trainer);   
            await _context.SaveChangesAsync();
            string deleteFile = Path.Combine(folderPath, trainer.ImagePath);
            FileHelpers.FileDelete(deleteFile);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if(trainer is null)
            {
                return NotFound();
            }
            TrainerUpdaetVM trainerUpdaetVM = new TrainerUpdaetVM()
            {
                Id = trainer.Id,
                FullName = trainer.FullName,
                Experience = trainer.Experience,
                CategoryId = trainer.CategoryId,
            };
            await SendCategoryWithViewBag();
            return View(trainerUpdaetVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TrainerUpdaetVM trainerUpdaetVM)
        {
            await SendCategoryWithViewBag();
            if(!ModelState.IsValid)
            {
                return View(trainerUpdaetVM);
            }
            var existProduct = await _context.Trainers.FindAsync(trainerUpdaetVM.Id);
            if(existProduct is null)
            {
                return BadRequest();
            }
            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == trainerUpdaetVM.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("CategoryId", "This Category not found");
                return View(trainerUpdaetVM);
            }
            if (trainerUpdaetVM.Image?.CheckSize(2)??false)
            {
                ModelState.AddModelError("Image", "Max size image 2 mb");
            }
            if (!trainerUpdaetVM.Image?.CheckType("image")??false)
            {
                ModelState.AddModelError("Image", "Ancaq sekil formatinda");
                return View(trainerUpdaetVM);
            }
            existProduct.FullName = trainerUpdaetVM.FullName;
            existProduct.Experience = trainerUpdaetVM.Experience;
            existProduct.CategoryId = trainerUpdaetVM.CategoryId;
            if(trainerUpdaetVM.Image is { })
            {
                string newImagePath = await trainerUpdaetVM.Image.FileUploadAsync(folderPath);
                string deleteImagePath = Path.Combine(folderPath, existProduct.ImagePath);
                FileHelpers.FileDelete(deleteImagePath);
                existProduct.ImagePath = newImagePath;
            }
            _context.Trainers.Update(existProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task SendCategoryWithViewBag()
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;
        }
    }
}
