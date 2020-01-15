using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
using Microcredit_System.Windows;
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

namespace Microcredit_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ControlSystem.ControlSystem control = new ControlSystem.ControlSystem();

        public MainWindow()
        {
            InitializeComponent();
            Database.DB.Init();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = control.LogIn(txtLogin.Text, txtPassword.Password);
            if(employee == null)
            {
                MessageBox.Show("Wrong password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                new ControlWindow(control, employee).Show();
                this.Close();
            }
        }
    }
}
