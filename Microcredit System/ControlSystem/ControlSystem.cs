using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Persons.ClientStuff;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem
{
    public class ControlSystem
    {
        /// <summary>
        /// Logging into system
        /// </summary>
        internal Employee LogIn(string _login, string _password)
        {
            return Database.DB.LogIn(_login, _password);
        }

        /// <summary>
        /// Adding client to database
        /// </summary>
        internal void AddClientToBase(string name, string surname, string passport)
        {
            Database.DB.AddClientToBase(new Client(name, surname, passport, 0));
        }
    }
}
