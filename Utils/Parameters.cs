using System.Configuration;

namespace Utils
{
    /// <summary>
    /// This class contains a set of parameters stored in the app.config file.
    /// </summary>
    public class Parameters
    {
        private static Parameters _this;

        public string userID;
        public string password;
        public string server;
        public string database;


        static Parameters()
        {
            _this = new Parameters();
        }

        public Parameters()
        {

            userID = ConfigurationManager.AppSettings["userID"];
            password = ConfigurationManager.AppSettings["password"];
            server = ConfigurationManager.AppSettings["server"];
            database = ConfigurationManager.AppSettings["database"];
        }

        public static Parameters Get()
        {
            return _this;
        }
    }
}
