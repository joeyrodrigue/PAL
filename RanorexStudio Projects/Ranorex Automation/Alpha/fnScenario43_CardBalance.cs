﻿/*
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
    public class fnDoScenario43 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario43()
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
        {	//*****************Start  Scenario 40 - Retech - Card Balance ******************
        	
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

        	Global.CurrentScenario = 43;

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
			
			Global.LogText = @"---> fnDoScenario43 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 40 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			
			
			// Start 		


			// Check card balance
       		MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();	
       		repo.ShellView.CardBalanceButton.Click();
       		Thread.Sleep(100);
       		repo.CardBalanceInquiryView.AccountNumber.TextValue = "3876608052056";
       		repo.CardBalanceInquiryView.AccountNumber.PressKeys("{Enter}");
       		while(!repo.CardBalanceInquiryView.PowerUpRewardsCardBalanceText.Visible)
       			{   Thread.Sleep(1000); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
			Global.CurrentMetricDesciption = "Check Card Balance";
			Global.Module = "Check Card Balance";                
			DumpStatsQ4.Run();  	 
			Global.CurrentMetricDesciption = "Module Total Time";
			DumpStatsQ4.Run();	
			Thread.Sleep(2000);
       		Keyboard.Press("{Escape}");	
       		
       				
            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 43";
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

			Global.LogText = "<--- fnDoScenario43 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 43 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
			
            Thread.Sleep(2000);
			
			// ***********End Scenario 43*****************        	
        }
        
   
        
        
    }
}
