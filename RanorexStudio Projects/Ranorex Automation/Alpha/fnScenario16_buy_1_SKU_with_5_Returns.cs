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
    public class fnDoScenario16 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario16()
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
        {	//*****************Start  Scenario 16 - Retech - Returns ******************
        	
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
        	FnEnterSKU EnterSKU = new FnEnterSKU();

        	Global.CurrentScenario = 16;

	       	if(!Global.IndirectCall)
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
				{ 	return;
				}
	       	
	       	Global.ScenarioExecuted = true;
			
			Global.RetechScenariosPerformed++;
			UpdatePALStatusMonitor.Run();
			
           	// Create new stopwatch
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	
			
			Stopwatch MystopwatchF1 = new Stopwatch();				
			
			Stopwatch MystopwatchTotal = new Stopwatch();	
			MystopwatchTotal.Reset();
			MystopwatchTotal.Start();
			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();				
			
			Global.LogText = @"---> fnDoScenario16 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 16 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
			// Start 			
            
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	 	

			StartTransaction.Run();

			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	 
			
			Global.LogText = @"Add Item";
			WriteToLogFile.Run(); 	
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();			

			if(Global.DomesticRegister)
			{
				// Press F1 add item
				Keyboard.Press("{F1}");
	            MystopwatchF1.Reset();	    
				MystopwatchF1.Start();			
				while(!Host.Local.TryFindSingle(repo.AddItemTextInfo.AbsolutePath.ToString(), out element))				
				{	Thread.Sleep(100);  }
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				Global.CurrentMetricDesciption = "[F1] wait for add item";
				Global.Module = "F1 Add Item";                
				DumpStatsQ4.Run();  	
				Global.CurrentMetricDesciption = "Module Total Time";
				DumpStatsQ4.Run();	
			}
			else
			{
				repo.IPOSScreen.Self.Click();
			}

			Global.CurrentSKU = Global.S17Trade5;
			EnterSKU.Run();
			
// 8-1-18			var activityRoot = repo.Retech.ActivityRoot;

			// Press returns
			if(Global.DomesticRegister)
			{
//				while(!repo.Retech.ReturnsF4.Enabled) // 7-23-18
//					{	Thread.Sleep(100); }
//				repo.Retech.ReturnsF4.Click();
				Keyboard.Press("{F4}");				
				Thread.Sleep(100);
			}
			else	// International
			{
				repo.IPOSInternationalEnterSKUField.Text.PressKeys("{F3}");  // Returns
			//	while(!repo.FormPOS1.ReturnSKuField.Enabled) // 3/25/19
				while(!repo.CodeF4Password.Enabled)
				{	Thread.Sleep(100); }
			}
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();				
			
			for (int RetNum = 1; RetNum <= 5; RetNum++)
			{
				if(Global.DomesticRegister)
				{
					// select add item and add the SKU
					while(!repo.AddLineItemCommand.Enabled)
					{  Thread.Sleep(100); }
					Thread.Sleep(100);
					// repo.AddLineItemCommand.Click(); 1-21-19
					Keyboard.Press("{F1}");
					Thread.Sleep(100);
					repo.AddItemText.TextValue = Global.S4Sku1;
					Keyboard.Press("{Enter}");
					
	            	// Select Defective for retrun reason
	            	//repo.ReturnReason.Click();  1-21-19
	            	repo.ShellView.Rectangle.Click();
	            	Thread.Sleep(200);
					//repo.Defective.Click();  1-21-19
					repo.Source.Defective.Click();
					Thread.Sleep(100);	
					
					//Click on Scan or select receipt and select no receipt
					repo.Retech.SelectReceipt.Click();
					Thread.Sleep(100);			
	            	repo.Retech.NoReceiptAvailableCommand.Click(); 
	            	Thread.Sleep(200);
	            
		            // Press F5 continue
		            Keyboard.Press("{F5}");
				}
				else// International
				{
		            while(!repo.CodeF4Password.Visible)
		            { Thread.Sleep(100); }
		            
		            repo.CodeF4Password.Click("72;8");
		            Delay.Milliseconds(200);
		            
		            repo.CodeF4Password.PressKeys("238325{Return}");

		            Keyboard.Press("n{Return}");

		            Keyboard.Press("{F5}");
		            
		            Keyboard.Press("{F12}");
					
//					repo.InternationalReturnSKUField.TextValue = "238325";	// Enter SKU to return
//					repo.InternationalReturnSKUField.PressKeys("{Return}");
//					
//					repo.ItemSearch.Self.PressKeys("n");	// no box not opened
//					while(!repo.ItemSearch.Self.Enabled) Thread.Sleep(100);
//					repo.ItemSearch.Self.PressKeys("{Return}"); // return defective
//					while(!repo.ItemSearch.Self.Enabled) Thread.Sleep(100);
//					repo.ItemSearch.Self.PressKeys("{F5}");		// no receipt
//					while(!repo.ItemSearch.Self.Enabled) Thread.Sleep(100);			
//					repo.ItemSearch.Self.PressKeys("{F12}");	// OK
//					while(!repo.ItemSearch.Self.Enabled) Thread.Sleep(100);
				}
			}
			
			if(!Global.DomesticRegister)		// if international to final apply for returns
				Keyboard.Press("{F12}");
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
			Global.CurrentMetricDesciption = "Enter 5 Returns";
			Global.Module = "Returns";                
			DumpStatsQ4.Run();  	
					
			Global.CurrentMetricDesciption = "Module Total Time";
			DumpStatsQ4.Run();					
			

			// @#@#@# C H E C K O U T #@#@#@	
			Global.PayWithMethod = "Cash";
			Checkout.Run(); 
		
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 16";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario16 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 16 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
			
            Thread.Sleep(2000);
			
			// ***********End Scenario 16*****************        	
        }
    }
}
