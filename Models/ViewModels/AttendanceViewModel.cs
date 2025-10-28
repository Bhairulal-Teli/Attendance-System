namespace AttendanceSystem118.Models.ViewModels
{
    public class AttendanceViewModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public List<StudentAttendance> Students { get; set; } = new();
    }
    
    public class StudentAttendance
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string EnrollmentNumber { get; set; } = string.Empty;
        public bool IsPresent { get; set; }
    }
}