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

                String connectionString = "Data Source = (DESCRIPTION = " +
                    "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
                    "(CONNECT_DATA = " +
                    "  (SERVER = DEDICATED)" +
                    "  (SERVICE_NAME = orcl)" +
                    ")" +
                    ");User Id = kuser;password=qwe123qwe";
                OracleConnection oracleConnection = new OracleConnection();
                oracleConnection.ConnectionString = connectionString;

                oracleConnection.Open();

                using (OracleCommand cmd = new OracleCommand("system.getUserByLogin", oracleConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    // Входной параметр.
                    OracleParameter param = new OracleParameter();
                    param.ParameterName = "@p__userlogin";
                    param.OracleDbType = OracleDbType.Char;
                    param.Value = Login.Text;

                    //По умолчанию параметры считаются входными, но все же для ясности:
                    //param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_id";
                    param.OracleDbType = OracleDbType.Int64;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_passwordhash";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 100;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_lastname";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 30;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_firstname";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 30;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_patronimic";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 30;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_telephone";
                    param.OracleDbType = OracleDbType.Char;
                    param.Size = 20;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выходной параметр.
                    param = new OracleParameter();
                    param.ParameterName = "@o__user_bday";
                    param.OracleDbType = OracleDbType.Date;
                    param.Size = 8;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);

                    // Выполнение хранимой процедуры.
                    cmd.ExecuteNonQuery();
                    // Возврат выходного параметра.
                    //MessageBox.Show((cmd.Parameters["@o__user_lastname"].Value).ToString());

                    Client cl = new Client(Int32.Parse(cmd.Parameters["@o__user_id"].Value.ToString()), Login.Text, cmd.Parameters["@o__user_lastname"].Value.ToString(), 
                        cmd.Parameters["@o__user_firstname"].Value.ToString(),DateTime.Parse(cmd.Parameters["@o__user_bday"].Value.ToString()), 
                        cmd.Parameters["@o__user_telephone"].Value.ToString(), cmd.Parameters["@o__user_patronimic"].Value.ToString());

                    MessageBox.Show(cl.ToString());
                    if (SecurePasswordHasher.Verify(MyPassword.Password, cmd.Parameters["@o__user_passwordhash"].Value.ToString().Trim()))
                    {
                        MessageBox.Show("YES!");

                        SearchTrainWindow wind = new SearchTrainWindow();

                        wind.Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Nope");
                }

                //OracleCommand cmd = new OracleCommand();

                //cmd.CommandText = "select password_hash, client_id from system.Client where login = '" + Login.Text + "'";

                //cmd.Connection = oracleConnection;

                //cmd.CommandType = System.Data.CommandType.Text;

                //OracleDataReader dr = cmd.ExecuteReader();

                //dr.Read();

                //if (!dr.HasRows)
                //{
                //    MessageBox.Show("Database does not contain users with this login");
                //}
                //else
                //{

                //    String res = dr.GetString(0);

                //    int resId = dr.GetInt32(1);

                //    if (SecurePasswordHasher.Verify(MyPassword.Password, res))
                //    {
                //        MessageBox.Show("YES!");

                //        SearchTrainWindow wind = new SearchTrainWindow();

                //        wind.Show();
                //        Close();
                //    }
                //    else
                //        MessageBox.Show("Nope");
                //}

                ////string writePath = @"D:\1.txt";

                ////using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                ////{
                ////    sw.WriteLine(SecurePasswordHasher.Hash("qweqweqwe"));
                ////}

                oracleConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
