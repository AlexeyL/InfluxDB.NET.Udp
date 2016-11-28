using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluxDB.NET.Udp.Helpers
{
    internal static class ValidationHelper
    {
        internal static void NotNull<T>(T value, string paramName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        internal static void NotNull<T>(T value, string paramName, string message) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(paramName, message);
        }

        internal static void IfTrue(bool value, string message)
        {
            if (value)
                throw new ArgumentException(message);
        }

        internal static void NotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(paramName);
        }

        internal static void NotZeroLength<T>(T[] array, string paramName)
        {
            if (array.Length == 0)
                throw new ArgumentOutOfRangeException(paramName);
        }

        internal static void NotZeroLength<T>(T[] array, string paramName, string message)
        {
            if (array.Length == 0)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        internal static void NotZeroLength<T>(List<T> list, string paramName)
        {
            if (list.Count == 0)
                throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
