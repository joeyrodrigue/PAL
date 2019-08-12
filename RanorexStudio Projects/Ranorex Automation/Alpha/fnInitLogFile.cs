/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/24/13
 * Time: 2:01 PM
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
    /// Description of fnInitLogFile.
    /// </summary>
    [TestModule("B60C2AC6-0573-4B09-A304-30070FF79A4D", ModuleType.UserCode, 1)]
    public class fnInitLogFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnInitLogFile()
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
            
			RanorexRepository repo = new RanorexRepository();

			Global.LogFileName = Global.Register1DriveLetter + ":\\" + Global.ReportsFileDirectory + "\\Register" + Global.RegisterNumber + "Log.csv";			

			// If stats file does not exist then create it and init with headers
			if (!File.Exists(Global.LogFileName))
			{
				// Init output .csv file
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.LogFileName, Global.OpenFileForOutput))
				{
					file.WriteLine("Register" + "," +
					               "Time" + "," + 
					               "Iteration" + "," + 	
					               
								   "Scenario" + "," +
					               "Log Entry");
					
				}
		    }  
        }
    }
}
