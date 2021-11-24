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
            string re = $"INSERT INTO {info.TableName}" + "Values (";

            for(int i=0; i<data.Data.Length; i++) {
                re += $"@{data.Data[0]},";
            }
            re.Remove(0, re.Length - 1);
            re += ")";

            return re;
        }

        /// <summary>
        /// Uploads the given data to the database of the table with the given info.
        /// </summary>
        /// <param name="data">The data to upload, should mirror the tuple of the table</param>
        /// <param name="info">Information and metadata about the destination table and it's database</param>
        /// <returns>True if the SQL process completes without errors, otherwise false</returns>
        public static async Task<bool> UploadDataToDatabaseAsync(IData data, ITableInfo info) {
            try {
                using (SqlConnection connection = new SqlConnection(info.DataBase.ConnectionString)) {
                    using (SqlCommand command = new SqlCommand(SQLInsert(data, info), connection)) {

                        await command.Connection.OpenAsync();

                        int i = await command.ExecuteNonQueryAsync();
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
