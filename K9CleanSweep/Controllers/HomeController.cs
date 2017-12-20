using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using K9CleanSweep.Models;

namespace K9CleanSweep.Controllers
{
    //Login to Employee Portal With Username = Deekan142142 Password = password
    public class HomeController : Controller
    {
        string cs = "Server=(localdb)\\MSSQLLocalDB;Database=CleanSweepDB;Trusted_Connection=True;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BookService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookService(Client client)
        {
            if (ModelState.IsValid)
            {
                //If All Required Input Fields Are Valid
                using (SqlConnection myConn = new SqlConnection(cs))
                {
                    SqlCommand bookService = new SqlCommand();
                    bookService.Connection = myConn;
                    myConn.Open();

                    bookService.CommandType = CommandType.StoredProcedure;
                    bookService.CommandText = "spBookService";

                    bookService.Parameters.AddWithValue("@username", client.ClientUserName);
                    bookService.Parameters.AddWithValue("@password", client.Password);
                    bookService.Parameters.AddWithValue("@email", client.Email);
                    bookService.Parameters.AddWithValue("@fullName", client.ClientName);
                    bookService.Parameters.AddWithValue("@address", client.Address);
                    bookService.Parameters.AddWithValue("@postalCode", client.PostalCode);
                    bookService.Parameters.AddWithValue("@dogs", client.AmountOfDogs);
                    bookService.Parameters.AddWithValue("@yard", client.YardSqFootage);
                    bookService.Parameters.AddWithValue("@service", client.Service);
                    string tempDateTime = "noDate";
                    bookService.Parameters.AddWithValue("@serviceDateTime", tempDateTime);
                    bookService.Parameters.AddWithValue("@notes", client.Notes);
                    bookService.ExecuteNonQuery();
                    myConn.Close();
                    return View("Index");
                }    
            }
            else
            {
                //There Is A Validation Error (returns BookService View)
                return View();
            }
        }

        public IActionResult Reviews(Client client)
        {
            List<Review> reviews = Startup.utility.GetReviews();
            
            var tuple = new Tuple<List<Review>, Client>(reviews, client);
            return View(tuple);
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
