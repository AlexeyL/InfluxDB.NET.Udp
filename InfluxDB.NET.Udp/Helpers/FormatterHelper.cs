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
        /// Convert tags to string
        /// </summary>
        /// <param name="tags"></param>
        /// <returns>string that represents tags</returns>
        internal static string ConvertTagsToString(IDictionary<string, object> tags)
        {
            return ConvertDictionaryToString(tags);
        }

        /// <summary>
        /// Convert fields to string
        /// </summary>
        /// <param name="fields"></param>
        /// <returns>string that represents fields</returns>
        internal static string ConvertFieldsToString(IDictionary<string, object> fields)
        {
            return ConvertDictionaryToString(fields);
        }

        /// <summary>
        /// Concantenate tags and measurement
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="measurement"></param>
        /// <returns>concatenated measurement and tags if exsits</returns>
        internal static string GetKeyFormTagsAndMeasure(string tags, string measurement)
        {
            return string.IsNullOrEmpty(tags)
                ? EscapeNonTagValue(measurement)
                : string.Join(",", EscapeNonTagValue(measurement), tags);
        }

        /// <summary>
        /// Converts datetime to unix representation
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns>DateTime in unix representation</returns>
        internal static string ConvertToUnixTimeString(DateTime? datetime)
        {
            return datetime.HasValue
                ? string.Format("{0}000000000", (Int32)(datetime.Value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds)
                : string.Empty;
        }

        /// <summary>
        /// Escape non tag value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Escaped string</returns>
        private static string EscapeNonTagValue(string value)
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
        private static string EscapeTagValue(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            return value.Replace(@" ", @"\ ").Replace(@"=", @"\=").Replace(@",", @"\,");
        }

        /// <summary>
        /// Convert fields or tags dictionary to string
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        private static string ConvertDictionaryToString(IDictionary<string, object> dics)
        {
            return string.Join(",",
                dics.Select(d => string.Join("=", d.Key, EscapeTagValue(d.Value.ToString()))));
        }
    }
}
