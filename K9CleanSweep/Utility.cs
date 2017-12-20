using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using K9CleanSweep.Models;
using System.Data.SqlClient;

namespace K9CleanSweep
{
    public class Utility
    {
        string cs = "Server=(localdb)\\MSSQLLocalDB;Database=CleanSweepDB;Trusted_Connection=True;";

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            string queryString = "SELECT * FROM dbo.clients";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand getClients = new SqlCommand(queryString, conn);
                conn.Open();

                SqlDataReader reader = getClients.ExecuteReader();

                int clientID = reader.GetOrdinal("ClientID");
                int clientUserName = reader.GetOrdinal("ClientUserName");
                int clientFullName = reader.GetOrdinal("ClientName");
                int clientAddress = reader.GetOrdinal("Address");
                int clientPostalCode = reader.GetOrdinal("PostalCode");
                int clientDogs = reader.GetOrdinal("AmountOfDogs");
                int clientYard = reader.GetOrdinal("YardSqFootage");
                int clientService = reader.GetOrdinal("Service");
                int clientServiceDateTime = reader.GetOrdinal("ServiceDateTime");
                int clientServiceNotes = reader.GetOrdinal("Notes");

                while (reader.Read())
                {
                    Client client = new Client();
                    client.ClientID = reader.GetInt32(clientID);
                    client.ClientUserName = reader.GetString(clientUserName);
                    client.ClientName = reader.GetString(clientFullName);
                    client.Address = reader.GetString(clientAddress);
                    client.PostalCode = reader.GetString(clientPostalCode);
                    client.AmountOfDogs = reader.GetInt32(clientDogs);
                    client.YardSqFootage = reader.GetDecimal(clientYard);
                    client.Service = reader.GetString(clientService);
                    client.ServiceDateTime = reader.GetString(clientServiceDateTime);
                    client.Notes = reader.GetString(clientServiceNotes);
                    clients.Add(client);
                }
                conn.Close();
            }
            return clients;
        }

        public List<Review> GetReviews()
        {
            List<Review> reviews = new List<Review>();
            string queryString = "SELECT * FROM dbo.reviews";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand getReviews = new SqlCommand(queryString, conn);
                conn.Open();

                SqlDataReader reader = getReviews.ExecuteReader();

                int clientID = reader.GetOrdinal("ClientID");
                int reviewTitle = reader.GetOrdinal("Title");
                int reviewMessage = reader.GetOrdinal("Message");
                int reviewAuthor = reader.GetOrdinal("Author");
                int reviewRating = reader.GetOrdinal("starRating");

                while (reader.Read())
                {
                    Review review = new Review();
                    review.ClientID = reader.GetInt32(clientID);
                    review.Title = reader.GetString(reviewTitle);
                    review.Message = reader.GetString(reviewMessage);
                    review.Author = reader.GetString(reviewAuthor);
                    review.starRating = reader.GetInt32(reviewRating);
                    reviews.Add(review);
                }
                conn.Close();
            }
            return reviews;
        }

        public void GetSchedule()
        {
            //Schedule bookedDates = new Schedule();
            string queryString = "SELECT ServiceDateTime FROM dbo.clients";

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand getSchedule = new SqlCommand(queryString, conn);
                conn.Open();

                SqlDataReader reader = getSchedule.ExecuteReader();

                int serviceDateTime = reader.GetOrdinal("ServiceDateTime");

                while (reader.Read())
                {
                    string bookedDay = reader.GetString(serviceDateTime);

                    switch (bookedDay)
                    {
                        case "00":
                            {
                                Startup.schedule.Booked00 = true;
                                break;
                            }
                        case "01":
                            {
                                Startup.schedule.Booked01 = true;
                                break;
                            }
                        case "02":
                            {
                                Startup.schedule.Booked02 = true;
                                break;
                            }
                        case "03":
                            {
                                Startup.schedule.Booked03 = true;
                                break;
                            }
                        case "04":
                            {
                                Startup.schedule.Booked04 = true;
                                break;
                            }
                        case "05":
                            {
                                Startup.schedule.Booked05 = true;
                                break;
                            }
                        case "06":
                            {
                                Startup.schedule.Booked06 = true;
                                break;
                            }
                        case "07":
                            {
                                Startup.schedule.Booked07 = true;
                                break;
                            }
                        case "08":
                            {
                                Startup.schedule.Booked08 = true;
                                break;
                            }
                        case "09":
                            {
                                Startup.schedule.Booked09 = true;
                                break;
                            }
                        case "010":
                            {
                                Startup.schedule.Booked010 = true;
                                break;
                            }
                        case "011":
                            {
                                Startup.schedule.Booked011 = true;
                                break;
                            }
                        case "012":
                            {
                                Startup.schedule.Booked012 = true;
                                break;
                            }
                        case "10":
                            {
                                Startup.schedule.Booked10 = true;
                                break;
                            }
                        case "11":
                            {
                                Startup.schedule.Booked11 = true;
                                break;
                            }
                        case "12":
                            {
                                Startup.schedule.Booked12 = true;
                                break;
                            }
                        case "13":
                            {
                                Startup.schedule.Booked13 = true;
                                break;
                            }
                        case "14":
                            {
                                Startup.schedule.Booked14 = true;
                                break;
                            }
                        case "15":
                            {
                                Startup.schedule.Booked15 = true;
                                break;
                            }
                        case "16":
                            {
                                Startup.schedule.Booked16 = true;
                                break;
                            }
                        case "17":
                            {
                                Startup.schedule.Booked17 = true;
                                break;
                            }
                        case "18":
                            {
                                Startup.schedule.Booked18 = true;
                                break;
                            }
                        case "19":
                            {
                                Startup.schedule.Booked19 = true;
                                break;
                            }
                        case "110":
                            {
                                Startup.schedule.Booked110 = true;
                                break;
                            }
                        case "111":
                            {
                                Startup.schedule.Booked111 = true;
                                break;
                            }
                        case "112":
                            {
                                Startup.schedule.Booked112 = true;
                                break;
                            }
                        case "20":
                            {
                                Startup.schedule.Booked20 = true;
                                break;
                            }
                        case "21":
                            {
                                Startup.schedule.Booked21 = true;
                                break;
                            }
                        case "22":
                            {
                                Startup.schedule.Booked22 = true;
                                break;
                            }
                        case "23":
                            {
                                Startup.schedule.Booked23 = true;
                                break;
                            }
                        case "24":
                            {
                                Startup.schedule.Booked24 = true;
                                break;
                            }
                        case "25":
                            {
                                Startup.schedule.Booked25 = true;
                                break;
                            }
                        case "26":
                            {
                                Startup.schedule.Booked26 = true;
                                break;
                            }
                        case "27":
                            {
                                Startup.schedule.Booked27 = true;
                                break;
                            }
                        case "28":
                            {
                                Startup.schedule.Booked28 = true;
                                break;
                            }
                        case "29":
                            {
                                Startup.schedule.Booked29 = true;
                                break;
                            }
                        case "210":
                            {
                                Startup.schedule.Booked210 = true;
                                break;
                            }
                        case "211":
                            {
                                Startup.schedule.Booked211 = true;
                                break;
                            }
                        case "212":
                            {
                                Startup.schedule.Booked212 = true;
                                break;
                            }
                        case "30":
                            {
                                Startup.schedule.Booked30 = true;
                                break;
                            }
                        case "31":
                            {
                                Startup.schedule.Booked31 = true;
                                break;
                            }
                        case "32":
                            {
                                Startup.schedule.Booked32 = true;
                                break;
                            }
                        case "33":
                            {
                                Startup.schedule.Booked33 = true;
                                break;
                            }
                        case "34":
                            {
                                Startup.schedule.Booked34 = true;
                                break;
                            }
                        case "35":
                            {
                                Startup.schedule.Booked35 = true;
                                break;
                            }
                        case "36":
                            {
                                Startup.schedule.Booked36 = true;
                                break;
                            }
                        case "37":
                            {
                                Startup.schedule.Booked37 = true;
                                break;
                            }
                        case "38":
                            {
                                Startup.schedule.Booked38 = true;
                                break;
                            }
                        case "39":
                            {
                                Startup.schedule.Booked39 = true;
                                break;
                            }
                        case "310":
                            {
                                Startup.schedule.Booked310 = true;
                                break;
                            }
                        case "311":
                            {
                                Startup.schedule.Booked311 = true;
                                break;
                            }
                        case "312":
                            {
                                Startup.schedule.Booked312 = true;
                                break;
                            }
                        case "40":
                            {
                                Startup.schedule.Booked40 = true;
                                break;
                            }
                        case "41":
                            {
                                Startup.schedule.Booked41 = true;
                                break;
                            }
                        case "42":
                            {
                                Startup.schedule.Booked42 = true;
                                break;
                            }
                        case "43":
                            {
                                Startup.schedule.Booked43 = true;
                                break;
                            }
                        case "44":
                            {
                                Startup.schedule.Booked44 = true;
                                break;
                            }
                        case "45":
                            {
                                Startup.schedule.Booked45 = true;
                                break;
                            }
                        case "46":
                            {
                                Startup.schedule.Booked46 = true;
                                break;
                            }
                        case "47":
                            {
                                Startup.schedule.Booked47 = true;
                                break;
                            }
                        case "48":
                            {
                                Startup.schedule.Booked48 = true;
                                break;
                            }
                        case "49":
                            {
                                Startup.schedule.Booked49 = true;
                                break;
                            }
                        case "410":
                            {
                                Startup.schedule.Booked410 = true;
                                break;
                            }
                        case "411":
                            {
                                Startup.schedule.Booked411 = true;
                                break;
                            }
                        case "412":
                            {
                                Startup.schedule.Booked412 = true;
                                break;
                            }
                        case "50":
                            {
                                Startup.schedule.Booked50 = true;
                                break;
                            }
                        case "51":
                            {
                                Startup.schedule.Booked51 = true;
                                break;
                            }
                        case "52":
                            {
                                Startup.schedule.Booked52 = true;
                                break;
                            }
                        case "53":
                            {
                                Startup.schedule.Booked53 = true;
                                break;
                            }
                        case "54":
                            {
                                Startup.schedule.Booked54 = true;
                                break;
                            }
                        case "55":
                            {
                                Startup.schedule.Booked55 = true;
                                break;
                            }
                        case "56":
                            {
                                Startup.schedule.Booked56 = true;
                                break;
                            }
                        case "57":
                            {
                                Startup.schedule.Booked57 = true;
                                break;
                            }
                        case "58":
                            {
                                Startup.schedule.Booked58 = true;
                                break;
                            }
                        case "59":
                            {
                                Startup.schedule.Booked59 = true;
                                break;
                            }
                        case "510":
                            {
                                Startup.schedule.Booked510 = true;
                                break;
                            }
                        case "511":
                            {
                                Startup.schedule.Booked511 = true;
                                break;
                            }
                        case "512":
                            {
                                Startup.schedule.Booked512 = true;
                                break;
                            }
                        case "60":
                            {
                                Startup.schedule.Booked60 = true;
                                break;
                            }
                        case "61":
                            {
                                Startup.schedule.Booked61 = true;
                                break;
                            }
                        case "62":
                            {
                                Startup.schedule.Booked62 = true;
                                break;
                            }
                        case "63":
                            {
                                Startup.schedule.Booked63 = true;
                                break;
                            }
                        case "64":
                            {
                                Startup.schedule.Booked64 = true;
                                break;
                            }
                        case "65":
                            {
                                Startup.schedule.Booked65 = true;
                                break;
                            }
                        case "66":
                            {
                                Startup.schedule.Booked66 = true;
                                break;
                            }
                        case "67":
                            {
                                Startup.schedule.Booked67 = true;
                                break;
                            }
                        case "68":
                            {
                                Startup.schedule.Booked68 = true;
                                break;
                            }
                        case "69":
                            {
                                Startup.schedule.Booked69 = true;
                                break;
                            }
                        case "610":
                            {
                                Startup.schedule.Booked610 = true;
                                break;
                            }
                        case "611":
                            {
                                Startup.schedule.Booked611 = true;
                                break;
                            }
                        case "612":
                            {
                                Startup.schedule.Booked612 = true;
                                break;
                            }
                    }
                }
                conn.Close();
            }
            //return bookedDates;
        }
    }
}
