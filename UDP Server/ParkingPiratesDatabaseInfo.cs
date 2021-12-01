namespace UDP_Server {
    public class ParkingPiratesDatabaseInfo : IDataBaseInfo {
        public string ConnectionString {
            get {
                return "Data Source=(localdb)\\MSSQLLocaldb;Initial Catalog=EventMakerDB21;Integrated Security=True";
                //return "Server=tcp:emilzealanddb.database.windows.net,1433;Initial Catalog=ParkeringsDataDb;Persist Security Info=False;User ID=emiladmin;Password=Sql12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            }
        }

        public ParkingPiratesDatabaseInfo() {
            
        }

    }
}