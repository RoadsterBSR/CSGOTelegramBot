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
using CSGOTelegramData;
using QueryMaster.GameServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using CSGOTelegramData.Helpers;

namespace CSGOTelegramBot.Helpers
{
    /// <summary>
    /// Main Telegram Bot class
    /// Processes bot commands / user requests
    /// </summary>
    public class BotHelper
    {
        /// <summary>
        /// Telegram Api Bot instance 
        /// </summary>
        public Api Bot = new Api(Config.TelegramApiKey);

        /// <summary>
        /// Helper class instance for requesting server information from the CSGO server
        /// </summary>
        ServerHelper ServerHelper = new ServerHelper();

        /// <summary>
        /// Helper class instance for requesting user and clan information from the SteamWebapi and steamcommunity website
        /// </summary>
        SteamApiHelper SteamApiHelper = new SteamApiHelper();
        
        /// <summary>
        /// List with clan information
        /// <para>This is used for (runtime) caching of clanmembers information to reduce external requests to the steamcommunity website
        /// Clanmembers don't change every minute, Telegram Bot performance is more important then up-to-date clanmember information</para>
        /// </summary>
        List<Clan> ClanCache = new List<Clan>();

        /// <summary>
        /// Amount of hours before updating the Clan cache 
        /// </summary>
        int ClanUpdateHours = 1;

        /// <summary>
        /// Helper variable to determine wether to update the ClanCache 
        /// </summary>
        DateTime LastClanUpdate = DateTime.Now;

        /// <summary>
        /// Universal message describing a problem with requesting information from the gameserver
        /// <para>When a certain request towards the gameserver keeps failing and reached it maximum retries, this message will be added/sent in the Telegram message</para>
        /// </summary>
        String NoRequestError = "Unable to query gameserver";

        /// <summary>
        /// Request a Bot instance
        /// </summary>
        /// <returns>A new Bot instance which contains information in the form of a User object</returns>
        public async Task<User> GetBot()
        {
            return await Bot.GetMe();
        }

        /// <summary>
        /// Request Bot updates
        /// <para>This returns all updates (messages) from a Bot. 
        /// You need to implement, specify and manage up to where you have already processed the Bot's updates. That's possible by defining (and maintaining) the offset.</para>
        /// </summary>
        /// <param name="offset">The offset (id) from which point you want to receive updates</param>
        /// <returns>An array of Update objects </returns>
        public async Task<Update[]> GetUpdates(int offset)
        {
            return await Bot.GetUpdates(offset);
        }

        /// <summary>
        /// Send a Typing Action
        /// <para>This is primarily used to show in a Telegram client the message/status that this current bot is typing / 'thinking'
        /// This is used a lot in all the send functions, followed by a 1/2 second sleep call.
        /// Although this will delay the performance/reaction of the Bot with a tiny margin, which will probably be against your programming nature, 
        /// but it is actually recommended to use it to keep the user's Telegram experience intact. And also letting them know the bot has acknowledged their command and is busy with it</para>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <returns></returns>
        public async Task SendBotTypingAction(long chatId)
        {
            await Bot.SendChatAction(chatId, ChatAction.Typing);
        }

        /// <summary>
        /// Send a message
        /// <para>This function will send the message asynchronously. Below is the same function but sends it synchronously</para>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="message">Text of the message to be sent</param>
        /// <returns>On success, the sent Message object is returned.</returns>
        public async Task<Message> SendMessage(long chatId, string message)
        {
           return await Bot.SendTextMessage(chatId, message, false, 0, null, ParseMode.Html); 
        }

        /// <summary>
        /// Send a message
        /// <para>This function will send the message synchronously. Above is the same function but sends it asynchronously</para>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="message">Text of the message to be sent</param>
        /// <returns>On success, the sent Message object is returned.</returns>
        public void SendMessageNew(long chatId, string message)
        {
            Bot.SendTextMessage(chatId, message, false, 0, null, ParseMode.Html);
        }

