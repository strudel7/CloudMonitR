// ---------------------------------------------------------------------------------- 
// Microsoft Developer & Platform Evangelism 
//  
// Copyright (c) Microsoft Corporation. All rights reserved. 
//  
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,  
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES  
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
// ---------------------------------------------------------------------------------- 
// The example companies, organizations, products, domain names, 
// e-mail addresses, logos, people, places, and events depicted 
// herein are fictitious.  No association with any real company, 
// organization, product, domain name, email address, logo, person, 
// places, or events is intended or should be inferred. 
// ---------------------------------------------------------------------------------- 
// Thanks for installing CloudMonitR 
// ---------------------------------------------------------------------------------- 

The hard part is over, but there are just a few more steps 
you'll need to go through to get everything working. These 
steps should take less than 10 minutes to complete. 

1 - In the Windows Azure Cloud Service project containing 
the Web or Worker Role into which you just installed 
CloudMonitR, double-click the Roles/[RoleName] treeview
node in Visual Studio's Solution Explorer to open the
cloud service project's properties. 

2 - Click Settings

3 - Add a Connection String named CloudMonitRConnectionString
and set the value of the setting to point to the storage
account in which a table will be created to track the
performance counters that will be observed in the CloudMonitR
web client.

4 - If you don't have a web application project in the same
solution or you prefer to set up the CloudMonitR GUI in a separate
web application, add one to the solution. In the "New Project"
dialog, select "Empty ASP.NET Web Application."

5 - Install the CloudMonitR.Web NuGet package into the web
application project you either already had in your solution
or created in step 4. Open the web project's properties and
copy the URL on which the site runs to your clipboard 
(the URL, like http://localhost:45050/). 

6 - Add a string setting named HubURL and type (or paste)
in the value of the setting as the URL of the web application
project into which you installed the CloudMonitR.Web package in step 5. 