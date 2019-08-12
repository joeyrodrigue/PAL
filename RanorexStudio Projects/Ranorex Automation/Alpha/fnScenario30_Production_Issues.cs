/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:13 AM
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
    /// Description of fnDoScenario30.
    /// </summary>
    [TestModule("5673A555-C55D-4496-AD46-575A24E7D994", ModuleType.UserCode, 1)]
    public class fnDoScenario30 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario30()
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
        {	//**************Start  fnScenario30 Special Production Issues Test**********************
			
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;

            Global.CurrentScenario = 30;
            Global.AbortScenario = false;

			if (!Global.DoScenarioFlag[Global.CurrentScenario])	
			{ 	
				return;
			}    
           
			RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();    
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();   
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor(); 
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();

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

			Global.LogText = @"---> fnDoScenario30 Start Transaction Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 30 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			

			//#####################################################################################
			//	Production Issue: 1705/01 9/13 Access Lock Bug
			//	1.	Add SKU 930586 to the order.
			//	2.	Add a PUR pro customer to the order.
			//	3.	Pay $10.00 with cash.
			//	4.	Pay the rest with debit.
			bool Do_Issue_1705_01 = true;			
			//#####################################################################################            

			//#####################################################################################			
			//	Production Issue: 5886/01 8/10 Access Lock Bug Reg 1 Trans 14
			//	1.	Add a PUR pro customer to the order.
			//	2.	Return SKU 953162 as defective.
			//	3.	Purchase SKU 953162.
			bool Do_Issue_5886_01 = true;	
			//#####################################################################################			
			
			//#####################################################################################			
			//	Production Issue: 3432/01 6/21 Missing Transaction Tr5
			//	1.	Add a PUR pro customer to the order.
			//	2.	Trade SKU 112321
			//	3.	Trade SKU 954010
			//	4.	Trade SKU 109523
			//	5.	Trade SKU 954013
			//	6.	Trade SKU 109536
			//	7.	Trade SKU 103080
			//	8.	Trade SKU 107399
			//	9.	Purchase SKU 109386
			//	10.	Purchase SKU 101820
			//	11.	Pre-Order SKU 113170, with a deposit of $9.92
			//	12.	Pay the rest with cash.
			bool Do_Issue_3432_01 = true;	
			//#####################################################################################				

			// Exedute the issue tests
			if(Do_Issue_1705_01) Issue_1705_01();	
			if(Do_Issue_5886_01) Issue_5886_01();
			if(Do_Issue_3432_01) Issue_3432_01();

			Global.LogText = "<--- fnDoScenario30 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	
            Report.Log(ReportLevel.Info, "Scenario 30 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));            

        }
          

 		//#####################################################################################
		//	Production Issue: 1705/01 9/13 Access Lock Bug
		//	1.	Add SKU 930586 to the order.
		//	2.	Add a PUR pro customer to the order.
		//	3.	Pay $10.00 with cash.
		//	4.	Pay the rest with debit.
		//#####################################################################################       
       	private void Issue_1705_01()
        {
       		FnProductionIssueFunctions ProductionIssueFunctions = new FnProductionIssueFunctions();
       		fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
       		
			RanorexRepository repo = new RanorexRepository();
			
			Global.LogText = " Start Issue_1705_01"; 
			WriteToLogFile.Run();
			
			ProductionIssueFunctions.SpecialStartTransaction();
			
 			Global.NumberSpecialSKUs = 1;
 			Global.SpecialSKUs[1] = "930586";
            ProductionIssueFunctions.SpecialPurchaseSKUs();
            
 			Global.SpecialCustomerPhoneNumber = "8175551133";
 			ProductionIssueFunctions.SpecialEnterCustomer();            

			Global.SpecialCheckingOut = false;
			Global.PayWithMethod = "Cash";
			Global.SpecialPayThisAmount = "10.00";            
            ProductionIssueFunctions.SpecialPreCheckOut();		
            
        	Global.SpecialCheckingOut = true;
			Global.PayWithMethod = "Credit";
			Global.SpecialPayThisAmount = "All";            
            ProductionIssueFunctions.SpecialPreCheckOut();	
            
            ProductionIssueFunctions.SpecialCheckForCodeF4Alert();
            
            repo.RetechLoginView.TxtPassword.PressKeys("{Escape}"); // initial sign on screen
            
			Global.LogText = " End Issue_1705_01"; 
			WriteToLogFile.Run();

            if(Directory.Exists(Global.MiniDumpDirectory))
            {	Environment.Exit(0);	}             
       	}
      
       	
       	

       	
       	
		//#####################################################################################			
		//	Production Issue: 5886/01 8/10 Access Lock Bug Reg 1 Trans 14
		//	1.	Add a PUR pro customer to the order.
		//	2.	Return SKU 953162 as defective.
		//	3.	Purchase SKU 953162.
		//#####################################################################################		       	
       	private void Issue_5886_01() 
       	{
       		FnProductionIssueFunctions ProductionIssueFunctions = new FnProductionIssueFunctions();
       		fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
       		
			RanorexRepository repo = new RanorexRepository();
			
			Global.LogText = " Start Issue_5886_01"; 
			WriteToLogFile.Run();
			
			ProductionIssueFunctions.SpecialStartTransaction();
            
 			Global.SpecialCustomerPhoneNumber = "8175551133";
 			ProductionIssueFunctions.SpecialEnterCustomer(); 
 			
 			Global.NumberSpecialSKUs = 1;
 			Global.SpecialSKUs[1] = "953162";
            ProductionIssueFunctions.SpecialReturnSKUs();
            
 			Global.NumberSpecialSKUs = 1;
 			Global.SpecialSKUs[1] = "953162";
            ProductionIssueFunctions.SpecialPurchaseSKUs();    
            
			Global.SpecialCheckingOut = false;
			Global.PayWithMethod = "Cash";
			Global.SpecialPayThisAmount = "All";            
            ProductionIssueFunctions.SpecialPreCheckOut();	
            
            ProductionIssueFunctions.SpecialCheckForCodeF4Alert();
            
            repo.RetechLoginView.TxtPassword.PressKeys("{Escape}"); // initial sign on screen
            
            Global.LogText = " End Issue_5886_01"; 
            WriteToLogFile.Run();

            if(Directory.Exists(Global.MiniDumpDirectory))
            {	Environment.Exit(0);	}	       		
       	}       	
       	
       	
       	
		//#####################################################################################			
		//	Production Issue: 3432/01 6/21 Missing Transaction Tr5
		//	1.	Add a PUR pro customer to the order.
		//	2.	Trade SKU 112321
		//	3.	Trade SKU 954010
		//	4.	Trade SKU 109523
		//	5.	Trade SKU 954013
		//	6.	Trade SKU 109536
		//	7.	Trade SKU 103080
		//	8.	Trade SKU 107399
		//	9.	Purchase SKU 109386
		//	10.	Purchase SKU 101820
		//	11.	Pre-Order SKU 113170, with a deposit of $9.92
		//	12.	Pay the rest with cash.
		//#####################################################################################	       	
       	private void Issue_3432_01() 
       	{
       		FnProductionIssueFunctions ProductionIssueFunctions = new FnProductionIssueFunctions();
       		fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
       		
			RanorexRepository repo = new RanorexRepository();
			
			Global.LogText = " Start Issue_3432_01"; 
			WriteToLogFile.Run();
			
			ProductionIssueFunctions.SpecialStartTransaction();
            
 			Global.SpecialCustomerPhoneNumber = "8175551133";
 			ProductionIssueFunctions.SpecialEnterCustomer();    

            Global.NumberSpecialTrades = 7;	
            Global.SpecialTrades[1] = "112321";
            Global.SpecialTrades[2] = "954010";
            Global.SpecialTrades[3] = "109523";
            Global.SpecialTrades[4] = "954013";
            Global.SpecialTrades[5] = "109536";
            Global.SpecialTrades[6] = "103080";
            Global.SpecialTrades[7] = "107399";	
            ProductionIssueFunctions.SpecialEnterTradess();

 			Global.NumberSpecialSKUs = 3;
 			Global.SpecialSKUs[1] = "109386";
 			Global.SpecialSKUs[2] = "101820"; 	
 			Global.SpecialInitialDeposit = "9.92";
 			Global.SpecialSKUs[3] = "108274";  // Pre-Order item
            ProductionIssueFunctions.SpecialPurchaseSKUs();

			Global.SpecialCheckingOut = false;
			Global.PayWithMethod = "Cash";
			Global.SpecialPayThisAmount = "All";            
            ProductionIssueFunctions.SpecialPreCheckOut();	
            
            ProductionIssueFunctions.SpecialCheckForCodeF4Alert();

            repo.RetechLoginView.TxtPassword.PressKeys("{Escape}"); // initial sign on screen
            
            Global.LogText = " End Issue_3432_01"; 
            WriteToLogFile.Run();

            if(Directory.Exists(Global.MiniDumpDirectory))
            {	Environment.Exit(0);	}		     		
       	}            	
       	
       	
       	
        
        
    }
}
