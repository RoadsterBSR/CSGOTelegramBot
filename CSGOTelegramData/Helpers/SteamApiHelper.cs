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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace CSGOTelegramData.Helpers
{
    /// <summary>
    /// Helper class for retrieving information from the SteamWebapi
    /// </summary>
    public class SteamApiHelper
    {
        /// <summary>
        ///  Entry point url for the SteamWebapi
        /// </summary>
        public String ApiBaseURL { get; set; } = Config.SteamWebapiBaseUrl;

        /// <summary>
        /// Api key for the SteamWebapi
        /// <para>You need a personal steam api key which you can register for free at the Steam website.
        /// <see href="https://steamcommunity.com/dev/apikey"/></para>
        /// </summary>
        public String ApiKey { get; set; } = Config.SteamWebapiKey;

        /// <summary>
        /// Constructor
        /// </summary>
        public SteamApiHelper()
        {
                     
        }

        /// <summary>
        /// Get player information
        /// <para>To increase performance this function can retrieve a complete playerlist in one webapi call. 
        /// When you need information about more then one player, always try to bundle them in as minimal requests as possible.
        /// Apart for performance sake, the Steam Webapi has caps for amount of requests done per minute</para></summary>
        /// <param name="userSteamIDs">A list of steam identifiers (steamID64)</param>
        /// <returns></returns>
        public List<string> GetPlayerSummaries(List<String> userSteamIDs)
        {
            string steamIds = string.Join(",", userSteamIDs);
            string requestURL = $"{ApiBaseURL}ISteamUser/GetPlayerSummaries/v0002/?key={ApiKey}&steamids={steamIds}";
            try
            {
                HttpWebRequest request = WebRequest.Create(requestURL) as HttpWebRequest;
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
                string JsonResult = sr.ReadToEnd();
                JToken resultResponse = JObject.Parse(JsonResult);
                JToken players = resultResponse["response"]["players"];
                if (players != null)
                {
                    List<Player> playerList = JsonConvert.DeserializeObject<List<Player>>(players.ToString());

                    List<string> playerNamesList = new List<string>();
                    foreach (Player currentPlayer in playerList)
                    {
                        playerNamesList.Add(currentPlayer.PersonaName);
                    }

                    return playerNamesList;
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Get clan(members)
        /// </summary>
        /// <param name="ClanUrl">Full url to steamcommunity clan page</param>
        /// <returns></returns>
        public Clan GetClan(Clan currentClan)
        {
            currentClan.Members = new List<string>();
            string requestURL = $"{currentClan.Url}/memberslistxml/?xml=1";
            try
            {
                XmlDocument doc1 = new XmlDocument();
                doc1.Load(requestURL);
                XmlElement root = doc1.DocumentElement;

                XmlNodeList avatarNode = root.SelectNodes("/memberList/groupDetails/avatarIcon");
                if (avatarNode.Count > 0)
                {
                    currentClan.Avatar = avatarNode[0].InnerText;
                }

                XmlNodeList memberNodes = root.SelectNodes("/memberList/members/steamID64");
                foreach (XmlNode node in memberNodes)
                {
                    currentClan.Members.Add(node.InnerText);
                }

                return currentClan;
            }
            catch
            {
                return currentClan;
            }
        }

    }
}
