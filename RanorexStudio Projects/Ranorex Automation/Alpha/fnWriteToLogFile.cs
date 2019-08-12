/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/24/13
 * Time: 2:05 PM
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
    /// Description of fnWriteToLogFile.
    /// </summary>
    [TestModule("C0BC2937-77F3-4259-A37D-0FCE63B289FF", ModuleType.UserCode, 1)]
    public class fnWriteToLogFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnWriteToLogFile()
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
            
        	RanorexRepository repo = new RanorexRepository();
        	
            // System.DateTime DateTimeNow = System.DateTime.Now;
			// System.TimeSpan TimeNow = DateTimeNow.TimeOfDay;    

			// Write out failure to error .csv file	(Global.TempString contains text to be written)
			string TextForLog = 	Global.RegisterName + "," +
									System.DateTime.Now.ToString() + "," +
				            		Global.CurrentIteration + "," + 	
									"Scenario: " + Global.CurrentScenario + "," +
				            		"".PadLeft(Global.LogFileIndentLevel,' ') + "".PadLeft(Global.LogFileIndentLevel,' ') + Global.LogText;

			using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.LogFileName, Global.OpenFileForAppend))
			{	file.WriteLine(TextForLog);
			} 
			
			int a = Global.CurrentIteration;
			int b = Global.CurrentScenario;
			string t = Global.LogText;
				
			Report.Log(ReportLevel.Info, "fnWriteToLogFile", "Iteration: " + Global.CurrentIteration + "  " +
			             									 "Scenario: " + Global.CurrentScenario + 
			             									 " End: " + Global.IterationsText + "\n"	+
			             				             		 Global.LogText, new RecordItemIndex(0));				
        }
    }
}
			               