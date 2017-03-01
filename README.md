# CSGOTelegramBot
Telegram Bot which uses the Steam Apis to retrieve player and gameserver information

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

