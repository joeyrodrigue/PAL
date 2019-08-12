/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13F
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
    public class fnDoScenario18 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario18()
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
        	
        //*****************Start  Scenario 18 - Transaction Journal ******************
			Global.CurrentScenario = 18;

        	if(!Global.IndirectCall)			
				if (!Global.DoScenarioFlag[Global.CurrentScenario] || !Global.DomesticRegister )	
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
			
			Global.LogText = @"---> fnDoScenario18 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 18 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			

			Stopwatch MystopwatchModuleTotal = new Stopwatch();		
			MystopwatchModuleTotal.Reset();
			MystopwatchModuleTotal.Start();	             
            
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
	        Global.Module = "Journal:";                
	        DumpStatsQ4.Run();  			
			
	        MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	            
 
            Global.LogText = @"Waiting for F4 Transaction Journal";
			WriteToLogFile.Run();             
           	while(!Host.Local.TryFindSingle(repo.BackOffice275111HomeScreen.BackOffice275111HomeScreenInfo.AbsolutePath.ToString(), out element))	
            {	
            	Thread.Sleep(100);
            }  			
            
            Global.LogText = @"Clicking on F4 Transaction Journal";
			WriteToLogFile.Run();                
            repo.BackOffice275111HomeScreen.BackOffice275111HomeScreen.PressKeys("{F4}");
            Thread.Sleep(100);
			Global.LogText = @"logging in"; 
			WriteToLogFile.Run();    			
            Keyboard.Press("psu");
            Thread.Sleep(100);
            Keyboard.Press("advanced{Return}");
            
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "[F4] View Journal and login";
	        Global.Module = "Journal:";                
	        DumpStatsQ4.Run();  			
			
	        MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	            

			Global.LogText = @"Waiting for Transaction Journal screen"; 
			WriteToLogFile.Run(); 			
			//while(!repo.MailViewer.EmailHeader.Enabled)
			//while(!Host.Local.TryFindSingle(repo.ReservationDeposit.SelectTheTransactionYouWishToRepriInfo.AbsolutePath.ToString(), out element))
			//while(!Host.Local.TryFindSingle(repo.FormPOS.TransactionJournalTitleInfo.AbsolutePath.ToString(), out element))
			//while(!Host.Local.TryFindSingle(repo.ReservationDeposit.ButtonF10JournalTapeInfo.AbsolutePath.ToString(), out element))
			//while(!Host.Local.TryFindSingle(repo.FormPOS.ButtonF10JournalTapeInfo.AbsolutePath.ToString(), out element))
			
			// NOTE different object XPath for ReTEch 7.0.0.14 and 7.0.102.30
			if(Global.RetechVersion.Substring(0,5) == "7.0.0")
			{ // 7.0.0.14
				while(!Host.Local.TryFindSingle(repo.JournalPrinterLabelsInfo.AbsolutePath.ToString(), out element))
	            {	
	            	Thread.Sleep(100);
	            }  	        
			}	
			else
			{ //7.0.102.30
				while(!Host.Local.TryFindSingle(repo.ButtonF10JournalTapeInfo.AbsolutePath.ToString(), out element))
	            {	
	            	Thread.Sleep(100);
	            }  
			}

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "display Journal listing";
	        Global.Module = "Journal:";                
	        DumpStatsQ4.Run();  			
           	
			Global.TempFloat = (float) MystopwatchModuleTotal.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Module Total Time";
            Global.Module = "Journal:";                
            DumpStatsQ4.Run();   
	        
            Keyboard.Press("{Escape}");

            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 18";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario18 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 18 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));	
            
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
			
		// ***********End Scenario 18*****************        	
        }
    }
}
