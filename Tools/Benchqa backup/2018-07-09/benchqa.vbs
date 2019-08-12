strComputer = "." 
Dim t
Dim tcpu
Dim ccpu
Dim ctr
Dim percentdisktime
Dim Qlength
Dim DiskIdleTime
Dim PagesOutputPersec
Dim wshshell
dim machine
set wsn = createobject("wscript.network")
machine = wsn.computername
dim logfile
dim cores
cores = 1
set lcpu = createobject("scripting.dictionary")
lcpu.comparemode = 1
Set lmem = createobject("scripting.dictionary")
lmem.comparemode = 1
set lhandles = createobject("scripting.dictionary")
lhandles.comparemode = 1
set lthreads = createobject("scripting.dictionary")
lthreads.comparemode = 1
set objfso = createobject("scripting.filesystemobject")
set pfolder = objfso.getfolder("C:\PAL\Performance")
for each pfile in pfolder.files
	'wscript.echo pfile.name
	if datediff("D",pfile.datecreated,Date,0) > 14 and instr(1,pfile.name,"-perf.csv",1) then
		'wscript.echo "should delete"
		pfile.delete
	end if
next
Set WshShell = WScript.CreateObject("WScript.Shell")

if objfso.fileexists("C:\PAL\Performance\PerformanceMetrics" & machine & "-" & month(date) & "-" & day(date) & "-" &  year(date) & "-perf.csv") then
	set logfile = objfso.opentextfile("C:\PAL\Performance\PerformanceMetrics-" & machine & "-" & month(date) & "-" & day(date) & "-" &  year(date) & "-perf.csv",8,0,0)
Else
	set logfile = objfso.createtextfile("C:\PAL\Performance\PerformanceMetrics-" & machine & "-" & month(date) & "-" & day(date) & "-" &  year(date) & "-perf.csv")
	logfile.writeline "TimeStamp,Register_ID,Process,CPU,Memory(MB),threads,handles,gdiobjects,userobjects,PercentDiskTime,PagesOutputPersec"

end if

Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\CIMV2") 
Set MySink = WScript.CreateObject("WbemScripting.SWbemSink","SINK_")
Set MySink2 = WScript.CreateObject("WbemScripting.SWbemSink","SINK2_")
Set MySink3 = WScript.CreateObject("WbemScripting.SWbemSink","SINK3_")
Set MySink4 = WScript.CreateObject("WbemScripting.SWbemSink","SINK4_")
objWMIservice.ExecNotificationQueryAsync MySink,"SELECT * FROM __InstanceModificationEvent WITHIN 3 WHERE TargetInstance ISA 'Win32_PerfFormattedData_PerfOS_Processor' and targetinstance.Name='_Total'"
objWMIservice.ExecNotificationQueryAsync MySink2,"SELECT * FROM __InstanceModificationEvent WITHIN 3 WHERE TargetInstance ISA 'Win32_PerfFormattedData_PerfProc_Process' and (targetinstance.Name='_Total' or targetinstance.name like '%gamestop%' or targetinstance.name like 'PercentDiskTime%' or targetinstance.name like 'MicroHost%' or targetinstance.name like 'PerfDash%' or targetinstance.name like 'source%' or targetinstance.name like 'winlogbeat%' or targetinstance.name like 'RanorexAutomation%' or targetinstance.name like 'GS.SS.QueueProcessor%' or targetinstance.name like 'NotificationBar%' or targetinstance.name like 'pos%' or targetinstance.name like 'Cylance%' or targetinstance.name like 'xagt%' or targetinstance.name like 'BESClient%')"
objWMIservice.ExecNotificationQueryAsync MySink3,"SELECT * FROM __InstanceModificationEvent WITHIN 3 WHERE TargetInstance ISA 'Win32_PerfFormattedData_PerfDisk_PhysicalDisk' and targetinstance.Name like '%c:%'"
objWMIservice.ExecNotificationQueryAsync MySink4,"SELECT * FROM __InstanceModificationEvent WITHIN 3 WHERE TargetInstance ISA 'Win32_PerfFormattedData_PerfOS_Memory'"

while(true)
		Set Processes = objWMIService.execquery ("Select name from Win32_Process where name like '%monitor%' or name like '%polling%'")
			If Processes.count > 0 Then
				WScript.quit
			End If
	wscript.sleep 2800
	
	wshshell.run "c:\pal\tools\gdiget.exe",0,1
	
Wend

Sub SINK_OnObjectReady(objObject, objAsyncContext)
	t = t + 1
	tcpu = cdbl(objObject.targetinstance.PercentProcessorTime) + cdbl(tcpu)
	
	ccpu=objObject.targetinstance.PercentProcessorTime
	if isnull(ccpu) then
		ccpu=0
	end If
	
	'WshShell.RegWrite "HKLM\Software\GameStop\CPUMON\AvgCPU", cdbl(tcpu) / t,"REG_SZ"
	'wscript.echo 	tcpu & " / " & t & " = " & round(cdbl(tcpu/t),2)
End Sub

Sub SINK3_OnObjectReady(objti, objAsyncContext)
	percentdisktime = objti.targetinstance.PercentDiskTime
	Qlength = objti.targetinstance.avgdiskqueuelength
	DiskIdleTime = objti.targetinstance.PercentIdleTime
end Sub

Sub SINK4_OnObjectReady(objti, objAsyncContext)
	PagesOutputPersec = objti.targetinstance.PagesOutputPersec
	
End Sub

SUB SINK2_OnObjectReady(objObject,objAsyncContext)
if objObject.Targetinstance.name = "_Total" Then
	
	logfile.writeline now & "," & machine & "," & "Total," &  ccpu & "," & round(objobject.targetinstance.workingset / (1024 * 1024),0) & "," & objobject.targetinstance.threadcount & "," & objobject.targetinstance.handlecount & "," & percentdisktime & "," & Qlength & "," & DiskIdleTime & "," & PagesOutputPersec
Else
on error resume Next
gdiobjects = wshshell.regread("HKLM\Software\GameStop\GDIOBJECTS\GDIOBJECTS-" & objobject.targetinstance.name & ".exe")
userobjects = wshshell.regread("HKLM\Software\GameStop\GDIOBJECTS\userOBJECTS-" & objobject.targetinstance.name & ".exe")
logfile.writeline now & "," & machine & "," & objobject.targetinstance.name & "," &  objObject.targetinstance.PercentUserTime & "," & round(objobject.targetinstance.workingset / (1024 * 1024),0) & "," & objobject.targetinstance.threadcount & "," & objobject.targetinstance.handlecount & "," & gdiobjects & "," & userobjects

gdiobjects=""
userobjects=""
end if

end sub

Sub SINK_OnCompleted(objObject, objAsyncContext)
    'WScript.Echo "Event call complete."
End Sub
