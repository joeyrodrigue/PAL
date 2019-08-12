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
    /// Description of fnDoScenario6.
    /// </summary>
    [TestModule("5673A555-C55D-4496-AD46-575A24E7D994", ModuleType.UserCode, 1)]
    public class fnDoScenario50 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario50()
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
        
        // ####################################################
	     private void EndScenarioCleanup()
	     {
	     	Ranorex.Unknown element = null; 
			RanorexRepository repo = new RanorexRepository();
			
	   		if(Global.DomesticRegister)  // Domestic Store
			{	// Domestic Hanging Notification Bar 9/24/18
				if(Host.Local.TryFindSingle(repo.HOPSOverlayView.InstructionsLabelInfo.AbsolutePath.ToString(), out element))
					repo.HOPSOverlayView.ExpandButton.Click();
			}
            while(File.Exists("c:\\PAL\\pause")) {Thread.Sleep(100); }			
            
			Global.IndirectCall = false;         // 12-3-18   
			Global.CurrentSKUOveride = false;	// 12-3-18	 
			Global.DoingCollectible = false;	// 12-3/18
	     }

	     // ####################################################
	     private void InitScenarioStart()
	     {
	     	// Ranorex.Unknown element = null; 
			RanorexRepository repo = new RanorexRepository();
			
			GlobalOverhead.Stopwatch.Reset(); 	

			Global.IndirectCall = false;         // 12-3-18   
			Global.CurrentSKUOveride = false;	// 12-3-18		
			Global.DoingCollectible = false;	// 12-3/18			
	     }        
        
        public void Run()
        {	//**************Start  Scenario 50 - Prepare for Performacne Dashboard test **********************
			
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
			RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();         	
        	fnWaitForItemSearchToFinish WaitForItemSearchToFinish = new fnWaitForItemSearchToFinish();   
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();   
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor(); 
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
			
        	//Ranorex.Unknown element = null;
        	Global.AbortScenario = false;
        	
            Global.CurrentScenario = 50;

			if (!Global.DoScenarioFlag[Global.CurrentScenario])	
			{ 	
				return;
			}
			
			Global.ScenarioExecuted = true;
			
           // Backoffice
            fnDoScenario13 DoScenario13 = new fnDoScenario13();  
            fnDoScenario14 DoScenario14 = new fnDoScenario14();  
            fnDoScenario15 DoScenario15 = new fnDoScenario15(); 
            fnDoScenario16 DoScenario16 = new fnDoScenario16(); 
			fnDoScenario18 DoScenario18 = new fnDoScenario18();  
			fnDoScenario19 DoScenario19 = new fnDoScenario19();
			fnDoScenario20 DoScenario20 = new fnDoScenario20();
			fnDoScenario21 DoScenario21 = new fnDoScenario21();

            // Retech
            fnDoScenario33 DoScenario33 = new fnDoScenario33();   
            fnDoScenario34 DoScenario34 = new fnDoScenario34();  
            fnDoScenario36 DoScenario36 = new fnDoScenario36();    
            fnDoScenario37 DoScenario37 = new fnDoScenario37(); 
            fnDoScenario47 DoScenario47 = new fnDoScenario47();
            
            for (Global.CurrentIteration = 1; Global.CurrentIteration <= 5; Global.CurrentIteration++)
            {
	            // Back Office
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario13.Run(); EndScenarioCleanup(); 
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario18.Run(); EndScenarioCleanup(); 
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario19.Run(); EndScenarioCleanup(); 
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario20.Run(); EndScenarioCleanup(); 
	            
	            // ReTech
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario16.Run(); EndScenarioCleanup(); 
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario33.Run(); EndScenarioCleanup(); 
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario34.Run(); EndScenarioCleanup();             
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario36.Run(); EndScenarioCleanup(); 
	            //InitScenarioStart(); Global.IndirectCall = true; DoScenario47.Run(); EndScenarioCleanup();             
	            
				// Additional special data
				//Global.CurrentSKUOverideValue = "924089";  // Preowned
				//InitScenarioStart(); Global.IndirectCall = true; Global.CurrentSKUOveride = true; DoScenario33.Run(); EndScenarioCleanup();  
				
				Global.CurrentSKUOverideValue = "121407";  // Collectible
				InitScenarioStart(); Global.IndirectCall = true; Global.CurrentSKUOveride = true; Global.DoingCollectible = true; DoScenario33.Run(); EndScenarioCleanup(); 
            }
			
			
			
			
			

			Global.LogText = "<--- fnDoScenario50 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	
            Report.Log(ReportLevel.Info, "Scenario 50 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));
			
            //***********End Scenario 50 ***************        	
        }
    }
}
