using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server {
    public class DistanceSensorTableInfo : ITableInfo {
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

        public DistanceSensorTableInfo() {
            _database = new ParkingPiratesDatabaseInfo();
            _tupleData = new DataTypes[] { DataTypes.INT, DataTypes.INT, DataTypes.INT, DataTypes.INT };
            _tableName = "SpecielleParkeringsPladser";
            _columnNames = new string[] { "OmrådeID", "ParkeringsType", "Pladser", "OptagedePladser" };
        }
    }
}