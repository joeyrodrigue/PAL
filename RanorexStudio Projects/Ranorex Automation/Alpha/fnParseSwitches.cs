/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 09/11/13
 * Time: 10:48 AM
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
    /// Description of fnParseSwitches.
    /// </summary>
    [TestModule("FB45AA1B-A9E7-4B96-B01E-C80FF72394F6", ModuleType.UserCode, 1)]
    public class fnParseSwitches : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnParseSwitches()
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
        
        public void Run(string TempText)
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;        	
        	
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile(); 
        	fnPlayWavFile PlayWavFile = new fnPlayWavFile();
        	
			Global.LogFileIndentLevel++;
        	Global.LogText = "IN fnParseSwitches";
			WriteToLogFile.Run();	        	

			if (TempText.Contains("/"))
			{	// Process switches
				if(TempText.Contains("N") | TempText.Contains("L"))
				{	// if any of these are given then reset all and only use ones listed
					Global.SwitchPhoneNumbersNonLoyalty = false;
					Global.SwitchPhoneNumbersLoyalty = false;	
				}
				if ( TempText.Contains("P") ) { Global.SwitchPauseBetweenScenariosOff = true; }
				if ( TempText.Contains("N") ) { Global.SwitchPhoneNumbersNonLoyalty = true; }
				if ( TempText.Contains("L") ) { Global.SwitchPhoneNumbersLoyalty = true; }
				if ( TempText.Contains("A") ) { Global.SwitchAllRegistersUseAllPhoneNumbers = true; }
				if ( TempText.Contains("S") ) { Global.SwitchScenario9Use40SKUs = true; }
				if ( TempText.Contains("M") ) { Global.SwitchMetricOverRide = true; }
				if ( TempText.Contains("Q") ) { Global.SwitchQuitRunningOnError = true; }	
				if ( TempText.Contains("U") ) { Global.SwitchUploadOnly = true; Global.IsPerformanceTest = true;}	
				if ( TempText.Contains("X") ) 
				{ 
					Global.SwitchSkipCustomerLookup = true;
					if(		Global.DoScenarioFlag[17] || Global.DoScenarioFlag[20] 
					   || 	Global.DoScenarioFlag[21] || Global.DoScenarioFlag[22] || Global.DoScenarioFlag[23] || Global.DoScenarioFlag[25] )
					{
						Global.WavFilePath = "SlashXSwitchSelections.wav";
						PlayWavFile.Run();							
					}
					Global.DoScenarioFlag[17] = false;	// the /X switch cannot override phone looku						
					Global.DoScenarioFlag[20] = false;	
					Global.DoScenarioFlag[21] = false;							
					Global.DoScenarioFlag[22] = false;	
					Global.DoScenarioFlag[23] = false;	
					Global.DoScenarioFlag[25] = false;							
				}
			}
			else if (TempText.Contains("-"))
			{	// Process a range like 3-5
				string[] RangeItems = TempText.Split('-');	
				
				for (int RangeOff = Convert.ToInt32(RangeItems[0]); RangeOff <= Convert.ToInt32(RangeItems[1]); RangeOff++)
				{
					Global.DoScenarioFlag[RangeOff] = true;
				}
			}
			else
			{	// Process single item
				if(TempText != "")
				{
					Global.DoScenarioFlag[ Convert.ToInt32(TempText) ] = true;
				}
			}			
			
			Global.LogText = "OUT fnParseSwitches";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;
        }          
    }
}
