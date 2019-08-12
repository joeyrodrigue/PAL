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
    public class fnDoScenario42 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario42()
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
        {	//*****************Start  Scenario 40 - Retech - PRP ******************
        	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
			//Ranorex.Unknown element = null; 
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
        	FnProductionIssueFunctions ProductionIssueFunctions = new FnProductionIssueFunctions();

        	Global.CurrentScenario = 42;

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
			
			Global.LogText = @"---> fnDoScenario42 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 40 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
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

		
			// Add PRP
			EnterPRPSKU();		

/*
			// Price override
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	

			string TextAmt = repo.Retech.NetTotal.TextValue.Substring(1,repo.Retech.NetTotal.TextValue.Length - 1);
			string TextTotAmt = repo.Retech.TotalPurchases.TextValue.Substring(1,repo.Retech.TotalPurchases.TextValue.Length - 1);
			decimal DecAmt = Convert.ToDecimal(TextAmt);
			decimal DecTotAmt = Convert.ToDecimal(TextTotAmt);	
			decimal UseAmt = 	DecTotAmt - DecAmt + 1.00m;	
			Global.SpecialPayThisAmount = Convert.ToString(UseAmt);
			
			ProductionIssueFunctions.SpecialPriceOverride(1.00m);  // lower the price so not so many code F4 alerts
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Price Overide";
	        Global.Module = "Price Overide";                
	        DumpStatsQ4.Run(); 	
			Global.CurrentMetricDesciption = "Module Total Time";
			DumpStatsQ4.Run();		        
*/            
            
			// @#@#@# C H E C K O U T #@#@#@	
			Global.PayWithMethod = "Cash";
			Checkout.Run(); 
		
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 42";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
            if(!Global.AbortScenario)
            {
				// Write out metrics buffer
				WriteOutStatsQ4Buffer.Run();             	
            }
            else
            {
            	Global.Q4StatBuffer = "";   
            }          

			Global.LogText = "<--- fnDoScenario42 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 42 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
			
            Thread.Sleep(2000);
			
			// ***********End Scenario 42*****************        	
        }
        
        
        
        public void EnterPRPSKU()
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

			Stopwatch MystopwatchF1 = new Stopwatch();	
			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();
       		Stopwatch MystopwatchGPGPRP = new Stopwatch();	
       		
			if(Global.DomesticRegister)
			{
				// Press F1 add item
				Keyboard.Press("{F1}");
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
			}       		
       		
			// Enter SKU
			MystopwatchModuleTotal.Reset();			
			MystopwatchModuleTotal.Start();			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();		
			if(Global.DomesticRegister)
			{
				while(!repo.AddItemText.Enabled)
					{ Thread.Sleep(100);  }
				
				if(Global.CurrentSKUOveride)	// 12-3-18
					repo.AddItemText.TextValue = Global.CurrentSKUOverideValue;
				else
					repo.AddItemText.TextValue = "177966";
				
				repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");
				Global.LogText = @"Item added";
				WriteToLogFile.Run(); 	
				

				Global.LogText = "Adding PRP";
				WriteToLogFile.Run(); 	
				MystopwatchGPGPRP.Reset();	    
				MystopwatchGPGPRP.Start();	
				
				while(!repo.Retech.PRPAddButton.Enabled)
					{ Thread.Sleep(100); }				
				repo.Retech.PRPAddButton.Click();
				
				while(!repo.Retech.SelectCustomer.Enabled)
					{ Thread.Sleep(100); }
				repo.Retech.PRPSelectCustomerButton.Click();
	
				while(!repo.Retech.ButtonContent.Enabled)
					{ Thread.Sleep(100); }				
				repo.Retech.ButtonContent.Click(); // Search for Customer
				Thread.Sleep(100);
				
				string PRPPhoneNumber = "406254111" + Global.RegisterNumber;
				repo.RetechFindCustomerView.CustomerLookupTxt.TextValue = PRPPhoneNumber;
				repo.RetechFindCustomerView.Self.PressKeys("{Enter}");
				
				while(!Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
				{	Thread.Sleep(100);	}
				while(!repo.RetechFindCustomerView.ListBoxItem.Enabled)
					{ Thread.Sleep(100); }				
				repo.RetechFindCustomerView.ListBoxItem.Click(); // accept customer
				while(Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
				{	Thread.Sleep(100);	}				

				
				// Process Continue if exists
				if(Host.Local.TryFindSingle(repo.Retech.ProductReplacementContinueInfo.AbsolutePath.ToString(), out element))
				{
					repo.Retech.ProductReplacementContinue.Click();
					Thread.Sleep(100);
				}				


			}
			else
			{
				// International SKU OBS Mass Effect
				repo.IPOSScreen.InternationalOutsideSKUField.PressKeys("611547{Enter}");
				WriteToLogFile.Run(); 	 
				Thread.Sleep(100);
//				while(!repo.IPOS20167172.OBSMASSEFFECTEDGECARDInfo.Exists())
//				{	Thread.Sleep(100);	} 					
				if(Host.Local.TryFindSingle(repo.ReservationDeposit.RawTextESCCloseInfo.AbsolutePath.ToString(), out element)) 
					repo.ReservationDeposit.RawTextESCClose.Click();
			}

			TimeMinusOverhead.Run((float) MystopwatchGPGPRP.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Add PRP";
	        Global.Module = "Add PRP";                
	        DumpStatsQ4.Run();  				
			Global.CurrentMetricDesciption = "Module Total Time";
			DumpStatsQ4.Run();					
				
        }   
        
        
    }
}