        /// <summary>
        /// Process an user Telegram command into a class function call
        /// <para>
        /// In the BotCommand list is searched for a command which corresponds with the given user command. When found, the given class function is called.
        /// When a BotCommand is found, but not a corresponding class function a 'not yet implemented' message is send
        /// As stated in the Telegram development docs, the bot needs to be able to process standard commands (/example) and commands that are followed by its username. (/example@examplebot)</para>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="textMessage">The command send to the bot</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task ProcessCommand(long chatId, string textMessage, string userName)
        {
            if (textMessage.Contains("@" +Config.BotNickname))
            {
                textMessage = textMessage.Substring(0, textMessage.IndexOf("@"));
            }

            BotCommand currentCommand = Config.Commands.FirstOrDefault(comm => comm.Command.Equals(textMessage.ToLower()));

            if(currentCommand != null)
            {
                Type thisType = this.GetType();
                MethodInfo currentMethod = thisType.GetMethod(currentCommand.Function);
                if(currentMethod != null)
                {
                    ParameterInfo[] parameters = currentMethod.GetParameters();
                    currentMethod.Invoke(this, parameters.Length == 0 ? null : new object[] { chatId, userName});
                }
                else
                {
                    await SendMessage(chatId, "command not yet implemented");
                }
            }
        }

        /// <summary>
        /// Send Maintenance message
        /// <para>This function is just a sort of placeholder when in need of a general message in times of emergency
        /// i.e. you can change one or many BotCommands in the Config.cs to be redirected to this function when doing 
        /// maintenance and want to temporarily disable functions without putting the Telegram Bot offline
        /// (Registered Bot commands are shown in Telegram client regardless the state of this service application, 
        /// it will be confusing for users to issue commands but not getting any response and/or feedback)</para>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendMaintenance(long chatId, string userName)
        {
            await SendBotTypingAction(chatId);
            await Task.Delay(2000);
            string maintenanceMessage = $"<b>{Config.MainTitle}\r\n\r\nOffline</b>\r\nNextmap command is temporarily disabled because of unknown performance issues.";
            
            var t = await SendMessage(chatId, maintenanceMessage);
        }

        /// <summary>
        /// Send a start message
        /// <para>This function is required according to the Telegram Bot guidelines. Telegram clients will have interface shortcuts for these commands.
        /// Users are instructed to call this command ("/start") first after adding the Bot.
        /// You could use this function as starting point for registering new users, initiating custom user variables etc.</para></summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendStart(long chatId, string userName)
        {
            await SendBotTypingAction(chatId);
            await Task.Delay(2000);
            string startMessage = $"<b>{Config.MainTitle}\r\n\r\n</b>Welcome {userName}.\r\n\r\nThis is the bot for {Config.MainTitle}\r\n\r\nBot version {Config.BotVersion}";

            var t = await SendMessage(chatId, startMessage);
        }

        /// <summary>
        /// Send help message
        /// <para>This function is required according to the Telegram Bot guidelines. Telegram clients will have interface shortcuts for these commands.
        /// This function takes all BotCommand's from the Config and outputs them with the appropiate description letting the user know what command(s) they can use.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendHelp(long chatId, string userName)
        {
            await SendBotTypingAction(chatId);
            await Task.Delay(2000);
            string helpMessage = $"<b>{Config.MainTitle}\r\n\r\nBot Commands</b>\r\n\r\n";
            foreach(BotCommand currentCommand in Config.Commands)
            {
                helpMessage += $"<b>{currentCommand.Command}</b>\r\n";
                helpMessage += $"<i>{currentCommand.Description}</i>\r\n";
            }

            helpMessage += $"\r\n\r\n<code>Server owner\r\nMorell\r\n\r\nBot Version {Config.BotVersion}\r\nCreated by Roadster</code>";
            var t = await SendMessage(chatId, helpMessage);
        }

        /// <summary>
        /// Send message about an unknown command
        /// <para>This function is called when the user sends a command to the bot which isn't present in the BotCommand list in Config.cs</para>
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task SendUnknownCommand(long chatId, string userName)
        {
            await SendBotTypingAction(chatId);
            await Task.Delay(2000);
            string helpMessage = $"Hello {userName}. I don't understand what you're trying to do. Please type /help for instructions.";
            var t = await SendMessage(chatId, helpMessage);
        }

