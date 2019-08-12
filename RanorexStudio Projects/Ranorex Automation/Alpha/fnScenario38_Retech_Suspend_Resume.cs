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
    /// Description of fnDoScenario38.
    /// </summary>
    [TestModule("5372A447-AB16-4A86-8BD0-976B858B269C", ModuleType.UserCode, 1)]
    public class fnDoScenario38 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario38()
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
        {	//*****************Start  Scenario 38 - Retech - Suspend Resume ******************
        	
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

        	Global.CurrentScenario = 38;
        	
			// Get current register allowed to run Scenario 38 - Suspend/Resume 
			string FileToUse = "AllowedSuspendResumeRegister.txt";        	
        	string AllowedRegister = "1";
	        try
	        {
		        // Read in the allowed register
				using (System.IO.StreamReader RegisterScenarioFileGet = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + FileToUse))
				{
					AllowedRegister = RegisterScenarioFileGet.ReadLine();
					RegisterScenarioFileGet.Close();
				}	            	
	        }
	        catch
	        {
		        // Write out new allowed register
				using (System.IO.StreamWriter  RegisterScenarioFilePut = new System.IO.StreamWriter(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + FileToUse))
				{
					RegisterScenarioFilePut.WriteLine(AllowedRegister);
					RegisterScenarioFilePut.Close();
				}	            	
	        }        	
        	
	        bool OkForThisRegisterToRun = true;
	        if(AllowedRegister != Global.RegisterNumber)
				OkForThisRegisterToRun = false;        	

        	if(!Global.IndirectCall)
				if (!Global.DoScenarioFlag[Global.CurrentScenario] || !OkForThisRegisterToRun)	
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

			Stopwatch MystopwatchSusRes = new Stopwatch();				
			
			Global.LogText = @"---> fnDoScenario38 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 38 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
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
				
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
				Global.CurrentMetricDesciption = "[F1] wait for add item";
				Global.Module = "F1 Add Item";                
				DumpStatsQ4.Run();  	
						
				Global.CurrentMetricDesciption = "Module Total Time";
				DumpStatsQ4.Run();		
			}

			Global.CurrentSKU = Global.S4Sku1;
			EnterSKU.Run();
			Thread.Sleep(100);

			
			// Suspend Transaction ---------------------------------------------------------------
			MystopwatchSusRes.Reset();	
			MystopwatchSusRes.Start();	 			
			
			//repo.Retech.DataContextSuspendOrderCommand.Click(); Thread.Sleep(100);
			Keyboard.Press(System.Windows.Forms.Keys.F11 | System.Windows.Forms.Keys.Control, 87, Keyboard.DefaultKeyPressTime, 1, true);
            repo.UserConfirmationView.ConfirSuspend.Click();  Thread.Sleep(100);
			while(!repo.RetechLoginView.TxtPassword.Enabled) { Thread.Sleep(100); }
			Keyboard.Press("{Escape}");

			TimeMinusOverhead.Run((float) MystopwatchSusRes.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
			Global.CurrentMetricDesciption = "Suspend";
			Global.Module = "Suspend";                
			DumpStatsQ4.Run(); 			
			
			Global.CurrentMetricDesciption = "Module Total Time";
			DumpStatsQ4.Run();
			
			Global.LogText = "Pausing after supend " + Global.CurrentIteration;
			WriteToLogFile.Run();	 			
			Thread.Sleep(4000);  // give some time for processkng
			
			// Resume Transaction -----------------------------------------------------------------------
			MystopwatchSusRes.Reset();
			MystopwatchSusRes.Start();	 

			Global.AbortScenario = false;
			ResumeTransaction();

			TimeMinusOverhead.Run((float) MystopwatchSusRes.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
			Global.CurrentMetricDesciption = "Resume";
			Global.Module = "Resume";                
			DumpStatsQ4.Run(); 			
			
			Global.CurrentMetricDesciption = "Module Total Time";
			DumpStatsQ4.Run();			
			
			if(!Global.AbortScenario)
			{
				// @#@#@# C H E C K O U T #@#@#@	----------------------------------
				Global.PayWithMethod = "Cash";
				Checkout.Run(); 
			
	            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.CurrentMetricDesciption = @"Scenario 38";
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
	
				Global.LogText = "<--- fnDoScenario38 Iteration: " + Global.CurrentIteration;
				WriteToLogFile.Run();	      			
	            Report.Log(ReportLevel.Info, "Scenario 38 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));				
			}
			
	        // When Suspend/Resume done allow next register to run it
	        string StoreName = Global.RegisterName.Substring(0,8);
	        int MaxAllowed = 0;
			switch (StoreName) {
				case "USA00414":
					MaxAllowed = 3;
					break;
				case "USA04285":
					MaxAllowed = 3;
					break;
				case "USA01763":
					MaxAllowed = 6;
					break;
				case "USA02157":
					MaxAllowed = 2;
					break;
			}   
        		
	
        	decimal DecAllowed = Convert.ToDecimal(AllowedRegister);
 			if (DecAllowed + 1 > MaxAllowed)
				DecAllowed = 1;
 			else
 				DecAllowed = DecAllowed + 1;
	
 			AllowedRegister = DecAllowed.ToString();
 				
	        // Write out new allowed register
			using (System.IO.StreamWriter  RegisterScenarioFilePut = new System.IO.StreamWriter(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + FileToUse))
			{
				RegisterScenarioFilePut.WriteLine(AllowedRegister);
				RegisterScenarioFilePut.Close();
			}	              
            
            Thread.Sleep(2000);
			
			// ***********End Scenario 38*****************        	
        }
        
        
        
        
        
public void ResumeTransaction()
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

			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();
			MystopwatchModuleTotal.Reset();	
			MystopwatchModuleTotal.Start();	       	

			Global.LogText = @"Start Transaction";
			WriteToLogFile.Run(); 	

			MystopwatchQ4.Reset();	
			MystopwatchQ4.Start();					

			if(Global.DomesticRegister)
			{
 				Global.LogText = @"Clicking on ReTech screen then press F5";
	            WriteToLogFile.Run();
	            repo.ReTechStartTransaction.Click();
	            repo.ReTechStartTransaction.PressKeys("{F6}");           

				if(Host.Local.TryFindSingle(repo.GenericDialogView.CriticalErrorSavingTransactionCallHInfo.AbsolutePath.ToString(), out element))	
				{
					repo.GenericDialogView.ErrorSavingButtonOK.Click();
					Thread.Sleep(200);
					Global.TempErrorString = "Error Saving Transaction";
					WriteToErrorFile.Run();
					Global.LogText = Global.TempErrorString;
					WriteToLogFile.Run();	
				}	
				
 				Global.LogText = @"Waiting for logon";
	            WriteToLogFile.Run();
				while(!Host.Local.TryFindSingle(repo.RetechLoginView.TxtPasswordInfo.AbsolutePath.ToString(), out element))			
				{	
	            	Thread.Sleep(200);	
  
					if(Host.Local.TryFindSingle(repo.GenericDialogView.CriticalErrorSavingTransactionCallHInfo.AbsolutePath.ToString(), out element))	
					{
						repo.GenericDialogView.ErrorSavingButtonOK.Click();
						Thread.Sleep(200);
						Global.TempErrorString = "Error Saving Transaction";
						WriteToErrorFile.Run();
						Global.LogText = Global.TempErrorString;
						WriteToLogFile.Run();	
					}				
	            } 
			}
			else
			{
	            // Click on F5 POS Register - international
	            repo.IPOSScreen.ContainerF7BackOffice.Click("32;55");
            	Delay.Milliseconds(200);
            	Keyboard.Press("{F5}");

				while(!repo.LogOn.InternationalLoginID.Enabled)			
				{	Thread.Sleep(100);	} 
				
				repo.LogOn.InternationalLoginID.PressKeys("psu");
			}
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"[F6] Resume Transaction";
            Global.Module = "Log On";                
            DumpStatsQ4.Run();              
            
			// Enter Password
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	 			
        
			if(Global.DomesticRegister)
				{
 				Global.LogText = @"Waiting for Password field enabled";
	            WriteToLogFile.Run();				
	            while(!repo.ReTechLogonPassword.Enabled)
				{	Thread.Sleep(100);	} 		
	            Global.LogText = @"Entering password";
	            WriteToLogFile.Run();	
				repo.RetechLoginView.TxtPassword.PressKeys("advanced{Return}");

				// If no transactions found cancel and skip to next register so script will not stop
				if(Host.Local.TryFindSingle(repo.SelectSuspendedOrderView1.NoTransactionsHaveBeenSuspendedTodaInfo.AbsolutePath.ToString(), out element))
				{
					repo.SelectSuspendedOrderView1.CancelCommand.Click();
					Thread.Sleep(100);
					Global.AbortScenario = true;
				}
				else
				{
					// Wait for add line command 2/2/18
					if(repo.SelectSuspendedOrderView.ResumeCommandInfo.Exists(5000) )
					   {}
					while(!Host.Local.TryFindSingle(repo.SelectSuspendedOrderView.ResumeCommandInfo.AbsolutePath.ToString(), out element))			
						{ Thread.Sleep(200); }  // 1/31/18
					repo.SelectSuspendedOrderView.ResumeCommand.Click();
					Thread.Sleep(1000);
					
					// If transaction service error - try reclicking on save button
					if(Host.Local.TryFindSingle(repo.SelectSuspendedOrderView.ErrorMessageTxtInfo.AbsolutePath.ToString(), out element))
						repo.SelectSuspendedOrderView.ResumeCommand.Click();	           
		           	
		           	// If Customer Info then just skip by continue
		           	if(Host.Local.TryFindSingle(repo.Retech.ContinueButtonCommand2Info.AbsolutePath.ToString(), out element))
		           	{
		           		repo.Retech.ContinueButtonCommand2.Click();
		           		Thread.Sleep(100);
		           	}
		           	
		           	// If Continue click it
		           	if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
		           	{
		           		repo.ContinueButtonCommand.Click();
		           		Thread.Sleep(100);
		           	}					
				}
			}
			else
			{
				repo.LogOn.InternationalPassword.PressKeys("advanced{Return}");
	           	while(!repo.ReservationDeposit.EnterSerialNumber.Enabled)
				{	Thread.Sleep(100);	}  
	           	repo.ReservationDeposit.EnterSerialNumber.PressKeys("{Escape}");
			}

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "Enter Password";
            Global.Module = "Log On";                
            DumpStatsQ4.Run();          
            
            TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Module Total Time";
            Global.Module = "Log On";                   
            DumpStatsQ4.Run();   
         	
        }        
        
        
        
        
        
    }
}
