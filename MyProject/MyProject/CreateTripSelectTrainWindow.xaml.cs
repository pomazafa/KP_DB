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
    /// Логика взаимодействия для CreateTripSelectTrainWindow.xaml
    /// </summary>
    public partial class CreateTripSelectTrainWindow : Window
    {
        OracleConnection connection;
        List<TrainStop> stops;
        Employee employee;
        public CreateTripSelectTrainWindow(OracleConnection con, List<TrainStop> stops, Employee emp)
        {
            InitializeComponent();
            connection = con;
            this.stops = stops;
            employee = emp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "select * from system.train";

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
                    Train train = new Train(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetInt32(3));
                    ResSet.Items.Add(train);
                }
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (ResSet.SelectedItem != null)
            {
                if (Controller.InsertAll(((Train)ResSet.SelectedItem), stops, connection))
                {
                    AdminWindow wind = new AdminWindow(connection, employee);
                    wind.Show();
                    Close();
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            CreateTripTrainChoiceWindow wind = new CreateTripTrainChoiceWindow(connection, stops, employee);
            wind.Show();
            Close();
        }
    }
}
