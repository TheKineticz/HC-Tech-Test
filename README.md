# HaloConnect Hiring Tech Test
Developed on .NET 7.0
Follow the link below to install the latest .NET SDK:

https://dotnet.microsoft.com/en-us/download

The Tests.cs file contains a small XUnit unit test suite for the two exercises, which can be run via the IDE of your choice, if you wish to run them.
## Steps to Build and Run
1. Clone or download and unzip the repo to a folder on your computer
2. Open a terminal window and navigate to the app root folder
3. Enter the command `dotnet run` to build and run the application

Alternatively, you can open the root folder in your IDE of choice and run the application from there.

The application should now be running and accessible via http://localhost:5285/. If the application is running on a different port, it will be printed in the terminal window.

## How to interact with the API
The API exposes two GET endpoints which can be tested through your browser or API testing application, such as Postman:

`/convertstring` Accepts one query parameter `value` - a positive or negative integer in string format, which will be converted to a long integer format and returned.

`/chequetext` Accepts one query parameter `value` - a positive decimal number, which will be rounded to two decimal points, then converted to Word form in English. 
