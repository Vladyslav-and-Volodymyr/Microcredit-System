using Microcredit_System.ControlSystem.Persons.ClientStuff;
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
    /// Interaction logic for UserControlAddClient.xaml
    /// </summary>
    public partial class UserControlAddClient : UserControl
    {
        private ControlSystem.ControlSystem _control;

        public UserControlAddClient(ControlSystem.ControlSystem control)
        {
            InitializeComponent();
            _control = control;
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            _control.AddClientToBase(txtName.Text, txtSurname.Text, txtPassport.Text);
            MessageBox.Show("Client added!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            txtName.Text = String.Empty;
            txtSurname.Text = String.Empty;
            txtPassport.Text = String.Empty;
        }
    }
}
