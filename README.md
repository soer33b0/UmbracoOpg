# UmbracoOpg


A web appilication developed for Acme Corporation in Asp.NET core with entity framework for entering a contest pool, from which a winner will be drawn.

The rules for entering the draw are: 

	1. You are over 18. 
	
	2. You have a valid serial number of one of Acme Corporations products (serialNumbers.txt).
	
	3. Two submissions can be made with the same valid serial number.

Getting started:

You will need:

Windows OS

Visual Studio 2019 with the ASP.NET and web development workload.

.Net Core 3.1 SDK or later.

In Visual Studio, under Tools > NuGet Package Manager > Package Manager Console, enter the following command to set up the database: 
	
	Update-Database

The database will be seeded upon start using serialNumbers from line 88-100 in the serialNumbers.txt file. Therefore you will not be able to use these for creating a submission.

When the application is run (F5) with IIS Express, you land on the start page. From here, you can enter the draw by clicking the link below the picture of the very attractive main prize, and enter the draw twice, if you follow the rules of admission. 
A list of all the submissions, with 10 submission per page, can be seen under the "Submissions" tab at the top of each page. 
If you wish to draw a winner from all the submissions, head on over to https://localhost/_____/submission/drawwinner.

Unit tests for validating form input data can be found in the separate project called UnitTestProject1.
