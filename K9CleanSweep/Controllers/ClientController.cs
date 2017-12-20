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
    public class ClientController : Controller
    {
        string cs = "Server=(localdb)\\MSSQLLocalDB;Database=CleanSweepDB;Trusted_Connection=True;";

        public IActionResult ClientOrder(Client client)
        {
            int _clientID = Startup.status.GetUserID();
            string queryString = "SELECT * FROM dbo.clients WHERE ClientID = " + _clientID;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand getClient = new SqlCommand(queryString, conn);
                conn.Open();

                SqlDataReader reader = getClient.ExecuteReader();

                int clientID = reader.GetOrdinal("ClientID");
                int clientUserName = reader.GetOrdinal("ClientUserName");
                int clientPassword = reader.GetOrdinal("Password");
                int clientEmail = reader.GetOrdinal("Email");
                int clientFullName = reader.GetOrdinal("ClientName");
                int clientAddress = reader.GetOrdinal("Address");
                int clientPostalCode = reader.GetOrdinal("PostalCode");
                int clientDogs = reader.GetOrdinal("AmountOfDogs");
                int clientYard = reader.GetOrdinal("YardSqFootage");
                int clientService = reader.GetOrdinal("Service");
                int clientServiceDateTime = reader.GetOrdinal("ServiceDateTime");
                int clientNotes = reader.GetOrdinal("Notes");

                while (reader.Read())
                {
                    client.ClientID = reader.GetInt32(clientID);
                    client.ClientUserName = reader.GetString(clientUserName);
                    client.Password = reader.GetString(clientPassword);
                    client.Email = reader.GetString(clientEmail);
                    client.ClientName = reader.GetString(clientFullName);
                    client.Address = reader.GetString(clientAddress);
                    client.PostalCode = reader.GetString(clientPostalCode);
                    client.AmountOfDogs = reader.GetInt32(clientDogs);
                    client.YardSqFootage = reader.GetDecimal(clientYard);
                    client.Service = reader.GetString(clientService);
                    client.ServiceDateTime = reader.GetString(clientServiceDateTime);
                    client.Notes = reader.GetString(clientNotes);
                }
                //Sets Up Users Info On The Status Object
                Startup.status.SetUsername(client.ClientUserName);
                Startup.status.SetUserPassword(client.Password);
                Startup.status.SetUserEmail(client.Email);
                Startup.status.SetAddress(client.Address);
                Startup.status.SetPostalCode(client.PostalCode);
                Startup.status.SetFullName(client.ClientName);
                Startup.status.SetService(client.Service);
                Startup.status.SetServiceDateTime(client.ServiceDateTime);
                Startup.status.SetDogAmount(client.AmountOfDogs);
                Startup.status.SetYardSize(client.YardSqFootage);
                Startup.status.SetNotes(client.Notes);
                conn.Close();
            }
            return View(client);
        }

        [HttpGet]
        public ViewResult EditClientDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditClientDetails(Client client)
        {
            
                using (SqlConnection myConn = new SqlConnection(cs))
                {
                    SqlCommand updateClientInfo = new SqlCommand();
                    updateClientInfo.Connection = myConn;
                    myConn.Open();

                    updateClientInfo.CommandType = CommandType.StoredProcedure;
                    updateClientInfo.CommandText = "spUpdateClientInfo";

                    updateClientInfo.Parameters.AddWithValue("@clientID", client.ClientID);
                    updateClientInfo.Parameters.AddWithValue("@username", client.ClientUserName);
                    updateClientInfo.Parameters.AddWithValue("@password", client.Password);
                    updateClientInfo.Parameters.AddWithValue("@email", client.Email);
                    updateClientInfo.Parameters.AddWithValue("@fullName", client.ClientName);
                    updateClientInfo.Parameters.AddWithValue("@address", client.Address);
                    updateClientInfo.Parameters.AddWithValue("@postalCode", client.PostalCode);
                    updateClientInfo.Parameters.AddWithValue("@dogs", client.AmountOfDogs);
                    updateClientInfo.Parameters.AddWithValue("@yard", client.YardSqFootage);
                    updateClientInfo.Parameters.AddWithValue("@service", client.Service);

                    //Reset The status object (All Of Currently Logged in Clients Info)
                    Startup.status.SetUsername(client.ClientUserName);
                    Startup.status.SetUserPassword(client.Password);
                    Startup.status.SetUserEmail(client.Email);
                    Startup.status.SetAddress(client.Address);
                    Startup.status.SetPostalCode(client.PostalCode);
                    Startup.status.SetFullName(client.ClientName);
                    Startup.status.SetService(client.Service);
                    Startup.status.SetDogAmount(client.AmountOfDogs);
                    Startup.status.SetYardSize(client.YardSqFootage);
                    updateClientInfo.ExecuteNonQuery();
                    myConn.Close();
                    return View("ClientOrder", client);
                }
           
        }

        [HttpGet]
        public IActionResult LeaveReview()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SubmitReview(Review review)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection myConn = new SqlConnection(cs))
                {
                    SqlCommand submitReview = new SqlCommand();
                    submitReview.Connection = myConn;
                    myConn.Open();

                    submitReview.CommandType = CommandType.StoredProcedure;
                    submitReview.CommandText = "spSubmitReview";

                    submitReview.Parameters.AddWithValue("@reviewTitle", review.Title);
                    submitReview.Parameters.AddWithValue("@reviewMessage", review.Message);
                    submitReview.Parameters.AddWithValue("@clientID", Startup.status.GetUserID());
                    submitReview.Parameters.AddWithValue("@author", Startup.status.GetUsername());
                    submitReview.Parameters.AddWithValue("@rating", review.starRating);

                    submitReview.ExecuteNonQuery();
                    myConn.Close();

                    return View("Index");
                }
            }
            else
            {
                return View("ClientOrder");
            }
        }

        public IActionResult Calendar(Client client)
        {
            //Checking To See If Client Has Time / Day Picked Already
            int _clientID = Startup.status.GetUserID();
            string queryString = "SELECT * FROM dbo.clients WHERE ClientID = " + _clientID;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand getClient = new SqlCommand(queryString, conn);
                conn.Open();

                SqlDataReader reader = getClient.ExecuteReader();

                int clientID = reader.GetOrdinal("ClientID");
                int clientServiceDateTime = reader.GetOrdinal("ServiceDateTime");

                while (reader.Read())
                {
                    client.ClientID = reader.GetInt32(clientID);
                    client.ServiceDateTime = reader.GetString(clientServiceDateTime);
                }
                if (client.ServiceDateTime == "noDate")
                {
                    //Client Needs To Pick A Date
                    return View();
                }
                else
                {
                    //Client Has Already Picked A Date
                    return View("Index");
                }
            }
        }

        [HttpPost]
        public IActionResult SetDayTime(string timeDay)
        {
            int _clientID = Startup.status.GetUserID();
            using (SqlConnection myConn = new SqlConnection(cs))
            {
                SqlCommand updateClientInfo = new SqlCommand();
                updateClientInfo.Connection = myConn;
                myConn.Open();

                updateClientInfo.CommandType = CommandType.StoredProcedure;
                updateClientInfo.CommandText = "spAddClientDayTime";

                updateClientInfo.Parameters.AddWithValue("@clientID", _clientID);
                updateClientInfo.Parameters.AddWithValue("@dayTime", timeDay);

                updateClientInfo.ExecuteNonQuery();
                myConn.Close();
                return View("ClientOrder");
            }
        }
        [HttpPost]
        public IActionResult Login(Client client)
        {

            if (client.ClientUserName == null || client.Password == null)
            {
                return View("Index");
            }
            else
            {
                using (SqlConnection myConn = new SqlConnection(cs))
                {
                    SqlCommand login = new SqlCommand();
                    login.Connection = myConn;
                    myConn.Open();

                    login.CommandType = CommandType.StoredProcedure;
                    login.CommandText = "spLogin";

                    login.Parameters.AddWithValue("@username", client.ClientUserName);
                    login.Parameters.AddWithValue("@password", client.Password);

                    object result = login.ExecuteScalar();
                    int resultID = Convert.ToInt16(result);
                    if (resultID == 0)
                    {
                        myConn.Close();
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.userID = resultID;
                        //Sets Up Users Info On The Status Object
                        Startup.status.Login();
                        Startup.status.SetUserID(resultID);
                        Startup.status.SetUsername(client.ClientUserName);
                        Startup.status.SetUserPassword(client.Password);
                        Startup.status.SetUserEmail(client.Email);
                        Startup.status.SetAddress(client.Address);
                        Startup.status.SetPostalCode(client.PostalCode);
                        Startup.status.SetFullName(client.ClientName);
                        Startup.status.SetService(client.Service);
                        Startup.status.SetServiceDateTime(client.ServiceDateTime);
                        Startup.status.SetDogAmount(client.AmountOfDogs);
                        Startup.status.SetYardSize(client.YardSqFootage);
                        Startup.status.SetNotes(client.Notes);
                        myConn.Close();

                        Startup.utility.GetSchedule();

                        return View("Index");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Startup.status.Logout();
            return View("Index");
        }
    }
}
