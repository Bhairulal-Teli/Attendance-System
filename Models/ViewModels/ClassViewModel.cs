using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem118.Models.ViewModels
{
    public class ClassViewModel
    {
        [Required(ErrorMessage = "Class name is required")]
        [StringLength(100, ErrorMessage = "Class name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Subject is required")]
        [StringLength(50, ErrorMessage = "Subject cannot exceed 50 characters")]
        public string Subject { get; set; } = string.Empty;
        
        [StringLength(20, ErrorMessage = "Section cannot exceed 20 characters")]
        public string Section { get; set; } = string.Empty;
    }
}