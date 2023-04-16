# Issue Tracking System
This is a simple issue tracking system developed with C# and .NET Core. It allows users to create and manage issues and projects, assign them to team members, and track their progress.

# Table of Contents
* [Features](#Features)
* [Technologies](#Technologies)
* [Installation](#Installation)
* [Usage](#Usage)
* [Contributing](#Contributing)
* [License](#License)

# Features
* Create new issues/projects
* Assign issues to team members
* Track issue/project progress
* Add comments to issues
* Search issues by keyword, status, or assignee
* Edit or delete existing issues
* Mark issues as resolved

# Technologies
This project was developed with the following technologies:

.C#
.net core 6.0
SQLServer
Entity Framework Core 6.0
Razor Pages
Bootstrap 5.0.0
JS charting libraries for data visualization 

# Installation
To run this project, you will need to have .NET 6 and SQLServer installed on your system. Once you have these installed, follow these steps:

1. Clone this repository: git clone https://github.com/ChrispyCodes/issue-tracker.git
2. Create a new SQL database called IssueTrackingSystem
3. Modify the appsettings.json file with your database connection details
4. run an update-database command to push the migrations to db (using .net CLI or node package console in visual studio) 
5. Run the IssueTrackingSystem.sln 

# Usage
Once the application is running, you can access it at http://localhost:7880. From there, you can create new issues, assign them to team members, and track their progress. You can also search for issues by keyword, status, or assignee.

# Contributing
If you would like to contribute to this project, please follow these steps:

# Fork this repository
Create a new branch for your feature or bug fix
Make your changes and commit them with descriptive commit messages
Push your changes to your fork
Submit a pull request

# License
This project is licensed under the MIT License - see the LICENSE file for details.
