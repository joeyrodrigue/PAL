/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 10/07/13
 * Time: 11:42 AM
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
    /// Description of fnDumpQ4StatsBuffer.
    /// </summary>
    [TestModule("DF77DA09-64BC-4A80-AA39-D4950AA840AC", ModuleType.UserCode, 1)]
    public class FnWriteOutStatsQ4Buffer : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnWriteOutStatsQ4Buffer()
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
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();    
        	
			Global.LogFileIndentLevel++;
        	Global.LogText = "IN FnWriteOutStatsQ4Buffer";
			WriteToLogFile.Run();	        	

			// Write out metrics buffer
			// bool OpenFileForOutput = false;
			bool OpenFileForAppend = true;			
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.StatsFileNameQ4, OpenFileForAppend))
            {
				file.Write(	Global.Q4StatBuffer );
			}     
            
			Global.Q4StatBuffer = "";   
			
			int aa = Global.ScenariosToday;
			Global.ScenariosToday++;	// PAL Status Monitor
			aa = Global.ScenariosToday;
            
			Global.LogText = "OUT FnWriteOutStatsQ4Buffer";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;
        }          
    }
}
