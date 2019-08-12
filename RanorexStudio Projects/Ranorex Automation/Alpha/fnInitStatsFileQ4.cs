/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 08/22/13
 * Time: 9:26 AM
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
    /// Description of fnInitStatsFileQ4.
    /// </summary>
    [TestModule("4231410E-7CCA-446B-BA27-66ECD9957151", ModuleType.UserCode, 1)]
    public class fnInitStatsFileQ4 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnInitStatsFileQ4()
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

			bool OpenFileForOutput = false;
			// bool OpenFileForAppend = true;
            
			Global.StatsFileNameQ4 = "C:\\" + Global.StatsFileDirectory + "\\AutomationMetrics.csv";
	
			// If stats file does not exist then create it and init with headers
			if (!File.Exists(Global.StatsFileNameQ4))
			{
				// Init output .csv file
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.StatsFileNameQ4, OpenFileForOutput))
				{
					file.WriteLine("Test_ID" + "," +
								   "Register_ID" + "," +
					               "AutoVer_ID" + "," +
					               "Scenario" + "," + 
					               "Module" + "," +
					               "Metric Description" + "," + 	
					               "Iteration" + "," + 						               
								   "Metric" + "," +
								   "Metric Time"
					              );
					

					
				}
		    }  
        }
    }
}
