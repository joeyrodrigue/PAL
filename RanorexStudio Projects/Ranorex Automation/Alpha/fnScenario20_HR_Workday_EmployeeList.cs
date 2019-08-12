/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:03 AM
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
    /// Description of fnDoScenario3.
    /// </summary>
    [TestModule("5372A447-AB16-4A86-8BD0-976B858B269C", ModuleType.UserCode, 1)]
    public class fnDoScenario20 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario20()
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
        {	Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
        	RanorexRepository repo = new RanorexRepository();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor();  
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();   
        	
			Ranorex.Unknown element = null;
			Global.AbortScenario = false;
        	
        //*****************Start  Scenario 20 - HR Workday Employee List ******************
			Global.CurrentScenario = 20;

        	if(!Global.IndirectCall)			
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
	//			if(Global.CurrentScenario != 9999)
				{ 	
					return;
				}
        	
        	Global.ScenarioExecuted = true;
			
			Global.RetechScenariosPerformed++;
			UpdatePALStatusMonitor.Run();
			
			if(Global.CombinedIPOS)
			{
				repo.GoToBackOffice.Click();
				Thread.Sleep(200);
			}				
			
           	// Create new stopwatch
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	
			
			Stopwatch MystopwatchQ4 = new Stopwatch();		

			Global.LogText = @"---> fnDoScenario20 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 20 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			

							
			Stopwatch MystopwatchModuleTotal = new Stopwatch();		
			MystopwatchModuleTotal.Reset();
			MystopwatchModuleTotal.Start();	             
            
            if(Global.DomesticRegister)
            {
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start(); 	
            
	            Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Click item 'IPOS270141HomeScreen.Element1' at 269;333.", repo.BackOffice275111HomeScreen.BackOffice275111HomeScreenInfo, new RecordItemIndex(0));
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();    
				Global.LogText = @"Clicking on Back Office link";
				WriteToLogFile.Run(); 				
	            repo.BackOffice275111HomeScreen.BackOffice275111HomeScreen.Click();
				Global.LogText = @"Waiting for Back Office home screen";
				WriteToLogFile.Run();               
	            while(!repo.BackOffice275111HomeScreen.BackOffice275111HomeScreen.Enabled)
	            {	Thread.Sleep(100);
	            }
	            
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
		        Global.CurrentMetricDesciption = "Load Back Office";
		        Global.Module = "Employee List:";                
		        DumpStatsQ4.Run();  			
            }
	        
	        
	        
	        MystopwatchQ4.Reset();
			MystopwatchQ4.Start();	            

			Global.LogText = @"Clicking on F7 BackOffice";
			WriteToLogFile.Run();    

			if(Global.DomesticRegister)
            	repo.BackOffice275111HomeScreen.BackOffice275111HomeScreen.PressKeys("{F7}");
			else
			{
	            repo.InternationalHomeScreen.Click();
	            Delay.Milliseconds(50);	
	            Keyboard.Press("{F7}");            
			}
				
            Thread.Sleep(100);
			Global.LogText = @"logging in"; 
			WriteToLogFile.Run();    			
            Keyboard.Press("psu");
            Thread.Sleep(100);
            Keyboard.Press("advanced{Return}");
			Global.LogText = @"Waiting for BackOffice menu"; 
			WriteToLogFile.Run(); 			
           	while(!Host.Local.TryFindSingle(repo.ButtonF8HumanResourcesInfo.AbsolutePath.ToString(), out element))	
           	{
            	Thread.Sleep(100);
            }  
           	
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "[F7] Back Office and login";
	        Global.Module = "Employee List:";                
	        DumpStatsQ4.Run();  			
			
	        
	        
	        
	        MystopwatchQ4.Reset();
			MystopwatchQ4.Start();	           	
           	
			Global.LogText = @"Clicking on F8 Human Resources";
			WriteToLogFile.Run(); 
			while(!repo.ButtonF8HumanResources.Enabled)
			{ Thread.Sleep(100); }
			repo.ButtonF8HumanResources.Click();
			Global.LogText = @"Waiting for HR Functions Menu";
//			while(!repo.BackOffice.ButtonF8HumanResources.Enabled)
//			{ Thread.Sleep(100); }
			WriteToLogFile.Run();            
           	while(!Host.Local.TryFindSingle(repo.HRSelectDialog.HRFunctionsWorkdayF1Info.AbsolutePath.ToString(), out element))	
           	{
            	Thread.Sleep(100);
            }             	
           	
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "[F8] Display HR Menu";
	        Global.Module = "Employee List:";                
	        DumpStatsQ4.Run();  			
			
	        
	        
	        
	        MystopwatchQ4.Reset();
			MystopwatchQ4.Start();	           	
           	
			Global.LogText = @"Clicking on F1 Workday";
			WriteToLogFile.Run();   
			while(!repo.HRSelectDialog.HRFunctionsWorkdayF1.Enabled)
			{ Thread.Sleep(100); }
            repo.HRSelectDialog.HRFunctionsWorkdayF1.Click();
			Global.LogText = @"Waiting for Workday Emplayee List";
			WriteToLogFile.Run();            
           	while(!Host.Local.TryFindSingle(repo.EmployeeList.LblTitleInfo.AbsolutePath.ToString(), out element))	
           	{
            	Thread.Sleep(100);
            }    
           	
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "display eMail listing";
	        Global.Module = "Employee List:";                
	        DumpStatsQ4.Run();  			
           	
	        
	        
	        
	        MystopwatchQ4.Reset();
			MystopwatchQ4.Start();	

            repo.EmployeeList.LblTitle.PressKeys("{Escape}"); // Employee List
            Thread.Sleep(100);
            
            repo.HRSelectDialog.HRFunctionsMenu.PressKeys("{Escape}"); // HR Functions
            Thread.Sleep(100);
            
            repo.BackOffice.BackOffice.PressKeys("{Escape}"); // BackOffice Menu
            Thread.Sleep(100);   
            
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "ESC three screens";
	        Global.Module = "Employee List:";                
	        DumpStatsQ4.Run();  		            

            Global.TempFloat = (float) MystopwatchModuleTotal.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Module Total Time";
            Global.Module = "Employee List:";                
            DumpStatsQ4.Run();   
            
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 20";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario20 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 20 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));	
            
	     	if(Global.CombinedIPOS)
			{
	     		repo.BackOffice275111HomeScreen.Self.Focus();
	     		repo.BackOffice275111HomeScreen.Self.Click();
	     		Keyboard.Press("{F5}");
	     		Thread.Sleep(200);
				if(Host.Local.TryFindSingle(repo.ReservationDeposit.WrongBusinessDateInfo.AbsolutePath.ToString(), out element))
	     		{
	     			Keyboard.Press("{F5}");
	     			Thread.Sleep(200);
	     		}	     		
			}                
			
			return;
			
		// ***********End Scenario 20*****************        	
        }
    }
}
