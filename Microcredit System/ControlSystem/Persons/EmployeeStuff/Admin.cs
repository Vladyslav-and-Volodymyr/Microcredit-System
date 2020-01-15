using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Persons.EmployeeStuff
{
    class Admin : Employee
    {
        internal Admin(MySqlDataReader dataReader) : base(dataReader) { }
    }
}
