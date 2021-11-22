using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server {
    public static class DatabaseAccess {

        /// <summary>
        /// Uploads the given data to the database of the table with the given info.
        /// </summary>
        /// <param name="data">The data to upload, should mirror the tuple of the table</param>
        /// <param name="info">Information and metadata about the destination table and it's database</param>
        public static void UploadDataToDatabase(IData data, ITableInfo info) {
            try {
                using (SqlConnection connection = new SqlConnection(info.DataBase.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsert, connection)) {
                        //command.Parameters.AddWithValue($"@{_relationalKeys[0]}", evt.Id); //not needed for tables that are auto indexed
                        command.Parameters.AddWithValue($"@{_relationalAttributes[0]}", evt.Description);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[1]}", evt.Name);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[2]}", GetThemeToDatabase(evt.Theme));
                        command.Parameters.AddWithValue($"@{_relationalAttributes[3]}", evt.StartTime);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[4]}", evt.StopTime.TimeOfDay.TotalMinutes - evt.StartTime.TimeOfDay.TotalMinutes);
                        command.Parameters.AddWithValue($"@{_relationalAttributes[5]}", evt.RoomNr);

                        await command.Connection.OpenAsync();

                        int i = await command.ExecuteNonQueryAsync();
                        if (i != 1) {
                            return false;
                        } else {
                            int evt_Id = await GetHighstId();
                            bool re = true;
                            foreach (int participant in evt.Speakers) {
                                if (!await CreateSpeaker(evt_Id, participant)) {
                                    re = false;
                                }
                            }
                            return re;
                        }
                    }
                }
            } catch (Exception e) {
                string s = e.StackTrace;
                Console.WriteLine(s);
                Console.Beep();
            }
        }

    }
}
