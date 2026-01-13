using FitnessSimulation.Context;
using FitnessSimulation.Models;
using FitnessSimulation.ViewModels.CategoryViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessSimulation.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController(AppDbContext _context) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var categories = await _context.Categories.Select(x => new CategoryGetVM()
			{
				Id = x.Id,
				Name = x.Name,
			}).ToListAsync();
			return View(categories);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(CategoryCreateVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			var isExistCategory = await _context.Categories.AnyAsync(x => x.Name == vm.Name);
			if (isExistCategory)
			{
				ModelState.AddModelError("Name", "This category already exist");
				return View(vm);
			}
			Category category = new()
			{
				Name = vm.Name,
			};
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Update(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category is null)
			{
				return NotFound();
			}
			CategoryUpdateVM vm = new CategoryUpdateVM()
			{
				Name = category.Name,
			};
			return View(vm);

		}
		[HttpPost]
		public async Task<IActionResult> Update(CategoryUpdateVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			var category = await _context.Categories.FindAsync(vm.Id);
			if (category is null)
			{
				return NotFound();
			}
			var isExistCategory = await _context.Categories.AnyAsync(x => x.Name == vm.Name);
			if (isExistCategory)
			{
				ModelState.AddModelError("Name", "This category already exists");
				return View(vm);
			}
			category.Name = vm.Name;
			_context.Categories.Update(category);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}
		public async Task<IActionResult> Delete(int id)
		{
			var category = await _context.Categories.Include(x => x.Trainers).FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
                return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
	}
}