using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CreateEmployeeWindow.xaml
    /// </summary>
    public partial class CreateEmployeeWindow : Window
    {
        OracleConnection connection;
        Employee employee;
        Occupation occupation;
        Client client;
        public CreateEmployeeWindow(OracleConnection conn, Employee emp, Occupation occ, Client cl)
        {
            InitializeComponent();
            connection = conn;
            employee = emp;
            occupation = occ;
            client = cl;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if(CheckFields())
            {
                Card card = new Card(Number.Text, EspDate.Text, Owner.Text);
                try
                {
                    OracleCommand cmdIns = new OracleCommand();

                    cmdIns.CommandText = $"insert into system.Card(card_number, espiration_date, card_owner) values('{card.Number}', '{card.Espiration_date}', '{card.Owner}')";

                    cmdIns.Connection = connection;

                    cmdIns.CommandType = System.Data.CommandType.Text;

                    int c = cmdIns.ExecuteNonQuery();

                    if (c == 0)
                    {
                        MessageBox.Show("There something wrong with database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        using (OracleCommand cmd = new OracleCommand("system.getCardId", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            OracleParameter param = new OracleParameter();
                            param.ParameterName = "@p_card_number";
                            param.OracleDbType = OracleDbType.Varchar2;
                            param.Value = card.Number;
                            cmd.Parameters.Add(param);

                            param = new OracleParameter();
                            param.ParameterName = "@o_card_id";
                            param.OracleDbType = OracleDbType.Int32;
                            param.Direction = System.Data.ParameterDirection.Output;
                            cmd.Parameters.Add(param);

                            cmd.ExecuteNonQuery();
                            //MessageBox.Show(cmd.Parameters["@o_trip_id"].Value.ToString());
                            card.Card_ID = int.Parse(cmd.Parameters["@o_card_id"].Value.ToString());
                            employee.Card_ID = card.Card_ID;

                            IsAdminChoiceWindow wind = new IsAdminChoiceWindow(employee);

                            wind.ShowDialog();

                            OracleCommand cmdI = new OracleCommand();

                            cmdI.CommandText = $"insert into system.Employee(card_id, occupation_id, client_id, is_admin) values({employee.Card_ID}, {employee.Occupation_ID}, {client.Client_ID}, {employee.IsAdmin})";

                            cmdI.Connection = connection;

                            cmdI.CommandType = System.Data.CommandType.Text;

                            c = cmdI.ExecuteNonQuery();

                            if (c == 0)
                            {
                                MessageBox.Show("There something wrong with database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                MessageBox.Show("Employee successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information); ;
                                AdminWindow window = new AdminWindow(connection, employee);
                                window.Show();
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OccupationWindow wind = new OccupationWindow(connection, employee, client);
            wind.Show();
            Close();
        }

        private bool CheckFields()
        {
            Regex regex = new Regex(@"^[0-9]{16}$");
            MatchCollection matches = regex.Matches(Number.Text);
            if (matches.Count == 0)
            {
                MessageBox.Show("Card Number should contain 16 numbers", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            regex = new Regex(@"^0[1-9]|1[0-2]/[0-9]{2}$");
            matches = regex.Matches(EspDate.Text);
            if (matches.Count == 0)
            {
                MessageBox.Show("Espiration Date format should be mm/yy", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
