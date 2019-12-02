using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyProject
{
    public static class Controller
    {
        public static bool InsertAll(Train train, List<TrainStop> stops, OracleConnection connection)
        {
            Trip trip = new Trip(stops[0].Station, stops[stops.Count - 1].Station, train);

            if (InsertTrip(trip, connection))
            {
                if (GetTripId(trip, train, connection))
                {
                    if (InsertStations(trip, stops, connection))
                    {
                        MessageBox.Show("Everything inserted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool GetTripId(Trip trip, Train train, OracleConnection connection)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("system.getTripId", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    OracleParameter param = new OracleParameter();
                    param.ParameterName = "@p_stat1_id";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Value = trip.Station1.Station_ID;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@p_stat2_id";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Value = trip.Station2.Station_ID;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@p_train_id";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Value = train.TRAIN_ID;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter();
                    param.ParameterName = "@o_trip_id";
                    param.OracleDbType = OracleDbType.Int32;
                    param.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show(cmd.Parameters["@o_trip_id"].Value.ToString());
                    trip.Trip_ID = int.Parse(cmd.Parameters["@o_trip_id"].Value.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool InsertTrip(Trip trip, OracleConnection connection)
        {
            OracleCommand cmdIns = new OracleCommand();

            cmdIns.CommandText = "insert into system.trip(train_id, start_station_id, end_station_id) values (" + trip.Train.TRAIN_ID + ", " + trip.Station1.Station_ID + ", " + trip.Station2.Station_ID + ")";

            cmdIns.Connection = connection;

            cmdIns.CommandType = System.Data.CommandType.Text;

            int c = cmdIns.ExecuteNonQuery();

            if (c == 0)
            {
                MessageBox.Show("There something wrong with database. Trip is not inserted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                //MessageBox.Show("Trip successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
        }

        public static bool InsertStations(Trip trip, List<TrainStop> stops, OracleConnection connection)
        {
            foreach (TrainStop ts in stops)
            {
                try
                {
                    OracleCommand cmdIns = new OracleCommand();
                    MessageBox.Show(ts.Arrival_datetime.ToString());
                    cmdIns.CommandText = "insert into system.train_stop(arrival_time, departure_time, station_id, trip_id) values (TO_DATE('" + ts.Arrival_datetime.ToString() + "', 'DD-MM-YYYY HH24:MI:SS'), TO_DATE('" + ts.Departure_datetime.ToString() + "', 'DD-MM-YYYY HH24:MI:SS'), " + ts.Station.Station_ID + ", " + trip.Trip_ID + ")";

                    cmdIns.Connection = connection;

                    cmdIns.CommandType = System.Data.CommandType.Text;

                    int c = cmdIns.ExecuteNonQuery();

                    if (c == 0)
                    {
                        MessageBox.Show("There something wrong with database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
