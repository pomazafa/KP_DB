using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {

        OracleConnection connection;
        Employee emp;
        public EmployeeWindow(OracleConnection con, Employee emp)
        {
            InitializeComponent();
            connection = con;
            this.emp = emp;
        }

        private void ExportXML_Click(object sender, RoutedEventArgs e)
        {
            OracleDataAdapter adapter = new OracleDataAdapter("select * from system.client", connection);

            DataSet ds = new DataSet("Clients");
            DataTable dt = new DataTable("Client");
            ds.Tables.Add(dt);
            adapter.Fill(ds.Tables["Client"]);

            ds.WriteXml("clientsdb.xml");
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml("clientsdb.xml");

            DataTable dt = ds.Tables[0];

            int count = 0;

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = "insert into system.client(login, password_hash, lastname, firstname, patronimic, bday, telephone) values ('" + Convert.ToString(dt.Rows[j]["LOGIN"]) + "', '" +
                            Convert.ToString(dt.Rows[j]["PASSWORD_HASH"]) + "', '" + Convert.ToString(dt.Rows[j]["LASTNAME"]) + "', '" + Convert.ToString(dt.Rows[j]["FIRSTNAME"]) + "', '" + Convert.ToString(dt.Rows[j]["PATRONIMIC"])+ 
                            "', TO_DATE('" + Convert.ToDateTime(dt.Rows[j]["BDAY"]) + "', 'DD-MM-YYYY HH24:MI:SS'), '" + Convert.ToString(dt.Rows[j]["TELEPHONE"]) + "')";

                    cmd.Connection = connection;

                    cmd.CommandType = System.Data.CommandType.Text;

                    int c = cmd.ExecuteNonQuery();

                    if (c != 0)
                        count += c;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show(count + " rows exported", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wind = new MainWindow();
            wind.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText =
                "select ti.ticket_id, ti.date_of_trip, cl.client_id, tr.trip_id, tr.start_station_id, tr.end_station_id,  trn.cost_per_station, trn.train_id, trn.train_number, trn.count_seats, cl.lastname from system.ticket ti inner join system.Trip tr on ti.trip_id = tr.trip_id inner join system.train trn on tr.train_id = trn.train_id inner join system.client cl on ti.client_id = cl.client_id where ti.date_of_purchase is null";

            cmd.Connection = connection;

            cmd.CommandType = System.Data.CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();


            if (!dr.HasRows)
            {
                MessageBox.Show("Sorry. There is no rows in database :c", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                while (dr.Read())
                {
                    Train t = new Train(dr.GetInt32(7), dr.GetString(8), dr.GetInt32(9), dr.GetInt32(6));

                    Trip trip = new Trip(dr.GetInt32(3), new Station(), new Station(), t);

                    Ticket tk = new Ticket(dr.GetInt32(0), dr.GetDateTime(1), trip, new Client( dr.GetInt32(2), "", dr.GetString(10), "", new DateTime(), "", ""));

                    ResSet.Items.Add(tk);
                }
            }
        }

        private void ResSet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResSet.SelectedItem != null)
            {
                Process.IsEnabled = true;
            }
            else
            {
                Process.IsEnabled = false;
            }
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = "update system.ticket set employee_id = " + emp.Employee_ID + ", date_of_purchase = TO_DATE('" + DateTime.Now + "', 'DD.MM.YYYY HH24:MI:SS') where ticket_id = " + ((Ticket)ResSet.SelectedItem).TicketID;

            cmd.Connection = connection;

            cmd.CommandType = System.Data.CommandType.Text;

            int c = cmd.ExecuteNonQuery();

            if(c != 0)
            {
                MessageBox.Show("Ticket is processed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ResSet.Items.Remove(ResSet.SelectedItem);
            }

            ResSet.Items.Refresh();
        }
    }
}
