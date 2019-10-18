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
    "(CONNECT_DATA = "+
    "  (SERVER = DEDICATED)" +
    "  (SERVICE_NAME = orcl)" +
    ")" +
  ");User Id = system;password=pomazafaP1";
                OracleConnection oracleConnection = new OracleConnection();
                oracleConnection.ConnectionString = connectionString;

                oracleConnection.Open();

                OracleCommand cmd = new OracleCommand();

                cmd.CommandText = "select password_hash, client_id from Client where login = '" + Login.Text + "'";

                cmd.Connection = oracleConnection;

                cmd.CommandType = System.Data.CommandType.Text;

                OracleDataReader dr = cmd.ExecuteReader();

                dr.Read();

                if (!dr.HasRows)
                {
                    MessageBox.Show("Database does not contain users with this login");
                }
                else
                {

                    String res = dr.GetString(0);

                    int resId = dr.GetInt32(1);

                    if (SecurePasswordHasher.Verify(MyPassword.Password, res))
                    {
                        MessageBox.Show("Success!!!");
                    }
                    else
                        MessageBox.Show("Nope");
                }

                //string writePath = @"D:\1.txt";

                //using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                //{
                //    sw.WriteLine(SecurePasswordHasher.Hash("qweqweqwe"));
                //}

                oracleConnection.Close();

                //// Hash
                //var hash = SecurePasswordHasher.Hash("mypassword");

                //// Verify
                //var result = SecurePasswordHasher.Verify("mypassword", hash);

                // MainWindow wind = new MainWindow();
                // wind.Show();
                //int password = MyPassword.Password.GetHashCode();
                //string login = Login.Text;
                //if (login != "" && MyPassword.Password != "")
                //{
                //    var user = users.FirstOrDefault(x => x.PASSWORD_HASH == password && x.LOGIN == login);
                //    if (user != null)
                //    {
                //        if (user.LOGIN == "admin")
                //        {
                //            AdminWindow wind = new AdminWindow();
                //            wind.Show();
                //            Close();
                //        }
                //        else
                //        {
                //            FirstWindowTherapist wind = new FirstWindowTherapist(user);
                //            wind.Show();
                //            Close();
                //        }
                //    }
                //    else
                //        MessageBox.Show("Неправильный логин или пароль");
                //}
                //else
                //{
                //    MessageBox.Show("Заполните поля!");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
