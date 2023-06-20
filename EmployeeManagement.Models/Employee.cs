using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int Age { get; set; }

        public string? Address { get; set; }

        public string? ImageUrl { get; set; }

    }
}