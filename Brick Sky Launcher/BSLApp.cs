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
// Filename:            BSLApp.cs
// 
// Created:             05.11.2015 (10:48)
// Last Modified:       07.11.2015 (13:30)

#endregion

#region Imports

using System;
using System.Windows;
using BrickSkyLauncher.Exceptions;
using BrickSkyLauncher.Modules;
using BrickSkyLauncher.Windows;

#endregion

namespace BrickSkyLauncher
{
    /// <summary>
    ///     This class contains the application entry point and global fields.
    /// </summary>
    internal sealed class BslApp : Application
    {
        /// <summary>
        ///     Start arguments of the application
        /// </summary>
        internal static readonly string[] StartArguments = Environment.GetCommandLineArgs();

        /// <summary>
        ///     Constructor of application. Used to start windows.
        /// </summary>
        private BslApp()
        {
            var launcherWindow = new LauncherWindow();

            launcherWindow.Show();

            Logger.AddLogEntry(Logger.LogTypes.Debug, "BslApp",
                "Application started. REMEMBER THIS IS AN ALPHA VERSION!");
        }

        /// <summary>
        ///     This is the application entry point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                Logger.Init();

                ConfigurationManager.Init();

                var bslApp = new BslApp();

                bslApp.Resources.MergedDictionaries.Add(
                    LoadComponent(new Uri("Brick Sky Launcher;component/Resources/Metro.xaml", UriKind.Relative)) as
                        ResourceDictionary);

                bslApp.Run();
            }
            catch (LoggerException loggerException)
            {
                // TODO: Log exceptions
                Console.WriteLine(loggerException.StackTrace);
            }
            catch (Exception exception)
            {
                // TODO: Log exceptions 
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}