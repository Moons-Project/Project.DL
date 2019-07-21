$mypath = Get-Location


# # CMD version
# set mypath=%~dp0

# "C:\App\Unity\Editor\2019.1.0f2\Editor\Unity.exe" ^
#   -batchmode ^
#   -logFile stdout.log ^
#   -projectPath %mypath% ^
#   -executeMethod Builder.build

# $job = Start-Job -Name "Proc1" -ScriptBlock { 
  # &"C:\App\Unity\Editor\2019.1.0f2\Editor\Unity.exe" `
  # -batchmode `
  # -logFile stdout.log `
  # -projectPath $mypath `
  # -executeMethod Builder.build | Out-Null
# }

# Wait-Job -Job $job

&"C:\App\Unity\2019.1.10f1\Editor\Unity.exe" `
-batchmode `
-logFile stdout.log `
-projectPath $mypath `
-executeMethod Builder.build | Out-Null