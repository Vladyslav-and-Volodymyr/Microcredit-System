using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Persons.EmployeeStuff
{
    class Employee : IPerson
    {

        private string _name;
        private string _surname;
        private string _pesel;
        // private string _passport;

        private string _login;
        private string _password;

        public string Login { get => _login; }
        public string Password { get => _password; }
        public string Surname { get => _surname; }
        public string Pesel { get => _pesel; }
        // public string Passport { get => _passport; }
        public string Name { get => _name; }

        internal Employee(MySqlDataReader dataReader)
        {
            _name = (string) dataReader["name"];
            _surname = (string) dataReader["surname"];
            _pesel = (string) dataReader["pesel"];
            // _passport = (string) dataReader["id"];
            _login = (string) dataReader["login"];
            _password = (string)dataReader["password"];
            // _password = (string) dataReader["password"];
        }

        public Employee(string name, string surname, string pesel, string login, string password)
        {
            _name = name;
            _surname = surname;
            _pesel = pesel;
            _login = login;
            _password = password;
        }
    }
}
