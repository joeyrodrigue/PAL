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
    /// Description of fnDoScenario47.
    /// </summary>
    [TestModule("5673A555-C55D-4496-AD46-575A24E7D994", ModuleType.UserCode, 1)]
    public class fnDoScenario47 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario47()
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
        {	//**************Start  fnScenario47 High volume trade 20 items**********************
			
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
        	
            Global.CurrentScenario = 47;
            Global.AbortScenario = false;

           	if(!Global.IndirectCall)
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
				{ 	
					return;
				}  

			Global.ScenarioExecuted = true;           	
            
            Global.NumberOfTradesMinusOne = 4;	// Coded for 4 and 99
            
            FnCommon47and48 Common47and48 = new FnCommon47and48();
            fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();   

			Global.LogText = @"---> fnDoScenario47 Start Transaction Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 47 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));	            
            
            Common47and48.Run();  
            
			Global.LogText = "<--- fnDoScenario47 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	
            Report.Log(ReportLevel.Info, "Scenario 47 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));            

        }
    }
}
