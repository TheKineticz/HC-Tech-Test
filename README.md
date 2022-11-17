# HaloConnect Hiring Tech Test
Developed on .NET 7.0
Follow the link below to install the latest .NET SDK:

https://dotnet.microsoft.com/en-us/download

A unit testing project in HC-Tech-Test.Tests contains a small XUnit unit test suite for the two exercises, which can be run via the IDE of your choice, or by running `dotnet test` from the tests folder.

## Steps to Build and Run
1. Clone or download and unzip the repo to a folder on your computer
2. Open a terminal window and navigate to the app root/HC-Tech-Test folder
3. Enter the command `dotnet run` to build and run the application

Alternatively, you can open the folder in your IDE of choice and run the main application from there.

The application should now be running with swagger docs accessible via http://localhost:5285/swagger. If the application is running on a different port, it will be printed in the terminal window.

## How to interact with the API
The API exposes two GET endpoints which can be tested through your browser or API testing application, such as Postman:

`/exa` for Exercise A: Accepts one query parameter `value` - a positive or negative integer in string format, which will be converted to a long integer format and returned.

`/exb` for Exercise B: Accepts one query parameter `value` - a positive decimal number, which will be rounded to two decimal points, then converted to Word form in English. 
