using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Oracle.DataAccess.Client;

namespace MyProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow wind = new NewUserWindow();
            wind.ShowDialog();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                

                using (OracleCommand cmd = new OracleCommand("system.getUserByLogin", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter param = new OracleParameter();
                    param.ParameterName = "@p__userlogin";
                    param.OracleDbType = OracleDbType.Char;
                    param.Value = Login.Text;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_id";
                    param.OracleDbType = OracleDbType.Int64;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_passwordhash";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 100;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_lastname";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 30;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_firstname";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 30;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_patronimic";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 30;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_telephone";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 20;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o__user_bday";
                    param.OracleDbType = OracleDbType.Date;
                    param.Size = 8;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                    //MessageBox.Show((cmd.Parameters["@o__user_lastname"].Value).ToString());

                    Client cl = new Client(Int32.Parse(cmd.Parameters["@o__user_id"].Value.ToString()), Login.Text, cmd.Parameters["@o__user_lastname"].Value.ToString(), 
                        cmd.Parameters["@o__user_firstname"].Value.ToString(),DateTime.Parse(cmd.Parameters["@o__user_bday"].Value.ToString()), 
                        cmd.Parameters["@o__user_telephone"].Value.ToString(), cmd.Parameters["@o__user_patronimic"].Value.ToString());

                    //MessageBox.Show(cl.ToString());
                    if (SecurePasswordHasher.Verify(MyPassword.Password, cmd.Parameters["@o__user_passwordhash"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("YES!");

                        SearchTrainWindow wind = new SearchTrainWindow(connection);

                        wind.Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Nope");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String connectionString = "Data Source = (DESCRIPTION = " +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
                    "(CONNECT_DATA = " +
                    "  (SERVER = DEDICATED)" +
                    "  (SERVICE_NAME = orcl)" +
                    ")" +
                    ");User Id = kuser;password=qwe123qwe";
            connection = new OracleConnection();
            connection.ConnectionString = connectionString;

            connection.Open();
        }
    }
}
