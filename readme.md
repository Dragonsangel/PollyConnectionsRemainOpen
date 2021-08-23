### Description
While using Polly with the RetryAndWaitAsync() extension, we started receiving notifications that socket depletion was happening on our staging servers.  
After investigations, it was identified that these connections originate from the Polly Policy WaitAndRetryAsync.

This repository is here to show this problem and help the devs.

The solution was created to also illustrate the problem for the issues in the [Polly](https://github.com/App-vNext/Polly/issues/790) and [dotnet/aspnetcore](https://github.com/dotnet/aspnetcore/issues/28384) repositories.

#### NOTE
~From running the test on other peoples system, it seems that this only happens when the program is run von within Visual Studio.
I have not found the reason for this.~
It seems that it is not always the case. When you run this locally, try first with the provided scripts / running it from the console. If the problem is not present, trying from Visual Studio.

### How to reproduce
* Clone the repository locally.
* You can then either use the _HelperScript_00_All_Steps.ps1_ script to run all the steps below automatically  

 or

* Use either the Powershell script to Build&Run (_HelperScript_01_Build_And_Run.ps1_) the application or load the solution in Visual Studio and run it from there.
* Ensure that the program is running and listening on Port 5450
* Execute the Script _HelperScript_02_Call_Without_Polly.ps1_. This will call the controller that will execute http requests without the Polly-Logic applied.
* Wait atleast 2 Minutes, but 4 Minutes are better.
* Execute the Script _HelperScript_04_Show_Connections.ps1_ to show all active connections related to this application.  
You can alternatively run the comman _netstat -ano | findstr 5450_, that is all that the powershell script does.  
There should be a small number of ESTABLISHED or TIME_WAIT connections
* Execute the Script _HelperScript_03_Call_With_Polly.ps1_. This will call the controller that will execute http requests with the Polly-Logic applied.
* Wait atleast 2 Minutes, but 4 Minutes are better.
* Execute the Script _HelperScript_04_Show_Connections.ps1_ to show all active connections related to this application.  
You can alternatively run the comman _netstat -ano | findstr 5450_, that is all that the powershell script does.  
There should be a large number of ESTABLISHED or CLOSE_WAIT connections still active.
