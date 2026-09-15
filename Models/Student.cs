using System.ComponentModel.DataAnnotations;

namespace StudentManagementMVC.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [PastDate(ErrorMessage = "Date of birth must be in the past.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Grade { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }

    // Custom validation attribute ensuring the date is in the past
    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date < DateTime.Now;
            }
            return false;
        }
    }
}
