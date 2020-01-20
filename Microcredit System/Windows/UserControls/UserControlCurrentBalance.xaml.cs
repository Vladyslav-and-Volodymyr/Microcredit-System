using Microcredit_System.ControlSystem.DatabaseStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Microcredit_System.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlCurrentBalance.xaml
    /// </summary>
    public partial class UserControlCurrentBalance : UserControl, IRefreshable
    {
        public UserControlCurrentBalance()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            var balance = Database.DB.GetBalance();

            txtEur.Text = balance.Eur.ToString();
            txtUsd.Text = balance.Usd.ToString();
            txtPln.Text = balance.Pln.ToString();
        }
    }
}
