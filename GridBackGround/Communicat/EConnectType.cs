using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GridBackGround.Communicat
{
    public enum EConnectType
    {
        //TCP连接
        TCP = 1,

        //UDP连接
        UDP = 2,

        //串口连接
        DEBUG = 3,
    }
}
