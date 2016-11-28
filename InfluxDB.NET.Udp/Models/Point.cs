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
        public Point()
        {
            Tags = new Dictionary<string, object>();
            Fields = new Dictionary<string, object>();
        }

        /// <summary>
        /// Measurement name (Serie name to write the data into).
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
        /// Explicit point timestamp (optional).
        /// </summary>
        public DateTime? Timestamp { get; set; }


        /// <summary>
        /// Convert point to string
        /// </summary>
        /// <returns>string ready to be written into database</returns>
        public override string ToString()
        {
            ValidationHelper.NotNullOrEmpty(this.Measurement, "measurement");
            ValidationHelper.NotNull(this.Tags, "tags");
            ValidationHelper.NotNull(this.Fields, "fields");

            var tags = string.Join(",",
                this.Tags.Select(t => string.Join("=", t.Key, FormatterHelper.EscapeTagValue(t.Value.ToString()))));

            var fields = string.Join(",", this.Fields.Select(t => this.ToString()));

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
