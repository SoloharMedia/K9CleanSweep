using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using K9CleanSweep.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace K9CleanSweep.Controllers
{
    public class EmployeeController : Controller
    {
        string cs = "Server=(localdb)\\MSSQLLocalDB;Database=CleanSweepDB;Trusted_Connection=True;";

        public IActionResult EmpPage()
        {
            return View();
        }

        public IActionResult EmpJobs()
        {
            Employee employee = new Employee();
            List<Client> clients = Startup.utility.GetClients();
            var tuple = new Tuple<List<Client>, Employee>(clients, employee);
            return View(tuple);
        }

        [HttpGet]
        public IActionResult EmpLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmpLogin(Employee employee)
        {
            if (employee.EmpUserName == null || employee.EmpPassword == null)
            {
                return View("EmpLogin");
            }
            else
            {
                using (SqlConnection myConn = new SqlConnection(cs))
                {
                    SqlCommand login = new SqlCommand();
                    login.Connection = myConn;
                    myConn.Open();

                    login.CommandType = CommandType.StoredProcedure;
                    login.CommandText = "spEmpLogin";

                    login.Parameters.AddWithValue("@empUsername", employee.EmpUserName);
                    login.Parameters.AddWithValue("@empPassword", employee.EmpPassword);

                    object result = login.ExecuteScalar();
                    int resultID = Convert.ToInt16(result);
                    if (resultID == 0)
                    {
                        myConn.Close();
                        return View("EmpLogin");
                    }
                    else
                    {
                        ViewBag.EmpID = resultID;
                        Startup.empStatus.Login();
                        Startup.empStatus.SetEmpID(resultID);
                        Startup.empStatus.SetEmpUsername(employee.EmpUserName);
                        myConn.Close();
                        return View("EmpPage");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult EmpLogout()
        {
            Startup.empStatus.Logout();
            return View("Index");
        }
    }
}
