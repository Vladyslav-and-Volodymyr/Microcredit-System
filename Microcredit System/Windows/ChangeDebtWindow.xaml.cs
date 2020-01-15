using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Persons.ClientStuff;
using Microcredit_System.Windows.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ChangeDebtWindow.xaml
    /// </summary>
    public partial class ChangeDebtWindow : Window
    {
        private Client _client;
        private UserControl _userControl;

        internal ChangeDebtWindow(Client client, UserControl userControl)
        {
            InitializeComponent();

            Title = client.Name + " " + client.Surname;

            txtDebt.Text = client.Debt.ToString();
            txtName.Text = client.Name;
            txtPassport.Text = client.Passport;
            txtSurname.Text = client.Surname;

            this._client = client;
            this._userControl = userControl;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void ButtonIncrease_Click(object sender, RoutedEventArgs e)
        {
            Database.DB.ChangeDebt(_client, float.Parse(txtDelta.Text));
            txtDebt.Text = _client.Debt.ToString();

            RefreshParentControl();

            txtDelta.Text = "";
        }

        private void ButtonDecrease_Click(object sender, RoutedEventArgs e)
        {
            Database.DB.ChangeDebt(_client, -1 * float.Parse(txtDelta.Text));
            txtDebt.Text = _client.Debt.ToString();
            RefreshParentControl();
            txtDelta.Text = "";
        }

        private void RefreshParentControl()
        {
            if (_userControl is UserControlDebtors)
            {
                ((UserControlDebtors)_userControl).Refresh();
            }
            else
            {
                ((UserControlClientList)_userControl).Refresh();
            }
        }
    }
}
