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
// Filename:            NewsReader.cs
// 
// Created:             06.11.2015 (22:43)
// Last Modified:       07.11.2015 (18:48)

#endregion

#region Imports

using System.ServiceModel.Syndication;
using System.Xml;

#endregion

namespace BrickSkyLauncher.Modules
{
    /// <summary>
    ///     Gets the news from the website and displays them in the launcher
    /// </summary>
    internal sealed class NewsReader
    {
        /// <summary>
        ///     Location of RSS-Feed
        /// </summary>
        private const string NewsUrl = "http://www.bricksky.de/forum/cms/index.php?news-feed/";

        internal SyndicationFeed Feed;

        /// <summary>
        ///     Gets the news from the website
        /// </summary>
        internal NewsReader()
        {
            using (var reader = XmlReader.Create(NewsUrl))
            {
                Feed = SyndicationFeed.Load(reader);
                foreach (var item in Feed.Items)
                {
                    Logger.AddLogEntry(Logger.LogTypes.Debug, "NewsReader", item.Title.Text);
                }
            }
        }
    }
}