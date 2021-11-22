namespace UDP_Server {
    public class MotionSensorTableInfo : ITableInfo {
        private MotionSensorDatabaseInfo _database;
        private DataTypes[] _tupleData;

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

        public MotionSensorTableInfo() {
            _database = new MotionSensorDatabaseInfo();
            _tupleData = new DataTypes[] { DataTypes.DateTime, DataTypes.BIT, DataTypes.INT, DataTypes.INT, DataTypes.INT };
        }
    }
}