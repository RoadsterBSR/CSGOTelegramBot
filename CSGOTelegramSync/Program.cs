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
using System.Threading.Tasks;
using System.Reflection;
using CSGOTelegramSync.Helpers;
using QueryMaster.GameServer;
using CSGOTelegramData.Models;
using CSGOTelegramData;

namespace CSGOTelegramSync
{
    /// <summary>
    /// Main Application Class 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Interval in milliseconds for updating playerlist database
        /// </summary>
        private static int RefreshDelay = 300000;

        /// <summary>
        /// Number of tries to make a request to the server until successful attempt
        /// </summary>
        private static int ForceRetries = 40;      

        /// <summary>
        /// Application entry point
        /// <para>This run's the main Telegram Sync Task and restarts the application when exceptions occur.</para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Run().Wait();
            }
            catch (Exception ex)
            {
                Config.LogSync("FATAL MAIN ERROR!");
                Config.LogSync($"Error Message: {ex.Message}");
                Restart();
            }
        }

        /// <summary>
        /// Main Telegram Sync Task
        /// <para>Current playerlist is requested from the server and added to the database</para>
        /// </summary>
        /// <returns></returns>
        static async Task Run()
        {
            ServerHelper serverHelper = new ServerHelper();
            Config.LogSync("CSGO Telegram Sync Online...");

            while (true)
            {
                try
                {
                    Config.LogSync("*** START playerlist cache refresh run ***");
                    DateTime StartTime = DateTime.Now;

                    List<PlayerInfo> playerList = serverHelper.GetPlayersForced(ForceRetries);

                    if (playerList.Count > 0)
                    {
                        using (var mc = new CSGODataContext())
                        {
                            List<PlayerInfoRecord> currentPlayerRecords = mc.PlayerInfos.ToList();
                            mc.PlayerInfos.RemoveRange(currentPlayerRecords);
                            mc.SaveChanges();

                            foreach (PlayerInfo currentPlayer in playerList)
                            {           
                                if(currentPlayer.Name.Length > 0)
                                {
                                    PlayerInfoRecord tempPlayerRecord = new PlayerInfoRecord { Name = currentPlayer.Name, Date = DateTime.Now, Time = currentPlayer.Time};
                                    mc.PlayerInfos.Add(tempPlayerRecord);
                                }
                                
                            }

                            mc.SaveChanges();

                            Config.LogSync($"{playerList.Count} players added to database");
                            Config.LogSync("*** END playerlist cache refresh run ***");
                        }
                    }

                    DateTime EndTime = DateTime.Now;

                    TimeSpan span = EndTime - StartTime;
                    int timeRunning = (int)span.TotalMilliseconds;

                    int newDelay = 0;
                    if(timeRunning > RefreshDelay)
                    {
                        newDelay = 1000;
                    }
                    else
                    {
                        newDelay = RefreshDelay - timeRunning;
                    }

                    Config.LogSync($"*** SLEEP {newDelay} milliseconds ***");
                    await Task.Delay(newDelay);
       
                }
                catch(Exception ex)
                {
                    Config.LogSync("FATAL ERROR!");
                    Config.LogSync($"Error Message: {ex.Message}");
                    Restart();
                }
               
            }
        }

        /// <summary>
        /// Restarts the application
        /// </summary>
        static void Restart()
        {
            Config.LogSync("*** Restarting application ***");
            System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);            
            Environment.Exit(0);
        }

    }
}