        /// <summary>
        /// Send amount of online players
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendCurrentPlayersCount(long chatId, string userName)
        {
            ServerInfo ServerInfo = ServerHelper.GetServerInfo();
            await SendBotTypingAction(chatId);
            await Task.Delay(1000);
            string playerMessage = $"<b>{Config.MainTitle}\r\n\r\nAmount of online players\r\n</b>{NoRequestError}";
            if (ServerInfo != null)
            {
                playerMessage = $"<b>{Config.MainTitle}\r\n\r\nAmount of online players\r\n</b>{ServerInfo.Players}/{ServerInfo.MaxPlayers}";
            }
            
            var t = await SendMessage(chatId, playerMessage);
        }

        /// <summary>
        /// Send the current online players
        /// <para>Current playerlist is retrieved from the database (which is updated by the CSGOTelegramSync app)
        /// It is possible to not use the sync app and get the players directly from the gameserver through this function.
        /// But when the gameserver is busy or doesn't have the specs to process it fast, the performance issues/feedback in 
        /// Telegram when requesting this information will be awful and confusing to the user.
        /// With the sync app you can be certain at this point to always have a playerlist available and deliver it fast to 
        /// the Telegram user.
        /// PS: If you're not using the sync app, it's advised to use a local player cache since retrieving the playerlist often needs a few retries</para></summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendCurrentPlayers(long chatId, string userName)
        {
            string playerMessage = $"<b>{Config.MainTitle}\r\n\r\nOnline players\r\n</b>";
            List<PlayerInfoRecord> playerList = new List<PlayerInfoRecord>();
            using(var mc = new CSGODataContext())
            {
                playerList = mc.PlayerInfos.ToList();
            }
            await SendBotTypingAction(chatId);
            await Task.Delay(1000);

            playerMessage = $"<b>{Config.MainTitle}</b>\r\n\r\n<b>Online players\r\n\r\n</b>";
            
            foreach (PlayerInfoRecord currentPlayer in playerList)
            {
                if (currentPlayer.Name.Trim() != "")
                {
                    string currentName = currentPlayer.Name;
                    string cleanName = WebUtility.HtmlEncode(currentName);
                    playerMessage += $"{cleanName}\r\n";
                }
            }

            playerMessage += $"\r\n\r\n<code>Updated {GetDifference(playerList[0].Date)} minute(s) ago</code>";

            var t = await SendMessage(chatId, playerMessage);
        }

        /// <summary>
        /// Send the current online clan members
        /// <para>Checking the clancache is always called first to determine if it needs to be updated.
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendCurrentClanPlayers(long chatId, string userName)
        {
            string playerMessage = $"<b>{Config.MainTitle}\r\n\r\nOnline clan members\r\n</b>";
            List<PlayerInfoRecord> playerList = new List<PlayerInfoRecord>();
            using (var mc = new CSGODataContext())
            {
                playerList = mc.PlayerInfos.ToList();
            }
            await SendBotTypingAction(chatId);
            await Task.Delay(1000);

            playerMessage = $"<b>{Config.MainTitle}</b>\r\n\r\n<b>Online clan members</b>\r\n\r\n";
            
            CheckClanCache();           

            if (ClanCache.Count > 0)
            {
                foreach(Clan currentClan in ClanCache)
                {
                    playerMessage += $"<b>{currentClan.Name}\r\n</b>";
                    List<string> currentClanPlayerNames = SteamApiHelper.GetPlayerSummaries(currentClan.Members);
                    List<String> currentOnlineClanMembers = currentClanPlayerNames.Where(clanmembername => playerList.Any(player => player.Name != "" && player.Name.ToLower().Equals(clanmembername.ToLower()))).ToList();
                    if(currentOnlineClanMembers.Count > 0)
                    {
                        foreach(String onlineClanMember in currentOnlineClanMembers)
                        {
                            playerMessage += $"{HtmlEncode(onlineClanMember)}\r\n";
                        }
                    }
                    else
                    {
                        playerMessage += "No players online\r\n";
                    }
                    playerMessage += "\r\n";
                }              
            }
            else
            {
                playerMessage += "No clan information available";
            }

            playerMessage += $"\r\n<code>Updated {GetDifference(playerList[0].Date)} minute(s) ago</code>";

            var t = await SendMessage(chatId, playerMessage);
        }

