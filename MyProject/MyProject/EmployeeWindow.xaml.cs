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
        public EmployeeWindow(OracleConnection con)
        {
            InitializeComponent();
            connection = con;
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
            // выбираем первую таблицу
            DataTable dt = ds.Tables[0];

            string[] LOGIN = new string[dt.Rows.Count];

            string[] PASSWORD = new string[dt.Rows.Count];

            string[] LASTNAME = new string[dt.Rows.Count];

            string[] FIRSTNAME = new string[dt.Rows.Count];

            string[] PATRONIMIC = new string[dt.Rows.Count];

            DateTime[] BDAY = new DateTime[dt.Rows.Count];

            string[] TELEPHONE = new string[dt.Rows.Count];

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LOGIN[j] = Convert.ToString(dt.Rows[j]["LOGIN"]);
                PASSWORD[j] = Convert.ToString(dt.Rows[j]["PASSWORD_HASH"]);
                LASTNAME[j] = Convert.ToString(dt.Rows[j]["LASTNAME"]);
                FIRSTNAME[j] = Convert.ToString(dt.Rows[j]["FIRSTNAME"]);
                PATRONIMIC[j] = Convert.ToString(dt.Rows[j]["PATRONIMIC"]);
                BDAY[j] = Convert.ToDateTime(dt.Rows[j]["BDAY"]);
                TELEPHONE[j] = Convert.ToString(dt.Rows[j]["TELEPHONE"]);
            

            OracleParameter login = new OracleParameter();
            login.OracleDbType = OracleDbType.Varchar2;
            login.Value = LOGIN;

            OracleParameter password = new OracleParameter();
            password.OracleDbType = OracleDbType.Varchar2;
            password.Value = PASSWORD;

            OracleParameter lastname = new OracleParameter();
            lastname.OracleDbType = OracleDbType.Varchar2;
            lastname.Value = LASTNAME;

            OracleParameter firstname = new OracleParameter();
            firstname.OracleDbType = OracleDbType.Varchar2;
            firstname.Value = FIRSTNAME;

            OracleParameter patronimic = new OracleParameter();
            patronimic.OracleDbType = OracleDbType.Varchar2;
            patronimic.Value = PATRONIMIC;

            OracleParameter bday = new OracleParameter();
            bday.OracleDbType = OracleDbType.Date;
            bday.Value = BDAY;

            OracleParameter telephone = new OracleParameter();
            telephone.OracleDbType = OracleDbType.Varchar2;
            telephone.Value = TELEPHONE;
            try
            {
                OracleCommand cmd = new OracleCommand();
                    MessageBox.Show(BDAY[j].Date.ToString());
                cmd.CommandText = "insert into system.client(login, password_hash, lastname, firstname, patronimic, bday, telephone) values ('" + LOGIN[j] + "', '"+
                        PASSWORD[j] + "', '" + LASTNAME[j] + "', '" + FIRSTNAME[j] + "', '" + PATRONIMIC[j] + "', TO_DATE('" + BDAY[j].Date.ToString() +
                        "', 'DD-MM-YYYY HH24:MI:SS'), '" + TELEPHONE[j] + "')";

                cmd.Connection = connection;

                cmd.CommandType = System.Data.CommandType.Text;

                int c = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
           // foreach (DataColumn column in dt.Columns)
             //   MessageBox.Show("\t" + column.ColumnName, column.ColumnName);
            //// перебор всех строк таблицы
            //foreach (DataRow row in dt.Rows)
            //{
            //    var cells = row.ItemArray;
            //    foreach (object cell in row.ItemArray)
            //        MessageBox.Show("\t" + cell.ToString());
            //}

            //OracleDataAdapter da = new OracleDataAdapter();
            //OracleCommand cmdOra = new OracleCommand("system.insertClient", connection);
            //cmdOra.CommandType = CommandType.StoredProcedure;

            //da.InsertCommand = cmdOra;
            //da.Update(ds);

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
                MessageBox.Show("Sorry. There is something wrong with database :c");
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
    }
}
