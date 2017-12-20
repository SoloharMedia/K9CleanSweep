using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    public class Employee
    {
        // Setup for Future Employees
        [Key]
        public int EmployeeID { get; set; }

        public string EmpUserName { get; set; }

        public string EmpPassword { get; set; }

        public string EmpName { get; set; }

        public string EmpAddress { get; set; }

        public string EmpPostalCode { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EmpStartDate { get; set; }
    }
}
