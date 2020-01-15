using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Persons.ClientStuff
{
    class Client : IPerson
    {

        private string _name;
        private string _surname;
        private string _passport;
        private float _debt;

        public string Name { get => _name; }
        public string Surname { get => _surname; }
        public string Passport { get => _passport; }
        public float Debt { get => _debt; set => _debt = value; }

        protected internal Client(string name, string surname, string passport, float debt)
        {
            _name = name;
            _surname = surname;
            _passport = passport;
            _debt = debt;
        }

        protected internal Client(MySqlDataReader dataReader) 
            : this((string) dataReader["name"], 
                   (string) dataReader["surname"], 
                   (string) dataReader["passport"], 
                   (float) dataReader["debt"])
        { }
    }
}
