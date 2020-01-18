using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
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
    /// Interaction logic for UserControlEmployeeList.xaml
    /// </summary>
    public partial class UserControlEmployeeList : UserControl
    {

        List<Employee> items;

        public UserControlEmployeeList()
        {
            InitializeComponent();

            items = new List<Employee>();
            Refresh();
        }

        private void Refresh()
        {
            items = Database.DB.GetEmployees();
            ListOfEmployees.ItemsSource = items;
        }

        private void ListOfEmployees_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
