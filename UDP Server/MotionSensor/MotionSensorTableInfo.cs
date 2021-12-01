namespace UDP_Server {
    public class MotionSensorTableInfo : ITableInfo {
        private ParkingPiratesDatabaseInfo _database;
        private DataTypes[] _tupleData;
        private string _tableName;
        private string[] _columnNames;

        public IDataBaseInfo DataBase {
            get {
                return _database;
            }
        }
        public DataTypes[] TupleData {
            get {
                return _tupleData;
            }
        }
        public string TableName {
            get {
                return _tableName;
            }
        }

        public string[] ColumnNames {
            get {
                return _columnNames;
            }
        }

        public MotionSensorTableInfo() {
            _database = new ParkingPiratesDatabaseInfo();
            _tupleData = new DataTypes[] { DataTypes.DateTime, DataTypes.BIT, DataTypes.INT, DataTypes.INT, DataTypes.INT };
            _tableName = "Log";
            _columnNames = new string[] { "Tidspunkt", "Retning", "Nedbør", "Temperatur", "Vindhastighed" };
        }
    }
}