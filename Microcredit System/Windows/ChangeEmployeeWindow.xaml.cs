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
using System.Windows.Shapes;

namespace Microcredit_System.Windows
{
    /// <summary>
    /// Interaction logic for ChangeEmployeeWindow.xaml
    /// </summary>
    public partial class ChangeEmployeeWindow : Window
    {
        internal ChangeEmployeeWindow(Employee employee)
        {
            InitializeComponent();

            txtAdmin.Text = (employee is Admin).ToString();

            txtLogin.Text = employee.Login;
            txtName.Text = employee.Name;
            txtSurname.Text = employee.Surname;
            txtPesel.Text = employee.Pesel;
            txtPassword.Text = employee.Password;
        }
    }
}
