namespace UDP_Server {
    public interface ITableInfo {
        public IDataBaseInfo DataBase { get; }
        public DataTypes[] TupleData { get; }
    }
}