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
    /// Логика взаимодействия для ChooseStationWindow.xaml
    /// </summary>
    public partial class ChooseStationWindow : Window
    {
        object Field;
        OracleConnection connection;
        SearchTrainWindow window;
        CreateTripWindow windowTrip;
        public ChooseStationWindow(object field, SearchTrainWindow wind, OracleConnection connection)
        {
            InitializeComponent();
            Field = field;

            this.connection = connection;
            this.window = wind;
        }

        public ChooseStationWindow(CreateTripWindow wind, OracleConnection connection)
        {
            InitializeComponent();

            this.connection = connection;
            this.windowTrip = wind;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "select * from system.station inner join system.address on system.station.address_id = system.address.address_id ";

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
                    Station st = new Station(dr.GetInt32(0), dr.GetString(1), new Address(dr.GetInt32(2), dr.GetString(4), dr.GetString(5), dr.GetString(6)));
                    ResSet.Items.Add(st);
                }
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if(ResSet.SelectedItem != null)
            {
                if (window != null)
                {
                    ((TextBox)Field).Text = ((Station)ResSet.SelectedItem).Name;
                    if (((TextBox)Field).Name == "Station1")
                        window.stat1 = (Station)ResSet.SelectedItem;
                    if (((TextBox)Field).Name == "Station2")
                        window.stat2 = (Station)ResSet.SelectedItem;
                }
                if (windowTrip != null)
                {
                    windowTrip.stat1 = (Station)ResSet.SelectedItem;
                   // MessageBox.Show(windowTrip.Station1.Text);
                }
                Close();
            }
            else
            {
                MessageBox.Show("You didn't select a station", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
