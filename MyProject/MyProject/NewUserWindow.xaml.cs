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
                        #region
                        //    using (OracleCommand cmd = new OracleCommand("system.insertClient", connection))
                        //    {
                        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        //        OracleParameter param = new OracleParameter();
                        //        param.ParameterName = "@in_user_passwordhash";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = SecurePasswordHasher.Hash(Password.Text);
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_bday";
                        //        param.OracleDbType = OracleDbType.Date;
                        //        param.Value = DateBlock.Text;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_user_telephone";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = Telephone.Text;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_user_patronimic";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = FatherName.Text;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_user_firstname";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = FirstName.Text;
                        //        cmd.Parameters.Add(param);

                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_user_lastname";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = Surname.Text;
                        //        cmd.Parameters.Add(param);


                        //        param = new OracleParameter();
                        //        param.ParameterName = "@in_userlogin";
                        //        param.OracleDbType = OracleDbType.Char;
                        //        param.Value = Login.Text;
                        //        cmd.Parameters.Add(param);

                        //        cmd.ExecuteNonQuery();

                        //    }
                        #endregion
                        OracleCommand cmd = new OracleCommand();

                        cmd.CommandText = "insert into system.Client(login, password_hash, lastname, firstname, bday) values ('" + Login.Text + "', '" + SecurePasswordHasher.Hash(Password.Text) + "','" + Surname.Text + "','" + FirstName.Text + "', TO_DATE('" + DateBlock.Text + "', 'DD-MM-YYYY'))";

                        cmd.Connection = connection;

                        cmd.CommandType = System.Data.CommandType.Text;

                        int c = cmd.ExecuteNonQuery();

                        if (c != 0)
                        {
                            if(AddAdditionalFields(connection) == 1)
                                MessageBox.Show("User is successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        Close();
                    }
                    catch(OracleException ex)
                    {
                        if(ex.Number == 1)
                        {
                            MessageBox.Show("You entered not unique login", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Fill in the fields! Required fields are: Surname, Name, Day of Birth, Login, Password, Repeat Password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private int AddAdditionalFields(OracleConnection con)
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

                    return cmd.ExecuteNonQuery();
                }
                return 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
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
