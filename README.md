# CSGOTelegramBot

## Quick Start
1. Register a new bot on Telegram
	See: https://core.telegram.org/bots#creating-a-new-bot
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
**How do I add a new Telegram command?**  
  
1. Add a **BotCommand** to the BotCommand List in **Config.cs**:  
```
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
  
2. Create the function in **BotHelper.cs** with this method signature:  
```
public async Task SendFunctionName(long, string)
```

3. (Optional) If you want Telegram to show your new command to the user as an available command, register it at the BotFather with **/setcommands** 
  
  
**Why is the bot return message not showing (correctly) in Telegram?**
  
Most of the time this is because of an improper format in the message. (At the time of writing this bot) Telegram uses a limited set of HTML OR Markdown to show the message.
If you're message isn't properly formatted, Telegram denies the SendMessage call. That's why you always need to HTML encode the playernames to filter unsupported characters and make sure your message has correct opening and closing tags.  
PS: CSGOTelegramBot is configured to use HTML instead of Markdown. If you want to change this behaviour alter the **ParseMode** in the general **SendMessage** function:  
```
public async Task<Message> SendMessage(long chatId, string message)
	{
	   return await Bot.SendTextMessage(chatId, message, false, 0, null, ParseMode.Html); 
	}
```
  
  
**I don't want to use the CSGOTelegramSync app/database, how can I change this?**  
  
Requesting the playernames from a CSGO gameserver is often prone to failure. Especially on first try and/or when a gameserver has all playerslots filled.  
The result is that a Telegram user can experience a lot of waiting time when requesting player related commands.  
This often leads to confusion and i.e. the user will enter the command again which can lead to numerous playerlist refresh calls which only leads to an even worse experience/performance.  
That's the reason I implemented a seperate app which handles the playerlist updating and the database also acts as a sort of cache.  
With this setup you always have a playerlist available for the Telegram Bot to use, and there are no delays on serving playerdata to the Telegram user.  
  
If you for whatever reason still don't want to use the sync app and rather let the Telegram Bot request the CSGO gameserver directly, change these lines of code in all player related Send functions in **BotHelper.cs**:  
**Old code**  
```
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
```
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
