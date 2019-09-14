using DeviceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest
{
    class ComTest
    {
        public static ITransaction CreateTransaction(string _guid,string connectionStr)
        {
            Console.Write("begin creating transaction");
            ITransaction iTransaction = null;
            try
            {
                Guid guid = new Guid(_guid);
                Type transactionType = Type.GetTypeFromCLSID(guid);
                object transaction = Activator.CreateInstance(transactionType);
                iTransaction = transaction as ITransaction;
                iTransaction.Connect(connectionStr);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            Console.Write("end creating transaction");
            return iTransaction;
        }

        public static string add(string guid,string connectionStr,int a, int b)
        {
            ITransaction transaction = CreateTransaction(guid, connectionStr);
            return transaction.add(a, b);
        }

        public static string multi(string guid, string connectionStr, int a, int b)
        {
            ITransaction transaction = CreateTransaction(guid, connectionStr);
            return transaction.multi(a, b);
        }

    }
}
