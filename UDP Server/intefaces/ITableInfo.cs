namespace UDP_Server {
    public interface ITableInfo {
        public string TableName { get; }
        public IDataBaseInfo DataBase { get; }
        public DataTypes[] TupleData { get; }
        public string[] ColumnNames { get; }
    }
}