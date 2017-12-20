using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    //For Mike
    //When Logging In As User / Client use  Username = Deekan     Password = password
    public class Status
    {
        string username;
        string password;
        string fullName;
        string address;
        string postalCode;
        string email;
        int dogAmount;
        decimal yardSize;
        string notes;
        string service;
        string serviceDateTime;
        int userID;
        bool loggedIn;

        public Status()
        {
            username = null;
            userID = 0;
            loggedIn = false;
        }

        public void SetUserID(int id)
        {
            userID = id;
        }
        public int GetUserID()
        {
            return userID;
        }
        public void SetUsername(string _username)
        {
            username = _username;
        }
        public string GetUsername()
        {
            return username;
        }
        public void Login()
        {
            loggedIn = true;
        }
        public void Logout()
        {
            userID = 0;
            loggedIn = false;
        }
        public bool GetStatus()
        {
            return loggedIn;
        }

        public string GetUserPassword()
        {
            return password;
        }
        public void SetUserPassword(string _password)
        {
            password = _password;
        }
        public string GetUserEmail()
        {
            return email;
        }
        public void SetUserEmail(string _email)
        {
            email = _email;
        }
        public string GetFullName()
        {
            return fullName;
        }
        public void SetFullName(string _fullName)
        {
            fullName = _fullName;
        }
        public string GetAddress()
        {
            return address;
        }
        public void SetAddress(string _address)
        {
            address = _address;
        }
        public string GetPostalCode()
        {
            return postalCode;
        }
        public void SetPostalCode(string _postalCode)
        {
            postalCode = _postalCode;
        }
        public int GetDogAmount()
        {
            return dogAmount;
        }
        public void SetDogAmount(int _dogAmount)
        {
            dogAmount = _dogAmount;
        }
        public decimal GetYardSize()
        {
            return yardSize;
        }
        public void SetYardSize(decimal _yardSize)
        {
            yardSize = _yardSize;
        }
        public string GetService()
        {
            return service;
        }
        public void SetService(string _service)
        {
            service = _service;
        }
        public string GetServiceDateTime()
        {
            return serviceDateTime;
        }
        public void SetServiceDateTime(string _serviceDateTime)
        {
            serviceDateTime = _serviceDateTime;
        }
        public string GetNotes()
        {
            return notes;
        }
        public void SetNotes(string _notes)
        {
            notes = _notes;
        }
    }
}
