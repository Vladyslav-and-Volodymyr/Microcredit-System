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
        private bool _middleOfTheDay = true;

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
        }

        private void Run()
        {
            while (true)
            {
                if (!_middleOfTheDay)
                {
                    if(DateTime.Now.TimeOfDay.Hours > 1 && DateTime.Now.TimeOfDay.Hours < 23)
                    {
                        _middleOfTheDay = true;
                    }
                }
                else
                {
                    if(DateTime.Now.TimeOfDay.Hours >= 23 || DateTime.Now.TimeOfDay.Hours <= 1)
                    {
                        IncreaseDebts();
                        _middleOfTheDay = false;
                    }
                }
            }
        }

        private void IncreaseDebts()
        {
            throw new NotImplementedException();
        }
    }
}