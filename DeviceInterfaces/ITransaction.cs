using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInterfaces
{
    [Guid("9EDA6EA7-BB80-4B78-AE68-0C01C966F72D")]
    [ComVisible(true)]
    public interface ITransaction
    {
        void Connect(string connectString);

        void Disconnect();

        string GetVersion();

        string add(int a, int b);
        string multi(int a, int b);
    }
}
