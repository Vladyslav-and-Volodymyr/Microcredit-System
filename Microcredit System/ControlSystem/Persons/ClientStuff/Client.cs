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

        public string Name { get => _name; }
        public string Surname { get => _surname; }
        public string Passport { get => _passport; }

        protected internal Client(string name, string surname, string passport)
        {
            _name = name;
            _surname = surname;
            _passport = passport;
        }
    }
}
