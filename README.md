ColProj  
An application for creating and managing hostels and collectives.  
Made by Henrik Hafell


Requirements  
Visual Studio 2019  
.Net Core 2.2  
Internet connection if used with database. Local database is supported offline.  
User Login to access features.  

Due to lack of time I had to cut a lot of features, although the ones implemented should be fully functional and completed. Main feature I had to cut was commenting on posts as this took too much time to implement.
Known Issues
I have extensively debugged the project and everything should work properly if internet connection is maintained. Due to lack of time and extremely poor resources online about UWP NavigationView I had to write “DO NOT CLICK UNLESS LOGGED IN” on certain menu items. If these are clicked I cannot guarantee the program will not crash although I have tested this and done error handling to account for a majority of issues.

If Internet Connection is lost you will get a popup message telling you to reconnect, the application will not crash if you click on menu items, although they will not show you any content. Reconnecting should work well as long as you refresh the page.

My ViewModels have some duplicate code in them ReadSetting, SaveSetting and InternetConnectivity, I acknowledge this is not optimal but I did not have time to alter the existing code to fit with a Helper Class containing these.

Certain Try-Catch exception handlers are non-specific as debugging this would take too much time and hardly give any meaningful benefits. Look in Visual Studio output to see any error messages in the rare case these fail.

Project may contain old unused files as removing them caused everything to stop working for some reason, please ignore.

In case of random catastrophic failures please restart your computer or try this https://errorhandlinginskills.wordpress.com/2018/07/28/how-to-clear-visual-studio-cache/

Visual Studio is not the most stable program.

Project Description  
I live in a collective and have issues with cleaning lists, communal purchases, and displaying important knowledge to new residents. To fix this I intend to make a web platform that can register users and create new collectives to which the users may add calendars, cleaning lists, checklists, announcements, and create text posts on a simple board. 

Obviously this is a huge task so I limited myself to a couple of key features as shown below.
Features:

Register  
First name, Last name, Username, Password.
Password is hashed and salted for security.
Input validation with regex in GUI and API to avoid SQL Injection.

Login  
Username, Password
Input validation with regex in GUI and API to avoid SQL Injection.
Checks if Username exists, password is checked against hashed values in database.
On successful Login, the username and password is converted to a Base64String which is used as a basic authentication in the api to only allow logged in users to send requests to the database.

Update User  
First name, Last name, Username, Password.
Input validation with regex in GUI and API to avoid SQL Injection
Every Field has to be filled in.
Checks if Username exists.
Password is hashed and salted for security.

Create Collective  
Name, Description, Size.
Input validation with regex in GUI and API to avoid SQL Injection.
Join ID is next to name in Your Collectives.

Join Collective  
Enter Unique ID generated when Collective is created.
Input validation with regex in GUI and API to avoid SQL Injection.

Collectives  
Your Collectives
Shows list of collectives you’ve joined and lets you leave or open them.

Manage Collectives  
Shows list of collectives you’ve created and lets you remove or edit them.

Update Collectives    
Name, Description, Size.
Input validation with regex in GUI and API to avoid SQL Injection.
Only the creator of a collective can update.

Go to Collective  
Shows Users that have joined the collective
Shows created posts.
Lets you create, open and delete a post

Create Post  
Title, Content
Input validation with regex in GUI and API to avoid SQL Injection

