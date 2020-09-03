### Description
While using Polly with the RetryAndWaitAsync() extension, we started receiving notifications that socket depletion was happening on our staging servers.  
After investigations, it was identified that these connections originate from the Polly Policy WaitAndRetryAsync.

This repository is here to show this problem and help the devs.

### How to reproduce
* Clone the repository locally.
* You can either use the Powershell script to Build&Run (_HelperScript_01_Build_And_Run.ps1_) the application or load the solution in Visual Studio and run it from there.
* Ensure that the program is running and listening on Port 5450
* Execute the Script _HelperScript_02_Call_Without_Polly.ps1_. This will call the controller that will execute http requests without the Polly-Logic applied.
* Wait atleast 2 Minutes, but 3 Minutes are better.
* Execute the Script _HelperScript_04_Show_Connections.ps1_ to show all active connections related to this application.  
You can alternatively run the comman _netstat -ano | findstr 5450_, that is all that the powershell script does.  
There should be a small number of ESTABLISHED or TIME_WAIT connections
* Execute the Script _HelperScript_03_Call_With_Polly.ps1_. This will call the controller that will execute http requests with the Polly-Logic applied.
* Wait atleast 2 Minutes, but 3 Minutes are better.
* Execute the Script _HelperScript_04_Show_Connections.ps1_ to show all active connections related to this application.  
You can alternatively run the comman _netstat -ano | findstr 5450_, that is all that the powershell script does.  
There should be a large number of ESTABLISHED or CLOSE_WAIT connections still active.