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
using System.Data.Entity;

namespace CSGOTelegramData
{
    /// <summary>
    /// Databasecontext for TelegramBotSuite
    /// </summary>
    public class CSGODataContext : DbContext
    {
        /// <summary>
        /// Constructor, including connection string name
        /// </summary>
        public CSGODataContext() :base("CSGOContext")
        {

        }

        /// <summary>
        /// Dataset for current online players
        /// </summary>
        public DbSet<PlayerInfoRecord> PlayerInfos { get; set; }
    }
}
