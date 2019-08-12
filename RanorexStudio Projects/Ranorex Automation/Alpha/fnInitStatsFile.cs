/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:28 AM
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
    /// Description of fnInitStatsFile.
    /// </summary>
    [TestModule("8B54F461-1B56-4E0A-A33D-01CBCAD18E58", ModuleType.UserCode, 1)]
    public class fnInitStatsFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnInitStatsFile()
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
            
			Global.StatsFileName = Global.Register1DriveLetter + ":\\" + Global.StatsFileDirectory + "\\Register" + Global.RegisterNumber + ".csv";
	
			// If stats file does not exist then create it and init with headers
			if (!File.Exists(Global.StatsFileName))
			{
				// Init output .csv file
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.StatsFileName, OpenFileForOutput))
				{
					file.WriteLine("Register" + "," +
					               "Start Time" + "," + 
					               "End Time" + "," + 
					               "Iteration" + "," + 	
					               
								   "Scenario 2 Customer Lookup" + "," +
					               "Scenario 2 Customer Enter to list" + "," + 
					               "Scenario 2 Transaction Screen" + "," + 
					               "Scenario 2 Select Discount Type" + "," + 
					               "Scenario 2 Discount Type Select Time" + "," + 
					               "Scenario 2 Discount Type F12 Time" + "," + 
					               "Scenario 2 Check Transaction" + "," + 
					               "Scenario 2 F10 To End Transaction" + "," + 
					               "Scenario 2 Total" + "," + 

								   "Scenario 3 Item Lookup" + "," +
					               "Scenario 3 Records Found" + "," + 
					               "Scenario 3 Total" + "," + 

					               "Scenario 4 Customer Lookup" + "," + 
					               "Scenario 4 Customer Enter to list" + "," + 
					               "Scenario 4 F12 Complete Tradein" + "," + 
					               "Scenario 4 F12 Total" + "," + 
					               "Scenario 4 Last Enter To Login" + "," + 
					               "Scenario 4 Total Rec/Sec" + "," + 
					               
					               "Scenario 5 Customer Lookup" + "," + 
					               "Scenario 5 Customer Enter to list" + "," + 
					               "Scenario 5 F12 to Payment" + "," + 
					               "Scenario 5 F5 to Logout" + "," + 
					               "Scenario 5 Total" + "," + 
					               
					               "Scenario 6 Customer Lookup" + "," + 
					               "Scenario 6 Customer Enter to list" + "," + 
					               "Scenario 6 F12 Customer Search Time" + "," + 
					               "Scenario 6 Records Found" + "," + 
					               "Scenario 6 Add Transaction" + "," + 
					               "Scenario 6 Logout 2nd F2 Time" + "," + 
					               "Scenario 6 Total" + "," + 
					               
					               "Scenario 7 Customer Lookup" + "," + 
					               "Scenario 7 Customer Enter to list" + "," + 
					               "Scenario 7 F12 Total" + "," + 
					               "Scenario 7 Total" + "," + 
					               
					               "Scenario 8 Load Browser" + "," + 
					               "Scenario 8 Add to Cart" + "," + 
					               "Scenario 8 First Check Out" + "," + 
					               "Scenario 8 Continue to Shipping" + "," + 
					               "Scenario 8 Continue from Shipping" + "," + 
					               "Scenario 8 Submit Order" + "," + 
					               "Scenario 8 POS Order Lookup" + "," + 
					               "Scenario 8 F12 to Total" + "," + 
					               "Scenario 8 F5 Total" + "," + 
					               "Scenario 8 Total" + "," + 
					               
					               "Scenario 9 Customer Lookup" + "," + 
					               "Scenario 9 Customer Enter to list" + "," + 
					               "Scenario 9 Enter 10 SKUs" + "," + 
					               "Scenario 9 F12 to Total" + "," + 
					               "Scenario 9 F5 Total" + "," + 
					               "Scenario 9 Total" + "," + 
					               
					               "Scenario 10 Customer Lookup" + "," + 
					               "Scenario 10 Customer Enter to list" + "," + 
					               "Scenario 10 Enter 10 SKUs" + "," + 
					               "Scenario 10 F12 to Total" + "," + 
					               "Scenario 10 F5 Total" + "," + 
					               "Scenario 10 Total" + "," + 
					               
					               "Scenario 11 Customer Lookup" + "," + 
					               "Scenario 11 Customer Enter to list" + "," + 
					               "Scenario 11 F3 Search" + "," + 
					               "Scenario 11 Logout 2nd F2 Time" + "," + 
					               "Scenario 11 Total" + "," + 
					               
					               "Scenario 12 Load Browser" + "," + 	
					               "Scenario 12 Recommerce Link" + "," + 
					               "Scenario 12 Recommerce Search" + "," + 
					               "Scenario 12 GI Conversion App" + "," + 
					               "Scenario 12  WorkDay" + "," + 
					               "Scenario 12 GoStores" + "," + 
					               "Scenario 12 Total" + "," + 
					               
									"POS Version"					              
					              );
					

					
				}
		    }             
        }

    }
}
