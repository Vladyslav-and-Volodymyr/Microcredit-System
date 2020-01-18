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
    /// Interaction logic for UserContolAddEmployee.xaml
    /// </summary>
    public partial class UserContolAddEmployee : UserControl
    {
        public UserContolAddEmployee()
        {
            InitializeComponent();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if(AllDataIsValid())
            {
                Database.DB.AddEmployeeToBase(new Employee(txtName.Text, txtSurname.Text,
                                            txtPesel.Text, txtLogin.Text, txtPassword.Password),
                                            (bool)chIsAdmin.IsChecked);
                Button_Click_Clear(null, null);
                MessageBox.Show("Employee added!");
            }
        }

        private bool AllDataIsValid()
        {
            if (txtName.Text.Length <= 2)
            {
                MessageFieldLength("Name", 2);
                return false;
            }
            if (txtSurname.Text.Length <= 2)
            {
                MessageFieldLength("Name", 2);
                return false;
            }
            if (txtPesel.Text.Length != 11)
            {
                MessageBox.Show("Invalid PESEL length");
                return false;
            }
            return true;
        }

        private void MessageFieldLength(string nameOfField, int length)
        {
            MessageBox.Show(nameOfField + " field should be longer " + length + " characters");
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = "";
            txtName.Text = "";
            txtPassword.Password = "";
            txtPesel.Text = "";
            txtSurname.Text = "";
        }
    }
}
