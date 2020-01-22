using Microcredit_System.ControlSystem.DatabaseStuff;
using Microcredit_System.ControlSystem.Funds;
using Microcredit_System.ControlSystem.Funds.CurrencyStuff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

namespace Microcredit_System
{
    /// <summary>
    /// Interaction logic for UserControlExchanges.xaml
    /// </summary>
    public partial class UserControlExchanges : UserControl
    {

        public UserControlExchanges()
        {
            InitializeComponent();
        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void ComboboxCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((comboboxCurrency.SelectedItem as ComboBoxItem).Content is null)
            {
                txtRate.IsReadOnly = false;
                txtRate.Text = CurrencyRatesInformation.GetCurrencyRate("usd");
                txtRate.IsReadOnly = true;
                return;
            }
            txtRate.IsReadOnly = false;
            var rateString = CurrencyRatesInformation.GetCurrencyRate((comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString());
            // txtRate.Text = ;
            if(rateString.Length > 10)
            { // problem with internet connection
                // TODO: smth
                return;
            }

            double rate = double.Parse(rateString);

            if (txtChoose.Text.Equals("Choose a Currency To"))
            {
                rate *= 1.02;
                txtCurrencyReceive.Text = string.Format("You receive: ({0})", new string[] { (comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString() });
            }
            else
            {
                rate *= 0.98;
                txtCurrencyReceive.Text = "You receive: (PLN)";
            }
            txtRate.Text = rate.ToString();
            txtRate.IsReadOnly = true;

            TxtAmount_TextChanged(null, null);
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtRate.IsReadOnly = false;
            var rateString = CurrencyRatesInformation.GetCurrencyRate((comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString());
            if (rateString.Length > 10)
            { // problem with internet connection
                // TODO: smth
                return;
            }

            double rate = double.Parse(rateString);
            if (txtChoose.Text.Equals("Choose a Currency To"))
            {
                rate *= 0.98;
                txtChoose.Text = "Choose a Currency From";
                txtCurrencyReceive.Text = "You receive: (PLN)";
            }
            else
            {
                rate *= 1.02;
                txtChoose.Text = "Choose a Currency To";
                txtCurrencyReceive.Text = string.Format("You receive: ({0})", new string[] { (comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString() });
            }
            txtRate.Text = rate.ToString();
            txtRate.IsReadOnly = true;

            TxtAmount_TextChanged(null, null);
        }

        private void TxtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtAmount.Text.Equals(""))
            {
                return;
            }
            txtConvert.IsReadOnly = false;
            double rate = double.Parse(txtRate.Text);
            double amount = double.Parse(txtAmount.Text);
            if (txtChoose.Text.Equals("Choose a Currency To"))
            {
                // divide
                txtConvert.Text = Math.Round(amount / rate, 2) + "";
            }
            else
            {
                txtConvert.Text = Math.Round(amount * rate, 2) + "";
            }
            txtConvert.IsReadOnly = true;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text.Equals(""))
            {
                return;
            }
            if(txtCurrencyReceive.Text.Equals("You receive: (USD)"))
            {
                // decrease usd
                // increase pln
                Balance.Instance.AddDeltaToUsd(-double.Parse(txtConvert.Text));
                Balance.Instance.AddDeltaToPln(double.Parse(txtAmount.Text));
            }
            else if(txtCurrencyReceive.Text.Equals("You receive: (EUR)"))
            {
                // decrease eur
                // increase pln
                Balance.Instance.AddDeltaToEur(-double.Parse(txtConvert.Text));
                Balance.Instance.AddDeltaToPln(double.Parse(txtAmount.Text));
            }
            else
            {
                // decrease pln
                // increase selected
                Balance.Instance.AddDeltaToPln(-double.Parse(txtConvert.Text));
                if ((comboboxCurrency.SelectedItem as ComboBoxItem).Content.ToString().Equals("USD"))
                {
                    // increase usd
                    Balance.Instance.AddDeltaToUsd(double.Parse(txtAmount.Text));
                }
                else
                {
                    // increase eur
                    Balance.Instance.AddDeltaToEur(double.Parse(txtAmount.Text));
                }
            }
            MessageBox.Show("Exchange succesfull");
        }
    }
}
