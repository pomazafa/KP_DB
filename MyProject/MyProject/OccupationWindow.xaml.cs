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
    /// Логика взаимодействия для OccupationWindow.xaml
    /// </summary>
    public partial class OccupationWindow : Window
    {
        OracleConnection connection;
        Employee employee;
        Client client;
        public OccupationWindow(OracleConnection con, Employee emp, Client cl)
        {
            connection = con;
            InitializeComponent();
            client = cl;
            employee = emp;
        }

        private void Existing_Click(object sender, RoutedEventArgs e)
        {
            SelectOccupationWindow wind = new SelectOccupationWindow(connection, employee, client);
            wind.Show();
            Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SelectEmployeeUserWindow wind = new SelectEmployeeUserWindow(connection, employee);
            wind.Show();
            Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if(CheckFields())
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();

                    cmd.CommandText = $"insert into system.Occupation (name, cost_per_hour, hours_per_week) values ('{name.Text}', {cost.Text}, {Hours.Text})";

                    cmd.Connection = connection;

                    cmd.CommandType = System.Data.CommandType.Text;

                    int c = cmd.ExecuteNonQuery();

                    if (c != 0)
                    {
                        MessageBox.Show("Occupation is successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Occupation occupation = new Occupation(name.Text, decimal.Parse(cost.Text), int.Parse(Hours.Text)); ;


                        CreateEmployeeWindow wind = new CreateEmployeeWindow(connection, employee, occupation, client);
                        wind.Show();
                        Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static bool GetOccupationId(Occupation occupation, OracleConnection connection)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("system.getOccupationId", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter param = new OracleParameter();
                    param.ParameterName = "@p_name";
                    param.OracleDbType = OracleDbType.Varchar2;
                    param.Value = occupation.Name;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@p_hours";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Value = occupation.HoursPerWeek;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@p_cost";
                    param.OracleDbType = OracleDbType.Decimal;
                    param.Value = occupation.CostPerHour;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o_occ_id";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(cmd.Parameters["@o_trip_id"].Value.ToString());
                    occupation.Occupation_ID = int.Parse(cmd.Parameters["@o_occ_id"].Value.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool CheckFields()
        {
            if (name.Text == "" || cost.Text == "" || Hours.Text == "")
            {
                MessageBox.Show("Fill in the fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            Regex regex = new Regex(@"^[0-9]*[.]?[0-9]{0,2}$");
            MatchCollection matches = regex.Matches(cost.Text);
            if (matches.Count == 0)
            {
                MessageBox.Show("Cost field allows only numbers and a dot", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            regex = new Regex(@"^[0-9]+$");
            matches = regex.Matches(Hours.Text);
            if (matches.Count == 0)
            {
                MessageBox.Show("Hours field allows only numbers", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
    }
}
