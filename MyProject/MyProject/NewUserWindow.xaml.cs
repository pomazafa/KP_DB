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
        OracleConnection connection;
        public NewUserWindow(OracleConnection connection)
        {
            this.connection = connection;
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

                        OracleCommand cmd = new OracleCommand();

                        cmd.CommandText = "insert into system.Client(login, password_hash, lastname, firstname, bday) values ('" + Login.Text + "', '" + SecurePasswordHasher.Hash(Password.Text) + "','" + Surname.Text + "','" + FirstName.Text + "', TO_DATE('" + DateBlock.Text + "', 'DD-MM-YYYY'))";

                        cmd.Connection = connection;

                        cmd.CommandType = System.Data.CommandType.Text;

                        int c = cmd.ExecuteNonQuery();



                        if (c != 0)
                        {
                            AddAdditionalFields(connection);
                            MessageBox.Show("Success");
                        }

                        Close();
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

        private bool AddAdditionalFields(OracleConnection con)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();

                string command = "update system.client set ";
                bool isO = false;

                if (Telephone.Text != "")
                {
                    command += "telephone = '" + Telephone.Text + "'";
                    isO = true;
                }

                if (FatherName.Text != "")
                {
                    if (isO)
                        command += ',';

                    command += "patronimic = '" + FatherName.Text + "'";
                    isO = true;
                }
                if (isO)
                {
                    command += " where login = '" + Login.Text + "'";

                    cmd.CommandText = command;

                    cmd.Connection = con;

                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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
