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
    /// Логика взаимодействия для NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                if (CheckPassword())
                {
                    try
                    {

                        String connectionString = "Data Source = (DESCRIPTION = " +
            "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
            "(CONNECT_DATA = " +
            "  (SERVER = DEDICATED)" +
            "  (SERVICE_NAME = orcl)" +
            ")" +
          ");User Id = system;password=pomazafaP1";
                        OracleConnection oracleConnection = new OracleConnection();
                        oracleConnection.ConnectionString = connectionString;

                        oracleConnection.Open();

                        OracleCommand cmd = new OracleCommand();

                        cmd.CommandText = "insert into Client(login, password_hash, lastname, firstname, bday) values ('" + Login.Text + "', '" + SecurePasswordHasher.Hash(Password.Text) + "','" + Surname.Text + "','" + FirstName.Text + "', TO_DATE('" + DateBlock.Text + "', 'YYYY-MM-DD'))";

                        MessageBox.Show(cmd.CommandText);

                        cmd.Connection = oracleConnection;

                        cmd.CommandType = System.Data.CommandType.Text;

                        int c = cmd.ExecuteNonQuery();

                        if (c != 0)
                        {
                            MessageBox.Show("Success");
                        }

                        oracleConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match");
                }
            }
            else
            {
                MessageBox.Show("Fill in the fields! Required fields are: Surname, Name, Day of Birth, Login, Password, Repeat Password", "Warning");
            }

        }
        private bool CheckFields()
        {
            if (Surname.Text == "" || FirstName.Text == "" || DateBlock.Text == "" || Password.Text == "" || PasswordCheck.Text == "" || Login.Text == "")
                return false;
            return true;
        }

        private bool CheckPassword()
        {
            if (Password.Text != PasswordCheck.Text)
                return false;
            return true;
        }
    }
}
