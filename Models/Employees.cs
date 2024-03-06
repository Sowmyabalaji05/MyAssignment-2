using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ofz1.Models
{
        public class Employees
        {

            [Key]
          
            public int Id { get; set; }
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; } 
            public string Email { get; set; }
            public int ManagerID { get; set; }
            public string ManagerName { get; set; } 
            public bool IsActive { get; set; }
            public DateTime LastUpdated { get; set; }


    }
    }
    

