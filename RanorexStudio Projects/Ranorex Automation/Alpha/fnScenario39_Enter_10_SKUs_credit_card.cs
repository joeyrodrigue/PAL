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
    public class fnDoScenario39 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario39()
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
        {	//*****************Start  Scenario 39 - Retech - Enter 10 SKUs - credit card ******************
        	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
			Ranorex.Unknown element = null;
			Global.AbortScenario = false;

            // Uses same SKU and Customer for Scenarios 3, 4, and 5
            
        	RanorexRepository repo = new RanorexRepository();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();   
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor();   
			FnCheckout Checkout = new FnCheckout();
			FnStartTransaction StartTransaction = new FnStartTransaction();

        	Global.CurrentScenario = 39;
 			
			if (!Global.DoScenarioFlag[Global.CurrentScenario])	
			{ 	return;
			}

			Global.RetechScenariosPerformed++;
			UpdatePALStatusMonitor.Run();
			
           	// Create new stopwatch
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	
			
			Stopwatch MystopwatchTotal = new Stopwatch();	
			MystopwatchTotal.Reset();
			MystopwatchTotal.Start();
			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();	
			Stopwatch MystopwatchF1 = new Stopwatch();				
			
			Global.LogText = @"---> fnDoScenario39 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 39 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
			// Start 		
			MystopwatchModuleTotal.Reset();
			MystopwatchModuleTotal.Start();	 				

			Global.LogText = @"Start Transaction";
			WriteToLogFile.Run(); 	
			StartTransaction.Run();            

			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	     

			string[] MySKUs = new string[10];
			MySKUs[0] = Global.S9SKU1;
			MySKUs[1] = Global.S9SKU2;
			MySKUs[2] = Global.S9SKU3;
			MySKUs[3] = Global.S9SKU4;
			MySKUs[4] = Global.S9SKU5;
			MySKUs[5] = Global.S9SKU6;
			MySKUs[6] = Global.S9SKU7;
			MySKUs[7] = Global.S9SKU8;
			MySKUs[8] = Global.S9SKU9;
			MySKUs[9] = Global.S9SKU10;		
			
			MystopwatchF1.Reset();	
			MystopwatchF1.Start();		
			
			Global.LogText = @"Enter SKUs";
			WriteToLogFile.Run(); 	
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start(); 			

			for (int soff = 0; soff <= 9  ; soff++ )
			{
				// Press F1 add item
				Keyboard.Press("{F1}");
	            MystopwatchF1.Reset();	    
				MystopwatchF1.Start();			
				while(!Host.Local.TryFindSingle(repo.AddItemTextInfo.AbsolutePath.ToString(), out element))				
				{	
					Thread.Sleep(100);
					if(MystopwatchF1.ElapsedMilliseconds > 1000)
					{
						Keyboard.Press("{F1}");
						Thread.Sleep(100);	
						MystopwatchF1.Reset();	
						MystopwatchF1.Start();	
					}
				}
				
				// Enter SKU
				Global.LogText = @"Entering SKU: " + MySKUs[soff];
				WriteToLogFile.Run(); 					
				repo.AddItemText.TextValue = MySKUs[soff];
				repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");
				while(!repo.AddLineItemCommand.Enabled)
				{ 
					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.ContinueButtonCommand.Click();
					}					
					Thread.Sleep(100); 
				}

			}
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Enter SKUs";
	        Global.Module = "Enter 10 SKUs";                
	        DumpStatsQ4.Run();  			
			
	        MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
			Keyboard.Press("{F5}"); // Continue F5 Recommended Pre-Owned Items
			Thread.Sleep(200);	
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "[F5] Continue";
            Global.Module = "Enter 10 SKUs";                
            DumpStatsQ4.Run();  
            
	        MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	 
			
			Keyboard.Press("{F6}"); // I've explained these related products to the customer F6
			Thread.Sleep(100);	
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "[F6]: explain related products";
            Global.Module = "Enter 10 SKUs";                
            DumpStatsQ4.Run();  			
			
	        MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();				
			Keyboard.Press("{F5}"); // Continue F5
			Thread.Sleep(200);	
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "[F5]: GPG";
            Global.Module = "Enter 10 SKUs";                
            DumpStatsQ4.Run(); 			

			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.NowCustomerLookup = Global.AdjustedTime;            
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Enter 10 SKUs";                   
            DumpStatsQ4.Run();   
            
			// @#@#@# C H E C K O U T #@#@#@	
			Global.PayWithMethod = "Credit";
			Checkout.Run();        
            
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 39";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario39 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 39 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
            
            Thread.Sleep(2000);            
			
			// ***********End Scenario 39*****************        	
        }
    }
}
