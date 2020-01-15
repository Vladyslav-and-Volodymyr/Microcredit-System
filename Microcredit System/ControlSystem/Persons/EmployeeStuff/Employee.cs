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
        /*public void printReceipt(Debt debt)
        {
        }

        public bool BorrowMoney(Client client, double amount)
        {
            // TODO: implement
            return false;
        }

        public void ReturnMoney(Client client, double amount)
        {
            // TODO: implement
        }*/

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
            dataReader.Close();
            // _password = (string) dataReader["password"];
        }
    }
}
