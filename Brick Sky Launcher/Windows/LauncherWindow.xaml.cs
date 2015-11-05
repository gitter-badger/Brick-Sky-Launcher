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
// Filename:            LauncherWindow.xaml.cs
// 
// Created:             05.11.2015 (10:51)
// Last Modified:       05.11.2015 (13:10)

#endregion

#region Imports

using System.Diagnostics;
using System.Windows;

#endregion

namespace BrickSkyLauncher.Windows
{
    /// <summary>
    ///     This is the main window of the application.
    /// </summary>
    internal sealed partial class LauncherWindow
    {
        /// <summary>
        ///     This is the constructor of the main window.
        /// </summary>
        public LauncherWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Opens a new Internet window and navigates to Github Issues.
        /// </summary>
        /// <param name="sender">The source of the routed event.</param>
        /// <param name="e">
        ///     State information and event data associated with the routed event.
        /// </param>
        private void ReportBug(object sender, RoutedEventArgs e)
            => Process.Start("https://github.com/SirRammspopo/Brick-Sky-Launcher/issues/new");
    }
}