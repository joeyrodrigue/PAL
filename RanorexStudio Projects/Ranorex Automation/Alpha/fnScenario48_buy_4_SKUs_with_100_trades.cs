/*
 * Created by Ranorex
 * User: storeuser
 * Date: 06/01/15
 * Time: 8:18 AM
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
    /// Description of FnScenario47_buy_4_SKUs_with_100_trades.
    /// </summary>
    [TestModule("C5BAAD7E-4067-417A-9FB8-0FF09C87AF7C", ModuleType.UserCode, 1)]
    public class fnDoScenario48 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario48()
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
        {	//**************Start  fnScenario48 High volume trade 20 items**********************
			
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
        	
            Global.CurrentScenario = 48;
            Global.AbortScenario = false;

			if (!Global.DoScenarioFlag[Global.CurrentScenario])	
			{ 	
				return;
			}            
            
            Global.NumberOfTradesMinusOne = 99;	// Coded for 24 and 99
            
            FnCommon47and48 Common47and48 = new FnCommon47and48();
            fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();   

			Global.LogText = @"---> fnDoScenario48 Start Transaction Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 48 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));	            
            
            Common47and48.Run();   
            
			Global.LogText = "<--- fnDoScenario48 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	
            Report.Log(ReportLevel.Info, "Scenario 48 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));            

        }
    }
}
