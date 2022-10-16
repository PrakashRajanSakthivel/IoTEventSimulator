using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTEventSimulator
{
    public static class Helpers
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="locations"></param>
        /// <param name="fleetId"></param>
        /// <param name="batchId"></param>
        public static void AddSimulatedData(List<string> locations, string fleetId, string batchId)
        {
            MySqlConnection dbConnection = new MySqlConnection(Constants.connString);
            try
            {
                dbConnection.Open();
                for (int i = 0; i < locations.Count; i++)
                {
                    MySqlCommand command = dbConnection.CreateCommand();
                    command.CommandText = $"INSERT INTO fleet_simulator_data(fleet_id,batch_id,recorded_location,recorded_date_time) VALUES(@fleetid,@batchid,@datetime,@location)";
                    command.Parameters.Add("@fleetid", MySqlDbType.VarChar).Value = fleetId;
                    command.Parameters.Add("@batchid", MySqlDbType.VarChar).Value = batchId;
                    command.Parameters.Add("@datetime", MySqlDbType.DateTime).Value = DateTime.Now.AddMinutes(45*i);
                    command.Parameters.Add("@location", MySqlDbType.VarChar).Value = locations[i];
                    command.ExecuteNonQuery();
                    Thread.Sleep(2500);
                }
                dbConnection.Close();
            }
            finally
            {
                dbConnection.Dispose();
            }
        }

        /// <summary>
        /// Fetch Data
        /// </summary>
        /// <param name="simulatorDetails"></param>
        /// <param name="tableToLookFor"></param>
        /// <param name="columnToLookFor"></param>
        /// <returns></returns>
        public static string GetFleetId(SimulatorDetails simulatorDetails, string tableToLookFor, string columnToLookFor)
        {
            string fleed_id = "";
            MySqlConnection dbConnection = new MySqlConnection(Constants.connString);
            try
            {
                dbConnection.Open();
                MySqlCommand cmd = dbConnection.CreateCommand();
                cmd.CommandText = $"SELECT {columnToLookFor} from {tableToLookFor} WHERE batch_id = @batch_id";
                cmd.Parameters.AddWithValue("@batch_id", simulatorDetails.BatchId);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    fleed_id = reader.GetString(0);
                }
            }
            finally
            {
                dbConnection.Close();
            }

            return fleed_id;
        }
    }
}
