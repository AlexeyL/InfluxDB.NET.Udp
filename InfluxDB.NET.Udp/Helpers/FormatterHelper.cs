using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDB.NET.Udp.Helpers
{
    internal static class FormatterHelper
    {
        /// <summary>
        /// Escape non tag value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Escaped string</returns>
        internal static string EscapeNonTagValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            return value.Replace(@"""", @"\""").Replace(@" ", @"\ ").Replace(@"=", @"\=").Replace(@",", @"\,");
        }

        /// <summary>
        /// Escape tag value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Escaped string</returns>
        internal static string EscapeTagValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            return value.Replace(@" ", @"\ ").Replace(@"=", @"\=").Replace(@",", @"\,");
        }

        /// <summary>
        /// Converts datetime to unix representation
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns>DateTime in unix representation</returns>
        internal static int ConvertToUnixTime(DateTime datetime)
        {
            return (Int32)(datetime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
