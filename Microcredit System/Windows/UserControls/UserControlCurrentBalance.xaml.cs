using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Funds;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Microcredit_System.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlCurrentBalance.xaml
    /// </summary>
    public partial class UserControlCurrentBalance : UserControl, IRefreshable
    {
        private Employee employee;

        internal UserControlCurrentBalance(Employee employee)
        {
            InitializeComponent();

            this.employee = employee;
        }

        private static readonly Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        private static bool IsTextAllowed(string text)
        {
            return !regex.IsMatch(text);
        }

        public void Refresh()
        {
            var balance = Database.DB.GetBalance();

            txtEur.Text = Math.Round(balance.Eur, 2).ToString();
            txtUsd.Text = Math.Round(balance.Usd, 2).ToString();
            txtPln.Text = Math.Round(balance.Pln, 2).ToString();

            if(!(employee is Admin))
            {
                var name = gridForPutting.GetValue(NameProperty).ToString();
                gridParent.Children.Remove(gridForPutting);
                NameScope.GetNameScope(this).UnregisterName(name);
            }
        }

        private void BtnPutIn_Click(object sender, RoutedEventArgs e)
        {
            switch ((comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString())
            {
                case "USD":
                    Balance.Instance.AddDeltaToUsd(double.Parse(txtAmount.Text));
                    break;
                case "EUR":
                    Balance.Instance.AddDeltaToEur(double.Parse(txtAmount.Text));
                    break;
                case "PLN":
                    Balance.Instance.AddDeltaToPln(double.Parse(txtAmount.Text));
                    break;
            }
            Refresh();
        }

        private void BtnPutOut_Click(object sender, RoutedEventArgs e)
        {
            switch ((comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString())
            {
                case "USD":
                    Balance.Instance.AddDeltaToUsd(-double.Parse(txtAmount.Text));
                    break;
                case "EUR":
                    Balance.Instance.AddDeltaToEur(-double.Parse(txtAmount.Text));
                    break;
                case "PLN":
                    Balance.Instance.AddDeltaToPln(-double.Parse(txtAmount.Text));
                    break;
            }
            Refresh();
        }

        private void TxtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
