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
// Last Modified:       06.11.2015 (18:33)

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
    internal sealed class ConfigurationManager
    {
        /// <summary>
        ///     Loaded configuration file
        /// </summary>
        private readonly XmlDocument _configFile = new XmlDocument();

        /// <summary>
        ///     Path to the configuration file (e.g. C:\Users\CurrentUser\AppData\Roaming\Brick
        ///     Sky\Launcher\Config\Launcher.brickskycfg)
        /// </summary>
        private readonly string _pathToConfigFile =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
            "\\Brick Sky\\Launcher\\Config\\Launcher.brickskycfg";

        /// <summary>
        ///     Starts the configuration manager (see <see cref="InitConfigurationManager" />)
        /// </summary>
        /// <exception cref="LoggerException">Logger was not able to log a configuration exception.</exception>
        internal ConfigurationManager()
        {
            try
            {
                InitConfigurationManager();
            }
            catch (ConfigurationManagerException configurationManagerException)
            {
                BslApp.ApplicationLogger.AddLogEntry(Logger.LogTypes.Fatal, "ConfigurationManager",
                    configurationManagerException.Message);
            }
        }

        /// <summary>
        ///     Starts the configuration manager and creates the config file if it is missing
        /// </summary>
        /// <exception cref="ConfigurationManagerException"></exception>
        private void InitConfigurationManager()
        {
            var path = Path.GetDirectoryName(_pathToConfigFile);

            if (string.IsNullOrEmpty(path))
            {
                throw new ConfigurationManagerException("Path to configuration is null or empty.");
            }

            Directory.CreateDirectory(path);

            if (!File.Exists(_pathToConfigFile))
            {
                using (
                    var stream =
                        Assembly.GetExecutingAssembly()
                            .GetManifestResourceStream("BrickSkyLauncher.Resources.Launcher.brickskycfg"))
                {
                    using (var file = new FileStream(_pathToConfigFile, FileMode.Create, FileAccess.Write))
                    {
                        stream?.CopyTo(file);
                    }
                }
            }

            _configFile.Load(_pathToConfigFile);
        }

        /// <summary>
        ///     Sets a setting in the config file
        /// </summary>
        /// <param name="category">The category of the setting</param>
        /// <param name="key">The key of the setting</param>
        /// <param name="value">The value of the setting</param>
        /// <exception cref="ConfigurationManagerException">Configuration file is corrupt</exception>
        internal void Set(string category, string key, string value)
        {
            if (_configFile.DocumentElement == null)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt");
            }

            XmlNode configKey;

            try
            {
                configKey =
                    _configFile.DocumentElement.SelectSingleNode("/BrickSkyLauncherConfiguration/" + category + "/" +
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
        internal string Get(string category, string key)
        {
            if (_configFile.DocumentElement == null)
            {
                throw new ConfigurationManagerException("Configuration file is corrupt");
            }

            XmlNode configKey;

            try
            {
                configKey =
                    _configFile.DocumentElement.SelectSingleNode("/BrickSkyLauncherConfiguration/" + category + "/" +
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