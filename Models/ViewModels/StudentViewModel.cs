using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem118.Models.ViewModels
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Student name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Enrollment number is required")]
        [StringLength(50, ErrorMessage = "Enrollment number cannot exceed 50 characters")]
        public string EnrollmentNumber { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
        
        public int ClassId { get; set; }
    }
}