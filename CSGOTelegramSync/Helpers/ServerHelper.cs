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

using System;
using System.Collections.Generic;
using System.Linq;
using QueryMaster;
using QueryMaster.GameServer;
using CSGOTelegramData;

namespace CSGOTelegramSync.Helpers
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
        /// Number of tries to make a request to the server until successful attempt
        /// </summary>
        private int attemptRetries = 5;

        /// <summary>
        /// Whether the gameserver requests should throw an exception on failure
        /// </summary>
        static bool throwExceptions = true;

        /// <summary>
        /// QueryMaster Server instance which is used to make server requests
        /// </summary>
        Server CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort,false, serverSendTimeOut, serverReceiveTimeOut, requestAmount, throwExceptions);

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
        /// <param name="forceRetries">Amount of tries that should be attempted to retrieve playerlist from CSGO gameserver</param>
        /// <returns></returns>
        public List<PlayerInfo> GetPlayersForced(int forceRetries)
        {
            Config.Log("GetPlayersForced(): Called");
            QueryMasterCollection<PlayerInfo> playerList = new QueryMasterCollection<PlayerInfo>(new List<PlayerInfo>());

            CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, serverSendTimeOut, serverReceiveTimeOut, requestAmount, throwExceptions);

            int retries = 0;

            int currentServerSendTimeOut = serverSendTimeOut;
            int currentServerReceiveTimeOut = serverReceiveTimeOut;
            int currentRequestAmount = requestAmount;

            while (playerList.Count < 1 && retries < forceRetries)
            {
                try
                {
                    playerList = CurrentServer.GetPlayers();
                }
                catch(Exception ex)
                {
                    Config.Log("Exception in request:");
                    Config.Log(ex.Message);
                    Config.Log("");
                }
                
                if (playerList == null)
                {
                    playerList = new QueryMasterCollection<PlayerInfo>(new List<PlayerInfo>());
                }

                CurrentServer.Dispose();
                CurrentServer = ServerQuery.GetServerInstance(EngineType.Source, Config.ServerIP, Config.ServerPort, false, currentServerSendTimeOut, currentServerReceiveTimeOut, currentRequestAmount, throwExceptions);

                retries++;
                Config.Log($"GetPlayersForced(): List count: {playerList.Count}  Try: {retries}   RequestAmount: {currentRequestAmount}  SendTimeOut: {currentServerSendTimeOut}  ReceiveTimeOut: {currentServerReceiveTimeOut}");
            }

            List<PlayerInfo> sortedList = new List<PlayerInfo>();
            if (playerList.Count > 1)
            {
                Config.Log("GetPlayers(): Sorting list...SUCCESS");
                sortedList = playerList.ToList();
                sortedList = sortedList.OrderBy(playerInfo => playerInfo.Name).ToList();
            }
            else
            {
                Config.Log("GetPlayers(): FAILED");
            }

            Config.Log($"GetPlayersForced(): DONE  {retries} retries needed");

            return sortedList;
        }

    }
}
