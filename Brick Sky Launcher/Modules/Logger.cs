#region Information and License

// This file is part of Brick Sky Launcher.
// 
// Copyright (C) 2015-2015 Glenn Kintscher (Sir Rammspopo)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// Part of Solution:    Brick Sky Launcher
// Name of Project:     Brick Sky Launcher
// Filename:            Logger.cs
// 
// Created:             05.11.2015 (14:41)
// Last Modified:       07.11.2015 (13:30)

#endregion

#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using BrickSkyLauncher.Exceptions;

#endregion

namespace BrickSkyLauncher.Modules
{
    /// <summary>
    ///     Logs messages to logfiles at a specific path (see
    ///     <see cref="PathToLogfile" />). Start a new logger instance with
    ///     <code>var logger = new Logger()</code>. You can add a new log entry
    ///     with
    ///     <code>logger.AddLogEntry(logType, logTitle, logMessage)</code>.
    /// </summary>
    internal static class Logger
    {
        /// <summary>
        ///     Path to logfile (e.g. C:\Users\CurrentUser\AppData\Roaming\Brick
        ///     Sky\Launcher\Logs\201501011200.brickskylog)
        /// </summary>
        private static readonly string PathToLogfile =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            "\\Brick Sky\\Launcher\\Logs\\" +
            DateTime.Now.ToString("yyyyMMddHHmmss") + ".brickskylog";

        /// <summary>
        ///     Starts the logger and creates a new logfile. If there are more than ten logfiles in the log-folder, all will be
        ///     deleted.
        /// </summary>
        /// <exception cref="LoggerException">Path to log is null or empty.</exception>
        internal static void Init()
        {
            var path = Path.GetDirectoryName(PathToLogfile);

            if (string.IsNullOrEmpty(path))
            {
                throw new LoggerException("Path to log is null or empty");
            }

            Directory.CreateDirectory(path);

            if (Directory.GetFiles(path, "*.brickskylog", SearchOption.TopDirectoryOnly).Length > 10)
            {
                foreach (var file in Directory.GetFiles(path).Where(file => Path.GetExtension(file) == ".brickskylog"))
                {
                    File.Delete(file);
                }
            }

            AddLogEntry(LogTypes.Information, "Logger", "Initialization of Logger finished");
        }

        /// <summary>
        ///     This function adds an entry to the logfile with the given
        ///     information.
        /// </summary>
        /// <param name="logType">
        ///     The type of the message (see
        ///     <see cref="LogTypes" />)
        /// </param>
        /// <param name="logTitle">The title of the message to log</param>
        /// <param name="logMessage">The message to log</param>
        /// <exception cref="LoggerException">Logger was not able to log an entry.</exception>
        internal static void AddLogEntry(string logType, string logTitle, string logMessage)
        {
            if ((logType == LogTypes.Debug) && !BslApp.StartArguments.Contains("-debug"))
            {
                return;
            }

            try
            {
                using (var w = File.AppendText(PathToLogfile))
                {
                    w.WriteLine("{0} | {1} | {2, -10} | {3, -20} | {4}", DateTime.Now.ToString("yyyy-MM-dd"),
                        DateTime.Now.ToLongTimeString(), logType, logTitle, logMessage);
                }
            }
            catch (Exception ex)
            {
                throw new LoggerException("Logger was not able to log an entry.", ex);
            }
        }

        /// <summary>
        ///     Available types of log messages
        /// </summary>
        internal static class LogTypes
        {
            /// <summary>
            ///     Used to log debug information
            /// </summary>
            public const string Debug = "DEBUG";

            /// <summary>
            ///     Used to log important information
            /// </summary>
            public const string Information = "INFO";

            /// <summary>
            ///     Used to log exceptions without consequences
            /// </summary>
            public const string Warning = "WARNING";

            /// <summary>
            ///     Used to log exceptions with consequences
            /// </summary>
            public const string Error = "ERROR";

            /// <summary>
            ///     Used to log exceptions that blocks the application
            /// </summary>
            public const string Fatal = "FATAL";
        }
    }
}