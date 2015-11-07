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
// Filename:            LoggerException.cs
// 
// Created:             06.11.2015 (17:03)
// Last Modified:       07.11.2015 (18:48)

#endregion

#region Imports

using System;
using System.Runtime.Serialization;

#endregion

namespace BrickSkyLauncher.Exceptions
{
    /// <summary>
    ///     This <see cref="Exception" /> is thrown on logging errors.
    /// </summary>
    [Serializable]
    public sealed class LoggerException : Exception
    {
        /// <summary>
        ///     Creates the exception
        /// </summary>
        public LoggerException()
        {
        }

        /// <summary>
        ///     Create the exception with description
        /// </summary>
        /// <param name="message">The description of the exception</param>
        public LoggerException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">The description of the exception</param>
        /// <param name="innerException">
        ///     The inner exception that caused this exception
        /// </param>
        public LoggerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        ///     Create the exception from serialized data. Usual scenario is when
        ///     exception is occured somewhere on the remote workstation and we have
        ///     to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The serialization context</param>
        /// <exception cref="SerializationException">
        ///     The class-name is <see langword="null" /> , or
        ///     <see cref="System.Exception.HResult" /> is 0.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="info" /> -parameter is <see langword="null" /> .
        /// </exception>
        private LoggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}