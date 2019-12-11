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
        string date;
        Client cl;
        public SearchTrainWindow()
        {
            InitializeComponent();
        }

        public SearchTrainWindow(OracleConnection oConnection, Client client)
        {
            InitializeComponent();
            connection = oConnection;
            cl = client;
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
                MessageBox.Show("Sorry. There is something wrong with database :c", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                while (dr.Read())
                {
                    Train t = new Train(dr.GetInt32(1), dr.GetString(5), dr.GetInt32(6), dr.GetInt32(7));
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
                MessageBox.Show("Fill in the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wind = new MainWindow();

            wind.Show();

            Close();
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
            if (DateTime.TryParse(Date1.Text, out dt))
            {
                date = Date1.Text;
                if (Date1.Text == "" || Station1.Text == "" || Station2.Text == "")
                    MessageBox.Show("Fill in the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (dt.Date >= DateTime.Now.Date)
                    {
                        OracleCommand cmd = new OracleCommand();

                        cmd.CommandText = "select distinct * from system.Trip inner join system.train on system.Trip.train_id = system.Train.train_id inner join system.station on system.Trip.start_station_id = system.station.station_id inner join system.station on system.Trip.end_station_id = system.station.station_id WHERE  EXISTS(SELECT * " +
                         " FROM system.train_stop" +
                      " WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = " + stat1.Station_ID + " and '" + date + "'= trim(TO_CHAR(arrival_time, 'dd-mm-yyyy')) or '" + date + "'= trim(TO_CHAR(arrival_time, 'dd.mm.yyyy')) or '" + date + "'= trim(TO_CHAR(arrival_time, 'dd/mm/yyyy'))) " +
                      " and exists(SELECT *" +
                         " FROM system.train_stop" +
                      " WHERE system.trip.trip_id = system.train_stop.trip_id and system.train_stop.station_id = " + stat2.Station_ID + " and '" + date + "'= trim(TO_CHAR(arrival_time, 'dd-mm-yyyy')) or '" + date + "'= trim(TO_CHAR(arrival_time, 'dd.mm.yyyy')) or '" + date + "'= trim(TO_CHAR(arrival_time, 'dd/mm/yyyy'))) ";

                        cmd.Connection = connection;

                        cmd.CommandType = System.Data.CommandType.Text;

                        OracleDataReader dr = cmd.ExecuteReader();

                        ResSet.Items.Clear();

                        if (!dr.HasRows)
                        {
                            MessageBox.Show("Sorry. There is no results :c", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            while (dr.Read())
                            {
                                int id = dr.GetInt32(0);

                                cmd.CommandText = "select distinct t1.trip_id from system.train_stop t1 inner join system.train_stop t2 on " +
                            "t1.trip_id = t2.trip_id where t1.station_id = " + stat1.Station_ID + " and t2.station_id = " + stat2.Station_ID +
                            "  and t1.departure_time < t2.departure_time ";

                                OracleDataReader dr2 = cmd.ExecuteReader();
                                while (dr2.Read())
                                {
                                    if (dr2.GetInt32(0).Equals(id))
                                    {
                                        OracleCommand cmd3 = new OracleCommand();

                                        cmd3.CommandText = "select count(*) from system.ticket where trip_id = " + id;

                                        cmd3.Connection = connection;

                                        cmd3.CommandType = System.Data.CommandType.Text;
                                        //MessageBox.Show(cmd3.ExecuteScalar().ToString());
                                        int countSeats = int.Parse(cmd3.ExecuteScalar().ToString());

                                        Station st1 = new Station(dr.GetInt32(8), dr.GetString(9), new Address());
                                        Station st2 = new Station(dr.GetInt32(11), dr.GetString(12), new Address());
                                        Train t = new Train(dr.GetInt32(1), dr.GetString(5), dr.GetInt32(6) - countSeats, dr.GetInt32(7));
                                        Trip trip = new Trip(id, st1, st2, t);
                                        ResSet.Items.Add(trip);
                                        break;
                                    }
                                }
                            }

                            if (!ResSet.HasItems)
                            {
                                MessageBox.Show("Sorry. There is no results for your query :c", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        #region
                        //try
                        //{

                        //    using (OracleCommand cmd = new OracleCommand("system.getTrip", connection))
                        //    {
                        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        //        OracleParameter param = new OracleParameter();
                        //        param.ParameterName = "@in_st_name1";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = Station1.Text;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_st_name2";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = Station2.Text;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_trip_id";
                        //        param.OracleDbType = OracleDbType.Int32;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_station_id1";
                        //        param.OracleDbType = OracleDbType.Int32;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_station_name1";
                        //        param.OracleDbType = OracleDbType.Varchar2;
                        //        param.Size = 30;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_station_id2";
                        //        param.OracleDbType = OracleDbType.Int32;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_station_name2";
                        //        param.OracleDbType = OracleDbType.Varchar2;
                        //        param.Size = 30;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_train_id";
                        //        param.OracleDbType = OracleDbType.Int32;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_train_number";
                        //        param.OracleDbType = OracleDbType.Varchar2;
                        //        param.Size = 30;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_count_seats";
                        //        param.OracleDbType = OracleDbType.Int32;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@out_cost_per_station";
                        //        param.OracleDbType = OracleDbType.Int32;
                        //        param.Direction = System.Data.ParameterDirection.Output;
                        //        cmd.Parameters.Add(param);

                        //        OracleDataReader rdr = cmd.ExecuteReader();

                        //        // get the country name from the data reader
                        //        if (rdr.Read())
                        //        {
                        //            MessageBox.Show(rdr.GetString(2));
                        //        }

                        //        //    Client cl = new Client(Int32.Parse(cmd.Parameters["@o__user_id"].Value.ToString()), Login.Text, cmd.Parameters["@o__user_lastname"].Value.ToString(),
                        //        //        cmd.Parameters["@o__user_firstname"].Value.ToString(), DateTime.Parse(cmd.Parameters["@o__user_bday"].Value.ToString()),
                        //        //        cmd.Parameters["@o__user_telephone"].Value.ToString(), cmd.Parameters["@o__user_patronimic"].Value.ToString());
                        //        //}
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message);
                        //}
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("You entered date in past", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid date format", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (ResSet.SelectedItem != null)
            {
                if (((Trip)ResSet.SelectedItem).Train.COUNTSEATS > 0)
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand();

                        cmd.CommandText = "insert into system.ticket(date_of_trip, client_id, trip_id) values (TO_DATE('" + date + "', 'DD-MM-YYYY HH24:MI'), " + cl.Client_ID + ", " + ((Trip)ResSet.SelectedItem).Trip_ID + ")";

                        cmd.Connection = connection;

                        cmd.CommandType = System.Data.CommandType.Text;

                        int c = cmd.ExecuteNonQuery();

                        MessageBox.Show("Ticket is successfully booked", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        Search_Click(sender, null);
                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show("Sorry. There is something wrong with database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, there is no place for you", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
