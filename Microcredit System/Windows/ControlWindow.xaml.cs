using MaterialDesignThemes.Wpf;
using Microcredit_System.ControlSystem.Persons.EmployeeStuff;
using Microcredit_System.ViewModel;
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
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        private ControlSystem.ControlSystem control;
        private Employee employee;

        internal ControlWindow(ControlSystem.ControlSystem control, Employee employee)
        {
            InitializeComponent();

            this.employee = employee;

            var _menuClient = new List<SubItem>();
            _menuClient.Add(new SubItem("Client List", new UserControlClientList()));
            _menuClient.Add(new SubItem("Add new Client", new UserControlAddClient(control)));
            _menuClient.Add(new SubItem("Debtors", new UserControlDebtors()));

            var _itemClient = new ItemMenu("Clients", _menuClient, PackIconKind.Person);

            

            var _menuFinances = new List<SubItem>
            {
                new SubItem("Current balance", new UserControlCurrentBalance()),
                new SubItem("Exchange", new UserControlExchanges()),
               
            };

            var _itemFinances = new ItemMenu("Finances", _menuFinances, PackIconKind.ScaleBalance);

            Menu.Children.Add(new UserControlMenuItem(_itemClient, this));
            Menu.Children.Add(new UserControlMenuItem(_itemFinances, this));

            

            if (employee is Admin)
            {
                var _menuEmployee = new List<SubItem>
                {
                    new SubItem("Add Employee", new UserContolAddEmployee()),
                    new SubItem("Change Employee", new UserControlEmployeeList())
                };
                var _itemEmployee = new ItemMenu("Employee", _menuEmployee, PackIconKind.Person);
                Menu.Children.Add(new UserControlMenuItem(_itemEmployee, this));
            }
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if(sender is IRefreshable)
            {
                ((IRefreshable)sender).Refresh();
            }

            /*if(sender is UserControlClientList)
            {
                ((UserControlClientList)sender).Refresh();
            }
            else if(sender is UserControlDebtors)
            {
                ((UserControlDebtors)sender).Refresh();
            }
            else if(sender is UserControlEmployeeList)
            {
                ((UserControlEmployeeList)sender).Refresh();
            }
            */
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);

            }
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();
        }
    }
}