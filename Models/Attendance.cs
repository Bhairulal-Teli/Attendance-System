using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem118.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
        public DateTime Date { get; set; }
        
        public bool IsPresent { get; set; }
        
        public string? Remarks { get; set; }
    }
}