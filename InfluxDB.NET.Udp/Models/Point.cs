using System;
using System.Collections.Generic;
using System.Linq;
using InfluxDB.NET.Udp.Helpers;

namespace InfluxDB.NET.Udp.Models
{
    /// <summary>
    /// Represents a time series point for InfluxDb writes.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Poin constructor
        /// </summary>
        public Point()
        {
            Tags = new Dictionary<string, object>();
            Fields = new Dictionary<string, object>();
        }

        /// <summary>
        /// Measurement name.
        /// </summary>
        public string Measurement { get; set; }

        /// <summary>
        /// Tags to write.
        /// </summary>
        public IDictionary<string, object> Tags { get; set; }

        /// <summary>
        /// Fields to write.
        /// </summary>
        public IDictionary<string, object> Fields { get; set; }

        /// <summary>
        /// Point timestamp (optional).
        /// </summary>
        public DateTime? Timestamp { get; set; }


        /// <summary>
        /// Sttring representation of point
        /// </summary>
        /// <returns>string ready to be written into database</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Measurement))
                throw new ArgumentException("Measurement missed.");

            var tags = string.Join(",",
                this.Tags.Select(t => string.Join("=", t.Key, FormatterHelper.EscapeTagValue(t.Value.ToString()))));

            var fields = string.Join(",",
                this.Fields.Select(f => string.Join("=", f.Key, FormatterHelper.EscapeTagValue(f.Value.ToString()))));

            var key = string.IsNullOrEmpty(tags)
                ? FormatterHelper.EscapeNonTagValue(this.Measurement)
                : string.Join(",", FormatterHelper.EscapeNonTagValue(this.Measurement), tags);

            var ts = this.Timestamp.HasValue
                ? FormatterHelper.ConvertToUnixTime(this.Timestamp.Value).ToString()
                : string.Empty;

            var result = $"{key} {fields} {ts}";

            return result;
        }
    }
}
