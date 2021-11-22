namespace UDP_Server {
    public interface ITableInfo {
        public IDataBaseInfo DataBase { get; set; }
        public DataTypes[] TupleData { get; set; }
    }
}