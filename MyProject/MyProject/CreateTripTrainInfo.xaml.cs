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
    /// Логика взаимодействия для CreateTripTrainInfo.xaml
    /// </summary>
    public partial class CreateTripTrainInfoWindow : Window
    {
        List<TrainStop> stops;
        OracleConnection connection;
        public CreateTripTrainInfoWindow(OracleConnection con, List<TrainStop> stops)
        {
            InitializeComponent();
            connection = con;
            this.stops = stops;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            CreateTripTrainChoiceWindow wind = new CreateTripTrainChoiceWindow(connection, stops);
            wind.Show();
            Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                Train train = new Train(Number.Text, int.Parse(Count.Text), int.Parse(Cost.Text));
                try
                {
                    OracleCommand cmdIns = new OracleCommand();

                    cmdIns.CommandText = "insert into system.train (train_number, count_seats, cost_per_station) values ('" + train.TRAINNUMBER + "', " + train.COUNTSEATS + ", " + train.COSTPERSTATION + ")";

                    cmdIns.Connection = connection;

                    cmdIns.CommandType = System.Data.CommandType.Text;

                    int c = cmdIns.ExecuteNonQuery();

                    if (c == 0)
                    {
                        MessageBox.Show("There something wrong with database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        //MessageBox.Show("Train successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (GetTrainId(train))
                        {
                            if(Controller.InsertAll(train, stops, connection))
                            {
                                AdminWindow wind = new AdminWindow(connection);
                                wind.Show();
                                Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CheckFields()
        {
            if (Cost.Text != "" && Count.Text != "" && Number.Text != "")
            {
                int cost;
                if (int.TryParse(Cost.Text, out cost))
                {
                    if (int.TryParse(Count.Text, out cost))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Count field should contain only numbers", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Cost field should contain only numbers", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Fill in the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

        private bool GetTrainId(Train train)
        {
            try
            {

                using (OracleCommand cmd = new OracleCommand("system.getTrainIdByNumber", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter param = new OracleParameter();
                    param.ParameterName = "@p_train_number";
                    param.OracleDbType = OracleDbType.Varchar2;
                    param.Value = train.TRAINNUMBER;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o_train_id";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();

                    train.TRAIN_ID = int.Parse(cmd.Parameters["@o_train_id"].Value.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    } 
}
