using System;
using System.Windows;
using Oracle.DataAccess.Client;
using System.Data;

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
            NewUserWindow wind = new NewUserWindow(connection);
            wind.ShowDialog();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" && MyPassword.Password != "")
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

                        Client cl = new Client(Int32.Parse(cmd.Parameters["@o__user_id"].Value.ToString()), Login.Text, cmd.Parameters["@o__user_lastname"].Value.ToString(),
                            cmd.Parameters["@o__user_firstname"].Value.ToString(), DateTime.Parse(cmd.Parameters["@o__user_bday"].Value.ToString()),
                            cmd.Parameters["@o__user_telephone"].Value.ToString(), cmd.Parameters["@o__user_patronimic"].Value.ToString());

                        if (SecurePasswordHasher.Verify(MyPassword.Password, cmd.Parameters["@o__user_passwordhash"].Value.ToString().Trim()))
                        {
                            //MessageBox.Show("YES!");

                            using (OracleCommand cmd2 = new OracleCommand("system.getEmployeeByUserId", connection))
                            {
                                cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                                OracleParameter param2 = new OracleParameter();
                                param2.ParameterName = "@in_user_id";
                                param2.OracleDbType = OracleDbType.Int32;
                                param2.Value = cl.Client_ID;
                                cmd2.Parameters.Add(param2);

                                param2 = new OracleParameter();
                                param2.ParameterName = "@o_emp_id";
                                param2.OracleDbType = OracleDbType.Int32;
                                param2.Direction = System.Data.ParameterDirection.Output;
                                cmd2.Parameters.Add(param2);

                                param2 = new OracleParameter();
                                param2.ParameterName = "@o_card_id";
                                param2.OracleDbType = OracleDbType.Int32;
                                param2.Direction = System.Data.ParameterDirection.Output;
                                cmd2.Parameters.Add(param2);

                                param2 = new OracleParameter();
                                param2.ParameterName = "@o_occupation_id";
                                param2.OracleDbType = OracleDbType.Int32;
                                param2.Direction = System.Data.ParameterDirection.Output;
                                cmd2.Parameters.Add(param2);

                                param2 = new OracleParameter();
                                param2.ParameterName = "@o_is_admin";
                                param2.OracleDbType = OracleDbType.Int32;
                                param2.Direction = System.Data.ParameterDirection.Output;
                                cmd2.Parameters.Add(param2);

                                try
                                {
                                    cmd2.ExecuteNonQuery();

                                    Employee emp = new Employee(Int32.Parse(cmd2.Parameters["@o_emp_id"].Value.ToString()), cl.Client_ID, Int32.Parse(cmd2.Parameters["@o_card_id"].Value.ToString()), Int32.Parse(cmd2.Parameters["@o_occupation_id"].Value.ToString()), Int32.Parse(cmd2.Parameters["@o_is_admin"].Value.ToString()));

                                    String connectionAdminString = "Data Source = (DESCRIPTION = " +
                                    "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
                                    "(CONNECT_DATA = " +
                                    "  (SERVER = DEDICATED)" +
                                    "  (SERVICE_NAME = orcl))" +
                                    ");User Id = DPVCORE;password=pomazafaP1";

                                    connection = new OracleConnection();
                                    connection.ConnectionString = connectionAdminString;

                                    connection.Open();

                                    if (emp.IsAdmin == 1)
                                    {
                                        AdminWindow wind = new AdminWindow(connection, emp);

                                        wind.Show();
                                    }
                                    else
                                    {
                                        EmployeeWindow wind = new EmployeeWindow(connection, emp);

                                        wind.Show();
                                    }

                                    Close();
                                        
                                }
                                catch (OracleException ex)
                                {
                                    SearchTrainWindow wind = new SearchTrainWindow(connection, cl);

                                    wind.Show();
                                    Close();
                                }
                            }
                        }
                        else
                            MessageBox.Show("You entered incorrect password", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (OracleException ex)
                {
                    MessageBox.Show("There is no such user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Fill in the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    ");User Id = c##kuser;password=pomazafaP_1";
            connection = new OracleConnection();
            connection.ConnectionString = connectionString;

            connection.Open();
        }
    }
}
