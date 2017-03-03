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
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System.Reflection;
using CSGOTelegramBot.Helpers;
using CSGOTelegramData;

namespace CSGOTelegramBot
{
    /// <summary>
    /// Main Application Class 
    /// </summary>
    public class Program
    {        
        /// <summary>
        /// Application entry point
        /// <para>This run's the main Telegram Bot Task and restarts the application when exceptions occur.</para>
        /// </summary>
        /// <param name="args">Not used</param>
        static void Main(string[] args)
        {
            try
            {
                Run().Wait();
            }
            catch (Exception ex)
            {
                Config.Log("Process stopped!");
                Config.Log($"Error Message: {ex.Message}");
                Restart();
            }
        }

        /// <summary>
        /// Main Telegram Bot Task
        /// <para>Telegram messages are requested from the Telegram service and processed accordingly.
        /// Messages are filtered on text-messages only and passed to the BotHelper class for further dispatching</para>
        /// </summary>
        /// <returns></returns>
        static async Task Run()
        {
            BotHelper BotHelper = new BotHelper();            
            User currentBot = await BotHelper.GetBot();
            Config.Log($"Bot: {currentBot.Username} - Status Online...");

            var offset = 0;

            while (true)
            {
                try
                {
                    var updates = await BotHelper.GetUpdates(offset);
                    foreach (var update in updates)
                    {
                        if (update.Message.Type == MessageType.TextMessage)
                        {
                            string username = update.Message.Chat.FirstName;
                            if (update.Message.Chat.Type == ChatType.Group)
                            {
                                username = update.Message.From.FirstName;                                
                                Config.Log($"Group: {update.Message.Chat.Title} User: {username}  Message: {update.Message.Text}");
                            }
                            else
                            {
                                Config.Log($"User: {username} Message: {update.Message.Text}");
                            }

                            await BotHelper.ProcessCommand(update.Message.Chat.Id, update.Message.Text, username);
                        }
                        offset = update.Id + 1;
                    }

                    await Task.Delay(1000);       
                }
                catch(Exception ex)
                {
                    Config.Log("Process stopped!");
                    Config.Log($"Error Message: {ex.Message}");
                    Restart();
                }
               
            }
        }

        /// <summary>
        /// Restarts the application
        /// <para>I chose for this internal solution instead of using an external process/application 
        /// because there are several services we use in this application which are prone to failure and with this method the application is back online in a second.
        /// e.g. Telegram api service resets at least ones a night, SteamApi has lots of downtime, CSGO server's are often restarted etc.
        /// Of course best practice would be to implement proper error handling of all given scenarios. But atm the main thread at least catches all these exceptions.
        /// </para>
        /// </summary>
        static void Restart()
        {
            Config.Log("*** Restarting application ***");
            System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);            
            Environment.Exit(0);
        }

    }
}
