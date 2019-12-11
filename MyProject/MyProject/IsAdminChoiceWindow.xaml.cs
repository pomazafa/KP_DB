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

namespace MyProject
{
    /// <summary>
    /// Логика взаимодействия для IsAdminChoiceWindow.xaml
    /// </summary>
    public partial class IsAdminChoiceWindow : Window
    {
        Employee employee;
        public IsAdminChoiceWindow(Employee emp)
        {
            InitializeComponent();
            employee = emp;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            employee.IsAdmin = 1;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            employee.IsAdmin = 0;
            Close();
        }
    }
}