        /// <summary>
        /// Send the current online admins
        /// </summary>
        /// <para>Players can only be retrieved/identified from the gameserver with their steam displaynames.
        /// So we always need to get the current displaynames from the admins through the steamwebapi first 
        /// to be able to identify them from the gameserver playerlist</para>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendCurrentAdmins(long chatId, string userName)
        {
            string adminMessage = $"<b>{Config.MainTitle}\r\n\r\nAdmins\r\n</b>";
            List<PlayerInfoRecord> playerList = new List<PlayerInfoRecord>();
            using (var mc = new CSGODataContext())
            {
                playerList = mc.PlayerInfos.ToList();
            }
            await SendBotTypingAction(chatId);
            await Task.Delay(1000);

            adminMessage = $"<b>{Config.MainTitle}</b>\r\n\r\n<b>Online Admins\r\n</b>";
            
            List<String> AdminNames = SteamApiHelper.GetPlayerSummaries(Config.Admins);
            int adminCount = 0;
            foreach (PlayerInfoRecord currentPlayer in playerList)
            {
                string cleanName = currentPlayer.Name;
                if (cleanName != "")
                {
                    if (AdminNames.Any(member => cleanName.ToLower().Equals(member.ToLower())))
                    {
                        adminMessage += $"{HtmlEncode(cleanName)}\r\n";
                        adminCount++;
                    }
                }
            }

            if(adminCount == 0)
            {
                adminMessage += "No admins online";
            }

            adminMessage += $"\r\n\r\n<code>Updated {GetDifference(playerList[0].Date)} minute(s) ago</code>";

            var t = await SendMessage(chatId, adminMessage);
        }

        /// <summary>
        /// Send general information about the server
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendServerInfo(long chatId, string userName)
        {
            ServerInfo ServerInfo = ServerHelper.GetServerInfo();
            
            await SendBotTypingAction(chatId);
            await Task.Delay(1000);
            
            string serverMessage = $"<b>{Config.MainTitle}\r\n\r\nServer Info\r\n</b>";

            if (ServerInfo != null)
            {
                serverMessage += "\r\nName";
                serverMessage += $"\r\n{ServerInfo.Name}";

                serverMessage += "\r\n\r\nAddress";
                serverMessage += $"\r\n{ServerInfo.Address}";

                serverMessage += "\r\n\r\nDescription";
                serverMessage += $"\r\n{ServerInfo.Description}";

                serverMessage += "\r\n\r\nGame Version";
                serverMessage += $"\r\n{ServerInfo.GameVersion}";

                serverMessage += "\r\n\r\nMaximum Players";
                serverMessage += $"\r\n{ServerInfo.MaxPlayers}";
            }
            else
            {
                serverMessage += NoRequestError;
            }

            var t = await SendMessage(chatId, serverMessage);
        }

        /// <summary>
        /// Send the current map
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendCurrentMap(long chatId, string userName)
        {
            ServerInfo ServerInfo = ServerHelper.GetServerInfo();

            await SendBotTypingAction(chatId);
            await Task.Delay(1000);
            
            string mapMessage = $"<b>{Config.MainTitle}\r\n\r\nCurrent Map\r\n</b>";

            if (ServerInfo != null)
            {
                mapMessage += ServerInfo.Map;
            }
            else
            {
                mapMessage += NoRequestError;
            }
            
            var t = await SendMessage(chatId, mapMessage);
        }

