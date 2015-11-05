﻿#region Information and License

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
// Last Modified:       05.11.2015 (13:46)

#endregion

#region Imports

using System;
using System.Windows;
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
        ///     Constructor of application. Used to initialize global fields.
        /// </summary>
        private BslApp()
        {
            var launcherWindow = new LauncherWindow();
            launcherWindow.Show();
        }

        /// <summary>
        ///     This is the application entry point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var bslApp = new BslApp();

            try
            {
                Current.Resources.MergedDictionaries.Add(
                    LoadComponent(new Uri("Brick Sky Launcher;component/Resources/Metro.xaml", UriKind.Relative)) as
                        ResourceDictionary);
            }
            catch (ArgumentNullException)
            {
                // TODO: Log
            }
            catch (ArgumentException)
            {
                // TODO: Log
            }
            catch (UriFormatException)
            {
                // TODO: Log
            }
            catch (Exception)
            {
                // TODO: Log
            }

            try
            {
                bslApp.Run();
            }
            catch (InvalidOperationException)
            {
                // TODO: Log
            }
        }
    }
}