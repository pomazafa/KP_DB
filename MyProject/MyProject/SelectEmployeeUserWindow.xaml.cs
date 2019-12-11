using Oracle.DataAccess.Client;
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
    /// Логика взаимодействия для SelectEmployeeUserWindow.xaml
    /// </summary>
    public partial class SelectEmployeeUserWindow : Window
    {
        OracleConnection connection;
        Employee employee;
        public SelectEmployeeUserWindow(OracleConnection conn, Employee emp)
        {
            InitializeComponent();
            connection = conn;
            employee = emp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "select * from system.client c where not exists (select * from system.employee e where c.client_id = e.client_id)";

            cmd.Connection = connection;

            cmd.CommandType = System.Data.CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                MessageBox.Show("Sorry. There is something wrong with database :c", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ResSet.Items.Clear();
                while (dr.Read())
                {
                    Client cl = new Client(dr.GetInt32(0), dr.GetString(1), dr.GetString(3), dr.GetString(4), dr.GetDateTime(7), " ", " ");

                    ResSet.Items.Add(cl);
                }
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (ResSet.SelectedItem != null)
            {
                OccupationWindow wind = new OccupationWindow(connection, employee, (Client)ResSet.SelectedItem);
                wind.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please, select user", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow wind = new AdminWindow(connection, employee);
            wind.Show();
            Close();
        }
    }
}
