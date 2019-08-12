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
    public class fnDoScenario15 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario15()
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
			fnDumpStats DumpStats = new fnDumpStats();
        	
			//Ranorex.Unknown element = null;   
			Global.AbortScenario = false;
        	
        //*****************Start  Scenario 15 - Shift-F12 Performance Summary ******************
			Global.CurrentScenario = 15;

        	if(!Global.IndirectCall)			
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
	//			if(Global.CurrentScenario != 9999)
				{ 	
					return;
				}
			

			
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
			
			Global.LogText = @"---> fnDoScenario15 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 15 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			

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

            
while(1 == 1)
{
			Global.CurrentScenario = 15; // 11-30-18
			
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	
			
			Global.IterationsToday++;	// PAL Status Monitor		
			Global.RetechScenariosPerformed++;
			UpdatePALStatusMonitor.Run();
			
			CommonScenario14and15.Run();     
			
            
	        TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	        Global.CurrentMetricDesciption = @"Scenario 15 Loop";
	        Global.Module = "Total Time";                
	        DumpStatsQ4.Run();  	
	            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();                 
            
            //  Dump stats for all scenarios to the stats file
			Global.CurrentScenario = 0;            	
            if(Global.DoDumpStats) 
            { 	DumpStats.Run(); 
            }
	        	 	
	        Global.CurrentIteration++;            
			
			
}
			
		// ***********End Scenario 15*****************        	
        }
    }
}
