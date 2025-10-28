using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem118.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string EnrollmentNumber { get; set; } = string.Empty;
        
        [EmailAddress]
        public string? Email { get; set; }
        
        [Phone]
        public string? Phone { get; set; }
        
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
        
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}