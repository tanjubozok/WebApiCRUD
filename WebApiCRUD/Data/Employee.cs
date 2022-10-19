using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCRUD.Data
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string FirstName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        [Required]
        [MaxLength(5)]
        [MinLength(6)]
        public string PostalCode { get; set; }
    }
}