Running both server and client in PowerShell
.\UnitySandboxNetcode.exe -mode server ; .\UnitySandboxNetcode.exe -mode client

With custom logs:
.\UnitySandboxNetcode.exe -logfile log-server.txt -mode server & .\UnitySandboxNetcode.exe -logfile log-client.txt -mode client
