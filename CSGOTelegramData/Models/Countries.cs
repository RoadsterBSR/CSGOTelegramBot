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

namespace CSGOTelegramData.Models
{
    /// <summary>
    /// Helper class for managing country(codes)
    /// </summary>
    static class Countries
        {
            static List<Country> CurrentCountries = new List<Country>();

            static Countries()
            {
                CurrentCountries.Add(new Country { Name = "Afghanistan", Code = "AF" });
                CurrentCountries.Add(new Country { Name = "Aland Islands", Code = "AX" });
                CurrentCountries.Add(new Country { Name = "Albania", Code = "AL" });
                CurrentCountries.Add(new Country { Name = "Algeria", Code = "DZ" });
                CurrentCountries.Add(new Country { Name = "American Samoa", Code = "AS" });
                CurrentCountries.Add(new Country { Name = "Andorra", Code = "AD" });
                CurrentCountries.Add(new Country { Name = "Angola", Code = "AO" });
                CurrentCountries.Add(new Country { Name = "Anguilla", Code = "AI" });
                CurrentCountries.Add(new Country { Name = "Antarctica", Code = "AQ" });
                CurrentCountries.Add(new Country { Name = "Antigua and Barbuda", Code = "AG" });
                CurrentCountries.Add(new Country { Name = "Argentina", Code = "AR" });
                CurrentCountries.Add(new Country { Name = "Armenia", Code = "AM" });
                CurrentCountries.Add(new Country { Name = "Aruba", Code = "AW" });
                CurrentCountries.Add(new Country { Name = "Australia", Code = "AU" });
                CurrentCountries.Add(new Country { Name = "Austria", Code = "AT" });
                CurrentCountries.Add(new Country { Name = "Azerbaijan", Code = "AZ" });
                CurrentCountries.Add(new Country { Name = "Bahamas", Code = "BS" });
                CurrentCountries.Add(new Country { Name = "Bahrain", Code = "BH" });
                CurrentCountries.Add(new Country { Name = "Bangladesh", Code = "BD" });
                CurrentCountries.Add(new Country { Name = "Barbados", Code = "BB" });
                CurrentCountries.Add(new Country { Name = "Belarus", Code = "BY" });
                CurrentCountries.Add(new Country { Name = "Belgium", Code = "BE" });
                CurrentCountries.Add(new Country { Name = "Belize", Code = "BZ" });
                CurrentCountries.Add(new Country { Name = "Benin", Code = "BJ" });
                CurrentCountries.Add(new Country { Name = "Bermuda", Code = "BM" });
                CurrentCountries.Add(new Country { Name = "Bhutan", Code = "BT" });
                CurrentCountries.Add(new Country { Name = "Bolivia", Code = "BO" });
                CurrentCountries.Add(new Country { Name = "Bosnia and Herzegovina", Code = "BA" });
                CurrentCountries.Add(new Country { Name = "Botswana", Code = "BW" });
                CurrentCountries.Add(new Country { Name = "Bouvet Island", Code = "BV" });
                CurrentCountries.Add(new Country { Name = "Brazil", Code = "BR" });
                CurrentCountries.Add(new Country { Name = "British Virgin Islands", Code = "VG" });
                CurrentCountries.Add(new Country { Name = "British Indian Ocean Territory", Code = "IO" });
                CurrentCountries.Add(new Country { Name = "Brunei Darussalam", Code = "BN" });
                CurrentCountries.Add(new Country { Name = "Bulgaria", Code = "BG" });
                CurrentCountries.Add(new Country { Name = "Burkina Faso", Code = "BF" });
                CurrentCountries.Add(new Country { Name = "Burundi", Code = "BI" });
                CurrentCountries.Add(new Country { Name = "Cambodia", Code = "KH" });
                CurrentCountries.Add(new Country { Name = "Cameroon", Code = "CM" });
                CurrentCountries.Add(new Country { Name = "Canada", Code = "CA" });
                CurrentCountries.Add(new Country { Name = "Cape Verde", Code = "CV" });
                CurrentCountries.Add(new Country { Name = "Cayman Islands", Code = "KY" });
                CurrentCountries.Add(new Country { Name = "Central African Republic", Code = "CF" });
                CurrentCountries.Add(new Country { Name = "Chad", Code = "TD" });
                CurrentCountries.Add(new Country { Name = "Chile", Code = "CL" });
                CurrentCountries.Add(new Country { Name = "China", Code = "CN" });
                CurrentCountries.Add(new Country { Name = "Hong Kong", Code = "HK" });
                CurrentCountries.Add(new Country { Name = "Macao", Code = "MO" });
                CurrentCountries.Add(new Country { Name = "Christmas Island", Code = "CX" });
                CurrentCountries.Add(new Country { Name = "Cocos (Keeling) Islands", Code = "CC" });
                CurrentCountries.Add(new Country { Name = "Colombia", Code = "CO" });
                CurrentCountries.Add(new Country { Name = "Comoros", Code = "KM" });
                CurrentCountries.Add(new Country { Name = "Congo (Brazzaville)", Code = "CG" });
                CurrentCountries.Add(new Country { Name = "Congo, Democratic Republic of the", Code = "CD" });
                CurrentCountries.Add(new Country { Name = "Cook Islands", Code = "CK" });
                CurrentCountries.Add(new Country { Name = "Costa Rica", Code = "CR" });
                CurrentCountries.Add(new Country { Name = "Côte d'Ivoire", Code = "CI" });
                CurrentCountries.Add(new Country { Name = "Croatia", Code = "HR" });
                CurrentCountries.Add(new Country { Name = "Cuba", Code = "CU" });
                CurrentCountries.Add(new Country { Name = "Cyprus", Code = "CY" });
                CurrentCountries.Add(new Country { Name = "Czech Republic", Code = "CZ" });
                CurrentCountries.Add(new Country { Name = "Denmark", Code = "DK" });
                CurrentCountries.Add(new Country { Name = "Djibouti", Code = "DJ" });
                CurrentCountries.Add(new Country { Name = "Dominica", Code = "DM" });
                CurrentCountries.Add(new Country { Name = "Dominican Republic", Code = "DO" });
                CurrentCountries.Add(new Country { Name = "Ecuador", Code = "EC" });
                CurrentCountries.Add(new Country { Name = "Egypt", Code = "EG" });
                CurrentCountries.Add(new Country { Name = "El Salvador", Code = "SV" });
                CurrentCountries.Add(new Country { Name = "Equatorial Guinea", Code = "GQ" });
                CurrentCountries.Add(new Country { Name = "Eritrea", Code = "ER" });
                CurrentCountries.Add(new Country { Name = "Estonia", Code = "EE" });
                CurrentCountries.Add(new Country { Name = "Ethiopia", Code = "ET" });
                CurrentCountries.Add(new Country { Name = "Falkland Islands (Malvinas)", Code = "FK" });
                CurrentCountries.Add(new Country { Name = "Faroe Islands", Code = "FO" });
                CurrentCountries.Add(new Country { Name = "Fiji", Code = "FJ" });
                CurrentCountries.Add(new Country { Name = "Finland", Code = "FI" });
                CurrentCountries.Add(new Country { Name = "France", Code = "FR" });
                CurrentCountries.Add(new Country { Name = "French Guiana", Code = "GF" });
                CurrentCountries.Add(new Country { Name = "French Polynesia", Code = "PF" });
                CurrentCountries.Add(new Country { Name = "French Southern Territories", Code = "TF" });
                CurrentCountries.Add(new Country { Name = "Gabon", Code = "GA" });
                CurrentCountries.Add(new Country { Name = "Gambia", Code = "GM" });
                CurrentCountries.Add(new Country { Name = "Georgia", Code = "GE" });
                CurrentCountries.Add(new Country { Name = "Germany", Code = "DE" });
                CurrentCountries.Add(new Country { Name = "Ghana", Code = "GH" });
                CurrentCountries.Add(new Country { Name = "Gibraltar", Code = "GI" });
                CurrentCountries.Add(new Country { Name = "Greece", Code = "GR" });
                CurrentCountries.Add(new Country { Name = "Greenland", Code = "GL" });
                CurrentCountries.Add(new Country { Name = "Grenada", Code = "GD" });
                CurrentCountries.Add(new Country { Name = "Guadeloupe", Code = "GP" });
                CurrentCountries.Add(new Country { Name = "Guam", Code = "GU" });
                CurrentCountries.Add(new Country { Name = "Guatemala", Code = "GT" });
                CurrentCountries.Add(new Country { Name = "Guernsey", Code = "GG" });
                CurrentCountries.Add(new Country { Name = "Guinea", Code = "GN" });
                CurrentCountries.Add(new Country { Name = "Guinea-Bissau", Code = "GW" });
                CurrentCountries.Add(new Country { Name = "Guyana", Code = "GY" });
                CurrentCountries.Add(new Country { Name = "Haiti", Code = "HT" });
                CurrentCountries.Add(new Country { Name = "Heard Island and Mcdonald Islands", Code = "HM" });
                CurrentCountries.Add(new Country { Name = "Holy See (Vatican City State)", Code = "VA" });
                CurrentCountries.Add(new Country { Name = "Honduras", Code = "HN" });
                CurrentCountries.Add(new Country { Name = "Hungary", Code = "HU" });
                CurrentCountries.Add(new Country { Name = "Iceland", Code = "IS" });
                CurrentCountries.Add(new Country { Name = "India", Code = "IN" });
                CurrentCountries.Add(new Country { Name = "Indonesia", Code = "ID" });
                CurrentCountries.Add(new Country { Name = "Iran, Islamic Republic of", Code = "IR" });
                CurrentCountries.Add(new Country { Name = "Iraq", Code = "IQ" });
                CurrentCountries.Add(new Country { Name = "Ireland", Code = "IE" });
                CurrentCountries.Add(new Country { Name = "Isle of Man", Code = "IM" });
                CurrentCountries.Add(new Country { Name = "Israel", Code = "IL" });
                CurrentCountries.Add(new Country { Name = "Italy", Code = "IT" });
                CurrentCountries.Add(new Country { Name = "Jamaica", Code = "JM" });
                CurrentCountries.Add(new Country { Name = "Japan", Code = "JP" });
                CurrentCountries.Add(new Country { Name = "Jersey", Code = "JE" });
                CurrentCountries.Add(new Country { Name = "Jordan", Code = "JO" });
                CurrentCountries.Add(new Country { Name = "Kazakhstan", Code = "KZ" });
                CurrentCountries.Add(new Country { Name = "Kenya", Code = "KE" });
                CurrentCountries.Add(new Country { Name = "Kiribati", Code = "KI" });
                CurrentCountries.Add(new Country { Name = "Korea", Code = "KP" });
                CurrentCountries.Add(new Country { Name = "Korea, Republic of", Code = "KR" });
                CurrentCountries.Add(new Country { Name = "Kuwait", Code = "KW" });
                CurrentCountries.Add(new Country { Name = "Kyrgyzstan", Code = "KG" });
                CurrentCountries.Add(new Country { Name = "Lao PDR", Code = "LA" });
                CurrentCountries.Add(new Country { Name = "Latvia", Code = "LV" });
                CurrentCountries.Add(new Country { Name = "Lebanon", Code = "LB" });
                CurrentCountries.Add(new Country { Name = "Lesotho", Code = "LS" });
                CurrentCountries.Add(new Country { Name = "Liberia", Code = "LR" });
                CurrentCountries.Add(new Country { Name = "Libya", Code = "LY" });
                CurrentCountries.Add(new Country { Name = "Liechtenstein", Code = "LI" });
                CurrentCountries.Add(new Country { Name = "Lithuania", Code = "LT" });
                CurrentCountries.Add(new Country { Name = "Luxembourg", Code = "LU" });
                CurrentCountries.Add(new Country { Name = "Macedonia", Code = "MK" });
                CurrentCountries.Add(new Country { Name = "Madagascar", Code = "MG" });
                CurrentCountries.Add(new Country { Name = "Malawi", Code = "MW" });
                CurrentCountries.Add(new Country { Name = "Malaysia", Code = "MY" });
                CurrentCountries.Add(new Country { Name = "Maldives", Code = "MV" });
                CurrentCountries.Add(new Country { Name = "Mali", Code = "ML" });
                CurrentCountries.Add(new Country { Name = "Malta", Code = "MT" });
                CurrentCountries.Add(new Country { Name = "Marshall Islands", Code = "MH" });
                CurrentCountries.Add(new Country { Name = "Martinique", Code = "MQ" });
                CurrentCountries.Add(new Country { Name = "Mauritania", Code = "MR" });
                CurrentCountries.Add(new Country { Name = "Mauritius", Code = "MU" });
                CurrentCountries.Add(new Country { Name = "Mayotte", Code = "YT" });
                CurrentCountries.Add(new Country { Name = "Mexico", Code = "MX" });
                CurrentCountries.Add(new Country { Name = "Micronesia", Code = "FM" });
                CurrentCountries.Add(new Country { Name = "Moldova", Code = "MD" });
                CurrentCountries.Add(new Country { Name = "Monaco", Code = "MC" });
                CurrentCountries.Add(new Country { Name = "Mongolia", Code = "MN" });
                CurrentCountries.Add(new Country { Name = "Montenegro", Code = "ME" });
                CurrentCountries.Add(new Country { Name = "Montserrat", Code = "MS" });
                CurrentCountries.Add(new Country { Name = "Morocco", Code = "MA" });
                CurrentCountries.Add(new Country { Name = "Mozambique", Code = "MZ" });
                CurrentCountries.Add(new Country { Name = "Myanmar", Code = "MM" });
                CurrentCountries.Add(new Country { Name = "Namibia", Code = "NA" });
                CurrentCountries.Add(new Country { Name = "Nauru", Code = "NR" });
                CurrentCountries.Add(new Country { Name = "Nepal", Code = "NP" });
                CurrentCountries.Add(new Country { Name = "Netherlands", Code = "NL" });
                CurrentCountries.Add(new Country { Name = "Netherlands Antilles", Code = "AN" });
                CurrentCountries.Add(new Country { Name = "New Caledonia", Code = "NC" });
                CurrentCountries.Add(new Country { Name = "New Zealand", Code = "NZ" });
                CurrentCountries.Add(new Country { Name = "Nicaragua", Code = "NI" });
                CurrentCountries.Add(new Country { Name = "Niger", Code = "NE" });
                CurrentCountries.Add(new Country { Name = "Nigeria", Code = "NG" });
                CurrentCountries.Add(new Country { Name = "Niue", Code = "NU" });
                CurrentCountries.Add(new Country { Name = "Norfolk Island", Code = "NF" });
                CurrentCountries.Add(new Country { Name = "Northern Mariana Islands", Code = "MP" });
                CurrentCountries.Add(new Country { Name = "Norway", Code = "NO" });
                CurrentCountries.Add(new Country { Name = "Oman", Code = "OM" });
                CurrentCountries.Add(new Country { Name = "Pakistan", Code = "PK" });
                CurrentCountries.Add(new Country { Name = "Palau", Code = "PW" });
                CurrentCountries.Add(new Country { Name = "Palestinian", Code = "PS" });
                CurrentCountries.Add(new Country { Name = "Panama", Code = "PA" });
                CurrentCountries.Add(new Country { Name = "Papua New Guinea", Code = "PG" });
                CurrentCountries.Add(new Country { Name = "Paraguay", Code = "PY" });
                CurrentCountries.Add(new Country { Name = "Peru", Code = "PE" });
                CurrentCountries.Add(new Country { Name = "Philippines", Code = "PH" });
                CurrentCountries.Add(new Country { Name = "Pitcairn", Code = "PN" });
                CurrentCountries.Add(new Country { Name = "Poland", Code = "PL" });
                CurrentCountries.Add(new Country { Name = "Portugal", Code = "PT" });
                CurrentCountries.Add(new Country { Name = "Puerto Rico", Code = "PR" });
                CurrentCountries.Add(new Country { Name = "Qatar", Code = "QA" });
                CurrentCountries.Add(new Country { Name = "Réunion", Code = "RE" });
                CurrentCountries.Add(new Country { Name = "Romania", Code = "RO" });
                CurrentCountries.Add(new Country { Name = "Russian Federation", Code = "RU" });
                CurrentCountries.Add(new Country { Name = "Rwanda", Code = "RW" });
                CurrentCountries.Add(new Country { Name = "Saint-Barthélemy", Code = "BL" });
                CurrentCountries.Add(new Country { Name = "Saint Helena", Code = "SH" });
                CurrentCountries.Add(new Country { Name = "Saint Kitts and Nevis", Code = "KN" });
                CurrentCountries.Add(new Country { Name = "Saint Lucia", Code = "LC" });
                CurrentCountries.Add(new Country { Name = "Saint-Martin", Code = "MF" });
                CurrentCountries.Add(new Country { Name = "Saint Pierre and Miquelon", Code = "PM" });
                CurrentCountries.Add(new Country { Name = "Saint Vincent and Grenadines", Code = "VC" });
                CurrentCountries.Add(new Country { Name = "Samoa", Code = "WS" });
                CurrentCountries.Add(new Country { Name = "San Marino", Code = "SM" });
                CurrentCountries.Add(new Country { Name = "Sao Tome and Principe", Code = "ST" });
                CurrentCountries.Add(new Country { Name = "Saudi Arabia", Code = "SA" });
                CurrentCountries.Add(new Country { Name = "Senegal", Code = "SN" });
                CurrentCountries.Add(new Country { Name = "Serbia", Code = "RS" });
                CurrentCountries.Add(new Country { Name = "Seychelles", Code = "SC" });
                CurrentCountries.Add(new Country { Name = "Sierra Leone", Code = "SL" });
                CurrentCountries.Add(new Country { Name = "Singapore", Code = "SG" });
                CurrentCountries.Add(new Country { Name = "Slovakia", Code = "SK" });
                CurrentCountries.Add(new Country { Name = "Slovenia", Code = "SI" });
                CurrentCountries.Add(new Country { Name = "Solomon Islands", Code = "SB" });
                CurrentCountries.Add(new Country { Name = "Somalia", Code = "SO" });
                CurrentCountries.Add(new Country { Name = "South Africa", Code = "ZA" });
                CurrentCountries.Add(new Country { Name = "South Georgia", Code = "GS" });
                CurrentCountries.Add(new Country { Name = "South Sudan", Code = "SS" });
                CurrentCountries.Add(new Country { Name = "Spain", Code = "ES" });
                CurrentCountries.Add(new Country { Name = "Sri Lanka", Code = "LK" });
                CurrentCountries.Add(new Country { Name = "Sudan", Code = "SD" });
                CurrentCountries.Add(new Country { Name = "Suriname", Code = "SR" });
                CurrentCountries.Add(new Country { Name = "Svalbard and Jan Mayen Islands", Code = "SJ" });
                CurrentCountries.Add(new Country { Name = "Swaziland", Code = "SZ" });
                CurrentCountries.Add(new Country { Name = "Sweden", Code = "SE" });
                CurrentCountries.Add(new Country { Name = "Switzerland", Code = "CH" });
                CurrentCountries.Add(new Country { Name = "Syrian Arab Republic", Code = "SY" });
                CurrentCountries.Add(new Country { Name = "Taiwan, Republic of China", Code = "TW" });
                CurrentCountries.Add(new Country { Name = "Tajikistan", Code = "TJ" });
                CurrentCountries.Add(new Country { Name = "Tanzania, United Republic of", Code = "TZ" });
                CurrentCountries.Add(new Country { Name = "Thailand", Code = "TH" });
                CurrentCountries.Add(new Country { Name = "Timor-Leste", Code = "TL" });
                CurrentCountries.Add(new Country { Name = "Togo", Code = "TG" });
                CurrentCountries.Add(new Country { Name = "Tokelau", Code = "TK" });
                CurrentCountries.Add(new Country { Name = "Tonga", Code = "TO" });
                CurrentCountries.Add(new Country { Name = "Trinidad and Tobago", Code = "TT" });
                CurrentCountries.Add(new Country { Name = "Tunisia", Code = "TN" });
                CurrentCountries.Add(new Country { Name = "Turkey", Code = "TR" });
                CurrentCountries.Add(new Country { Name = "Turkmenistan", Code = "TM" });
                CurrentCountries.Add(new Country { Name = "Turks and Caicos Islands", Code = "TC" });
                CurrentCountries.Add(new Country { Name = "Tuvalu", Code = "TV" });
                CurrentCountries.Add(new Country { Name = "Uganda", Code = "UG" });
                CurrentCountries.Add(new Country { Name = "Ukraine", Code = "UA" });
                CurrentCountries.Add(new Country { Name = "United Arab Emirates", Code = "AE" });
                CurrentCountries.Add(new Country { Name = "United Kingdom", Code = "GB" });
                CurrentCountries.Add(new Country { Name = "United States of America", Code = "US" });
                CurrentCountries.Add(new Country { Name = "United States Minor Outlying Islands", Code = "UM" });
                CurrentCountries.Add(new Country { Name = "Uruguay", Code = "UY" });
                CurrentCountries.Add(new Country { Name = "Uzbekistan", Code = "UZ" });
                CurrentCountries.Add(new Country { Name = "Vanuatu", Code = "VU" });
                CurrentCountries.Add(new Country { Name = "Venezuela", Code = "VE" });
                CurrentCountries.Add(new Country { Name = "Viet Nam", Code = "VN" });
                CurrentCountries.Add(new Country { Name = "Virgin Islands, US", Code = "VI" });
                CurrentCountries.Add(new Country { Name = "Wallis and Futuna Islands", Code = "WF" });
                CurrentCountries.Add(new Country { Name = "Western Sahara", Code = "EH" });
                CurrentCountries.Add(new Country { Name = "Yemen", Code = "YE" });
                CurrentCountries.Add(new Country { Name = "Zambia", Code = "ZM" });
                CurrentCountries.Add(new Country { Name = "Zimbabwe", Code = "ZW" });
            }


            public static string GetCountryName(string countryCode)
            {
                if (CurrentCountries.Any(country => country.Code.Equals(countryCode)))
                {
                    return CurrentCountries.FirstOrDefault(country => country.Code.Equals(countryCode)).Name;
                }
                else
                {
                    return "Unknown Country";
                }
            }
        }

        public class Country
        {
            public String Code { get; set; }
            public String Name { get; set; }
        }

}
