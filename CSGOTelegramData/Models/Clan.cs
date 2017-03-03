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

namespace CSGOTelegramData.Models
{
    /// <summary>
    /// Clan object
    /// </summary>
    public class Clan
    {
        /// <summary>
        /// Clantag as registered on Steam
        /// </summary>
        public String Tag { get; set; }

        /// <summary>
        /// Displayname
        /// <para>Name of the clan as displayed in Telegram Bot</para>
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Full URL to Steam avatar icon
        /// </summary>
        public String Avatar { get; set; }

        /// <summary>
        /// Full URL to steamcommunity site of the clan
        /// </summary>
        public String Url { get; set; }

        /// <summary>
        /// Members of the clan in steamID64 format
        /// </summary>
        public List<String> Members { get; set; }
    }
}