        /// <summary>
        /// Send the next map
        /// <para>Be aware that this shows the next map in the gameserver maprotation configuration
        /// If a server has a mapvote plugin, the next map is often determined at the end of a match.
        /// So the next map will probably differ with the one shown through this function until after the vote. 
        /// After the vote the mapvote plugin changes the maprotation and this function will show the correct map.
        /// In those situations it can be confusing to a Telegram user. So rather explain the functionality or don't use it at all seems best.</para></summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userName">Username for the target user</param>
        /// <returns></returns>
        public async Task SendNextMap(long chatId, string userName)
        {
            string mapMessage = $"<b>{Config.MainTitle}\r\n\r\nNext Map\r\n</b>";
            List<Rule> rulesList = ServerHelper.GetRules(this, chatId, mapMessage);

            await SendBotTypingAction(chatId);
            await Task.Delay(1000);

            string nextMap = "";

            if (rulesList.Count > 0)
            {
                Rule currentRule = rulesList.FirstOrDefault(rule => rule.Name == "sm_nextmap");
                if(currentRule != null)
                {
                    nextMap = currentRule.Value;
                    mapMessage += nextMap;
                }
                else
                {
                    mapMessage += "Unable to retrieve next map atm";
                }
            }
            else
            {
                mapMessage += "Unable to retrieve next map atm";
            }
                        
            var t = await SendMessage(chatId, mapMessage);
        }

        /// <summary>
        /// Encodes text to HTML
        /// <para>Telegram uses (a sort of) html to display their messages. 
        /// Thus playernames need to be encoded to prevent unexpected behaviour and/or outputs.
        /// You can also use other .Net libraries to do the encoding (as shown in SendCurrentPlayers). 
        /// It's more about the Telegram service accepting the given message to be valid syntax instead of checking if it's completely HTML compliant.
        /// Because of that, I just created this function to only change the characters that the Telegram service will stumble upon</para>
        /// </summary>
        /// <param name="text">The text that needs to be encoded</param>
        /// <returns></returns>
        public static string HtmlEncode(string text)
        {
            if (text == null)
                return null;

            StringBuilder sb = new StringBuilder(text.Length);

            int len = text.Length;
            for (int i = 0; i < len; i++)
            {
                switch (text[i])
                {
                    case '<':
                        sb.Append("&lt;");
                        break;
                    case '>':
                        sb.Append("&gt;");
                        break;
                    case '"':
                        sb.Append("&quot;");
                        break;
                    case '&':
                        sb.Append("&amp;");
                        break;
                    default:
                        if (text[i] > 159)
                        {
                            sb.Append("&#");
                            sb.Append(((int)text[i]).ToString(CultureInfo.InvariantCulture));
                            sb.Append(";");
                        }
                        else
                            sb.Append(text[i]);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Updates the local clan cache
        /// <para>This function needs to be rewritten to use a clan list instead of handling each clan seperately/explicit.
        /// NOTE: Be careful with using clans with a lot of members. For each clan, every single member needs to be retrieved from the steamwebapi.
        /// When using clans with hundreds if not thousands of members, this will decrease performance drastically!
        /// With that in mind, it may be a good solution to lookup/store claninformation in de database and use a seperate process (CSGOTelegramSync) 
        /// to process the needed information.</para>
        /// </summary>
        public void CheckClanCache()
        {
            TimeSpan clanCacheDifference = DateTime.Now - LastClanUpdate;
            if (clanCacheDifference.Hours > ClanUpdateHours || ClanCache.Count < 1)
            {
                List<Clan> tempClanCache = new List<Clan>();

                foreach(Clan currentClan in Config.Clans)
                {
                    Clan thisClan  = SteamApiHelper.GetClan(currentClan);
                    tempClanCache.Add(thisClan);
                }

                if (tempClanCache.Count > 0)
                {
                    ClanCache = tempClanCache;
                }
            }
        }

        /// <summary>
        /// Helper for computing difference in time (in minutes)
        /// </summary>
        /// <param name="moment">Datetime from when to calculate difference until current Datetime</param>
        /// <returns></returns>
        public int GetDifference(DateTime moment)
        {
            TimeSpan difference = new TimeSpan(999999);
            difference = DateTime.Now - moment;
            int realDiff = difference.Minutes;
            if (difference.Hours > 0)
            {
                realDiff = realDiff + (difference.Hours * 60);
            }
            return realDiff;
        }
  
    }

   
}
