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
    /// Player object as used in SteamWebapi
    /// For more information:
    /// <see href="https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.28v0002.29"/></summary>
    public class Player
    {
        public String SteamId { get; set; }
        public int CommunityVisibilityState { get; set; }
        public int ProfileState { get; set; }
        public String PersonaName { get; set; }
        public int LastLogoff { get; set; }
        public int CommentPermission { get; set; }
        public String ProfileUrl { get; set; }
        public String Avatar { get; set; }
        public String AvatarMedium { get; set; }
        public String AvatarFull { get; set; }
        public int PersonaState { get; set; }
        public String RealName { get; set; }
        public String PrimaryClanId { get; set; }
        public int TimeCreated { get; set; }
        public int PersonaStateFlags { get; set; }
        private string _locCountryCode;
        public String LocCountryCode
        {
            get
            {
                return _locCountryCode;
            }
            set
            {
                _locCountryCode = value;
                CountryName = Countries.GetCountryName(_locCountryCode);
            }
        }


        private String _countryName;
        public String CountryName
        {
            get { return _countryName; }
            set
            {
                if (_countryName != value)
                {
                    _countryName = value;
                }
            }
        }
    }
}
