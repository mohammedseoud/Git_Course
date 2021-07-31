using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Common
{
    public static class Guard
    {
        /// <summary>
        /// Throw ArgumentNullException if object is null.
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <param name="paramName">Parameter name to include in exception.</param>
        public static void ArgumentIsNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throw ArgumentNullException if object is null.
        /// </summary>
        /// <param name="obj">Object to check.</param>
        /// <param name="paramName">Parameter name to include in exception.</param>
        /// <param name="message">Message to include in exception.</param>
        public static void ArgumentIsNull(object obj, string paramName, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = string.Format($"{paramName} can not be null.");
            }

            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        /// <summary>
        /// Throw ArgumentNullException is string is null.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="paramName">Parameter name to include in exception.</param>
        public static void StringIsEmpty(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throw ArgumentNullException is string is null.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="paramName">Parameter name to include in exception.</param>
        /// <param name="message">Message to include in exception.</param>
        public static void StringIsEmpty(string value, string paramName, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                message = string.Format($"{paramName} can not be null or empty.");
            }

            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
