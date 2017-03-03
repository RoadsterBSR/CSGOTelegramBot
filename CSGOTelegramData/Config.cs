/*
 *  This file is part of CSGOTelegramBot.
 *
 *  CSGOTelegramBot is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  CSGOTelegramBot is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 *  GNU General Public License for more details.
 *
 *  You are not authorized to remove or change this license notice and/or
 *  contact information from the (original) author(s).
 *
 *  You should have received a copy of the GNU General Public License
 *  along with CSGOTelegramBot.  If not, see<http://www.gnu.org/licenses/>.
 *
 *  CSGOTelegramBot
 *  Copyright (C) 2017 Patrick Thomissen (RoadsterBSR)
 *  Email: RoadsterBSR@gmail.com
 *  Steam: http://steamcommunity.com/id/RoadsterBSR/
*/

using CSGOTelegramData.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace CSGOTelegramData
{
    /// <summary>
    /// Configuration class
    /// <para>General settings for the Telegram bot, Telegram Sync and CSGO server</para>
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// The version of the Telegram bot
        /// <para>Only used for display purposes in telegram messages</para>
        /// </summary>
        public static string BotVersion = "1.0";

        /// <summary>
        /// The title/name of the bot 'service'
        /// <para>Most of the bot telegram messages start with this, to create some uniformity</para>
        /// </summary>
        public static string MainTitle = "My CSGO Telegram Bot";

        /// <summary>
        /// The precise username which is registered to the bot in Telegram
        /// <para>It has to be completely identical since this is used for filtering commands when multiple bots are used in one group chat</para>
        /// </summary>
        public static string BotNickname = "MyBot";

        /// <summary>
        /// The Telegram authorization token which is given when creating the bot on Telegram
        /// </summary>
        public static string TelegramApiKey = "MyTelegramBotApiKey";

        /// <summary>
        /// The Webapi key for authorising SteamWebApi access
        /// <see href="https://steamcommunity.com/dev/apikey"/> 
        /// </summary>
        public static string SteamWebapiKey = "MySteamWebapiKey";

        /// <summary>
        /// Full URL to SteamWebApi
        /// </summary>
        public static string SteamWebapiBaseUrl = "http://api.steampowered.com/";

        /// <summary>
        /// A list of bot commands the bot can interpret
        /// <para>Every new bot function which can be called by Telegram users needs to be declared in this list
        /// This is also used to sent a (automated) list of available bot commands as a help message to the user
        /// See BotCommand class for more information about property use</para>
        /// </summary>
        public static List<BotCommand> Commands = new List<BotCommand> {
            new BotCommand { Command = "/start", Description = "List available commands", Function = "SendStart" },
            new BotCommand { Command = "/help", Description = "List available commands", Function = "SendHelp" },
            new BotCommand { Command = "/map", Description = "Shows current map", Function = "SendCurrentMap" },
            new BotCommand { Command = "/nextmap", Description = "Shows next map", Function = "SendNextMap" },            
            new BotCommand { Command = "/playercount", Description = "Shows amount of online players", Function = "SendCurrentPlayersCount" },
            new BotCommand { Command = "/players", Description = "List online players", Function = "SendCurrentPlayers" },
            new BotCommand { Command = "/admins", Description = "List online admins", Function = "SendCurrentAdmins" },
            new BotCommand { Command = "/clans", Description = "List online clan members", Function = "SendCurrentClanPlayers" },
            new BotCommand { Command = "/info", Description = "Server information", Function = "SendServerInfo" }
        };

        /// <summary>
        /// IP Address of the CSGO gameserver 
        /// </summary>
        public static string ServerIP = "xxx.xxx.xxx.xxx";

        /// <summary>
        /// Network port of the CSGO gameserver 
        /// </summary>
        public static ushort ServerPort = 0;

        /// <summary>
        /// List of Steam ID's (steamID64) which represent the Admins on the CSGO gameserver
        /// <para>The SteamApiHelper class is used to retrieve extra player information belonging to these Steam ID's.
        /// String type is used instead of official steamID64 type to </para>
        /// </summary>
        public static List<string> Admins = new List<string>()
        {
            "12345678901234567",
        };

        /// <summary>
        /// List of Clans which are active on the CSGO gameserver
        /// <para>
        /// Url -> The complete url to the steamcommunity page of the clan. This url is used to grab the current members of a clan
        /// Name -> The name / tag of the clan, this will be displayed as clan header in the Telegram message
        /// NOTE: DON'T ASSIGN OTHER PROPERTIES APART FROM THE ABOVE TWO. THE REST WILL BE ASSIGNED AUTOMATICALLY BY REQUESTING THE STEAMWEBAPI
        /// </para>
        /// </summary> 
        public static List<Clan> Clans = new List<Clan>()
        {
            new Clan()
            {
                Url = "http://steamcommunity.com/groups/ZeIES",
                Name = "๖IΣŠ"
            },
            new Clan()
            {
                Url = "http://steamcommunity.com/groups/TZEG",
                Name = "TZΣG"
            },
            new Clan()
            {
                Url = "http://steamcommunity.com/groups/XtraordinaryEscapers",
                Name = "ҲΘĘ"
            },
            new Clan()
            {
                Url = "http://steamcommunity.com/groups/therelaxed",
                Name = "TRŁG"
            },
        };

        /// <summary>
        /// Filename of the logfile for the Bot
        /// </summary>
        public static string BotLogFilename = "TelegramBot Log.txt";

        /// <summary>
        /// Filename of the logfile for the sync app
        /// </summary>
        public static string SyncLogFilename = "TelegramBot Sync Log.txt";

        /// <summary>
        /// General Telegram Bot Logging function
        /// <para>It uses simple command line logging atm. 
        /// But you can use or rewrite this entrypoint to implement proper loglevel and / or tracing and such.</para>
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Log(string logMessage)
        {
            using (StreamWriter w = File.AppendText(BotLogFilename))
            {
                Console.WriteLine("[" + DateTime.Now + "] " + logMessage);
                w.WriteLine("[" + DateTime.Now + "] " + logMessage);
            }
        }

        /// <summary>
        /// General Telegram Sync Logging function
        /// </summary>
        /// <param name="logMessage"></param>
        public static void LogSync(string logMessage)
        {
            using (StreamWriter w = File.AppendText(SyncLogFilename))
            {
                Console.WriteLine("[" + DateTime.Now + "] " + logMessage);
                w.WriteLine("[" + DateTime.Now + "] " + logMessage);
            }
        }
    }
}
