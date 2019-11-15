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

            cmd.CommandText = "select * from system.Train";

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
                    Train t = new Train(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetFloat(3));
                    ResSet.Items.Add(t);
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Info.IsEnabled = true;
        }
    }
}
