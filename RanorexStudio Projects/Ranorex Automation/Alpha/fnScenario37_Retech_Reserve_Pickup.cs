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
    public class fnDoScenario37 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario37()
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
        {	//*****************Start  Scenario 37 - Retech - Reserve Pickup ******************
        	
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
        	FnProductionIssueFunctions ProductionIssueFunctions = new FnProductionIssueFunctions();

        	Global.CurrentScenario = 37;

	       	if(!Global.IndirectCall)
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
				{ 	return;
				}
	       	
	       	Global.ScenarioExecuted = true;
			
	       	//string ReservePickupPhoneNumber = "9727651234";
	       	string ReservePickupPhoneNumber = "406254111" + Global.RegisterNumber;
	       	
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
			
			Global.LogText = @"---> fnDoScenario37 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 37 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
			// Start 			
            
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	 	

			//#### Reserve the item
			StartTransaction.Run();
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();			
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Start Transaction";
	        Global.Module = "Reserve:";                
	        DumpStatsQ4.Run();  			
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();			
			
	        Global.LogText = @"Reserve Item";
			WriteToLogFile.Run(); 	
			
			repo.Retech.FindAProductCtrlPlusS.Click("19;5"); // click in search box
			Thread.Sleep(100);
			Keyboard.Press("{F2}"); // Upcoming
			Thread.Sleep(100);
	


	
			

// ########################################################			
			// Line item: repo.RetechQuickSearchView.SearchResultsView.ListBoxItem;
			// Price: repo.RetechQuickSearchView.SearchResultsView.NewPrice;
			// PreOrder: repo.RetechQuickSearchView.SearchResultsView.PreOrder;
			// SKU: repo.RetechQuickSearchView.SearchResultsView.NewSku;
			// Number of items in list: repo.RetechQuickSearchView.DisplayedCount;
	        //    	string SearchCount = repo.RetechQuickSearchView.DisplayedCount.TextValue;
			//		NumberRecordsFound = Convert.ToInt32(SearchCount.Substring(0,SearchCount.IndexOf(" ")));

			//repo.RetechQuickSearchView.FirstPreOrderItem.Click();  // Click on first pre-order item	
			
			bool PreOrderFound = false;
			Global.LogText = @"Searching for PreOrder item in list";
			WriteToLogFile.Run(); 	
			
			while(!PreOrderFound)
			{
				// Search for and select the first PRE-ORDER item > $10.00
				List list = "/form[@automationid='QuickSearchView']/?/?/list[@automationid='SearchResultsView']";
				IList<ListItem> items = list.Items; 
				foreach (ListItem EachItem in list.Items)
				{
					string TextAmt = EachItem.Children[1].Children[0].GetAttributeValue<string>("Caption"); // Price
					TextAmt = TextAmt.Substring(1,TextAmt.Length - 1);  // remove $
					decimal DecAmt = Convert.ToDecimal(TextAmt); // convert to decimal	
				
					string TheCaption = EachItem.Children[1].Children[1].GetAttributeValue<string>("Caption");  // Item type looking for PRE-ORDER
					
					if(DecAmt > 10.00m && TheCaption == "PRE-ORDER")
					{
						EachItem.Click();
						PreOrderFound = true;
						break;
					}
				
				}

				if(!PreOrderFound)
				{  // page down on list of items
		            repo.ShellView.SearchResultsView.Click("627;44");
		            Delay.Milliseconds(200);
		           	Keyboard.Press("{Next}");					
				}
			}
			
			Thread.Sleep(100);
			Keyboard.Press("{Escape}");  // Escape from search box
			Thread.Sleep(100);
			
			// Process age verification if exists
			if(Host.Local.TryFindSingle(repo.Retech.AgeVerifiedCommand1Info.AbsolutePath.ToString(), out element))
			{
				repo.Retech.AgeVerifiedCommand1.Click();
				Thread.Sleep(100);
			}
		




			// Get the Initial Deposit amout from the screen
			Global.SpecialPayThisAmount = repo.Retech.DepositText.TextValue.Substring(1);
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Reserve Item";
	        Global.Module = "Reserve:";                
	        DumpStatsQ4.Run();  
			
			// Select Customer
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
			repo.Retech.SelectCustomer.Click();
			Thread.Sleep(100);
			repo.Retech.ButtonContent.Click("105;10"); // Search for Customer
			Thread.Sleep(100);
			// repo.RetechFindCustomerView.CustomerLookupTxt.PressKeys("9727651234{Enter}"); 12-13-18
			repo.RetechFindCustomerView.CustomerLookupTxt.TextValue = ReservePickupPhoneNumber;
			repo.RetechFindCustomerView.Self.PressKeys("{Enter}");
			
			while(!Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
			{	Thread.Sleep(100);	}
			repo.RetechFindCustomerView.ListBoxItem.Click(); // accept customer
			Thread.Sleep(500);
			
			
			while(Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
			{	Thread.Sleep(100);	}			
			
			// Process continue 1
			if(Host.Local.TryFindSingle(repo.Retech.ContinueButtonCommand1Info.AbsolutePath.ToString(), out element))
			{
				repo.Retech.ContinueButtonCommand1.Click();
				Thread.Sleep(200);
			}	
			
			// Process continue 2
			if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.ContinueButtonCommand.Click();
				Thread.Sleep(200);
			}		
			
			// Process SMS text message - not interested
			if(Host.Local.TryFindSingle(repo.Retech.NotInterestedCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.Retech.NotInterestedCommand.Click();
				Thread.Sleep(200);
			}			
			
			
			
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Select Customer";
	        Global.Module = "Reserve:";                
	        DumpStatsQ4.Run(); 	

	        // Check for Product Replacement Plan
			if(Host.Local.TryFindSingle(repo.Retech.ProductReplacementContinueInfo.AbsolutePath.ToString(), out element))
			{
				repo.Retech.ProductReplacementContinue.Click();
				Thread.Sleep(200);
			}	

			
			
			if(Host.Local.TryFindSingle(repo.Retech.ContinueButtonCommand1Info.AbsolutePath.ToString(), out element))
			{
				repo.Retech.ContinueButtonCommand1.Click();
				Thread.Sleep(100);
			}	

			if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.ContinueButtonCommand.Click();
				Thread.Sleep(100);
			}					
			
			
			// Checkout and pay required deposit
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
			Global.SpecialCheckingOut = false;
			Global.PayWithMethod = "Cash";
            ProductionIssueFunctions.SpecialPreCheckOut();	
            
            ProductionIssueFunctions.SpecialCheckForCodeF4Alert();
			
			repo.RetechLoginView.TxtPassword.PressKeys("{Escape}"); // initial sign on screen
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Pay deposit and checkout";
	        Global.Module = "Reserve:";                
	        DumpStatsQ4.Run(); 			
			
			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.NowCustomerLookup = Global.AdjustedTime;            
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Reserve:";                    
            DumpStatsQ4.Run();   	        
	        
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	 	
			
			
			
			
			// #####  Now do the pickup
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
			StartTransaction.Run();
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Start Transaction 2";
	        Global.Module = "Pick Up:";                
	        DumpStatsQ4.Run(); 		
	        
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();		        
			
			// repo.Retech.FindCustomerTextBox.PressKeys("9727651234{Enter}"); // get the customer  12-13-18
			repo.Retech.FindCustomerTextBox.TextValue = ReservePickupPhoneNumber;
			repo.Retech.FindCustomerTextBox.PressKeys("{Enter}");			

			while(!Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
			{	Thread.Sleep(100);	}
			repo.RetechFindCustomerView.ListBoxItem.Click(); // accept customer
			while(Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
			{	Thread.Sleep(100);	}	
			
			// View customer details to get reserve item
			repo.Retech.ShowCustomerDetails.Click();
			while(!Host.Local.TryFindSingle(repo.CustomerDetailsView.OpenReservationListItemControlInfo.AbsolutePath.ToString(), out element))
			{	Thread.Sleep(100);	}	
			// Get the reserved item
			repo.CustomerDetailsView.OpenReservationListItemControl.Click();  // Click on first reserved item
			repo.CustomerDetailsView.PickupReservation.Click();
			Thread.Sleep(100);
			repo.OptionSelectorView.StreetDatedProductConfirm.Click(); // Yes sell this street dated itme
			Thread.Sleep(100);
			Keyboard.Press("{Escape}");
			if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.ContinueButtonCommand.Click();
				Thread.Sleep(200);
			}				

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Get customer and pick up item";
	        Global.Module = "Pick Up:";                
	        DumpStatsQ4.Run(); 		
	        
/*	        
			// Price override
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();		
			
			ProductionIssueFunctions.SpecialPriceOverride(1.00m);  // lower the price so not so many code F4 alerts
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Price Overide";
	        Global.Module = "Pick Up:";                
	        DumpStatsQ4.Run(); 	        
*/	        
			// Process age verification if exists
			if(Host.Local.TryFindSingle(repo.Retech.AgeVerifiedCommand1Info.AbsolutePath.ToString(), out element))
			{
				repo.Retech.AgeVerifiedCommand1.Click();
				Thread.Sleep(100);
			}
			// Process continue 2
			if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.ContinueButtonCommand.Click();
				Thread.Sleep(200);
			}			
			
			// Now pickup and pay total for item
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
        	Global.SpecialCheckingOut = false;
			Global.PayWithMethod = "Cash";
			Global.SpecialPayThisAmount = "All";  
			repo.Retech.DataContextShowCheckoutViewCommand.Click();
            ProductionIssueFunctions.SpecialPreCheckOut();	
            
            ProductionIssueFunctions.SpecialCheckForCodeF4Alert();
            
            repo.RetechLoginView.TxtPassword.PressKeys("{Escape}"); // initial sign on screen			
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Pay for picked up item";
	        Global.Module = "Pick Up:";                
	        DumpStatsQ4.Run(); 		
	        
			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.NowCustomerLookup = Global.AdjustedTime;            
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Pick Up:";                    
            DumpStatsQ4.Run();   	        
		
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 37";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario37 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 37 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
			
            Thread.Sleep(2000);
			
			// ***********End Scenario 37*****************        	
        }
    }
}
