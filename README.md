# UmbracoOpg


A web appilication developed for Acme Corporation in Asp.NET core with entity framework for entering a contest pool, from which a winner will be drawn.

The rules for entering the draw are: 
	1. You are over 18. 
	2. You have a valid serial number of one of Acme Corporations products (serialNumbers.txt).
	3. Two submissions can be made with the same valid serial number.

Getting started:
In Visual Studio, under Tools > NuGet Package Manager > Package Manager Console, enter the following commands: 
	Install-Package Microsoft.EntityFrameworkCore.SqlServer
	Install-Package Microsoft.EntityFrameworkCore.InMemory
For more info on installing Entity Framework Core: https://docs.microsoft.com/en-us/ef/core/get-started/install/

When the application is run (F5) with IIS Express, you land on the start page. From here, you can enter the draw by clicking the link below the picture of the very attractive main prize, and enter the draw twice, if you follow the rules of admission. 
A list of all the submissions, with 10 submission per page, can be seen under the "Submissions" tab at the top of each page. 
If you wish to draw a winner from all the submissions, head on over to https://localhost/_____/submission/drawwinner.

Unit tests for validating form input data can be found in the separate project called UnitTestProject1.
