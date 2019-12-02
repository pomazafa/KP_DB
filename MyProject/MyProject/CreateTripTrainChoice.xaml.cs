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
    /// Логика взаимодействия для CreateTripTrainChoice.xaml
    /// </summary>
    public partial class CreateTripTrainChoiceWindow : Window
    {
        OracleConnection connection;
        List<TrainStop> stops;
        public CreateTripTrainChoiceWindow(OracleConnection con, List<TrainStop> st)
        {
            InitializeComponent();
            connection = con;
            stops = st;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CreateTripWindow wind = new CreateTripWindow(connection, stops);
            wind.Show();
            Close();
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            CreateTripTrainInfoWindow wind = new CreateTripTrainInfoWindow(connection, stops);
            wind.Show();
            Close();
        }

        private void SelectExisting_Click(object sender, RoutedEventArgs e)
        {
            CreateTripSelectTrainWindow wind = new CreateTripSelectTrainWindow(connection, stops);
            wind.Show();
            Close();
        }
    }
}
