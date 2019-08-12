/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:13 AM
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
    /// Description of fnDoScenario6.
    /// </summary>
    [TestModule("5673A555-C55D-4496-AD46-575A24E7D994", ModuleType.UserCode, 1)]
    public class fnDoScenario36 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario36()
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
        {	//**************Start  Scenario 36 - Customer Lookup - Item Lookup - Void transaction**********************
			
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
			RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();         	
        	fnWaitForItemSearchToFinish WaitForItemSearchToFinish = new fnWaitForItemSearchToFinish();   
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();   
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor(); 
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
			
        	Ranorex.Unknown element = null;
        	Global.AbortScenario = false;
        	
            Global.CurrentScenario = 36;

          	if(!Global.IndirectCall)
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
				{ 	
					return;
				}
          	
          	Global.ScenarioExecuted = true;
			
			Global.RetechScenariosPerformed++;
			UpdatePALStatusMonitor.Run();

			Global.DoDumpStats = true;
			Global.PhoneNumbertype = ""; // all types					
			
			Global.LogText = "---> fnDoScenario36 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();				
            Report.Log(ReportLevel.Info, "Scenario 36 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));

           	// Create new stopwatch
			Stopwatch Mystopwatch = new Stopwatch();	
			Mystopwatch.Reset();
			Stopwatch MystopwatchQ4 = new Stopwatch();	
			MystopwatchQ4.Reset();	
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();	
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();		
			
			Global.LogText = @"Search for xbox";
			WriteToLogFile.Run(); 		

			int NumberRecordsFound = 0;
            MystopwatchQ4.Reset();	
			if(Global.DomesticRegister)
			{
				// Search for XBOX Description
				Thread.Sleep(50);
	            repo.Retech.FindAProductCtrlPlusS.Click("19;5");
	            Thread.Sleep(50);
	            Keyboard.Press("xbox{Return}");
	            MystopwatchQ4.Start();	     
				Global.LogText = @"Wait for search to finish";
				WriteToLogFile.Run(); 	
				while(!Host.Local.TryFindSingle(repo.RetechQuickSearchView.DisplayedCountInfo.AbsolutePath.ToString(), out element)	)
				{	Thread.Sleep(100);	}
				
	            string SearchCount = repo.RetechQuickSearchView.DisplayedCount.TextValue;
				NumberRecordsFound = Convert.ToInt32(SearchCount.Substring(0,SearchCount.IndexOf(" ")));
			}
			else	// International
			{
// 08-14-2018 			repo.IPOSHomeScreen.InternationalIPOS.PressKeys("{F10}");  // Select Item Lookup
				repo.IPOSScreen.Self.PressKeys("{F10}");
				repo.ReservationDeposit.InternationalLookupDescription.TextValue = "xbox";
				repo.ReservationDeposit.InternationalLookupDescription.PressKeys("{F12}");
				MystopwatchQ4.Start();	
				Global.LogText = @"Wait for search to finish";
				WriteToLogFile.Run(); 
				int LastNumberRecordsFound = 0;
				do 
				{	// wait for count to stop changing
					LastNumberRecordsFound = repo.InternationalSearchCount.Items.Count;
					Thread.Sleep(100);
					NumberRecordsFound = repo.InternationalSearchCount.Items.Count;
				} while (LastNumberRecordsFound != NumberRecordsFound);
				
			}

			// Q4 stats
		    Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
		    float RecordsPerSecond = NumberRecordsFound / Global.TempFloat;
		    Global.Q4StatLine = RecordsPerSecond.ToString("R");	   
	        Global.CurrentMetricDesciption = @"Records/Second";
	        Global.Module = "Item Search";                
	        DumpStatsQ4.Run();     
	            
            Keyboard.Press("{Escape}");		
            
            TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Module Total Time";            
            Global.Module = "Item Search";                
            DumpStatsQ4.Run();  	
            
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 36";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();                
			
			Global.LogText = "<--- fnDoScenario36 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	
            Report.Log(ReportLevel.Info, "Scenario 36 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
			
            //***********End Scenario 36***************        	
        }
    }
}
