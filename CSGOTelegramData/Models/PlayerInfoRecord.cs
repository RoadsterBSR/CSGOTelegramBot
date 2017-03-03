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

namespace CSGOTelegramData.Models
{
    /// <summary>
    /// Player object
    /// <para>Used for storing current online players in database</para> 
    /// </summary>
    public class PlayerInfoRecord
    {
        /// <summary>
        /// Id for use in local database (CSGOTelegramSync)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Displayname as retrieved from gameserver
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current playing time on server
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Date created record for use in local database (CSGOTelegramSync)
        /// </summary>
        public DateTime Date { get; set; }
    }
}
