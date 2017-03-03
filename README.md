# CSGOTelegramBot

## Quick Start
1. Register a new bot on Telegram
	See: [Telegram API Docs](https://core.telegram.org/bots#creating-a-new-bot)
2. Build the solution to download and install NuGet packages
3. Open **CSGOTelegramData** project and edit in **Config.cs** the variables:  
  a. **BotNickname** (The precise username which is chosen when creating the bot in step 1, withouth the "@")  
  b. **TelegramApiKey** (The Telegram authorization token which is given when creating the bot in step 1)  
  c. (Optional) Set the **MainTitle** and **BotVersion** to anything you want  
  d. **ServerIP** (IPAddress of the CSGO gameserver you want to retrieve info from)  
  e. **ServerPort** (Network port of the CSGO gameserver you want to retrieve info from)  
  f. (Optional) Fill the **Admins** list with the steamID64's of the players who are an admin on the CSGO gameserver  
  g. (Optional) Fill the  **Clans** list in the clan section  
4.	Open each app.config and change the **connectionString** to your database setup.  
	This solution is developed Code First so you can create the required database automatically:  
  a. Open **Package Manager Console**  
  b. In the console, set **CSGOTelegramData** as default project  
  c. Run the **enable-migrations** command  
  d. Run the **update-database** command  
5. 	(Optional) Build the solution (it's already been build in step 4c, but just to be sure)  
6. 	In the output folder of the **CSGOTelegramSync** project, run **CSGOTelegramSync.exe**  
7. 	In the output folder of the **CSGOTelegramBot** project, run **CSGOTelegramBot.exe**  
8. 	You're done! Now open Telegram and test your bot!  

## FAQ
### How do I add a new Telegram command?
  
##### Add a **BotCommand** to the BotCommand List in **Config.cs**:  
```C#
public static List<BotCommand> Commands = new List<BotCommand> {
	new BotCommand { Command = "/start", Description = "List available commands", Function = "SendStart" },
	new BotCommand { Command = "/help", Description = "List available commands", Function = "SendHelp" },
	new BotCommand { Command = "/map", Description = "Shows current map", Function = "SendCurrentMap" },
	new BotCommand { Command = "/nextmap", Description = "Shows next map", Function = "SendNextMap" },            
	new BotCommand { Command = "/playercount", Description = "Shows amount of online players", Function = "SendCurrentPlayersCount" },
	new BotCommand { Command = "/players", Description = "List online players", Function = "SendCurrentPlayers" },
	new BotCommand { Command = "/admins", Description = "List online admins", Function = "SendCurrentAdmins" },
	new BotCommand { Command = "/clans", Description = "List online clan members", Function = "SendCurrentClanPlayers" },
	new BotCommand { Command = "/info", Description = "Server information", Function = "SendServerInfo" }
};
```
Command: Command the user has to type in Telegram  
Description: A short description about the command. This will be displayed in the help command.  
Function: The exact name of the function which will be called in **BotHelper.cs**  
  
##### Create the function in **BotHelper.cs** with this method signature:  
```C#
public async Task SendFunctionName(long, string)
```

##### (Optional) If you want Telegram to show your new command to the user as an available command, register it at the BotFather with **/setcommands** 
  
  
  
### Why is the bot return message not showing (correctly) in Telegram?
  
Most of the time this is because of an improper format in the message. (At the time of writing this bot) Telegram uses a limited set of HTML OR Markdown to show the message.
If you're message isn't properly formatted, Telegram denies the SendMessage call. That's why you always need to HTML encode the playernames to filter unsupported characters and make sure your message has correct opening and closing tags.  
PS: CSGOTelegramBot is configured to use HTML instead of Markdown. If you want to change this behaviour alter the **ParseMode** in the general **SendMessage** function to **ParseMode.Default** or **ParseMode.Markdown**:  
```C#
public async Task<Message> SendMessage(long chatId, string message)
	{
	   return await Bot.SendTextMessage(chatId, message, false, 0, null, ParseMode.Html); 
	}
```
  
  
  
### I don't want to use the CSGOTelegramSync app/database, how can I change this?
  
Requesting the playernames from a CSGO gameserver is often prone to failure. Especially on first try and/or when a gameserver has all playerslots filled.  
The result is that a Telegram user can experience a lot of waiting time when requesting player related commands.  
This often leads to confusion and i.e. the user will enter the command again which can lead to numerous playerlist refresh calls which only leads to an even worse experience/performance.  
That's the reason I implemented a seperate app which handles the playerlist updating and the database also acts as a sort of cache.  
With this setup you always have a playerlist available for the Telegram Bot to use, and there are no delays on serving playerdata to the Telegram user.  
  
If you for whatever reason still don't want to use the sync app and rather let the Telegram Bot request the CSGO gameserver directly, change these lines of code in all player related Send functions in **BotHelper.cs**:  
  
**Old code**  
```C#
List<PlayerInfoRecord> playerList = new List<PlayerInfoRecord>();
using(var mc = new CSGODataContext())
{
	playerList = mc.PlayerInfos.ToList();
}
[..]
foreach (PlayerInfoRecord currentPlayer in playerList)
{
	if (currentPlayer.Name.Trim() != "")
	{
		string currentName = currentPlayer.Name;
		string cleanName = WebUtility.HtmlEncode(currentName);
		playerMessage += $"{cleanName}\r\n";
	}
}
```  
**New code**  
```C#
List<PlayerInfo> players = ServerHelper.GetPlayers(this, chatId, userName);
[..]
foreach (PlayerInfo currentPlayer in players)
{
	if (currentPlayer.Name.Trim() != "")
	{
		string currentName = currentPlayer.Name;
		string cleanName = WebUtility.HtmlEncode(currentName);
		playerMessage += $"{cleanName}\r\n";
	}
}
```
  
  
  
### Why does it take a long time retrieving clan information? 
  
The only player information we can retrieve from a CSGO gameserver playerlist request is their Steam displayname.  
To determine which clanmembers are online, we need to retrieve all current Steam displaynames from every clanmember configured in the Clan list in **Config.cs**.  
Although the SteamApi calls are grouped to one request per clan, this still means there needs to be a lot of processing done for the return data.  
Be cautious of the clans you want to include in the Telegram Bot. In short, it doesn't matter how many clans you add as long as the amount of clanmembers is not reaching the hundreds if not thousands.
  
For above reason there is a cache for claninformation implemented. (**ClanCache** in **BotHelper.cs**)  
As a default the ClanCache is updated every hour. If you want to change this behaviour to another timeframe, adjust this variable in **BotHelper.cs**:  
```C#
int ClanUpdateHours = 1;
```
  
PS: Be aware that the **ClanCache** is checked if it needs updating after each **SendCurrentClanPlayers** call.
This also means that the very first Telegram user request after the Telegram Bot is put online will always take as long as it's needed to process the configured Clan information since the ClanCache is still empty at that point.
  
  
  
### How can I develop/debug my Telegram Bot without affecting the currently online Bot?
  
Just as a tip. If you want to develop your Bot without taking it offline, just register another Bot with BotFather.  
Give it an obscure name so nobody would (want to) find it when searching for new bots.  If you run several bots, you can use this one as development bot for each of them.  
To get the development bot running, you only need to change at least these variables in **Config.cs** to the 'development bot settings':  
```C#
public static string BotNickname = "YourBotName";

public static string TelegramApiKey = "YourBotAuthenticationKey";
```
  
That's it! Now you can run the bots side by side.  
Unless you want to work on the sync app, you don't have to start another instance of that project since the bot is still configured to connect to the same database and as a benefit you have the same data at your immediate disposal as the bot running in 'production'.  
  
  
  
## Additional Contact Information
  
Email: RoadsterBSR@gmail.com  
Steam: [RoadsterBSR](http://steamcommunity.com/id/RoadsterBSR/)
  
  
  
## Special Thanks

The very first CSGOTelegramBot (MapeadoresBot) was created for the CSGO Zombie Escape Community server [Mapeadores](http://mapeadores.com/).  
Being inspired and supported by the ZE Community is the reason this project has come to life and evolved to it's current form.  
  
Therefore I would like to thank everyone and in particular:  
  
[WAN](http://steamcommunity.com/id/wan186)  
For the help and ideas in improving functionality and making the code more robust  
  
[CATKrash](http://steamcommunity.com/id/7656119806107987)  
For providing data regarding the Mapeadores Server  
  
[Nick](http://steamcommunity.com/id/76561198006765411)  
For introducing me to the dark Clan side of CSGO ZE and his general support  
  
[Unorth](http://steamcommunity.com/profiles/76561197972298323)  
For adopting the MapeadoresBot and (potential) future improvements to CSGOTelegramBot  
  
[TNLE](http://steamcommunity.com/groups/TNLE)  
For providing a homebase with great players and inspiring persons. RIP  
  
[TRLG](http://steamcommunity.com/groups/therelaxed)  
For being a significant contributor to the current ZE community  
  
  
  
## License
  
This software is released and made available using the [GNU General Public License](https://www.gnu.org/licenses/gpl-3.0.txt)
  

  

