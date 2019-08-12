/*
 * Created by Ranorex
 * User: storeuser
 * Date: 06/01/15
 * Time: 8:28 AM
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
    /// Description of FnCommon47and48.
    /// </summary>
    [TestModule("90B5D0F2-13BC-4F9E-A1B0-C47598691670", ModuleType.UserCode, 1)]
    public class FnCommon47and48 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnCommon47and48()
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
        {	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;    
            
            
            Ranorex.Unknown element = null; 
            
			RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();    
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();            	
        	fnWaitForItemSearchToFinish WaitForItemSearchToFinish = new fnWaitForItemSearchToFinish();   
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();   
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor(); 
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
        	FnCheckout Checkout = new FnCheckout();
        	FnStartTransaction StartTransaction = new FnStartTransaction();
        	FnProductionIssueFunctions ProductionIssueFunctions= new FnProductionIssueFunctions();
        	FnEnterSKU EnterSKU = new FnEnterSKU();

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
			Stopwatch MystopwatchTrade = new Stopwatch();					

			// Start 		
			MystopwatchModuleTotal.Reset();
			MystopwatchModuleTotal.Start();	 				
            
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();  

			Global.LogText = @"Start Transaction";
			WriteToLogFile.Run(); 	
			StartTransaction.Run();            

			if(Global.DomesticRegister)
			{
				Global.LogText = @"Enter Customer Information";
				WriteToLogFile.Run();   
			
				// Enter customer
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();		
				
				repo.Retech.FindCustomerTextBox.PressKeys("9727651234{Enter}");	
				Thread.Sleep(100);
				while(!repo.RetechFindCustomerView.CustomerSearchCriteria.Enabled)
				{	Thread.Sleep(100);	} 	

				TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.CurrentMetricDesciption = "Member Card Lookup";
	            Global.Module = "Member Card Lookup";                   
	            DumpStatsQ4.Run(); 
	            Global.CurrentMetricDesciption = "Module Total Time";
	            DumpStatsQ4.Run();    
			}
            
    		MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();				
			
			if(Global.DomesticRegister)
			{			
				while(!Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element) )
				{	Thread.Sleep(100); } // change 1/8/17					
				repo.RetechFindCustomerView.ListBoxItem.Click();    
				
				while(Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
				{	Thread.Sleep(100);	}	

			}
			
			int NumberSKUs = 1;
			
			if(Global.NumberOfTradesMinusOne == 4)
			{
				NumberSKUs = 1;
			}
			else if(Global.NumberOfTradesMinusOne == 99)
			{
				NumberSKUs = 4;
			}
			
			for (int x = 1; x <= NumberSKUs; x++)
			{
				Global.LogText = @"Add Item";
				WriteToLogFile.Run(); 			
	
				if(Global.DomesticRegister)
				{
					// Press F1 add item
					Keyboard.Press("{F1}"); // Add Item F1			
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
				}
				
				Global.LogText = @"Enter purchase SKU";
				WriteToLogFile.Run(); 			
				
				// Enter SKUs 
				if(Global.DomesticRegister)
				{
					if(NumberSKUs == 1) repo.AddItemText.TextValue = "115571"; 
					else 				repo.AddItemText.TextValue = "883672"; 
					repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");	

					Thread.Sleep(200);
					
					while(!Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))	
						if(Host.Local.TryFindSingle(repo.ShellView.AgeVerifiedCommandWPFInfo.AbsolutePath.ToString(), out element))				
						{	repo.ShellView.AgeVerifiedCommandWPF.Click();
							Thread.Sleep(100);	
						}	// 11/16/18		

//					if(Host.Local.TryFindSingle(repo.ShellView.AgeVerifiedCommandWPFInfo.AbsolutePath.ToString(), out element))				
//					{	repo.ShellView.AgeVerifiedCommandWPF.Click();
//						Thread.Sleep(100);	
//					}

					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))				
					{	repo.ContinueButtonCommand.Click();
						Thread.Sleep(100);	
					}

					while(!repo.AddLineItemCommand.Enabled)
					{	
						if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))				
						{	
							repo.ContinueButtonCommand.Click();
						}						
						Thread.Sleep(100);	
					}
				}
				else // International
				{
					EnterSKU.Run();	// Value  set in EnterSKU	
				}
			}	        
			
//			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
//			Global.CurrentMetricDesciption = "Enter SKUs";
//		    Global.Module = "Enter SKUs";                
//		    DumpStatsQ4.Run(); 
//			Global.CurrentMetricDesciption = "Module Total Time";		        
// 			DumpStatsQ4.Run(); 				
		        
 			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();				

			if(Global.DomesticRegister)
			{
				switch (Global.RetechVersion.Substring(0,3))
				{
					case "5.6":
					    while(!repo.Retech.TradeKey_5_6_0_103.Enabled)
						{	Thread.Sleep(100);	}					
						break;
					case "5.7":
	           			while(!repo.Retech.TradesF3_5_7_1_2.Enabled)
						{	Thread.Sleep(100);	}  					
						break;	
					default:
	           			while(!repo.Retech.TradesF3_5_7_1_2.Enabled)
						{	Thread.Sleep(100);	}  					
						break;						
				}		        
			}
			
	        // Press the Trade Key 
			if(Global.DomesticRegister)
			{
		        Keyboard.Press("{F3}");
	
				while(!Host.Local.TryFindSingle(repo.Retech.TradesTotalInfo.AbsolutePath.ToString(), out element))				
				{	Thread.Sleep(100);	}	
			}
			else // International
			{
				repo.InternationalInsideSKUField.PressKeys("{F6}");  // Trades
				while(!repo.TradeIn.InternationalTradeSKUFIeld.Enabled)
				{	Thread.Sleep(100); }				
			}
			
           	string[] TradeInSKUList = new string[100];
           	TradeInSKUList[0] = Global.S17Trade1;
           	TradeInSKUList[1] = Global.S17Trade2;
           	TradeInSKUList[2] = Global.S17Trade3;
           	TradeInSKUList[3] = Global.S17Trade4;
           	TradeInSKUList[4] = Global.S17Trade6;  // 02-20-18      
           	TradeInSKUList[5] = Global.S17Trade6;
           	TradeInSKUList[6] = Global.S17Trade7;
           	TradeInSKUList[7] = Global.S17Trade8;
           	TradeInSKUList[8] = Global.S17Trade9;
           	TradeInSKUList[9] = Global.S17Trade10;  
           	TradeInSKUList[10] = Global.S17Trade11;
           	TradeInSKUList[11] = Global.S17Trade12;
           	TradeInSKUList[12] = Global.S17Trade13;
           	TradeInSKUList[13] = Global.S17Trade14;
            TradeInSKUList[14] = Global.S17Trade15;
           	TradeInSKUList[15] = Global.S17Trade16;
           	TradeInSKUList[16] = Global.S17Trade17;
           	TradeInSKUList[17] = Global.S17Trade18;
           	TradeInSKUList[18] = Global.S17Trade19;  
           	TradeInSKUList[19] = Global.S17Trade20;
           	TradeInSKUList[20] = Global.S17Trade21;
           	TradeInSKUList[21] = Global.S17Trade22;
           	TradeInSKUList[22] = Global.S17Trade23;
           	TradeInSKUList[23] = Global.S17Trade24;
           	TradeInSKUList[24] = Global.S17Trade25;  
           	TradeInSKUList[25] = Global.S17Trade1;
           	TradeInSKUList[26] = Global.S17Trade2;
           	TradeInSKUList[27] = Global.S17Trade3;
           	TradeInSKUList[28] = Global.S17Trade4;
           	TradeInSKUList[29] = Global.S17Trade5;
           	TradeInSKUList[30] = Global.S17Trade6;
           	TradeInSKUList[31] = Global.S17Trade7;
           	TradeInSKUList[32] = Global.S17Trade8;
           	TradeInSKUList[33] = Global.S17Trade9;
           	TradeInSKUList[34] = Global.S17Trade10;
           	TradeInSKUList[35] = Global.S17Trade11;
           	TradeInSKUList[36] = Global.S17Trade12;
           	TradeInSKUList[37] = Global.S17Trade13;
           	TradeInSKUList[38] = Global.S17Trade14;
           	TradeInSKUList[39] = Global.S17Trade15;
           	TradeInSKUList[40] = Global.S17Trade16;
           	TradeInSKUList[41] = Global.S17Trade17;
           	TradeInSKUList[42] = Global.S17Trade18;
           	TradeInSKUList[43] = Global.S17Trade19;
           	TradeInSKUList[44] = Global.S17Trade20;
           	TradeInSKUList[45] = Global.S17Trade21;
           	TradeInSKUList[46] = Global.S17Trade22;
           	TradeInSKUList[47] = Global.S17Trade23;
           	TradeInSKUList[48] = Global.S17Trade24;
           	TradeInSKUList[49] = Global.S17Trade25;
           	TradeInSKUList[50] = Global.S17Trade1;
           	TradeInSKUList[51] = Global.S17Trade2;
           	TradeInSKUList[52] = Global.S17Trade3;
           	TradeInSKUList[53] = Global.S17Trade4;
           	TradeInSKUList[54] = Global.S17Trade5;
           	TradeInSKUList[55] = Global.S17Trade6;
           	TradeInSKUList[56] = Global.S17Trade7;
           	TradeInSKUList[57] = Global.S17Trade8;
           	TradeInSKUList[58] = Global.S17Trade9;
           	TradeInSKUList[59] = Global.S17Trade10;
           	TradeInSKUList[60] = Global.S17Trade11;
           	TradeInSKUList[61] = Global.S17Trade12;
           	TradeInSKUList[62] = Global.S17Trade13;
           	TradeInSKUList[63] = Global.S17Trade14;
           	TradeInSKUList[64] = Global.S17Trade15;
           	TradeInSKUList[65] = Global.S17Trade16;
           	TradeInSKUList[66] = Global.S17Trade17;
           	TradeInSKUList[67] = Global.S17Trade18;
           	TradeInSKUList[68] = Global.S17Trade19;
           	TradeInSKUList[69] = Global.S17Trade20;
           	TradeInSKUList[70] = Global.S17Trade21;
           	TradeInSKUList[71] = Global.S17Trade22;
           	TradeInSKUList[72] = Global.S17Trade23;
           	TradeInSKUList[73] = Global.S17Trade24;
           	TradeInSKUList[74] = Global.S17Trade25;
           	TradeInSKUList[75] = Global.S17Trade1;
           	TradeInSKUList[76] = Global.S17Trade2;
           	TradeInSKUList[77] = Global.S17Trade3;
           	TradeInSKUList[78] = Global.S17Trade4;
           	TradeInSKUList[79] = Global.S17Trade5;
           	TradeInSKUList[80] = Global.S17Trade6;
           	TradeInSKUList[81] = Global.S17Trade7;
           	TradeInSKUList[82] = Global.S17Trade8;
           	TradeInSKUList[83] = Global.S17Trade9;
           	TradeInSKUList[84] = Global.S17Trade10;
           	TradeInSKUList[85] = Global.S17Trade11;
           	TradeInSKUList[86] = Global.S17Trade12;
           	TradeInSKUList[87] = Global.S17Trade13;
           	TradeInSKUList[88] = Global.S17Trade14;
           	TradeInSKUList[89] = Global.S17Trade15;
           	TradeInSKUList[90] = Global.S17Trade16;
           	TradeInSKUList[91] = Global.S17Trade17;
           	TradeInSKUList[92] = Global.S17Trade18;
           	TradeInSKUList[93] = Global.S17Trade19;
           	TradeInSKUList[94] = Global.S17Trade20;
           	TradeInSKUList[95] = Global.S17Trade21;
           	TradeInSKUList[96] = Global.S17Trade22;
           	TradeInSKUList[97] = Global.S17Trade23;
           	TradeInSKUList[98] = Global.S17Trade24;
           	TradeInSKUList[99] = Global.S17Trade25;
           	
			Global.LogText = @"Enter Trade SKUs";
			WriteToLogFile.Run(); 		           	

			for (int soff = 0; soff <= Global.NumberOfTradesMinusOne ; soff++ )
			{			
				if(Global.DomesticRegister)
				{
					// Press F1 add item
					Keyboard.Press("{F1}"); // Add Item F1
					MystopwatchF1.Reset();
					MystopwatchF1.Start();
					while(!Host.Local.TryFindSingle(repo.AddItemTextInfo.AbsolutePath.ToString(), out element))				
					{	
						Thread.Sleep(100);
						if(MystopwatchF1.ElapsedMilliseconds > 1000)
						{
							Keyboard.Press("{F1}"); // Add Item F1
							Thread.Sleep(100);	
							MystopwatchF1.Reset();	
							MystopwatchF1.Start();	
						}
					}
					
					// Enter SKUs
					repo.AddItemText.TextValue = TradeInSKUList[soff];
					repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");
				}
				else // International
				{
					// for International SKU search for staring with PO in description
					//	select * from SKU
					//	Where Price >= 1 and description like 'PO%'
					//	order by Price	
					repo.TradeIn.InternationalTradeSKUFIeld.PressKeys("703620{Enter}");						
				}
			}

			if(!Global.DomesticRegister)
			{
				while(!repo.TradeIn.InternationalTradeSKUFIeld.Enabled)
				{	Thread.Sleep(100);	}
				repo.TradeIn.InternationalTradeSKUFIeld.PressKeys("{F12}");	// complete tradces
			}
			
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
			Global.CurrentMetricDesciption = "Enter Trades";
		    Global.Module = "Enter Trades";                
		    DumpStatsQ4.Run(); 
			Global.CurrentMetricDesciption = "Module Total Time";		        
 			DumpStatsQ4.Run(); 	   
	        
 			
 			
 			if(Global.DomesticRegister)
 			{
	 			MystopwatchModuleTotal.Reset();
				MystopwatchModuleTotal.Start();		
				
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();				
				
				switch (Global.RetechVersion.Substring(0,3))
				{
					case "5.6":
						Global.LogText = @"Clicking on Purchase Mode F2";
						WriteToLogFile.Run(); 	
						repo.Retech.Self.Focus();
						Keyboard.Press("{F2}");  // Purchase Mode F2
						Thread.Sleep(100);
	
						Global.LogText = @"Clicking Cointinue in Purchase Mode F6";
						WriteToLogFile.Run(); 	
						repo.Retech.Self.Focus();
			            Keyboard.Press("{F6}"); // Continue in Purchase Mode F6
			//            repo.Retech.ContinueToPurchaseMode.Click(); // Continue in Purchase Mode F6
						Thread.Sleep(100);	
	
						Global.LogText = @"Waiting on CustomerInfoEditProfileIcon";
						WriteToLogFile.Run(); 	
						while(!Host.Local.TryFindSingle(repo.LineItemCustomerEditorView.CustomerInfoEditProfileIcon_5_6_0_103Info.AbsolutePath.ToString(), out element))			
						{	Thread.Sleep(100);	}		            
						break;
					case "5.7":
						Global.LogText = @"Clicking on Trades F3 Continue F5 button";
						WriteToLogFile.Run(); 	
						repo.Retech.ContinueButtonCommandTrade_5_7_1_2.Click();	
						Global.LogText = @"Waiting on CustomerInfoEditProfileIcon";
						WriteToLogFile.Run(); 	
						while(!Host.Local.TryFindSingle(repo.EditProfileCommandInfo.AbsolutePath.ToString(), out element))			
						{	Thread.Sleep(100);	}					
						break;	
					default:
						Global.LogText = @"Clicking on Trades F3 Continue F5 button";
						WriteToLogFile.Run(); 	
						repo.Retech.ContinueButtonCommandTrade_5_7_1_2.Click();	
//						Global.LogText = @"Waiting on CustomerInfoEditProfileIcon";
//						WriteToLogFile.Run(); 	
//						while(!Host.Local.TryFindSingle(repo.CustomerEditorView.EditProfileCommand_5_7_1_2Info.AbsolutePath.ToString(), out element))			
//						{	Thread.Sleep(100);	}				
						break;						
				}				
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
				Global.CurrentMetricDesciption = "Select Purchase Mode";
			    Global.Module = "Select Purchase Mode";                
			    DumpStatsQ4.Run(); 
				Global.CurrentMetricDesciption = "Module Total Time";		        
	 			DumpStatsQ4.Run(); 	
	 			
// Start block comment for program flow change	 			
	 			MystopwatchModuleTotal.Reset();
				MystopwatchModuleTotal.Start();	
				
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();	
				
				// ##### Start edit customer
				Global.LogText = @"F8 edit customer info";
				WriteToLogFile.Run(); 		
				
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();				
			    // Edit the customer information
//				Keyboard.Press("{F8}"); // edit F8
			    while(!repo.EditProfileCommand.Enabled)
			    	{ Thread.Sleep(100); }
			    repo.EditProfileCommand.Click();
			    while(!repo.Gender.Enabled)
			    	{ Thread.Sleep(100); }
				if(repo.Gender.SelectedItemText == null)		repo.Gender.SelectedItemText = "Female";
				if(repo.EyeColor.SelectedItemText == null)		repo.EyeColor.SelectedItemText = "Brown";
				if(repo.HairColor.SelectedItemText == null)		repo.HairColor.SelectedItemText = "Brown";
				
				Thread.Sleep(200);
				if(repo.CustomerEditorView1.DateOfBirth.TextValue == "") 
					repo.CustomerEditorView1.DateOfBirth.TextValue = "01/01/1940";
				
				if(repo.AlternatePhoneValue.TextValue == "") repo.AlternatePhoneValue.TextValue = "5555555555";
				if(repo.StreetTwoValue.TextValue == "")	repo.StreetTwoValue.TextValue = "10 St";
				if(repo.CustInfoEmail.TextValue == "")					
					repo.CustInfoEmail.PressKeys("me@gmail.com");

	
				// ##### End edit customer		    
				Global.LogText = @"Save Customer F6";
				WriteToLogFile.Run(); 			    
			    Keyboard.Press("{F6}"); // Save 
			    Thread.Sleep(200);
			    
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
				Global.CurrentMetricDesciption = "Edit Customer Information";
			    Global.Module = "Edit Customer Information";                
			    DumpStatsQ4.Run(); 
				Global.CurrentMetricDesciption = "Module Total Time";		        
	 			DumpStatsQ4.Run(); 	           
			    
	 			
// End block comment	 			
	 			
	 			
	 			MystopwatchModuleTotal.Reset();
				MystopwatchModuleTotal.Start();	
				
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();				    
			    
	
				Global.LogText = @"F6 Continue to Purchase & Contine to pin pad";
				WriteToLogFile.Run(); 	
			    while(!Host.Local.TryFindSingle(repo.DialogShellView.WaitingForSignatureInfo.AbsolutePath.ToString(), out element))
			    {
				    Keyboard.Press("{F6}"); // Continue in Purchase Mode F6 & Continue to Pin Pad F6
				    Thread.Sleep(500);		    	
			    }
	
				Global.LogText = @"Sign on Pin Pad";
				WriteToLogFile.Run(); 	
				
				Thread.Sleep(50);
				repo.RetechPeripheralHostWindow.SignOnPinPad.Click();
				
				Thread.Sleep(50);		           	
			    repo.RetechPeripheralHostWindow.AcceptOnPinPad.Click();
			    
				Thread.Sleep(50);			    
			    repo.RetechPeripheralHostWindow.MinimizePinPad.Click();  // minimize cause always on top!!!!
	
				Thread.Sleep(50);		    
			    repo.DialogShellView.SignatureAcceptedCommand.Click();
			    
			    Thread.Sleep(100);
	
				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))				
				{	repo.ContinueButtonCommand.Click();
					Thread.Sleep(100);	
				}		    
	
//				Global.LogText = @"SpecialPriceOverride";
//				WriteToLogFile.Run(); 				
//		        ProductionIssueFunctions.SpecialPriceOverride(12.00m);
	
				TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.NowCustomerLookup = Global.AdjustedTime;            
	            Global.CurrentMetricDesciption = "Sign on Pin Pad";
	            Global.Module = "Sign on Pin Pad";                   
	            DumpStatsQ4.Run(); 
	            Global.CurrentMetricDesciption = "Module Total Time";
	        	DumpStatsQ4.Run(); 
 			}
        	
        	
			// @#@#@# C H E C K O U T #@#@#@	
			Global.PayWithMethod = "Cash";
			Checkout.Run();

            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 47";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();                
			
	
            Thread.Sleep(2000);            
      	
		}
    }
}
