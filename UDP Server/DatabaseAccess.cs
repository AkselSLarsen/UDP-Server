using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server {
    public static class DatabaseAccess {

#warning Should define which columns it inserts into, so it also works with tables with autoincrementing columns.
        private static string SQLInsert(IData data, ITableInfo info) {
            string re = $"INSERT INTO {info.TableName}" + " Values (";

            for(int i=0; i<info.ColumnNames.Length; i++) {
                re += $"@{info.ColumnNames[i]}, ";
            }
            re = re.Remove(re.Length-2, 2);
            re += ");";

            return re;
        }

        /// <summary>
        /// Uploads the given data to the database of the table with the given info.
        /// </summary>
        /// <param name="data">The data to upload, should mirror the tuple of the table</param>
        /// <param name="info">Information and metadata about the destination table and it's database</param>
        /// <returns>True if the SQL process completes without errors, otherwise false</returns>
        public static bool UploadDataToDatabaseAsync(IData data, ITableInfo info) {
            try {
                using (SqlConnection connection = new SqlConnection(info.DataBase.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsert(data, info), connection)) {
                        for(int j=0; j<info.ColumnNames.Length; j++) {
                            command.Parameters.AddWithValue($"@{info.ColumnNames[j]}", data.Data[j]);
                        }

                        command.Connection.Open();

                        int i = command.ExecuteNonQuery();
                        if (i != 1) {
                            return false;
                        } else {
                            return true;
                        }
                    }
                }
            } catch (Exception e) {
                string s = e.StackTrace;
                Console.WriteLine(s);
                Console.Beep();
            }
            return false;
        }
    }
}
