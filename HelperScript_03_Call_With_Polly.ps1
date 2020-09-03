# Set the number of thread that will call the service
$numParallelJobs = 10

try 
{
  $totalRepeats = 3
  $repeatCount = 0
  while ($repeatCount -lt $totalRepeats)
  {
    $i = 1

    while($i -le $numParallelJobs)
    {
      Start-Job -ScriptBlock {
        Param($i)
        "Processing request {0}" -f $i

        $resp = try { Invoke-WebRequest -Uri http://localhost:5450/test/polly -Method GET } catch { $_.Exception.Response }
        $respBody = $resp.Content
         
        "Received response {0}: Status: {1}, Body: {2}" -f $i, $resp.StatusCode, $respBody
      } 
      
      #Start-Sleep 0.1
      
      $i++
    } 

    #Wait for all jobs
    Get-Job | Wait-Job

    #Get all job results
    Get-Job | Receive-Job

    Get-Job | Remove-Job
    
    $repeatCount++
  }
}
 finally {
    Get-Job | Remove-Job
}