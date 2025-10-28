using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem118.Models
{
    public class Class
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string Section { get; set; } = string.Empty;
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}