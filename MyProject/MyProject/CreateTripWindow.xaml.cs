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
    /// Логика взаимодействия для CreateTripWindow.xaml
    /// </summary>
    public partial class CreateTripWindow : Window
    {
        OracleConnection connection;
        public Station stat1;
        DateTime dt1;
        DateTime dt2;
        List<TrainStop> stops;
        public CreateTripWindow(OracleConnection con)
        {
            InitializeComponent();
            connection = con;
            stops = new List<TrainStop>();
            ResSet.ItemsSource = stops;
            ResSet.AutoGenerateColumns = false;
        }
        public CreateTripWindow(OracleConnection con, List<TrainStop> stops)
        {
            InitializeComponent();
            connection = con;
            this.stops = stops;
            ResSet.ItemsSource = this.stops;
            ResSet.AutoGenerateColumns = false;
            isSizeCorrect();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(Date1.Text, out dt1) && Date1.Text.Length == 16)
            {
                if (DateTime.TryParse(Date2.Text, out dt2) && Date2.Text.Length == 16)
                {
                    if (dt2 > dt1)
                    {
                        if (stat1 != null)
                        {
                            Station station = (Station)stat1.Clone();
                            //ResSet.Items.Add(new TrainStop(dt1, dt2, stat1));
                            stops.Add(new TrainStop(dt1, dt2, stat1));
                            ResSet.Items.Refresh();
                            ClearFields();
                            isSizeCorrect();
                        }
                        else
                        {
                            MessageBox.Show("You did not entered station", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Arrival date should be earlier than departure date", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Departure datetime is incorrect\nValid format is dd.mm.yyyy hh:mi", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Arrival datetime is incorrect", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Choose_Station1_Click(object sender, RoutedEventArgs e)
        {
            ChooseStationWindow wind;
            wind = new ChooseStationWindow(this, connection);
            wind.ShowDialog();
            if(stat1 != null)
                Station1.Text = stat1.Name;
        }

        private void ClearFields()
        {
            Date1.Text = "";
            Date2.Text = "";
            stat1 = null;
            Station1.Text = "";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow wind = new AdminWindow(connection);
            wind.Show();
            Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            stops.Sort(delegate (TrainStop c1, TrainStop c2) { return c1.Arrival_datetime.CompareTo(c2.Arrival_datetime); });
            CreateTripTrainChoiceWindow wind = new CreateTripTrainChoiceWindow(connection, stops);
            wind.Show();
            Close();
        }

        private void isSizeCorrect()
        {
            if (ResSet.Items.Count > 1)
                Next.IsEnabled = true;
            else
                Next.IsEnabled = false;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(ResSet.SelectedItem != null)
            {
                stops.Remove((TrainStop)ResSet.SelectedItem);
                ResSet.Items.Refresh();
                isSizeCorrect();
            }
        }

        private void ResSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResSet.SelectedItem != null)
                Delete.IsEnabled = true;
            else
                Delete.IsEnabled = false;
        }
    }
}
