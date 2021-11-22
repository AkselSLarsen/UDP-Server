using System;

namespace UDP_Server {
    public enum DataTypes : Byte {
        INT,
        BIT,
        DateTime,
        VarChar8,
        VarChar16,
        VarChar32,
        VarChar64,
        VarChar128,
        VarChar256,
        VarChar512
    }
}