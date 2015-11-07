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
// Filename:            ConfigurationManager.cs
// 
// Created:             06.11.2015 (16:46)
// Last Modified:       07.11.2015 (18:48)

#endregion

#region Imports

using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using BrickSkyLauncher.Exceptions;

#endregion

namespace BrickSkyLauncher.Modules
{
    /// <summary>
    ///     Saves settings in a configuration file to use them later
    /// </summary>
    internal static class ConfigurationManager
    {
        /// <summary>
        ///     Loaded configuration file
        /// </summary>
        private static readonly XmlDocument ConfigFile = new XmlDocument();

        /// <summary>
        ///     <see cref="Path" /> to the configuration file (e.g.
        ///     C:\Users\CurrentUser\AppData\Roaming\Brick
        ///     Sky\Launcher\Config\Launcher.brickskycfg)
        /// </summary>
        private static readonly string PathToConfigFile =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            "\\Brick Sky\\Launcher\\Config\\Launcher.brickskycfg";

        /// <summary>
        ///     Starts the configuration manager and creates the config file if it
        ///     is missing
        /// </summary>
        /// <exception cref="ConfigurationManagerException" />
        internal static void Init()
        {
            var path = Path.GetDirectoryName(PathToConfigFile);

            if (string.IsNullOrEmpty(path))
            {
                throw new ConfigurationManagerException("Path to configuration is null or empty.");
            }

            Directory.CreateDirectory(path);

            if (!File.Exists(PathToConfigFile))
            {
                using (
                    var stream =
                        Assembly.GetExecutingAssembly()
                            .GetManifestResourceStream("BrickSkyLauncher.Resources.Launcher.brickskycfg"))
                {
                    using (var file = new FileStream(PathToConfigFile, FileMode.Create, FileAccess.Write))
                    {
                        stream?.CopyTo(file);
                    }
                }
            }

            ConfigFile.Load(PathToConfigFile);
        }

        /// <summary>
        ///     Sets a setting in the config file
        /// </summary>
        /// <param name="category">The category of the setting</param>
        /// <param name="key">The key of the setting</param>
        /// <param name="value">The value of the setting</param>
        /// <exception cref="ConfigurationManagerException">
        ///     Configuration file is corrupt
        /// </exception>
        internal static void Set(string category, string key, string value)
        {
            if (ConfigFile.DocumentElement == null)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt");
            }

            XmlNode configKey;

            try
            {
                configKey =
                    ConfigFile.DocumentElement.SelectSingleNode("/BrickSkyLauncherConfiguration/" + category + "/" +
                                                                key);
            }
            catch (XPathException xPathException)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt", xPathException);
            }

            if (configKey == null)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt");
            }

            configKey.InnerText = value;
        }

        /// <summary>
        ///     Reads a setting from the config file
        /// </summary>
        /// <param name="category">The category of the setting</param>
        /// <param name="key">The key of the setting</param>
        /// <exception cref="ConfigurationManagerException">
        ///     Configuration file is corrupt
        /// </exception>
        /// <returns>
        ///     Value of the setting
        /// </returns>
        internal static string Get(string category, string key)
        {
            if (ConfigFile.DocumentElement == null)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt");
            }

            XmlNode configKey;

            try
            {
                configKey =
                    ConfigFile.DocumentElement.SelectSingleNode("/BrickSkyLauncherConfiguration/" + category + "/" +
                                                                key);
            }
            catch (XPathException xPathException)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt", xPathException);
            }

            if (configKey == null)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt");
            }

            return configKey.InnerText;
        }
    }
}