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

namespace CSGOTelegramData.Models
{
    /// <summary>
    /// Class which describes a Telegram Bot command
    /// </summary>
    public class BotCommand
    {
        /// <summary>
        /// The actual Telegram command, including the slash, which users need to type
        /// e.g. /start
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Short description of the results when calling this command. This text is shown in the command list when a Telegram user requests help
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Exact name of the BotHelper function which this command will be linked to. 
        /// That particular function will be searched for and called through reflection i.e. if the name is not correct, the function will never be called. 
        /// </summary>
        public string Function { get; set; }
    }
}
