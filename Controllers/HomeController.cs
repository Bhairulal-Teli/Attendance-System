using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem118.Data;

namespace AttendanceSystem118.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes
                .Include(c => c.Students)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
                
            return View(classes);
        }
    }
}