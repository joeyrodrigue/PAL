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
    public class fnDoScenario35 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario35()
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
        {	//*****************Start  Scenario 35 - Retech - Simple One SKU pay with PURCC ******************
        	
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
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor();  
        	FnCheckout Checkout = new FnCheckout();
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
        	FnEnterSKU EnterSKU = new FnEnterSKU();

        	Global.CurrentScenario = 35;
 			
			if (!Global.DoScenarioFlag[Global.CurrentScenario])	
//            if(Global.CurrentScenario != 9999)       					
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
			
			Global.LogText = @"---> fnDoScenario35 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 35 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
			// Start 
			
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	 				
            
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();      
			
			Global.LogText = @"Start Transaction";
			WriteToLogFile.Run(); 		

            // Click on start a transaction
            while(!Host.Local.TryFindSingle(repo.Retech.StartATransactionInfo.AbsolutePath.ToString(), out element))
            {
            	Thread.Sleep(100);
            }            
            repo.Retech.StartATransaction.Click();
			if(Host.Local.TryFindSingle(repo.GenericDialogView.CriticalErrorSavingTransactionCallHInfo.AbsolutePath.ToString(), out element))	
			{
				repo.GenericDialogView.ErrorSavingButtonOK.Click();
				Thread.Sleep(200);
				Global.TempErrorString = "Error Saving Transaction";
				WriteToErrorFile.Run();
				Global.LogText = Global.TempErrorString;
				WriteToLogFile.Run();	
			}	            
			MystopwatchF1.Reset();
			MystopwatchF1.Start();
			while(!Host.Local.TryFindSingle(repo.RetechLoginView.TxtPasswordInfo.AbsolutePath.ToString(), out element))			
			{	
            	Thread.Sleep(100);	
				if(MystopwatchF1.ElapsedMilliseconds > 1500)
				{
					repo.Retech.StartATransaction.Click();
					MystopwatchF1.Reset();	
					MystopwatchF1.Start();	
				}            	
            }
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"[F5] Start Transaction";
            Global.Module = "Log On";                
            DumpStatsQ4.Run();               
            
			// Enter Password
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	 			
//            if(repo.RetechLoginView.UserId.TextValue == "")
//            {
//            	repo.RetechLoginView.UserId.TextValue = "PSU";
//            }            
			repo.RetechLoginView.TxtPassword.PressKeys("advanced{Return}");
			
			switch (Global.RetechVersion.Substring(0,3))
			{
				case "5.6":
           			while(!repo.Retech.LetsGetStarted_5_6_0_103.Visible)
					{	Thread.Sleep(100);	}  					
					break;
				case "5.7":
           			while(!repo.AddLineItemCommand.Enabled)
					{	Thread.Sleep(100);	}  					
					break;	
				default:
           			while(!repo.AddLineItemCommand.Enabled)
					{ 
						if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
						{
							repo.ContinueButtonCommand.Click();
						}					
						Thread.Sleep(100); 
					}					
					break;						
			}			

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "Enter Password";
            Global.Module = "Log On";                
            DumpStatsQ4.Run();   
            
            TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Log On";                   
            DumpStatsQ4.Run();     
            
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	      
			
			Global.LogText = @"Add Item";
			WriteToLogFile.Run(); 					

			// Press F1 add item
			Keyboard.Press("{F1}");
			while(!repo.AddItemText.Enabled)
			{	Thread.Sleep(100);	}
			// Enter SKU
			Global.CurrentSKU = Global.S4Sku1;
			EnterSKU.Run();
			
			// Press F1 add item
			Keyboard.Press("{F1}");
			while(!repo.AddItemText.Enabled)
			{	Thread.Sleep(100);	}			
			// Enter SKU cost 1 penny
			Global.CurrentSKU = "644917";
			EnterSKU.Run();			
			
			// Return the SKU
			MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
			// Press returns
			repo.Retech.ReturnsF4.Click();
			Thread.Sleep(100);			
				
			// select add item and add the SKU
			repo.AddLineItemCommand.Click();
			//Thread.Sleep(100);
			repo.AddItemText.TextValue = Global.S4Sku1;
			Keyboard.Press("{Enter}");
				
            // Select Defective for retrun reason
	        repo.ReturnReason.Click("246;16");
	        repo.Retech.ListItemGameStopOESOrdersItemsRetur.Click("153;6");
	        repo.ReturnReason.PressKeys("{LControlKey down}{LMenu down}");
	        repo.ReturnReason.PressKeys("{LMenu up}{LControlKey up}");
	        repo.Retech.DataContextShowCheckoutViewCommand.Click("93;62");
	        repo.Retech.DataContextShowCheckoutViewCommand.Click("95;56");
				
			//Click on Scan or select receipt and select no receipt
			repo.Retech.SelectReceipt.Click("369;20");
            repo.Retech.NoReceiptAvailableCommand.Click("265;11");
            repo.Retech.SelectReceipt.PressKeys("{LControlKey down}{LMenu down}");
            repo.Retech.SelectReceipt.PressKeys("{LControlKey up}{LMenu up}");
            
	        // Press F5 continue
	        Keyboard.Press("{F5}");
	            
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Enter Return";
	        Global.Module = "Add Return";                
	        DumpStatsQ4.Run(); 				
			
            
            // @#@#@# C H E C K O U T #@#@#@	
			Global.PayWithMethod = "PURCC";
			Checkout.Run();

            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 35";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
           
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario35 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 35 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
            
            Thread.Sleep(2000);            
			
			// ***********End Scenario 35*****************        	
        }

    }
}
