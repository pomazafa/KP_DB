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
    /// Логика взаимодействия для SearchTrainWindow.xaml
    /// </summary>
    public partial class SearchTrainWindow : Window
    {
        OracleConnection connection;
        public Station stat1;
        public Station stat2;
        DateTime dt = new DateTime();
        public SearchTrainWindow()
        {
            InitializeComponent();
        }

        public SearchTrainWindow(OracleConnection oConnection)
        {

            InitializeComponent();
            connection = oConnection;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "select * from system.Trip inner join system.train on system.Trip.train_id = system.Train.train_id inner join system.station on system.Trip.start_station_id = system.station.station_id inner join system.station on system.Trip.end_station_id = system.station.station_id";

            cmd.Connection = connection;

            cmd.CommandType = System.Data.CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();


            if (!dr.HasRows)
            {
                MessageBox.Show("Sorry. There is something wrong with database :c");
            }
            else
            {
                while (dr.Read())
                {
                    Train t = new Train(dr.GetInt32(1), dr.GetString(5), dr.GetInt32(6), dr.GetFloat(7));
                    Station st1 = new Station(dr.GetInt32(8), dr.GetString(9), new Address());
                    Station st2 = new Station(dr.GetInt32(11), dr.GetString(12), new Address());

                    Trip trip = new Trip(dr.GetInt32(0), st1, st2, t);
                    ResSet.Items.Add(trip);
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            if (Date1.Text == "" || Station1.Text == "" || Station2.Text == "")
                MessageBox.Show("Fill in the fields");
            else
            {
                TrainsStationsWindow wind = new TrainsStationsWindow(this, connection, dt);
                wind.Show();
            }
        }

        private void ResSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResSet.SelectedItem != null)
            {
                Info.IsEnabled = true;
                Choose.IsEnabled = true;
            }
            else
            {
                Info.IsEnabled = false;
                Choose.IsEnabled = false;
            }
        }

        private void View_Stations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Choose_Station_Click(object sender, RoutedEventArgs e)
        {
            ChooseStationWindow wind;
            if (((Button)sender).Name == "Choose_Station1")
            {
                wind = new ChooseStationWindow(Station1, this,  connection);
            }
            else
            {
                wind = new ChooseStationWindow(Station2, this,  connection);
            }
            wind.ShowDialog();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(DateTime.TryParse(Date1.Text, out dt))
            {
                if (Date1.Text == "" || Station1.Text == "" || Station2.Text == "")
                    MessageBox.Show("Fill in the fields");
                else
                {
                    OracleCommand cmd = new OracleCommand();

                    cmd.CommandText = "select * from system.Trip inner join system.train on system.Trip.train_id = system.Train.train_id inner join system.station on system.Trip.start_station_id = system.station.station_id inner join system.station on system.Trip.end_station_id = system.station.station_id WHERE EXISTS(SELECT * " +
                     " FROM system.train_stop" +
                  " WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = " + stat1.Station_ID + ")" +
                  " and exists(SELECT *" +
                     " FROM system.train_stop" +
                  " WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = " + stat2.Station_ID + ")";

                    cmd.Connection = connection;

                    cmd.CommandType = System.Data.CommandType.Text;

                    OracleDataReader dr = cmd.ExecuteReader();

                    ResSet.Items.Clear();

                    if (!dr.HasRows)
                    {
                        MessageBox.Show("Sorry. There is no results :c");
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            Train t = new Train(dr.GetInt32(1), dr.GetString(5), dr.GetInt32(6), dr.GetFloat(7));
                            Station st1 = new Station(dr.GetInt32(8), dr.GetString(9), new Address());
                            Station st2 = new Station(dr.GetInt32(11), dr.GetString(12), new Address());

                            Trip trip = new Trip(dr.GetInt32(0), st1, st2, t);
                            ResSet.Items.Add(trip);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid date format");
            }
        }

    }
}
