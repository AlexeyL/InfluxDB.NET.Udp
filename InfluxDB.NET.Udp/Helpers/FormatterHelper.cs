using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDB.NET.Udp.Helpers
{
    internal static class FormatterHelper
    {
        internal static string EscapeNonTagValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            return value.Replace(@"""", @"\""").Replace(@" ", @"\ ").Replace(@"=", @"\=").Replace(@",", @"\,");
        }

        internal static string EscapeTagValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            return value.Replace(@" ", @"\ ").Replace(@"=", @"\=").Replace(@",", @"\,");
        }

        internal static int ConvertToUnixTime(DateTime datetime)
        {
            return (Int32)(datetime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
