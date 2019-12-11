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
    /// Логика взаимодействия для SelectOccupationWindow.xaml
    /// </summary>
    public partial class SelectOccupationWindow : Window
    {
        OracleConnection connection;
        Employee employee;
        Client client;
        public SelectOccupationWindow(OracleConnection con, Employee emp, Client cl)
        {
            connection = con;
            InitializeComponent();
            client = cl;
            employee = emp;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if(ResSet.SelectedItem != null)
            {
                CreateEmployeeWindow wind = new CreateEmployeeWindow(connection, employee, (Occupation)ResSet.SelectedItem, client);
                wind.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Select Occupation from the list", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OccupationWindow wind = new OccupationWindow(connection, employee, client);
            wind.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                cmd.CommandText = "select * from system.occupation";

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
                        Occupation occupation = new Occupation(dr.GetInt32(0), dr.GetString(1), dr.GetDecimal(2), dr.GetInt32(3));
                        ResSet.Items.Add(occupation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
