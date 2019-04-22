# CmsEngine

## Build status
![Build status](https://davidsonsousa.visualstudio.com/CMSEngine/_apis/build/status/CMSEngine%20CI%20master "Build status")

## What is it?
This the code-base of the CMS I am using in my website [https://davidsonsousa.net](https://davidsonsousa.net "my website").

I am making it available for anyone who would like to use a .NET Core engine to create their own website.

## Why?
Because I need a pet project to study a couple of things. And do it with my own website seems to be the right choice.

## What is this project using?
###### (or: _What I intend to practice with this project?_)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
* [C#](https://www.microsoft.com/net/tutorials/csharp/getting-started)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [AutoMapper](http://automapper.org/)
* [xUnit](https://xunit.github.io/) (in a later stage)

## Running the project
Since this is a .NET Core project you can run in 2 ways:

### .NET Core CLI (using PowerShell)
- Run the project
  - `cd ..\CmsEngine.Ui`
  - `dotnet -d watch run`
    - `-d` shows the PID for debugging purposes
  - Open your favorite browser and load `https://cmsengine.test:5001` (make sure this url is mapped to localhost in your hosts file)
- Create migrations
  - Make that sure you are in the **library folder (CmsEngine\CmsEngine)**
  - `dotnet ef migrations add MIGRATION_NAME --startup-project ..\CmsEngine.Ui\`
- Migrations should automatically be executed but, if necessary, here how to do
  - Make that sure you are in the **library folder (CmsEngine\CmsEngine.Ui)**
  - `dotnet ef database update`

### IIS Express
- Just run with <kbd>Ctrl</kbd> + <kbd>F5</kbd> (or <kbd>F5</kbd> for debugging) and let everything happens

### IIS
- You will need to setup your own IIS instance
- <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>P</kbd> to attach the process and debug
