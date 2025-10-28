using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem118.Data;
using AttendanceSystem118.Models;
using AttendanceSystem118.Models.ViewModels;

namespace AttendanceSystem118.Controllers
{
    public class ClassController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Class/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Class/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newClass = new Class
                {
                    Name = model.Name,
                    Subject = model.Subject,
                    Section = model.Section,
                    CreatedDate = DateTime.Now
                };
                
                _context.Classes.Add(newClass);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Class created successfully!";
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
        
        // GET: Class/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .ThenInclude(s => s.Attendances)
                .FirstOrDefaultAsync(c => c.Id == id);
                
            if (classEntity == null)
            {
                return NotFound();
            }
            
            return View(classEntity);
        }
        
        // POST: Class/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            
            if (classEntity == null)
            {
                return NotFound();
            }
            
            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Class deleted successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}