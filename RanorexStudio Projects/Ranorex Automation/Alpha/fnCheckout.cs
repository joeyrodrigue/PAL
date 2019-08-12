/*
 * Created by Ranorex
 * User: storeuser
 * Date: 02/10/15
 * Time: 6:37 AM
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
    /// Description of FnCheckout.
    /// </summary>
    [TestModule("1181A006-37DD-4A71-A038-DF70DC5C32B3", ModuleType.UserCode, 1)]
    public class FnCheckout : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnCheckout()
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
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4(); 
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
        	
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
        	
        	Global.LogFileIndentLevel++;
        	Global.LogText = "IN fnCheckout";
			WriteToLogFile.Run();	  
			
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	 			

			// Click on CheckOut F12 button
			Global.LogText = @"Clicking on F12 Checkout";
			WriteToLogFile.Run(); 
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
			
			if(Global.DomesticRegister)
			{
//				while(!repo.Retech.ButtonCheckout.Enabled)   // change 1/8/18
//				while( !Host.Local.TryFindSingle(repo.Retech.ButtonCheckoutInfo.AbsolutePath.ToString(), out element) )
				while( !Host.Local.TryFindSingle(repo.Retech.DataContextShowCheckoutViewCommandInfo.AbsolutePath.ToString(), out element) )					
				{ 
					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.ContinueButtonCommand.Click();
						Global.LogText = @"Clicked on Continue Button - 1 in fnCheckout()";
						WriteToLogFile.Run();
					}					
					Thread.Sleep(100); 
				}
//				Keyboard.Press("{F12}");
// Try fix 1/31/18
				while( !Host.Local.TryFindSingle(repo.Retech.RequestCashSettlementInfo.AbsolutePath.ToString(), out element) )					
				{ 
					// 07-25-18 check for "Pick Up At Store Requests" pop-up
					if(!Host.Local.TryFindSingle(repo.PickupAtStorePopupNotifier.PickupAtStoreRequestPopUpInfo.AbsolutePath.ToString(), out element))
					{
						Thread.Sleep(10);
					}
					else
					{
						Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Down item 'PopupNotifierView' at 434;30.", repo.PickupAtStorePopupNotifier.SelfInfo, new RecordItemIndex(0));
			            repo.PickupAtStorePopupNotifier.Self.MoveTo("434;30");
			            Keyboard.Press("{ENTER}");
			            //Mouse.ButtonDown(System.Windows.Forms.MouseButtons.Left);
			            Delay.Milliseconds(200);
						// repo.PopupNotifierView.Self.Click();
						Thread.Sleep(100);
					}
					
					if(Host.Local.TryFindSingle(repo.Retech.NotInterestedCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.Retech.NotInterestedCommand.Click();
						Thread.Sleep(100);
					}					

					
					
					

					if(repo.Retech.DataContextShowCheckoutViewCommand.Enabled)
						repo.Retech.DataContextShowCheckoutViewCommand.Click();
					Thread.Sleep(300);
					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.ContinueButtonCommand.Click();
						Global.LogText = @"Clicked on Continue Button - 1 in fnCheckout()";
						WriteToLogFile.Run();
					}					
					Thread.Sleep(100); 					
				}
//				repo.Retech.DataContextShowCheckoutViewCommand.Click();
			}
			else
			{
//				while(!repo.IPOS20167172.Self.Enabled)
//					{ Thread.Sleep(100); }
//				repo.IPOS20167172.Self.PressKeys("{F12}");
				if(repo.IPOS34069.F12TotalInfo.Exists(5000) )  // 02-08-18
					{}
				repo.IPOS34069.F12Total.PressKeys("{F12}");  // 02-08-18

				if(Global.CurrentScenario == 47)
				{		
					while(!repo.CustomerLookupPhone_Number.Enabled)
						Thread.Sleep(100);
					
					MystopwatchQ4.Start();
					MystopwatchQ4.Reset();
//					repo.CustomerLookupScreen.EdgePowerUpNumber.PressKeys("6364916031910400011");
					repo.CustomerLookupPhone_Number.PressKeys("9727651234");
					repo.CustomerLookupScreen.BtnSearch.Click();
					while(!repo.CustomerLookupSelectF5.Enabled)
						{ Thread.Sleep(100);	}
					repo.CustomerLookupSelectF5.Click();
					TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				    Global.CurrentMetricDesciption = @"Search for Customer";
				    Global.Module = "Finalize";                
				    DumpStatsQ4.Run();  
					
					MystopwatchQ4.Reset();	    
					MystopwatchQ4.Start();	 
	
		            repo.TransactionCustomerInformation.Email.Click("160;2");
		            Delay.Milliseconds(200);
		            
		            repo.TransactionCustomerInformation.Email.PressKeys("301 My Street");
		            Delay.Milliseconds(0);
		            
		            repo.TransactionCustomerInformation.LastName.Click("114;1");
		            Delay.Milliseconds(200);
		            
		            repo.TransactionCustomerInformation.LastName.PressKeys("9727651234");
		            Delay.Milliseconds(0);
		            
		            repo.TransactionCustomerInformation.Address1.Click("41;6");
		            Delay.Milliseconds(200);
		            
		            repo.TransactionCustomerInformation.Address1.PressKeys("My City");
		            Delay.Milliseconds(0);
		            
            repo.TransactionCustomerInformation.Open.Click();
            Delay.Milliseconds(200);

            repo.TransactionCustomerInformation.Open.Click();
            Delay.Milliseconds(200);
            
            repo.ListItemsValues.ListItemAB.Click();
            Delay.Milliseconds(200);
		            
		            repo.TransactionCustomerInformation.BtnAcceptButton.Click("23;27");
		            Delay.Milliseconds(200);
		            
		            repo.PopUpCustomerLookupUnavailable.OKF5.Click("17;22");
		            Delay.Milliseconds(200);		
	
					TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				    Global.CurrentMetricDesciption = @"Enter Customer Information";
				    Global.Module = "Finalize";                
				    DumpStatsQ4.Run();  	
				}
			}

			Thread.Sleep(1000);		

			// Check for Please select a charity to donate
			if(Host.Local.TryFindSingle(repo.RetechPeripheralHostWindow.PleaseSelectACharityToDonateInfo.AbsolutePath.ToString(), out element))
			{
				repo.RetechPeripheralHostWindow.DeclineDonateNoThanks.Click();
				Thread.Sleep(100);
			}

			if(Global.DomesticRegister)
			{
				Global.LogText = @"Waiting on Time to Check Out - in fnCheckout()";
				WriteToLogFile.Run();	
				Thread.Sleep(300); 				
				while(!Host.Local.TryFindSingle(repo.Retech.TimeToCheckOutInfo.AbsolutePath.ToString(), out element))
				{	
					if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
					{
						repo.ContinueButtonCommand.Click();
						Global.LogText = @"Clicked on Continue Button - 2 in fnCheckout()";
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
	
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.CurrentMetricDesciption = @"[F12] wait for payment icons";
	            Global.Module = "Finalize";                
	            DumpStatsQ4.Run();     	
			}
			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	           

			// Click on screen to make sure in focust - .focus not work
			//if(Global.DomesticRegister)    1-22-19
			//	repo.Retech.PaymentView.Click("689;82");
			
	     	switch (Global.PayWithMethod) 
	     	{			
	     		case "Cash": 
					Global.LogText = @"Waiting on Cash F5";
					WriteToLogFile.Run();	
					if(Global.DomesticRegister) repo.Retech.Self.Focus();
					Thread.Sleep(100);
					
					if(Global.DomesticRegister)
						while(!repo.Retech.RequestCashSettlement.Enabled) {	Thread.Sleep(500); } 
					else
						{
						
							while(!Host.Local.TryFindSingle(repo.FormPOS1.SelfInfo.AbsolutePath.ToString(), out element))
							{	Thread.Sleep(500); }
							//while(!repo.EnterSerialNumber.F5Cash.Enabled) {	Thread.Sleep(500); }
						}
					Global.LogText = @"Found Cash F5 Enabled";
					WriteToLogFile.Run();	     			
	     			break; 			
	     		case "Credit":
	     		case "PURCC":	     			
					Global.LogText = @"Waiting on Manual Card F6";
					WriteToLogFile.Run();			
					if(Global.DomesticRegister) repo.Retech.Self.Focus();
					Thread.Sleep(100);
					
					if(Global.DomesticRegister)
						while(!repo.Retech.RequestManualCardSettlement.Enabled) {	Thread.Sleep(500);	}					
					else
						while(!repo.EnterSerialNumber.F5Cash.Enabled) {	Thread.Sleep(500); } 
						
					Global.LogText = @"Found Manual Card F6 Enabled";
					WriteToLogFile.Run();		     			
	     			break;
	     	}
	     	
	     	// Check for cash back
	     	if (   Global.DomesticRegister
	     		&& repo.Retech.AmountDue.TextValue.Substring(0,1) == "("
	     	    && ( Global.CurrentScenario == 37 || Global.CurrentScenario == 47 )
	     	   )
	     	{
				Global.LogText = @"Clicking on Cash F5 button";
				WriteToLogFile.Run(); 
				// fix 1//22/18
				while(!Host.Local.TryFindSingle(repo.Retech.RequestCashSettlementInfo.AbsolutePath.ToString(), out element))
				{	Thread.Sleep(100);	}
				// end fix				
				repo.Retech.RequestCashSettlement.Click();
				Thread.Sleep(100);
				Global.LogText = @"Waiting on CashPaymentView";
				WriteToLogFile.Run(); 					
				while(!repo.RetechCardPaymentView.CashPaymentView.Enabled)
				{	Thread.Sleep(100);	}
				Keyboard.Press("{Enter}");				
	     	}
	     	else
	     	{
				string BalanceDueText = "";
				string BalTemp = "";
				float BalanceDueAmount = 0.0F;
				string BalanceDueAmountTxt = "";
					
				// Read Remaining Balance from screen
				if(Global.DomesticRegister)
				{
					BalanceDueText = repo.Retech.BalanceDue.TextValue;
					BalTemp = BalanceDueText.Substring(1,BalanceDueText.Length - 1);
					BalanceDueAmount = float.Parse(BalTemp);
					BalanceDueAmountTxt = Convert.ToString(BalanceDueAmount);
				}
				else
				{
					BalanceDueText = repo.IntListItemAmountDue.ToString();
					BalanceDueText = BalanceDueText.Replace("\t","  ");
					BalTemp = BalanceDueText.Substring(35,8);
					BalanceDueAmount = float.Parse(BalTemp);
					BalanceDueAmountTxt = Convert.ToString(BalanceDueAmount);					
				}
	
		     	switch (Global.PayWithMethod) 
		     	{
		     		case "Cash":	     			
						// Click on Cash F5 button
						Global.LogText = @"Clicking on Cash F5 button";
						WriteToLogFile.Run(); 						
						
						if(Global.DomesticRegister)
						{
							while(!Host.Local.TryFindSingle(repo.Retech.RequestCashSettlementInfo.AbsolutePath.ToString(), out element) )
							{	Thread.Sleep(100); } // change 1/8/17					
							repo.Retech.RequestCashSettlement.Click();
							while(!Host.Local.TryFindSingle(repo.RetechCardPaymentView.TxtAmountPaidInfo.AbsolutePath.ToString(), out element) )
							{	Thread.Sleep(100); }
							
							// Enter amount paid
							repo.RetechCardPaymentView.TxtAmountPaid.TextValue = BalanceDueAmountTxt;
							repo.RetechCardPaymentView.TxtAmountPaid.PressKeys("{Enter}");								
						}
						else
						{
							repo.EnterSerialNumber.Self.PressKeys("{F5}");

							// Enter amount paid
							repo.Amount.Self.PressKeys("{Enter}");								
						}

						TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
			            Global.CurrentMetricDesciption = @"[F5] enter amount press Enter";
			            Global.Module = "Finalize";                
			            DumpStatsQ4.Run();     	

			     		break;	 
		     			
		     		case "Credit":
			     		if(Global.DomesticRegister)
			     		{
							// Click on Manual Card F6 button
							Global.LogText = @"Clicking on Manual Card F6 button";
							WriteToLogFile.Run(); 	
							repo.Retech.RequestManualCardSettlement.Click();
							
							while(!repo.RetechCardPaymentView.RunAsCreditCard.Enabled)
							{
								if(!Host.Local.TryFindSingle(repo.RetechCardPaymentView.RetrySameCardInfo.AbsolutePath.ToString(), out element))
								{
									Global.LogText = @"Clicking on Retry same card";
									WriteToLogFile.Run(); 	
									Global.TempErrorString = Global.LogText;
									WriteToErrorFile.Run();								
									repo.RetechCardPaymentView.RetrySameCard.Click();
									Thread.Sleep(200);
								}
								Thread.Sleep(100);
							}
							
							// Click on Run as Credit button
							Global.LogText = @"Clicking on Run as Credit button";
							WriteToLogFile.Run(); 
							while(!Host.Local.TryFindSingle(repo.RetechCardPaymentView.RunAsCreditCardInfo.AbsolutePath.ToString(), out element) )
							{	Thread.Sleep(100); } // change 1/8/17								
							repo.RetechCardPaymentView.RunAsCreditCard.Click();
	
							
							string RetechVersion = Global.RetechVersion;
							string Sub = Global.RetechVersion.Substring(0,4);
	//						if(Global.RetechVersion.Substring(0,4) != "5.14")
							if(Global.RetechVersion.Substring(0,4) == "5.13")
							{	// ### For Retech 5.13 and below - see else code for Retech 5.14
								while(!repo.RetechCardPaymentView.TxtCardNumber.Enabled)
								{
									if(Host.Local.TryFindSingle(repo.RetechCardPaymentView.RetrySameCardInfo.AbsolutePath.ToString(), out element))							
									{
										repo.RetechCardPaymentView.RetrySameCard.Click();
										Thread.Sleep(200);
									}
									Thread.Sleep(100);
								}
		
		//						Thread.Sleep(500);
			
								repo.RetechCardPaymentView.TxtCardNumber.TextValue = Global.CreditCardNumber;
								string aa = Global.CreditCardMonth + "/" + Global.CreditCardYear;
								repo.RetechCardPaymentView.TxtExpirationDate.TextValue = Global.CreditCardMonth + "/" + Global.CreditCardYear;
								repo.RetechCardPaymentView.TxtZipCode.TextValue = Global.CreditCardZip; 
			
								repo.RetechCardPaymentView.ProcessCreditCardView.PressKeys("{Enter}");
		//						Thread.Sleep(100);
								
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
							}
							else
							{	// ### This required for Retech 5.14 and above because Ranorex not see detail of new pinpad
								// 11/6/18 use WPF not UAI
								repo.RetechPeripheralHostWindow.SomeTabPageList.PinPadAcceptCommand.Click();
								Thread.Sleep(100);
								repo.RetechPeripheralHostWindow.SomeTabPageList.PinPadCardNumber.TextValue = Global.CreditCardNumber;
								repo.RetechPeripheralHostWindow.SomeTabPageList.PinPadOK.Click();
								repo.RetechPeripheralHostWindow.SomeTabPageList.PinPadMonth.TextValue = Global.CreditCardMonth;
								repo.RetechPeripheralHostWindow.SomeTabPageList.PinPadYear.TextValue = Global.CreditCardYear;
								repo.RetechPeripheralHostWindow.SomeTabPageList.PinPadOK.Click();
					            repo.RetechPeripheralHostWindow.PinPadZipCode.TextValue = Global.CreditCardZip;
					            repo.RetechPeripheralHostWindow.SomeTabPageList.AcceptCommand.Click();

// 11/6/18 replace by above
//					            repo.RetechPeripheralHostWindow.TabControl.Click("189;453");
//					            repo.RetechPeripheralHostWindow.TabControl.Click("31;84");
//					            Keyboard.Press(Global.CreditCardNumber);
//					            repo.RetechPeripheralHostWindow.TabControl.Click("157;472");
//					            repo.RetechPeripheralHostWindow.TabControl.Click("44;115");
//					            Keyboard.Press(Global.CreditCardMonth + "{Tab}" + Global.CreditCardYear);
//					            Thread.Sleep(200);
//					            repo.RetechPeripheralHostWindow.TabControl.Click("93;466");
//					            Thread.Sleep(200);
//					            repo.RetechPeripheralHostWindow.TabControl.Click("40;126");
//					            Thread.Sleep(350);
//					            Keyboard.Press(Global.CreditCardZip);
//					            repo.RetechPeripheralHostWindow.TabControl.Click("155;445");
//					            Thread.Sleep(100);
							}
							
							TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				            Global.CurrentMetricDesciption = @"[F6] enter Card info";
				            Global.Module = "Finalize";                
				            DumpStatsQ4.Run(); 
				            
						    MystopwatchQ4.Reset();	    
							MystopwatchQ4.Start();	 				            
							
							if(!Global.AbortScenario)
							{
								// 11/6/18 use WPF not UAI
					            repo.RetechPeripheralHostWindow.SomeTabPageList.ApplySignatureCommand.Click();
					            repo.RetechPeripheralHostWindow.SomeTabPageList.AcceptCommand.Click();
					            repo.RetechCardPaymentView.AcceptSignature.Click();
					            
// 11/6/18 replace by above
//								while(!Host.Local.TryFindSingle(repo.RetechPeripheralHostWindow.SignOnPinPadInfo.AbsolutePath.ToString(), out element))
//								{ Thread.Sleep(100); }
//								repo.RetechPeripheralHostWindow.SignOnPinPad.Click();
//								Thread.Sleep(200);
//								repo.RetechPeripheralHostWindow.AcceptOnPinPad.Click();
//								Thread.Sleep(200);
//								repo.RetechCardPaymentView.Self.Focus();
//								Thread.Sleep(100);
//								repo.RetechCardPaymentView.SignatureAcceptedCommand.Click();
//								Thread.Sleep(200);		
								
								TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
					            Global.CurrentMetricDesciption = @"Sign on pin pad";
					            Global.Module = "Finalize";                
					            DumpStatsQ4.Run(); 							
							}					

			     		}
						else
						{
						    MystopwatchQ4.Reset();	    
							MystopwatchQ4.Start();							
							repo.EnterSerialNumber.Self.PressKeys("{F9}");  // Seclect Credit Card
							TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				            Global.CurrentMetricDesciption = @"[F9] Seclect Credit Card";
				            Global.Module = "Finalize";                
				            DumpStatsQ4.Run(); 
							
						    MystopwatchQ4.Reset();	    
							MystopwatchQ4.Start();		
				            repo.Amount.Self.PressKeys("{Enter}");		// Enter amount paid
							TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				            Global.CurrentMetricDesciption = @"Enter amount paid";
				            Global.Module = "Finalize";                
				            DumpStatsQ4.Run(); 							
							
						    MystopwatchQ4.Reset();	    
							MystopwatchQ4.Start();	
				            repo.InternatiuonalCreditCardType.Self.PressKeys("{F1}");  // F1=Visa F2=MasterCard F3=American Express
							TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				            Global.CurrentMetricDesciption = @"[F1] Seclect F1=Visa";
				            Global.Module = "Finalize";                
				            DumpStatsQ4.Run(); 				            

				            repo.F1VISA.InternationalLast4OfCreditCard.PressKeys("9474{F5}");  // Enter last 4 digits of credit card
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
		     	
			            
			    MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();	 	

				if(Global.DomesticRegister)
				{
					// Wait for Another Transaction pop-up
					while(!Host.Local.TryFindSingle(repo.RetechAnotherTransactionInfo.AbsolutePath.ToString(), out element) )
					{	
						Thread.Sleep(100);
						if(Host.Local.TryFindSingle(repo.RetechTillOverageWarningView.CodeF4AlertInfo.AbsolutePath.ToString(), out element)
						  	||
						   Host.Local.TryFindSingle(repo.RetechTillOverageWarningView.CodeF4Alert2Info.AbsolutePath.ToString(), out element)
						  )
						{
							GlobalOverhead.Stopwatch.Start();					
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
							GlobalOverhead.Stopwatch.Stop();            		
						}
					}		
				}
				else
				{
					// Wait for log on screen
					if(   Global.CurrentScenario == 47 
					   || Global.CurrentScenario == 20 
					   || Global.CurrentScenario == 16 
					   || Global.CurrentScenario == 33 
					   || Global.CurrentScenario == 34 					   
					  )
					{
						Thread.Sleep(100);
						while(!Host.Local.TryFindSingle(repo.IPOSScreen.ChangeDueInfo.AbsolutePath.ToString(), out element) 
						 		&& !repo.LogOn.LoginID.HasFocus    
						     )
							{	
								InternationalCheckForCodeF4Alert(); // 11/1/18
								Thread.Sleep(200);	
							}
						while(Host.Local.TryFindSingle(repo.IPOSScreen.ChangeDueInfo.AbsolutePath.ToString(), out element) 
						     	&& !repo.LogOn.LoginID.HasFocus 
						     )
							{	
								InternationalCheckForCodeF4Alert(); // 11/1/18
								Thread.Sleep(200);	
							}
					}
					while(!Host.Local.TryFindSingle(repo.LogOn.LoginIDInfo.AbsolutePath.ToString(), out element) )
					{	Thread.Sleep(200);	}	
					while(!repo.LogOn.LoginID.Enabled)
					{	Thread.Sleep(200);	}	
				}
	     	}
	     	

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
			Global.CurrentMetricDesciption = @"Wait for another transaction ESC";
			Global.Module = "Finalize";                
			DumpStatsQ4.Run();  
			
			switch (Global.PayWithMethod)
			{
				case "Cash":
					Global.NowPayWithCash = Global.AdjustedTime;			     			
					break;
				case "Credit":
					Global.NowPayWithCredit = Global.AdjustedTime;			     			
					break;
				case "PURCC":	
					Global.NowPayWithPURCC = Global.AdjustedTime;		     			
					break;
			}			
			            
			MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	 	     	

			if(Global.DomesticRegister)
			{
				// Press excape key on Another Transaction pop-up
				while(!Host.Local.TryFindSingle(repo.RetechLoginView.TxtPasswordInfo.AbsolutePath.ToString(), out element) )
				{	Thread.Sleep(100);	}	
				repo.RetechLoginView.TxtPassword.PressKeys("{Escape}");
			}
			else
			{
				// Press escape on log on screen
//				repo.LogOn.Self.PressKeys("{Escape}");
//				repo.LogOn.LogOn.Click("231;6");
//            	Delay.Milliseconds(200);
//            	Keyboard.Press("{Escape}");
				Thread.Sleep(200);
				while(!repo.LogOn.LogOn.Enabled)
					{ Thread.Sleep(200); }
				repo.LogOn.LogOn.PressKeys("");
            	Delay.Milliseconds(200);
            	repo.LogOn.LogOn.PressKeys("{Escape}");
			}
			
			if(!Global.DomesticRegister)  // International Store
			{	// Catch hanging logon
				Thread.Sleep(100);
	    		if(Host.Local.TryFindSingle(repo.LogOn.LogOnInfo.AbsolutePath.ToString(), out element) )
					Keyboard.Press("{Escape}");
			}

            TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Wait for Start a Transaction ESC";
            Global.Module = "Finalize";                   
            DumpStatsQ4.Run();  			
			
            TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Finalize";                   
            DumpStatsQ4.Run();   			

			Global.LogText = "OUT fnCheckout";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;        	

        } 
        
        // International CodeF4 Check ##############################################################################
        //
        public void InternationalCheckForCodeF4Alert()
        {
        	RanorexRepository repo = new RanorexRepository();

        	Ranorex.Unknown element = null;
        	
        	if(Host.Local.TryFindSingle(repo.ReservationDeposit.CodeF4WarningInfo.AbsolutePath.ToString(), out element) )
        	{
	            repo.ReservationDeposit.CodeF4Warning.Click("201;9");
	            Delay.Milliseconds(200);
	            
	            repo.ReservationDeposit.RawTextF4Drop.Click("23;10");
	            Delay.Milliseconds(200);
	            
	            repo.ReservationDeposit.AfxOleControl42.PressKeys("{F4}");
	            Delay.Milliseconds(100);
	            
	            Keyboard.Press("psu");
	            
	            Keyboard.Press("advanced{Return}");
	            Delay.Milliseconds(100);
	            
	            repo.ReservationDeposit.Self.PressKeys("{F4}");
	            Delay.Milliseconds(1000);
           
	            Keyboard.Press("40000");
	            Delay.Milliseconds(100);
	            
	            Keyboard.Press("{Return}");
	            Delay.Milliseconds(100);
	            
	            Keyboard.Press("{Escape}");

        	}

            
        }
    }
}
