using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// LogInfo.cs
// CopyRight (c) 2009-2010 HangJing Teconology. All Rights Reserved.
// zjf@ihangjing.com
// 2010-01-25
// 程序log信息类
namespace Hangjing.Model
{
    public class LogInfo
    {
        private LogType type;
        private DateTime dateTime;
        private string message;
        private string user;
        private int id;

        /// <summary>
        /// Initializes a new instance of the <b>LogInfo</b> class.
        /// </summary>
        /// <param name="type">The type of the Entry</param>
        /// <param name="dateTime">The DateTime.</param>
        /// <param name="message">The Message.</param>
        /// <param name="user">The User.</param>
        public LogInfo(int id, LogType type, DateTime dateTime, string message, string user)
        {
            this.id = id;
            this.type = type;
            this.dateTime = dateTime;
            this.message = message;
            this.user = user;
        }

        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Gets the EntryType.
        /// </summary>
        public LogType EntryType
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the DateTime.
        /// </summary>
        public DateTime DateTime
        {
            get { return dateTime; }
        }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Gets the User.
        /// </summary>
        public string User
        {
            get { return user; }
        }
    }

    /// <summary>
    /// Enumerates the Types of Log Entries.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Represents a simple Message.
        /// </summary>
        General,
        /// <summary>
        /// Represents a Warning.
        /// </summary>
        Warning,
        /// <summary>
        /// Represents an Error.
        /// </summary>
        Error
    }

    /// <summary>
    /// Lists legal logging level values.
    /// </summary>
    public enum LoggingLevel
    {
        /// <summary>
        /// All messages are logged.
        /// </summary>
        AllMessages = 3,
        /// <summary>
        /// Warnings and errors are logged.
        /// </summary>
        WarningsAndErrors = 2,
        /// <summary>
        /// Errors only are logged.
        /// </summary>
        ErrorsOnly = 1,
        /// <summary>
        /// Logging is completely disabled.
        /// </summary>
        DisableLog = 0
    }
}
