/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:24 AM
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
    /// Description of fnDumpStats.
    /// </summary>
    [TestModule("BBA78117-9FBB-4D85-8CF2-1F56B5D46D7B", ModuleType.UserCode, 1)]
    public class fnDumpStats : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDumpStats()
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

			// bool OpenFileForOutput = false;
			bool OpenFileForAppend = true;
         
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.StatsFileName, OpenFileForAppend))
            {
					file.WriteLine(	Global.RegisterName + "," +
					               	Global.ScenarioStartTime + "," +
					               	Global.ScenarioEndTime + "," + 
					               	Global.CurrentIteration + "," + 	
					               
					               	// Scenario 2 no longer exists
								   	Global.SkippedText + "," +
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               	Global.SkippedText + "," + 
					               
									Global.Scenario3ItemLookup + "," +
						            Global.Scenario3RecordsFound + "," + 
						            Global.Scenario3Total + "," + 										

									Global.Scenario4CustomerLookup + "," +
									Global.Scenario4CustomerLookupEnterToListTime + "," +
									Global.Scenario4F12CompleteTradein + "," +
									Global.Scenario4F12Total + "," +
									Global.Scenario4LastEnterToLogin + "," +
									Global.Scenario4Total + "," +
														
									Global.Scenario5CustomerLookup + "," +
									Global.Scenario5CustomerLookupEnterToListTime + "," +
									Global.Scenario5F12toTotal + "," +
									Global.Scenario5F5Total + "," +
									Global.Scenario5Total + "," +					               	

					               	Global.Scenario6customerLookup + "," + 
					               	Global.Scenario6CustomerLookupEnterToListTime + "," + 
					               	Global.Scenario6F12Search + "," + 
					               	Global.Scenario6RecordsFound + "," + 
					               	Global.Scenario6AddTranaction + "," + 
					               	Global.Scenario6F2Time + "," + 
					               	Global.Scenario6Total + "," + 
					               
									Global.Scenario7CustomerLookup + "," +
									Global.Scenario7CustomerLookupEnterToListTime + "," +
									Global.Scenario7F12toTotal + "," +
									Global.Scenario7Total + "," +
														
									Global.Scenario8LoadBrowser + "," +
									Global.Scenario8AddtoCart + "," +
									Global.Scenario8FirstCheckOut + "," +
									Global.Scenario8ContinuetoShipping + "," +
									Global.Scenario8ContinuefromShipping + "," +
									Global.Scenario8SubmitOrder + "," +
									Global.Scenario8POSOrderLookup + "," +
									Global.Scenario8F12toTotal + "," +
									Global.Scenario8F5Total + "," +
									Global.Scenario8Total + "," +
														
									Global.Scenario9CustomerLookup + "," +
									Global.Scenario9CustomerLookupEnterToListTime + "," +
									Global.Scenario9Enter10SKUs + "," +
									Global.Scenario9F12toTotal + "," +
									Global.Scenario9F5Total + "," +
									Global.Scenario9Total + "," +
														
									Global.Scenario10CustomerLookup + "," +
									Global.Scenario10CustomerLookupEnterToListTime + "," +
									Global.Scenario10Enter10SKUs + "," +
									Global.Scenario10F12toTotal + "," +
									Global.Scenario10F5Total + "," +
									Global.Scenario10Total + "," +
														
									Global.Scenario11CustomerLookup + "," +
									Global.Scenario11CustomerLookupEnterToListTime + "," +
									Global.Scenario11F3Search + "," +
									Global.Scenario11Logout2ndF2Time + "," +
									Global.Scenario11Total + "," +
														
									Global.Scenario12LoadBrowser + "," +	
									Global.Scenario12RecommerceLink + "," +
									Global.Scenario12RecommerceSearch + "," +
									Global.Scenario12GIConversionApp + "," +
									"," +	// Global.Scenario12WorkDay
									Global.Scenario12GoStores + "," +
									Global.Scenario12Total + "," +
									
									Global.IPOSVersion
								
								
								);
			} 

        }        
    }
}
