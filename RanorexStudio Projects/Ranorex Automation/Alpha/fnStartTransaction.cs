/*
 * Created by Ranorex
 * User: storeuser
 * Date: 02/19/15
 * Time: 2:21 PM 
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Alpha
{
    /// <summary>
    /// Description of FnStartTransaction.
    /// </summary>
    [TestModule("D06C80B6-AB80-4A4F-949D-6EB8E7C66D2C", ModuleType.UserCode, 1)]
    public class FnStartTransaction : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnStartTransaction()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
        }
        
        public void Run()
        {	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;    
            
			Ranorex.Unknown element = null;            
            
        	RanorexRepository repo = new RanorexRepository(); 
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4(); 
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();   
        	
           	// Create new stopwatch
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	

			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	       	

			Global.LogText = @"Start Transaction";
			WriteToLogFile.Run(); 	

			MystopwatchQ4.Reset();	
			MystopwatchQ4.Start();	
			bool StatsWritten = false;			

			if(Global.DomesticRegister)
			{
 				Global.LogText = @"Clicking on ReTech screen then press F5";
	            WriteToLogFile.Run();
	            repo.ReTechStartTransaction.PressKeys("{F5}");  
	            Thread.Sleep(100);

				if(Host.Local.TryFindSingle(repo.GenericDialogView.CriticalErrorSavingTransactionCallHInfo.AbsolutePath.ToString(), out element))	
				{
					repo.GenericDialogView.ErrorSavingButtonOK.Click();
					Thread.Sleep(200);
					Global.TempErrorString = "Error Saving Transaction";
					WriteToErrorFile.Run();
					Global.LogText = Global.TempErrorString;
					WriteToLogFile.Run();	
				}	
				
 				Global.LogText = @"Waiting for logon";
	            WriteToLogFile.Run();
				while(!Host.Local.TryFindSingle(repo.RetechLoginView.TxtPasswordInfo.AbsolutePath.ToString(), out element))			
				{	
	            	Thread.Sleep(200);
	            	float MetricTimeDiv1000 = MystopwatchQ4.ElapsedMilliseconds / 1000;
					if(MetricTimeDiv1000 > 20.0 && !StatsWritten)
					{
						StatsWritten = true;
		 				Global.LogText = @"Process Stats captured waiting for TryFindSingle([F5] Start Transaction TryFindSingle(repo.RetechLoginView.TxtPassword";
			            WriteToLogFile.Run();							
	            
        				// Write out Tasklist to file in C:\PAL\Reports String.Empty
						string TimeStampPart = System.DateTime.Now.ToString();
						TimeStampPart = Regex.Replace(TimeStampPart, @"[/]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[:]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[ ]", "");
						TimeStampPart = "_" + TimeStampPart;						
						string FullCsvFilename = Global.RegisterName;
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[-]", "R");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[(]", "_");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[)]", "_");	
						string CsvFilename = FullCsvFilename + TimeStampPart;
						FullCsvFilename = FullCsvFilename + TimeStampPart + "_F5_Start_Transaction_pressed_waiting_for_while(!Host.Local.TryFindSingle(repo.RetechLoginView.TxtPassword...)";
	
						var proc = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\GetTaskList.bat",
						        Arguments = FullCsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc.Start();	

						
						var proc2 = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\CPU.bat",
						        Arguments = CsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc2.Start();	
						
					}
  
					if(Host.Local.TryFindSingle(repo.GenericDialogView.CriticalErrorSavingTransactionCallHInfo.AbsolutePath.ToString(), out element))	
					{
						repo.GenericDialogView.ErrorSavingButtonOK.Click();
						Thread.Sleep(200);
						Global.TempErrorString = "Error Saving Transaction";
						WriteToErrorFile.Run();
						Global.LogText = Global.TempErrorString;
						WriteToLogFile.Run();	
					}				
	            } 
			}
			else
			{
	            // Click on F5 POS Register - international
	            //repo.IPOSScreen.ContainerF7BackOffice.Click("32;55");
	            repo.IPOSScreen.Self.Click();
            	Delay.Milliseconds(200);
            	Keyboard.Press("{F5}");

				while(!repo.LogOn.InternationalLoginID.Enabled)			
				{	Thread.Sleep(100);	} 
				
				repo.LogOn.InternationalLoginID.PressKeys("psu");
			}
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"[F5] Start Transaction";
            Global.Module = "Log On";                
            DumpStatsQ4.Run();              
            
			// Enter Password
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			StatsWritten = false;
        
			if(Global.DomesticRegister)
				{
 				Global.LogText = @"Waiting for Password field to appear";
	            WriteToLogFile.Run();	

	            while(!Host.Local.TryFindSingle(repo.ReTechLogonPasswordInfo.AbsolutePath.ToString(), out element))	 	            
				{	
	            	Thread.Sleep(100);
	            	float MetricTimeDiv1000 = MystopwatchQ4.ElapsedMilliseconds / 1000;
					if(MetricTimeDiv1000 > 20.0 && !StatsWritten)
					{
						StatsWritten = true;
		 				Global.LogText = @"Process Stats captured waiting for (!Host.Local.TryFindSingle(repo.ReTechLogonPassword...";
			            WriteToLogFile.Run();						
	            
        				// Write out Tasklist to file in C:\PAL\Reports String.Empty
						string TimeStampPart = System.DateTime.Now.ToString();
						TimeStampPart = Regex.Replace(TimeStampPart, @"[/]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[:]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[ ]", "");
						TimeStampPart = "_" + TimeStampPart;						
						string FullCsvFilename = Global.RegisterName;
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[-]", "R");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[(]", "_");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[)]", "_");	
						string CsvFilename = FullCsvFilename + TimeStampPart;
						FullCsvFilename = FullCsvFilename + TimeStampPart + "_waiting_for_(!Host.Local.TryFindSingle(repo.ReTechLogonPassword...)";
	
						var proc = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\GetTaskList.bat",
						        Arguments = FullCsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc.Start();	
						
						var proc2 = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\CPU.bat",
						        Arguments = CsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc2.Start();	
						
					}	            	
	            }

 				Global.LogText = @"Waiting for Password field to be enabled";
	            WriteToLogFile.Run();	
	            while(!repo.ReTechLogonPassword.Enabled)
				{	
	            	Thread.Sleep(100);
	            	float MetricTimeDiv1000 = MystopwatchQ4.ElapsedMilliseconds / 1000;
					if(MetricTimeDiv1000 > 20.0 && !StatsWritten)
					{
						StatsWritten = true;
		 				Global.LogText = @"Process Stats captured waiting for repo.ReTechLogonPassword.Enabled";
			            WriteToLogFile.Run();						
	            
        				// Write out Tasklist to file in C:\PAL\Reports String.Empty
						string TimeStampPart = System.DateTime.Now.ToString();
						TimeStampPart = Regex.Replace(TimeStampPart, @"[/]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[:]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[ ]", "");
						TimeStampPart = "_" + TimeStampPart;						
						string FullCsvFilename = Global.RegisterName;
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[-]", "R");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[(]", "_");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[)]", "_");
						string CsvFilename = FullCsvFilename + TimeStampPart;						
						FullCsvFilename = FullCsvFilename + TimeStampPart + "_waiting_for_while(!repo.ReTechLogonPassword.Enabled)";
	
						var proc = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\GetTaskList.bat",
						        Arguments = FullCsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc.Start();	
						
						var proc2 = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\CPU.bat",
						        Arguments = CsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc2.Start();	
						
					}	            	
	            }
	            Global.LogText = @"Entering password";
	            WriteToLogFile.Run();	
				repo.RetechLoginView.TxtPassword.PressKeys("advanced{Return}");
	            Global.LogText = @"Password entered - wating for Add Line F1";
	            WriteToLogFile.Run();	
	            Thread.Sleep(500);

           
	            // ############################################################
	            // If login takes longer than 20 seconds write out process stats
	            while(!Host.Local.TryFindSingle(repo.AddLineItemCommandInfo.AbsolutePath.ToString(), 1000, out element))
				{
	            	Thread.Sleep(200);
	            	float MetricTimeDiv1000 = MystopwatchQ4.ElapsedMilliseconds / 1000;
					if(MetricTimeDiv1000 > 20.0 && !StatsWritten)
					{
						StatsWritten = true;
		 				Global.LogText = @"Process Stats captured waiting for TryFindSingle(repo.AddLineItemCommandInfo";
			            WriteToLogFile.Run();							
	            
        				// Write out Tasklist to file in C:\PAL\Reports String.Empty
						string TimeStampPart = System.DateTime.Now.ToString();
						TimeStampPart = Regex.Replace(TimeStampPart, @"[/]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[:]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[ ]", "");
						TimeStampPart = "_" + TimeStampPart;						
						string FullCsvFilename = Global.RegisterName;
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[-]", "R");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[(]", "_");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[)]", "_");	
						string CsvFilename = FullCsvFilename + TimeStampPart;						
						FullCsvFilename = FullCsvFilename + TimeStampPart + "_waiting_for_while(!Host.Local.TryFindSingle(repo.AddLineItemCommand...)";
	
						var proc = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\GetTaskList.bat",
						        Arguments = FullCsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc.Start();		
						
						var proc2 = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\CPU.bat",
						        Arguments = CsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc2.Start();	
						
					}
				}
				

	           	Global.LogText = @"Wating for Add Line F1 - waiting till Enabled";
	            WriteToLogFile.Run();	
	           	while(!repo.AddLineItemCommand.Enabled)
				{
	            	Thread.Sleep(200);
	            	float MetricTimeDiv1000 = MystopwatchQ4.ElapsedMilliseconds / 1000;
					if(MetricTimeDiv1000 > 20.0 && !StatsWritten)
					{
						StatsWritten = true;
		 				Global.LogText = @"Process Stats captured waiting for TryFindSingle(repo.AddLineItemCommand.Enabled";
			            WriteToLogFile.Run();							
	            
        				// Write out Tasklist to file in C:\PAL\Reports String.Empty
						string TimeStampPart = System.DateTime.Now.ToString();
						TimeStampPart = Regex.Replace(TimeStampPart, @"[/]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[:]", "");	
						TimeStampPart = Regex.Replace(TimeStampPart, @"[ ]", "");
						TimeStampPart = "_" + TimeStampPart;						
						string FullCsvFilename = Global.RegisterName;
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[-]", "R");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[(]", "_");	
						FullCsvFilename = Regex.Replace(FullCsvFilename, @"[)]", "_");							
						string CsvFilename = FullCsvFilename + TimeStampPart;						
						FullCsvFilename = FullCsvFilename + TimeStampPart + "_waiting_for_while(!repo.AddLineItemCommand.Enabled)";						
	
						var proc = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\GetTaskList.bat",
						        Arguments = FullCsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc.Start();	
						
						var proc2 = new Process
						{
						    StartInfo = new ProcessStartInfo
						    {
						        FileName = Global.Register1DriveLetter + @":\PAL\Reports\CPU.bat",
						        Arguments = CsvFilename,	
						        UseShellExecute = false,
						        RedirectStandardOutput = true,
						        CreateNoWindow = true,
						        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
						    }
						};	
						
						proc2.Start();	
						
					}	           		
					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.ContinueButtonCommand.Click();
						Thread.Sleep(200);						
					}
	           	}
	            Global.LogText = @"Add Line F1 - found enabled";
	            WriteToLogFile.Run();		           	
	           	
	           	// If Customer Info then just skip by continue
	           	if(Host.Local.TryFindSingle(repo.Retech.ContinueButtonCommand2Info.AbsolutePath.ToString(), out element))
	           	{
	           		repo.Retech.ContinueButtonCommand2.Click();
	           		Thread.Sleep(100);
	           	}

			}
			else
			{  // International
				repo.LogOn.InternationalPassword.PressKeys("advanced{Return}");
	           	//while(!repo.ReservationDeposit.EnterSerialNumber.Enabled)
				//{	Thread.Sleep(100);	} 
	           	//repo.ReservationDeposit.EnterSerialNumber.PressKeys("{Escape}");
	           	//repo.FormPOS.Self.PressKeys("{Escape}");
	           	Thread.Sleep(100);
	           	repo.NotEnrolledEsc.Click();
	           	Thread.Sleep(100);
			}

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "Enter Password";
            Global.Module = "Log On";                
            DumpStatsQ4.Run();          
            
            TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Log On";                   
            DumpStatsQ4.Run();   
         	
        }
        
    }
}
