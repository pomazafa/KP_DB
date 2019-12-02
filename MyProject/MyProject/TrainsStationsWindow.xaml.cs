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
    /// Логика взаимодействия для TrainsStationsWindow.xaml
    /// </summary>
    public partial class TrainsStationsWindow : Window
    {
        OracleConnection connection;
        SearchTrainWindow window;
        DateTime datetime;
        public TrainsStationsWindow(SearchTrainWindow wind, OracleConnection connection, DateTime dt)
        {
            InitializeComponent();

            this.connection = connection;
            this.window = wind;
            this.datetime = dt;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "select * from system.train_stop inner join system.station on system.station.station_id = system.train_stop.station_id where trim(TO_CHAR(arrival_time, 'dd.mm.yyyy')) = '" + datetime.Date.ToString().Substring(0, 10) + "' and trip_id = " + ((Trip)window.ResSet.SelectedItem).Trip_ID + " order by departure_time";

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
                    Station st = new Station(dr.GetInt32(3), dr.GetString(6), new Address());
                    TrainStop stop = new TrainStop(dr.GetInt32(0), dr.GetDateTime(1), dr.GetDateTime(2), st);
                    ResSet.Items.Add(stop);
                }
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
