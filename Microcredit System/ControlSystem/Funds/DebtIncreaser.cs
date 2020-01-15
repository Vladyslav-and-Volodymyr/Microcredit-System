using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microcredit_System.ControlSystem.Funds
{
    class DebtIncreaser
    {
        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(ThreadProc));
        }
    }
}
