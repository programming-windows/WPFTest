using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Utils
{
    public class Log
    {

        private static Log _log;

        /// <summary>
        /// Whether the log is enabled.
        /// </summary>
        private static bool enabled;

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Log()
        {
            enabled = true;
            _log = new Log();
        }

        /// <summary>
        /// The writer to the log file.
        /// </summary>
        StreamWriter writer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Log()
        {
            writer = new StreamWriter(@"log.txt", false, new UTF8Encoding());
        }

        /// <summary>
        /// Write a line to the log file.
        /// </summary>
        /// <param name="line">The text line to write.</param>
        public static void Write(string line)
        {
            if (!enabled)
                return;

            _log.writer.WriteLine(line);
            _log.writer.Flush();
        }

        /// <summary>
        /// Enable or disable the log.
        /// </summary>
        /// <param name="e">Enable or disable.</param>
        public static void Enable(bool e)
        {
            enabled = e;
        }
    }
}
