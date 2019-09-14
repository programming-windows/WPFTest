using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInterfaces
{

    [Guid("B218DF77-16A0-44E7-A1D7-79394D0EA674")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Description("UA事务记录器")]
    public class UaTransaction : ITransaction
    {
        private String connectionString = "";

        public void Connect(string connectString)
        {
            connectionString = connectString;
        }

        public void Disconnect()
        {
            connectionString = "";
        }

        public string GetVersion()
        {
            return "1.0";
        }

        public string add(int a, int b)
        {
            return string.Concat(a,"-", b ,"=", a - b);
        }

        public string multi(int a, int b)
        {
            return string.Concat(a, "*", b, "* 10 =", a * b * 10);
        }
    }
}
