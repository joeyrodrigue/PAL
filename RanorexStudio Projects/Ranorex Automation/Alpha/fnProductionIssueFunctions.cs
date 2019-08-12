/*
 * Created by Ranorex
 * User: storeuser
 * Date: 09/25/15
 * Time: 6:41 AM
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

namespace Alpha
{
    /// <summary>
    /// Description of FnProductionIssueFunctions.
    /// </summary>
    [TestModule("E7FE53EC-475A-49F2-B1AC-90341E1A7CA4", ModuleType.UserCode, 1)]
    public class FnProductionIssueFunctions : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnProductionIssueFunctions()
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
        
  
      	 //
        // S T A R T   T R A N S A C T I O N ##############################################################################
        //
        public void SpecialStartTransaction()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	FnStartTransaction StartTransaction = new FnStartTransaction();
        	
			Global.LogText = @"Start Transaction";
			WriteToLogFile.Run(); 	
			StartTransaction.Run();      

        }       
                
 
        
      	 //
        // P R I C E   O V E R I D E ##############################################################################
        // 
        public void SpecialPriceOverride(decimal GoalAmount)
        {
        	Ranorex.Unknown element = null; 
        	
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
			Global.LogText = "Override: click edit transaction";
			WriteToLogFile.Run();	        	
        	
        	repo.Retech.DataContextViewOrderDetails.Click(); // click edit transaction
        	Thread.Sleep(100);
        	
			Global.LogText = "Override: Click on Promo not found F2";
			WriteToLogFile.Run();	         	
        	
			Keyboard.Press("{F2}");
        	//repo.OrderDetailsView.ApplyUnknownPromo.Click(); // Click on Promo not found F2
        	Thread.Sleep(200);
        	
			Global.LogText = "Override: select item to overide";
			WriteToLogFile.Run();	        	
        	
        	repo.ManualAdjustmentSelectionView.PromotionNotFoundCheckBox.Click(); // select item to overide
			
			Global.LogText = "Override: click on next";
			WriteToLogFile.Run();	   			
			
			while(!Host.Local.TryFindSingle(repo.ManualAdjustmentSelectionView.NextCommandInfo.AbsolutePath.ToString(), out element))
			{ Thread.Sleep(100); }
			repo.ManualAdjustmentSelectionView.NextCommand.Click();  // click on next
			
			
			decimal PurchaseSubTotal;
			if(Host.Local.TryFindSingle(repo.Retech.TotalPurchasesInfo.AbsolutePath.ToString(), out element))
			{
				string a1 = repo.Retech.TotalPurchases.TextValue;
				PurchaseSubTotal = SpecialConvertTextAmountToReturnToFloat(repo.Retech.TotalPurchases.TextValue);
			}
			else
			{
				
				// Note: see both of these automation-IDs during a run
				if(Host.Local.TryFindSingle(repo.Retech.TotalPurchasesNoAutoIDInfo.AbsolutePath.ToString(), out element))
				{
					string a2 = repo.Retech.TotalPurchasesNoAutoID.TextValue;	
					PurchaseSubTotal = SpecialConvertTextAmountToReturnToFloat(repo.Retech.TotalPurchasesNoAutoID.TextValue);
				}
				else
				{
					string a2 = repo.TotalPurchasesNoAutoID2.TextValue;					
					PurchaseSubTotal = SpecialConvertTextAmountToReturnToFloat(repo.TotalPurchasesNoAutoID2.TextValue);				
				}
				
			}
			
			
			PurchaseSubTotal = SpecialConvertTextAmountToReturnToFloat(repo.Retech.TotalPurchases.TextValue);
			decimal DepositAmount = SpecialConvertTextAmountToReturnToFloat(Global.SpecialPayThisAmount);


			// decimal DollarReductionAmount = PurchaseSubTotal;		 12-11-18

			decimal DollarReductionAmount = PurchaseSubTotal - DepositAmount - GoalAmount;

			
			Global.LogText = "Override: DollarReductionAmount " + Convert.ToString(DollarReductionAmount);
			WriteToLogFile.Run();	
			
			
			string ateststr = Convert.ToString(DollarReductionAmount);
			
			
			repo.ManualAdjustmentAmountView.PercentReductionTextBox.PressKeys(Convert.ToString(DollarReductionAmount));// 12-11-18 
			repo.ManualAdjustmentAmountView.PercentReductionTextBox.TextValue = Convert.ToString(DollarReductionAmount);

			Global.LogText = "Override: After Press Keys" ;
			WriteToLogFile.Run();	
			
			repo.ManualAdjustmentAmountView.SavePromotionNotFound.Click();
			
			Keyboard.Press("{Escape}");		

			// Check for GPG continue
			if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.ContinueButtonCommand.Click();
				Thread.Sleep(100);
			}			

			while(!repo.Retech.DataContextShowCheckoutViewCommand.Enabled)
			{
				Thread.Sleep(100);
				// Process age verification if exists
				if(Host.Local.TryFindSingle(repo.Retech.AgeVerifiedCommand1Info.AbsolutePath.ToString(), out element))
					repo.Retech.AgeVerifiedCommand1.Click();
				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					repo.ContinueButtonCommand.Click();
			}
        
//			Keyboard.Press("{Escape}");
        }
        
        
      	//
        // E N T E R   C U S T O M E R  ##############################################################################
        //
        public void SpecialEnterCustomer()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
        	Ranorex.Unknown element = null; 
        	
			Global.LogText = @"Enter Special Customer Phone Number";

			repo.Retech.FindCustomerTextBox.TextValue = Global.SpecialCustomerPhoneNumber;
			repo.Retech.FindCustomerTextBox.PressKeys("{Enter}");
			Thread.Sleep(100);
			
			while(!repo.RetechFindCustomerView.CustomerSearchCriteria.Enabled)
			{	Thread.Sleep(100);	} 	
			
			repo.RetechFindCustomerView.ListBoxItem.Click(); 
			
			while(Host.Local.TryFindSingle(repo.RetechFindCustomerView.ListBoxItemInfo.AbsolutePath.ToString(), out element))
			{	Thread.Sleep(100);	}				
        }

        //
        // E N T E R   S K U S  ################################################################################
        //        
        public void SpecialPurchaseSKUs()
        {
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
        	Global.LogText = @"Starting Purchases";
			WriteToLogFile.Run();
        	
        	Global.SpecialReturningSKUs = false;
        	SpecialEnterSKUs();
        }
        
        
        
        //
        // E N T E R   S K U S  ################################################################################
        //        
        public void SpecialEnterSKUs()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
        	Ranorex.Unknown element = null; 
        	
			for (int x = 1; x <= Global.NumberSpecialSKUs; x++)
			{
				if(Global.SpecialReturningSKUs)
				{
					repo.Retech.ReturnsF4.Click();	// Return Mode F4
				}
				else
				{
					repo.Retech.ActivatePurchaseMode.Click();	// Purchase Mode F2
				}
	        	Thread.Sleep(100);

				Global.LogText = @"Add Item";
				WriteToLogFile.Run(); 			
	
				// Press F1 add item
				Keyboard.Press("{F1}"); // Add Item F1			

				while(!Host.Local.TryFindSingle(repo.AddItemTextInfo.AbsolutePath.ToString(), out element))				
				{	Thread.Sleep(100);	}
				
				Global.LogText = @"Enter purchase SKU: " + x;
				WriteToLogFile.Run(); 			
				
				// Enter SKUs
				repo.AddItemText.TextValue = Global.SpecialSKUs[x];
				repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");
				while(!repo.AddLineItemCommand.Enabled)
				{ 
					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.ContinueButtonCommand.Click();
					}					
					Thread.Sleep(100); 
				}
				
				if(Global.SpecialReturningSKUs)
				{
	            	// Select Defective for retrun reason
	            	repo.ReturnReason.Click();
		            Delay.Milliseconds(200);
					repo.Defective.Click();
	
					//Click on Scan or select receipt and select no receipt
					repo.Retech.SelectReceipt.Click();
	            	repo.Retech.NoReceiptAvailableCommand.Click(); 
	            	Thread.Sleep(200);
	            
		            // Press F5 continue
		            Keyboard.Press("{F5}");	
		            Thread.Sleep(200);
				}
				
				// Check for Recommend Pre-Owned Items
				if(Host.Local.TryFindSingle(repo.Retech.PreOwnedCrossSellTaskControlInfo.AbsolutePath.ToString(), out element))	
				{
					repo.Retech.ContinueButtonCommand.Click();
					Thread.Sleep(200);
					if(Host.Local.TryFindSingle(repo.Retech.AcknowledgeRelatedProductsCommand1Info.AbsolutePath.ToString(), out element))
					{
						repo.Retech.AcknowledgeRelatedProductsCommand1.Click();
						Thread.Sleep(200);			
					}
		
				}

				// Check for age verification
				if(Host.Local.TryFindSingle(repo.Retech.CustomerAgeVerificationInfo.AbsolutePath.ToString(), out element))
				{
					repo.Retech.AgeVerifiedCommand1.Click();
					Thread.Sleep(200);
				}				
				
				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
				{
					repo.ContinueButtonCommand.Click();
					Thread.Sleep(200);
				}				

				// Check for Pre-Order
				if(Host.Local.TryFindSingle(repo.Retech.ThisProductIsNotAvailableForPurchaInfo.AbsolutePath.ToString(), out element))
				{
					repo.Retech.DepositText.TextValue = Global.SpecialInitialDeposit;
				}

				if(Host.Local.TryFindSingle(repo.Retech.AcknowledgeRelatedProductsCommand1Info.AbsolutePath.ToString(), out element))
				{
					repo.Retech.AcknowledgeRelatedProductsCommand1.Click();
					Thread.Sleep(200);
				}

				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
				{
					repo.ContinueButtonCommand.Click();
					Thread.Sleep(200);
				}
			}	 
        }        
        

        
        
        

        
        //
        // E N T E R   T R A D E S  ##################################################################
        //        
        public void SpecialEnterTradess()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
        	Ranorex.Unknown element = null; 

           	while(!repo.Retech.TradesF3_5_7_1_2.Enabled)
			{	Thread.Sleep(100);	}  					

           	// Press the Trade Key
           	repo.Retech.TradesF3_5_7_1_2.Click();
			while(!Host.Local.TryFindSingle(repo.Retech.TradesTotalInfo.AbsolutePath.ToString(), out element))				
			{	Thread.Sleep(100);	}	

			Global.LogText = @"Enter Trade SKUs";
			WriteToLogFile.Run(); 		           	

			for (int soff = 1; soff <= Global.NumberSpecialTrades ; soff++ )
			{			
				// Press F1 add item
				repo.AddLineItemCommand.Click();
				while(!Host.Local.TryFindSingle(repo.AddItemTextInfo.AbsolutePath.ToString(), out element))				
				{	Thread.Sleep(100);	}
				
				// Enter SKUs
				repo.AddItemText.TextValue = Global.SpecialTrades[soff];
				repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");
				Thread.Sleep(100);

				if(Host.Local.TryFindSingle(repo.Retech.AddATradeCustomerAndPressF6ToConInfo.AbsolutePath.ToString(), out element))
				{	// Click on Select a Customer - Then search for Customer
					repo.Retech.SelectCustomer.Click();
					Thread.Sleep(10000);
				}

				// Age verification
				if(Host.Local.TryFindSingle(repo.Retech.AgeVerifiedCommand1Info.AbsolutePath.ToString(), out element))
				{
					repo.Retech.AgeVerifiedCommand1.Click();
					Thread.Sleep(100);
				}

				// F5 Continue
				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
				{
					repo.ContinueButtonCommand.Click(); // Continue
					Thread.Sleep(100);
				}		

			}
			if(Host.Local.TryFindSingle(repo.Retech.ContinueButtonCommandTrade_5_7_1_2Info.AbsolutePath.ToString(), out element))
			{
				repo.Retech.ContinueButtonCommandTrade_5_7_1_2.Click();
				// Wait for Customer Informaiton popup screen
				while(!Host.Local.TryFindSingle(repo.CustomerEditorView.CustomersProfileInfoInfo.AbsolutePath.ToString(), out element))
				{	Thread.Sleep(100);	}					
				SpecialEnterCustomerInformation();	
			}					
	
        }        

        
 
        
        //
        // P R E   C H E C K   O U T ##############################################################################
        //        
        public void SpecialPreCheckOut()
        {	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;    
			
			bool BalanceIsZero = false;
            
        	RanorexRepository repo = new RanorexRepository(); 
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4(); 
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
        	
        	Ranorex.Unknown element = null;
 
        	Global.LogFileIndentLevel++;
        	Global.LogText = "IN fnCheckout";
			WriteToLogFile.Run();	  
			
			// Read Remaining Balance from screen
			BalanceIsZero = false;
			if(repo.Retech.AmountDue.TextValue == "$0.00")
			{
				BalanceIsZero = true;
			}

			// 12-7-18
			if(Host.Local.TryFindSingle(repo.Retech.NotInterestedCommandInfo.AbsolutePath.ToString(), out element))
			{
				repo.Retech.NotInterestedCommand.Click();
				Thread.Sleep(100);
			}
			
			
			if(!Host.Local.TryFindSingle(repo.Retech.RequestCashSettlementInfo.AbsolutePath.ToString(), out element))
//			if(!Global.SpecialCheckingOut)
			{
				// Click on CheckOut F12 button
				Global.LogText = @"Clicking on F12 Checkout - Prod Issues";
				WriteToLogFile.Run(); 	
				while(!repo.Retech.DataContextShowCheckoutViewCommand.Enabled)
				{ Thread.Sleep(100); }
				// Check for GPG Continue
				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
				{
					repo.ContinueButtonCommand.Click();
					Thread.Sleep(100);
				}
				// Click on Check Out				
//				repo.Retech.DataContextShowCheckoutViewCommand.Click();
//				Keyboard.Press("{F12}");
				repo.Retech.OrderWorkspaceView.PressKeys("{F12}");
				Thread.Sleep(1000);	
				if(!BalanceIsZero)
					while(!repo.Retech.RequestCashSettlement.Enabled)
					{ Thread.Sleep(100); }	
			}

			if(!BalanceIsZero)	
			{
				SpecialPreCheckOutNonZeroBalance();
			}
			
			Global.LogText = "OUT fnCheckout";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;        	
	
        }              
 
        
        
        //
        // P R E   C H E C K   O U T   N O N   Z E r o   b a l a n c e##############################################################################
        //        
        private void SpecialPreCheckOutNonZeroBalance()
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
        	
			// Check for Please select a charity to donate
			if(Host.Local.TryFindSingle(repo.RetechPeripheralHostWindow.PleaseSelectACharityToDonateInfo.AbsolutePath.ToString(), out element))
			{
				repo.RetechPeripheralHostWindow.DeclineDonateNoThanks.Click();
				Thread.Sleep(100);
			}

			
			Global.LogText = @"Waiting on Time to Check Out - fnProductionIssueFunctions()";
			WriteToLogFile.Run();	
			Thread.Sleep(100); 			
			while(!Host.Local.TryFindSingle(repo.Retech.TimeToCheckOutInfo.AbsolutePath.ToString(), out element))
			{	
				if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
				{
					repo.ContinueButtonCommand.Click();
					Global.LogText = @"Clicked on Continue Button - fnProductionIssueFunctions()";
					WriteToLogFile.Run();
					Thread.Sleep(100); 
				}					
					
				Thread.Sleep(100);
				if(Host.Local.TryFindSingle(repo.Retech.AcknowledgeRelatedProductsCommandInfo.AbsolutePath.ToString(), out element))
				{
					if(Host.Local.TryFindSingle(repo.Retech.AcknowledgeRelatedProductsCommandInfo.AbsolutePath.ToString(), out element))
						if(repo.Retech.AcknowledgeRelatedProductsCommand.Enabled)
						{
							Global.LogText = @"Clicking on acknowledge related products before F12";
							WriteToLogFile.Run();						
							repo.Retech.AcknowledgeRelatedProductsCommand.Click();
							Thread.Sleep(100);						
						}
				}					
				else if(Host.Local.TryFindSingle(repo.Retech.ContinueF5_5_6_0_103Info.AbsolutePath.ToString(), out element))
				{
					if(repo.Retech.ContinueF5_5_6_0_103.Enabled)
					{
						Global.LogText = @"Clicking on F5 Continue before F12";
						WriteToLogFile.Run();						
						repo.Retech.ContinueF5_5_6_0_103.Click();
						Thread.Sleep(100);	
						repo.Retech.ButtonCheckout.Click();
						Thread.Sleep(100);
					}
				}
			}
			
			// Click on screen to make sure in focust - .focus not work
			//repo.Retech.PaymentView.Click("689;82");  1-22-19

	     	switch (Global.PayWithMethod) 
	     	{			
	     		case "Cash": 
					Global.LogText = @"Waiting on Cash F5";
					WriteToLogFile.Run();			
					repo.Retech.Self.Focus();
					Thread.Sleep(100);
					while(!repo.Retech.RequestCashSettlement.Enabled)
					{	Thread.Sleep(500); } 
					Global.LogText = @"Found Cash F5 Enabled";
					WriteToLogFile.Run();	     			
	     			break; 			
	     		case "Credit":
	     		case "PURCC":	     			
					Global.LogText = @"Waiting on Manual Card F6";
					WriteToLogFile.Run();			
					repo.Retech.Self.Focus();
					Thread.Sleep(100);
					while(!repo.Retech.RequestManualCardSettlement.Enabled)
					{	Thread.Sleep(500);	}					
					Global.LogText = @"Found Manual Card F6 Enabled";
					WriteToLogFile.Run();		     			
	     			break;
	     	}

			// Read Remaining Balance from screen
			string BalanceDueText = repo.Retech.BalanceDue.TextValue;
			string BalTemp = BalanceDueText.Substring(1,BalanceDueText.Length - 1);
			float BalanceDueAmount = float.Parse(BalTemp);
			string BalanceDueAmountTxt = Convert.ToString(BalanceDueAmount);			

	     	switch (Global.PayWithMethod) 
	     	{
	     		case "Cash":	     			
					// Click on Cash F5 button
					Global.LogText = @"Clicking on Cash F5 button";
					WriteToLogFile.Run(); 						
					repo.Retech.RequestCashSettlement.Click();
					while(!Host.Local.TryFindSingle(repo.RetechCardPaymentView.TxtAmountPaidInfo.AbsolutePath.ToString(), out element) )
					{	
						Thread.Sleep(100);
					}
					
					// Enter amount paid
					if(Global.SpecialPayThisAmount == "All")
					{
						repo.RetechCardPaymentView.TxtAmountPaid.TextValue = BalanceDueAmountTxt;
					}
					else
					{
						repo.RetechCardPaymentView.TxtAmountPaid.TextValue = Global.SpecialPayThisAmount;
					}
					repo.RetechCardPaymentView.TxtAmountPaid.PressKeys("{Enter}");	  	
		
		     		break;	 
	     			
	     		case "Credit":
					// Click on Manual Card F6 button
					Global.LogText = @"Clicking on Manual Card F6 button";
					WriteToLogFile.Run(); 	
					repo.Retech.RequestManualCardSettlement.Click();
					
					while(!repo.RetechCardPaymentView.RunAsCreditCard.Enabled)
					{
						Thread.Sleep(100);
					}
					
					// Click on Run as Credit button
					Global.LogText = @"Clicking on Run as Credit button";
					WriteToLogFile.Run(); 		
					repo.RetechCardPaymentView.RunAsCreditCard.Click();
					Thread.Sleep(500);

					repo.RetechCardPaymentView.TxtCardNumber.TextValue = Global.CreditCardNumber;
					repo.RetechCardPaymentView.TxtExpirationDate.TextValue = Global.CreditCardMonth + "/" + Global.CreditCardYear;
					repo.RetechCardPaymentView.TxtZipCode.TextValue = Global.CreditCardZip; 
					// Enter amount paid
					if(Global.SpecialPayThisAmount == "All")
					{
						repo.RetechCardPaymentView.TxtAmountToCharge.TextValue = BalanceDueAmountTxt;
					}
					else
					{
						repo.RetechCardPaymentView.TxtAmountToCharge.TextValue = Global.SpecialPayThisAmount;
					}					
					repo.RetechCardPaymentView.ProcessCreditCardView.PressKeys("{Enter}");
					Thread.Sleep(100);
					while(!Global.AbortScenario && 
					      !Host.Local.TryFindSingle(repo.RetechCardPaymentView.Waiting_for_SignatureInfo.AbsolutePath.ToString(), out element))						
					{	
						Thread.Sleep(100);	
						if(Host.Local.TryFindSingle(repo.RetechCardPaymentView.TryAuthorizingAgainInfo.AbsolutePath.ToString(), out element) )						
						{
							Global.AbortScenario = true;
							Global.TempErrorString = "Credit Card Unable to Process Payment - transaction voided";
							WriteToErrorFile.Run();
							Global.LogText = Global.TempErrorString;
							WriteToLogFile.Run();	
							Keyboard.Press("{Escape}");
							Thread.Sleep(100);
							repo.Retech.DataContextVoidOrderCommand.Click();
							Thread.Sleep(100);
							repo.GenericDialogView.Yesvoidthistransaction.Click();
							Thread.Sleep(100);
							while(!repo.RetechAnotherTransaction.Visible)
							{	Thread.Sleep(100);	}
						}
						
					}
					if(!Global.AbortScenario)
					{
						repo.RetechPeripheralHostWindow.SignOnPinPad.Click();
						Thread.Sleep(200);
						repo.RetechPeripheralHostWindow.AcceptOnPinPad.Click();
						Thread.Sleep(200);
						repo.RetechCardPaymentView.Self.Focus();
						Thread.Sleep(100);
						repo.RetechCardPaymentView.SignatureAcceptedCommand.Click();
						Thread.Sleep(200);						
					}					

	     			break;	
	     			
	     		case "PURCC":
					// Click on Manual Card F6 button
					Global.LogText = @"Clicking on Manual Card F6 button";
					WriteToLogFile.Run(); 	
					repo.Retech.RequestManualCardSettlement.Click();
					while(!repo.RetechCardPaymentView.RunAsPURCCCard.Enabled)
					{
						Thread.Sleep(100);
					}
		
					// Click on Run as PURCC button (Run as PowerUp Rewards Cerdit Cart
					Global.LogText = @"Clicking on run as PURCC";
					WriteToLogFile.Run(); 						
					repo.RetechCardPaymentView.RunAsPURCCCard.Click();
					while(!Host.Local.TryFindSingle(repo.RetechCardPaymentView.TxtCardNumberInfo.AbsolutePath.ToString(), out element) )
					{	Thread.Sleep(100);	}		
					
					// Select ID type and Issuer (Drivers Lenense and State)
					repo.RetechCardPaymentView.PickIdType.SelectedItemText = "Drivers License";
					repo.RetechCardPaymentView.PickIdIssuer.Click();
					repo.RetechCardPaymentView.PickIdIssuer.SelectedItemText = "Texas";
					repo.RetechCardPaymentView.ProcessPrivateLabelCreditCardView.Click();

					// Fill in PowerUp Rewards Credit Card Manual Entry
					repo.RetechCardPaymentView.TxtCardNumber.TextValue = "7788400030000272";
					repo.RetechCardPaymentView.TxtNameFromId.TextValue = "PAL User";	
					repo.RetechCardPaymentView.ProcessPrivateLabelCreditCardView.Click();
					repo.RetechCardPaymentView.Self.PressKeys("{Enter}");

					Global.LogText = @"Info entered - watching for Waiting for Signature";
					WriteToLogFile.Run();

					while(!Global.AbortScenario && 
					      !Host.Local.TryFindSingle(repo.RetechCardPaymentView.Waiting_for_SignatureInfo.AbsolutePath.ToString(), out element))						
					{	
						Thread.Sleep(100);	
						if(Host.Local.TryFindSingle(repo.RetechCardPaymentView.PaymentCouldNotBeAuthorizedAnUnknInfo.AbsolutePath.ToString(), out element) )						
						{
							Global.AbortScenario = true;
							Global.TempErrorString = "PURCC Unable to Process Payment - transaction voided";
							WriteToErrorFile.Run();
							Global.LogText = Global.TempErrorString;
							WriteToLogFile.Run();	
							Keyboard.Press("{Escape}");
							Thread.Sleep(100);
							repo.Retech.DataContextVoidOrderCommand.Click();
							Thread.Sleep(100);
							repo.GenericDialogView.Yesvoidthistransaction.Click();
							Thread.Sleep(100);
							while(!repo.RetechAnotherTransaction.Visible)
							{	Thread.Sleep(100);	}
						}
					}
					if(!Global.AbortScenario)
					{
						repo.RetechPeripheralHostWindow.SignOnPinPad.Click();
						Thread.Sleep(200);
						repo.RetechPeripheralHostWindow.AcceptOnPinPad.Click();
						Thread.Sleep(200);
						repo.RetechCardPaymentView.Self.Focus();
						Thread.Sleep(100);
						repo.RetechCardPaymentView.SignatureAcceptedCommand.Click();
						Thread.Sleep(200);						
					}
	     			break;
	     	}			        	
        }
        
        
        
        //
        // F I N A L   C H E C K   O U T ##############################################################################
        //
        public void SpecialFinalCheckOut()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
        	Ranorex.Unknown element = null;

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
					Global.LogText = @"Waiting on CustomerInfoEditProfileIcon";
					WriteToLogFile.Run(); 	
					while(!Host.Local.TryFindSingle(repo.EditProfileCommandInfo.AbsolutePath.ToString(), out element))			
					{	Thread.Sleep(100);	}				
					break;						
			}				
        }
        
        
        
        //
        // F I N A L   C H E C K   O U T ##############################################################################
        //
        public void SpecialEnterCustomerInformation()
        {	
        	RanorexRepository repo = new RanorexRepository();
        	
        	Ranorex.Unknown element = null;
        	
        	fnWriteToLogFile WriteToLogFile= new fnWriteToLogFile();
        	
			// ##### Start edit customer
			Global.LogText = @"F8 edit customer info";
			WriteToLogFile.Run(); 
			
			repo.EditProfileCommand.Click();	// edit F8
		    Thread.Sleep(200);

		    if(repo.Gender.SelectedItemText == null)
		    	repo.Gender.SelectedItemText = "Female";

		    if(repo.EyeColor.SelectedItemText == null)
		    	repo.EyeColor.SelectedItemText = "Brown";

		    if(repo.HairColor.SelectedItemText == null)
		    	repo.HairColor.SelectedItemText = "Brown";

		    if(repo.DateOfBirth.TextValue == "")
		    	repo.DateOfBirth.TextValue = "11/11/1948";

		    if(repo.AlternatePhoneValue.TextValue == "")	
				repo.AlternatePhoneValue.TextValue = "5555555555";

		    if(repo.StreetTwoValue.TextValue == "")	
		    	repo.StreetTwoValue.TextValue = "10 St";

		    if(repo.EmailValue.TextValue == "")	
				repo.EmailValue.TextValue = "x@y.com";		
			// ##### End edit customer		    
			
			
			Global.LogText = @"Save Customer F6";
			WriteToLogFile.Run(); 			    
		    Keyboard.Press("{F6}"); // Save 
		    Thread.Sleep(200);
		    
			Global.LogText = @"F6 Continue to Purchase & Contine to pin pad";
			WriteToLogFile.Run(); 	
		    while(!Host.Local.TryFindSingle(repo.DialogShellView.WaitingForSignatureInfo.AbsolutePath.ToString(), out element))
		    {
			    Keyboard.Press("{F6}"); // Continue in Purchase Mode F6 & Continue to Pin Pad F6
			    Thread.Sleep(500);		    	
		    }

			Global.LogText = @"Sign on Pin Pad";
			WriteToLogFile.Run(); 	
			
		    repo.RetechPeripheralHostWindow.SignOnPinPad.Click();
		    Thread.Sleep(100);
		    
		    repo.RetechPeripheralHostWindow.AcceptOnPinPad.Click();
	    	Thread.Sleep(100);			
	    	
		    repo.DialogShellView.SignatureAcceptedCommand.Click();
		    Thread.Sleep(100);
	    	
//		    repo.RetechPeripheralHostWindow.MinimizePinPad.Click();  // minimize cause always on top!!!!
        }
            
       	//
        // R E T U R N  S K U ##############################################################################
        //
        public void SpecialReturnSKUs()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	FnProductionIssueFunctions ProductionIssueFunctions = new FnProductionIssueFunctions();
        	
        	Global.LogText = @"Starting Return";
			WriteToLogFile.Run(); 	

        	Global.SpecialReturningSKUs = true;
        	SpecialEnterSKUs();
        }               
        
  
        
 
        
        // R E T U R N  S K U ##############################################################################
        //
        public void SpecialCheckForCodeF4Alert()
        {
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();        
        
        	Ranorex.Unknown element = null;

			// Wait for Another Transaction pop-up
			while(!Host.Local.TryFindSingle(repo.RetechAnotherTransactionInfo.AbsolutePath.ToString(), out element) )
			{	
				Thread.Sleep(100);
					if(Host.Local.TryFindSingle(repo.RetechTillOverageWarningView.CodeF4AlertInfo.AbsolutePath.ToString(), out element)
					  	||
					   Host.Local.TryFindSingle(repo.RetechTillOverageWarningView.CodeF4Alert2Info.AbsolutePath.ToString(), out element)
					  )
				{
					repo.RetechTillOverageWarningView.DropCashCommand.Click();
					while(!repo.RetechLoginView.TxtPassword.Enabled)
					{	Thread.Sleep(100);	}						
		          		repo.RetechLoginView.TxtPassword.PressKeys("advanced{Return}");
					while(!repo.RetechTillDepositView.DropBoxAmount.Enabled)
					{	Thread.Sleep(100);	}	            		
		          		repo.RetechTillDepositView.DropBoxAmount.PressKeys("40000");  
		          		repo.RetechTillDepositView.AddCashDropButton.Click();
		          		Thread.Sleep(1000);
		          		repo.RetechTillDepositView.DropBoxAmount.PressKeys("{Escape}");  
				}
			}				

			// Press excape key on Another Transaction pop-up
			while(!Host.Local.TryFindSingle(repo.RetechLoginView.TxtPasswordInfo.AbsolutePath.ToString(), out element) )
			{	Thread.Sleep(100);	}	        
        
        }
        
        
        //
        // C ON V E R T   T E X T   T O   D E C I M  A L  ################################################################################
        //        
        public decimal SpecialConvertTextAmountToReturnToFloat(string InputText)
        {
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	
        	Global.LogText = @"Starting SpecialConvertTextAmountToReturnToFloat";
			WriteToLogFile.Run();
        	
			if(InputText.Substring(0,1) == "(")
			   	InputText = InputText.Substring(1,InputText.Length - 2);
			if(InputText.Substring(0,1) == "$")
			   	InputText = InputText.Substring(1,InputText.Length - 1);
			
			return Convert.ToDecimal(InputText);
        }                  
        
        
    }
}
