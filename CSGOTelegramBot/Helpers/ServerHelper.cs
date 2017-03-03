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

using System.Collections.Generic;
using System.Linq;
using QueryMaster;
using QueryMaster.GameServer;
using CSGOTelegramData;

namespace CSGOTelegramBot.Helpers
{
    /// <summary>
    /// Helper class for requesting information from the CSGO server
    /// For more information regarding return objects:
    /// <see href="https://developer.valvesoftware.com/wiki/Server_queries"/>
    /// </summary>
    public class ServerHelper
    {
        /// <summary>
        /// Timeout in milliseconds when sending a server request
        /// </summary>
        static int serverSendTimeOut = 2000;

        /// <summary>
        /// Timeout in milliseconds when awaiting a server request
        /// </summary>
        static int serverReceiveTimeOut = 2000;

        /// <summary>
        /// Number of times the ServerQuery library should make a request to the server on failure
        /// </summary>
        static int requestAmount = 2;

        /// <summary>
        /// Number of times this class should (try to) make a request to the server on failure
        /// </summary>
        private int attemptRetries = 5;

        /// <summary>
        /// QueryMaster Server instance which is used to make server requests
        /// </summary>
        Server CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort,false, serverSendTimeOut, serverReceiveTimeOut, requestAmount, false);

        /// <summary>
        /// Constructor
        /// </summary>
        public ServerHelper()
        {

        }

        /// <summary>
        /// Request current online players from the gameserver
        /// <para>This includes players who are still in process of joining the server but aren't actually in the server yet e.g. loading the map</para>
        /// </summary>
        /// <param name="currentBotHelper">Instance of the BotHelper to be able to send inbetween Telegram messages when failures occur</param>
        /// <param name="chatId">Id of the current chat request to be able to send inbetween Telegram messages when failures occur</param>
        /// <param name="playerMessage">Id of the current chat request to be able to send inbetween Telegram messages when failures occur</param>
        /// <returns></returns>
        public List<PlayerInfo> GetPlayers(BotHelper currentBotHelper, long chatId, string playerMessage)
        {
            Config.Log("GetPlayers(): Called");
            int retries = 0;
            QueryMasterCollection<PlayerInfo> playerList = new QueryMasterCollection<PlayerInfo>(new List<PlayerInfo>());
            while (playerList.Count < 1 && retries < attemptRetries)
            {
                playerList = CurrentServer.GetPlayers();
                if (playerList == null)
                {
                    playerList = new QueryMasterCollection<PlayerInfo>(new List<PlayerInfo>());
                }

                CurrentServer.Dispose();

                if(retries == 1 && playerList.Count < 1)
                {
                    currentBotHelper.SendMessageNew(chatId, $"{playerMessage} Unable to request playerlist\r\n\r\nRetrying...please wait\r\n");
                    CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, serverSendTimeOut + 1000, serverReceiveTimeOut + 1000, requestAmount + 2, false);
                }
                else
                {
                    CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, serverSendTimeOut, serverReceiveTimeOut, requestAmount, false);
                }

                retries++;
                Config.Log($"GetPlayers(): List count: {playerList.Count}  Try {retries}");
            }

            List<PlayerInfo> sortedList = new List<PlayerInfo>();
            if (playerList.Count > 1)
            {
                Config.Log("GetPlayers(): Sorting list...");
                sortedList = playerList.ToList();
                sortedList = sortedList.OrderBy(playerInfo => playerInfo.Name).ToList();
            }

            Config.Log($"GetPlayers(): Done. {retries} retries needed");

            return sortedList;
        }

      
        /// <summary>
        /// Request general information regarding the gameserver
        /// </summary>
        /// <returns></returns>
        public ServerInfo GetServerInfo()
        {
            return CurrentServer.GetInfo();
        }


        /// <summary>
        /// Request current ruleset from the gameserver
        /// <para>Be aware that this can be an instensive request and in general could take a few tries. 
        /// It's an extensive set, atm only used for finding the next map. You could use it to provide more information/functionality to the Telegram user</para></summary>
        /// <param name="currentBotHelper">Instance of the BotHelper to be able to send inbetween Telegram messages when failures occur</param>
        /// <param name="chatId">Id of the current chat request to be able to send inbetween Telegram messages when failures occur</param>
        /// <param name="playerMessage">Id of the current chat request to be able to send inbetween Telegram messages when failures occur</param>
        /// <returns></returns>
        public List<Rule> GetRules(BotHelper currentBotHelper, long chatId, string mapMessage)
        {
            int retries = 0;

            QueryMasterCollection<Rule> rulesCollection = new QueryMasterCollection<Rule>(new List<Rule>());
            while (rulesCollection.Count < 1 && retries < attemptRetries)
            {
                rulesCollection = CurrentServer.GetRules();
                if (rulesCollection == null)
                {
                    rulesCollection = new QueryMasterCollection<Rule>(new List<Rule>());
                }

                CurrentServer.Dispose();
                CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, serverSendTimeOut, serverReceiveTimeOut, requestAmount, false);

                if (retries == 1 && rulesCollection.Count < 1)
                {
                    currentBotHelper.SendMessageNew(chatId, mapMessage + "Unable to request rulesset\r\n\r\nRetrying...please wait\r\n");
                    CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, serverSendTimeOut + 1000, serverReceiveTimeOut + 1000, requestAmount + 2, false);
                }
                else
                {
                    CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, serverSendTimeOut, serverReceiveTimeOut, requestAmount, false);
                }

                retries++;
                Config.Log($"GetRules(): List count: {rulesCollection.Count} Try {retries}");
            }

            List<Rule> rulesList = new List<Rule>();
            if (rulesCollection.Count > 1)
            {
                Config.Log("GetRules(): Sorting list...");
                rulesList = rulesCollection.ToList();
            }

            Config.Log($"GetRules(): Done. {retries} retries needed");

            return rulesList;

        }       

    }
}
