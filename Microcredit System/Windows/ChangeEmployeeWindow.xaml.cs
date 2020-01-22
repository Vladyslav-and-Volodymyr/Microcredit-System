using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
using Microcredit_System.Windows.UserControls;
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
        private Employee employee;
        private UserControlEmployeeList userControl;

        internal ChangeEmployeeWindow(Employee employee, UserControlEmployeeList userControl)
        {
            InitializeComponent();

            this.employee = employee;
            this.userControl = userControl;

            txtAdmin.Text = (employee is Admin).ToString();

            txtLogin.Text = employee.Login;
            txtName.Text = employee.Name;
            txtSurname.Text = employee.Surname;
            txtPesel.Text = employee.Pesel;
            txtPassword.Text = employee.Password;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Database.DB.ExecuteQuery("update employee " +
                                     "set password='" + txtPassword.Text + "' " +
                                     "where login='" + txtLogin.Text + "';");
        }

        private void BtnFire_Click(object sender, RoutedEventArgs e)
        {
            Database.DB.DeleteEmployee(employee);
            userControl.Refresh();
            Close();
        }
    }
}
