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
    public class fnDoScenario14 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario14()
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
			fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();     
			FnCommon14and15 CommonScenario14and15 = new FnCommon14and15();
        	
			Ranorex.Unknown element = null;  
			Global.AbortScenario = false;
        	
        //*****************Start  Scenario 14 - Shift-F12 Performance Summary ******************
			Global.CurrentScenario = 14;

        	if(!Global.IndirectCall)			
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
	//			if(Global.CurrentScenario != 9999)
				{ 	
					return;
				}
			
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
			Stopwatch MystopwatchModuleTotal = new Stopwatch();		
			MystopwatchModuleTotal.Reset();
			MystopwatchModuleTotal.Start();	 				
			
			Stopwatch MystopwatchQ4 = new Stopwatch();				
			
			Global.LogText = @"---> fnDoScenario14 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 14 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			

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
	        Global.Module = "Dashboard:";                
	        DumpStatsQ4.Run();  			
			
	        MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	

            repo.BackOffice275111HomeScreen.Self.Click();

			Global.LogText = @"Clicking Shift-F12 Performance Summary";
			WriteToLogFile.Run(); 	
            Keyboard.Press("{LShiftKey down}{F12}{LShiftKey up}");
            
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();      
			Global.LogText = @"Waiting for UserID to be enabled";
			WriteToLogFile.Run(); 				
			while(!repo.ShellRoot.TxtUserId.Enabled)
            {	Thread.Sleep(100);
            }					

            Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Load Dashboard login";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();     
            
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();              

			Global.LogText = @"Log into Back Office";
			WriteToLogFile.Run();            
            repo.ShellRoot.TxtUserId.TextValue = "psu";
            repo.ShellRoot.TxtPassword.TextValue = "advanced";
            Keyboard.Press("{Return}");
            
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Log in";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();  
            
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();              

            CommonScenario14and15.Run();
            
			// All Done
			Global.LogText = "Press Alt-F4 to exit Dashboard";
	        Keyboard.Press("{Alt down}{F4}{Alt up}");
	        

	        while(!repo.BackOffice275111HomeScreen.Self.Enabled)
	        {	Thread.Sleep(100);
	        }       
	        
			Global.TempFloat = (float) MystopwatchModuleTotal.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Module Total Time";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();  	        
			
			TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 14";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();   	           

            
			Global.LogText = "<--- fnDoScenario14 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 14 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));	
            
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
			
		// ***********End Scenario 14*****************        	
        }
    }
}
