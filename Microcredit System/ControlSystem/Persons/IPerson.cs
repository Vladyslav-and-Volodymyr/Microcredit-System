using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Persons
{
    interface IPerson
    {
        /// <summary>
        /// Person's name
        /// </summary>
        String Name
        {
            get;
        }

        /// <summary>
        /// Person's surname
        /// </summary>
        String Surname
        {
            get;
        }
    }

}
