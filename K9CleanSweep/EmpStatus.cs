using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K9CleanSweep.Models
{
    public class EmpStatus
    {
        //For Mike
        //When Logging In As an Employee use  Username = DeekanDeekan142     Password = password

        string empUsername;
        int empID;
        bool empLoggedIn;

        public EmpStatus()
        {
            empUsername = null;
            empID = 0;
            empLoggedIn = false;
        }

        public void SetEmpID(int id)
        {
            empID = id;
        }

        public int GetEmpID()
        {
            return empID;
        }

        public void SetEmpUsername(string _empUsername)
        {
            empUsername = _empUsername;
        }
        public string GetEmpUsername()
        {
            return empUsername;
        }
        public void Login()
        {
            empLoggedIn = true;
        }
        public void Logout()
        {
            empID = 0;
            empLoggedIn = false;
        }
        public bool GetStatus()
        {
            return empLoggedIn;
        }
    }
}

