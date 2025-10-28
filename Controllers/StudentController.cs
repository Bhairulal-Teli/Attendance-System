using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystem118.Data;
using AttendanceSystem118.Models;
using AttendanceSystem118.Models.ViewModels;

namespace AttendanceSystem118.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Student/Create
        public async Task<IActionResult> Create(int classId)
        {
            var classEntity = await _context.Classes.FindAsync(classId);
            if (classEntity == null)
            {
                return NotFound();
            }
            
            ViewBag.ClassId = classId;
            ViewBag.ClassName = classEntity.Name;
            
            return View();
        }
        
        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    EnrollmentNumber = model.EnrollmentNumber,
                    Email = model.Email,
                    Phone = model.Phone,
                    ClassId = model.ClassId
                };
                
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Student added successfully!";
                return RedirectToAction("Details", "Class", new { id = model.ClassId });
            }
            
            var classEntity = await _context.Classes.FindAsync(model.ClassId);
            ViewBag.ClassId = model.ClassId;
            ViewBag.ClassName = classEntity?.Name;
            
            return View(model);
        }
        
        // GET: Student/Profile/5
        public async Task<IActionResult> Profile(int id)
        {
            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Attendances)
                .FirstOrDefaultAsync(s => s.Id == id);
                
            if (student == null)
            {
                return NotFound();
            }
            
            return View(student);
        }
        
        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }
            
            var classId = student.ClassId;
            
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Student deleted successfully!";
            return RedirectToAction("Details", "Class", new { id = classId });
        }
        
        // GET: Student/MarkAttendance/5
        public async Task<IActionResult> MarkAttendance(int classId)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId);
                
            if (classEntity == null)
            {
                return NotFound();
            }
            
            var model = new AttendanceViewModel
            {
                ClassId = classId,
                ClassName = classEntity.Name,
                Date = DateTime.Today,
                Students = classEntity.Students.Select(s => new StudentAttendance
                {
                    StudentId = s.Id,
                    StudentName = s.Name,
                    EnrollmentNumber = s.EnrollmentNumber,
                    IsPresent = true // Default to present
                }).ToList()
            };
            
            return View(model);
        }
        
        // POST: Student/MarkAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAttendance(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if attendance already exists for this date
                var existingAttendance = await _context.Attendances
                    .Where(a => a.Student.ClassId == model.ClassId && a.Date.Date == model.Date.Date)
                    .ToListAsync();
                
                if (existingAttendance.Any())
                {
                    // Remove existing attendance for this date
                    _context.Attendances.RemoveRange(existingAttendance);
                }
                
                // Add new attendance records
                foreach (var student in model.Students)
                {
                    var attendance = new Attendance
                    {
                        StudentId = student.StudentId,
                        Date = model.Date.Date,
                        IsPresent = student.IsPresent
                    };
                    
                    _context.Attendances.Add(attendance);
                }
                
                await _context.SaveChangesAsync();
                
                TempData["Success"] = "Attendance marked successfully!";
                return RedirectToAction("Details", "Class", new { id = model.ClassId });
            }
            
            return View(model);
        }
    }
}