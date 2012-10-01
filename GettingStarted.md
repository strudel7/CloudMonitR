# CloudMonitR Sample #

The CloudMonitR sample demonstrates how a Windows Azure Cloud Service can be instrumented so that developers using Windows Azure Cloud Services can have a view into how their application is performing in real-time. Using Performance Counter data reported in various charts in an HTML 5 dashboard and trace data that's displayed in a web browser window, application owners can continually see how instances are performing and using resources so that they'll better understand when their applications may need to be scaled. 

## Prerequisites ##

* [Visual Studio 2012](http://www.microsoft.com/visualstudio/en-us/products) 
* [Windows Azure SDK for .NET 1.7](http://www.windowsazure.com/en-us/develop/net/)

### Running the Sample Locally
To get the site and cloud service running locally on a development workstation execute the following steps. 

1. Open the solution in Visual Studio 2012 running in Administrator mode. This solution contains a Windows Azure Cloud Service project, so it must be opened under Administration mode or the Windows Azure Compute Emulator may fail to open properly. The solution contains 4 projects. 
	1. CloudMonitR - the core project and functionality providing the performance counter-observation and trace listener functionality. 
	1. CloudMonitR.Azure - the Windows Azure Cloud Service project containing a Worker Role that is monitored by the CloudMonitR runtime
	1. CloudMonitR.Web - a basic ASP.NET Empty Web Project containing the HTML assets that comprise the CloudMonitR web dashboard. 
	1. CloudMonitR.WorkerRole - a worker role that serves as the target for a use-case of the CloudMonitR source code. This project is primarily a demonstration project that is used during debugging to host the CloudMonitR components. 

	![](images/1.png)

1. Open the CloudMonitR.Web project properties and take note of the URL where the site runs. Copy the URL to your clipboard. 

	![](images/5.png)

1. Select the CloudMonitR.Web project. 

1. Open the CloudMonitR.Azure project's Roles folder and double-click the CloudMonitR.WorkerRole node to open the cloud project's properties window. Set the number of instances value to be 1-5, depending on how you want to test the CloudMonitR application.

	![](images/6.png)

1. Click the Settings tab of the CloudMonitR.WorkerRole Role configuration tab. Set the HubURL setting value to the URL copied from the CloudMonitR.Web project settings. 

	![](images/7.png)

1. Right-click the Solution and select Properties. Select Startup Project from the Property Pages Dialog. Check the radio button for "Current Project." 

	![](images/8.png)

	With this setting made, a developer can run any part of the solution without needing to run "everything" in the solution all at once. Click OK to save the settings, then select the CloudMonitR.Cloud project. 

1. Select the CloudMonitR.Web project in the Solution Explorer. Then hold down the Ctrl key and press F5 to run the web site. The web site will open in your browser. No metrics or trace will be visible in the browser until the CloudMonitR.Azure project is running. 

	![](images/11.png)

1. With the CloudMonitR.Azure project, selected in the Solution Explorer, hold down Ctrl and hit the F5 key to run the Cloud Service Application in the Windows Azure Computer Emulator. 

	![](images/10.png)

1. Once the Cloud Service project has spun up, the browser should reflect Worker Role instance(s) and web site have been connected via the SignalR Hub running in the web site. Trace data should be visible in the browser window. Additionally, the Counter Category, Counter Instance, and Counter menus should fill with options. 

	![](images/12.png)

1. Select **Processor** from the Counter Category menu. Then, select the option **_Total** from the Counter Instance menu and **% Processor Time** from the Counter menu. Click the **Add button**. 

	![](images/13.png)

1. Once the call completes, you should see a new line chart representing the Processor performance data for all of the Cloud Service instances in your application. 

	![](images/14.png)

1. As you add additional metrics to the dashboard, the charts will appear and your metric selection stored in Windows Azure Table Storage. Each subsequent visit to the CloudMonitR web dashboard will present you with the metrics already selected. 

### Running the Sample in Windows Azure
To get the site and cloud service running in Windows Azure, execute the following steps. A more comprehensive walk-through on setting up the entire application, see the [Getting Started](https://github.com/WindowsAzure-Samples/SiteMonitR/blob/master/GettingStarted.md) document for this sample. 

1. Log into the [Windows Azure Portal](http://manage.windowsazure.com). 

1. Create a new Windows Azure Storage account. 

	![](images/2.png)

1. Create a new Windows Azure Cloud Service. 

	![](images/3.png)

1. Create a new Windows Azure Web Site. 

	![](images/4.png)

1. Click the Windows Azure Web Site you just created to open the web site's dashboard. Once the web site dashboard opens in the portal, click the **Download Publish Profile** link and save the file to your local workstation. 

	![](images/15.png)

1. Open the solution in Visual Studio 2012 running in Administrator mode. This solution contains a Windows Azure Cloud Service project, so it must be opened under Administration mode or the Windows Azure Compute Emulator may fail to open properly.

1. Right-click the CloudMonitR.Web project and select **Publish** from the context menu. 

	![](images/16.png)

1. In the publish dialog that opens, click the **Import** button. Browse to the location you saved the publish settings file downloaded from the Windows Azure portal, and select the file. 

	![](images/17.png)

1. Click the Publish button to publish the web site to Windows Azure Web Sites. 

	![](images/18.png)

1. The web site will be published to Windows Azure Web Sites. Once the publish has completed, the site will open in a browser. The default start page will appear in the browser. When it does, copy the URL of the web site to the clipboard. 

	![](images/19.png)

1. Open the **CloudMonitR.Azure** project's Roles folder in the Visual Studio Solution Explorer. Double-click the CloudMonitR.WorkerRole role setting to open the properties page for the Role. Click the **Settings** tab to open the Worker Role's settings. Paste the URL of the web site copied from the web browser into the value of the **HubURL** setting. 

	![](images/20.png)

1. Click the ellipse button to set value to set the value of the **CloudMonitRConnectionString** setting.

	![](images/21.png)

1. Check the **Enter storage account credentials** radio button in the **Storage Account Connection String** dialog. 

	![](images/22.png)

1. In the Windows Azure portal, click the Storage Account created earlier to open the account's dashboard in the portal. When the dashboard page opens, click the **Manage Keys** button the bottom navigation bar of the portal. 

	![](images/23.png)

1. The Storage Account credentials dialog window will open. When it does, copy the storage account name from the dialog. 

	![](images/24.png)

1. Paste the Storage Account name into the Storage Account Connection String dialog in Visual Studio 2012. 

	![](images/25.png)

1. In the Windows Azure portal's **Manage Account Keys** dialog, copy the **Primary Access Key** to the clipboard. 

	![](images/26.png)

1. Paste the value into the Storage Account Connection String dialog in Visual Studio 2012. 

	![](images/27.png)

1. Click OK to accept the changes to the Storage Account Connection String.

1. Repeat steps 15-19 to set the **Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString** value to be the same as the **CloudMonitRConnectionString** value. When complete, the Role settings window should have the same connection string for both connection strings and should point to the Windows Azure Web Site deployed earlier for the **HubURL** setting. 

	![](images/28.png)

1. Right-click the CloudMonitR.Azure project and select **Publish** from the context menu. 

	![](images/29.png)

1. In the **Publish Windows Azure Application** dialog that opens, click the **Sign in to download credentials** link. 

	![](images/30.png)

1. When the web browser attempts to download the publishing file, click the **Save** button to save the publish file to your local workstation.

	![](images/31.png)

1. Once the file has downloaded, click the **Import** button in the publish dialog in Visual Studio and select the file from your workstation. 

	![](images/32.png)

1. Once the file is imported, click the **Next** button to move to the next step. 

1. Click the **Publish** button to publish the Cloud Service to Windows Azure. 

	![](images/33.png)

1. During the publishing process, Visual Studio 2012's Windows Azure Activity Log will provide status information about the publishing process. 

	![](images/34.png)

1. Once the publishing process completes, go back to the web browser in which the new Windows Azure Web Site is open. Manually enter the URL suffix **/CloudMonitR.html** to browse to the CloudMonitR dashboard page. 

1. Select any counters to be added to the dashboard and click the **Add** button. Each instance of the Worker Role in the Cloud Service will be visible in the metrics area, and trace output will continue to roll in the Trace Output area of the dashboard. 

![](images/35.png)

## MSDN Code Sample ###
This sample is also available on the MSDN Windows Azure Code Samples site. If you would like to download a ZIP file containing all the source code for the sample it is available [here](http://code.msdn.microsoft.com/CloudMonitR-6e224501). 