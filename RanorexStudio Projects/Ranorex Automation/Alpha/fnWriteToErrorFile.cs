/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 11:00 AM
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

using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace Alpha
{
    /// <summary>
    /// Description of fnWriteToErrorFile.
    /// </summary>
    [TestModule("A2A31D60-6873-4591-808B-0EF74D92D7AC", ModuleType.UserCode, 1)]
    public class fnWriteToErrorFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnWriteToErrorFile()
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
        	SpeechSynthesizer Speech = new SpeechSynthesizer();
        	fnPlayWavFile PlayWavFile = new fnPlayWavFile();     
        	
            // System.DateTime DateTimeNow = System.DateTime.Now;
			// System.TimeSpan TimeNow = DateTimeNow.TimeOfDay;          	
            
			// Play error sound
	           Global.WavFilePath = "Error.wav";
	           PlayWavFile.Run();
        
			// Write out failure to error .csv file	(Global.TempString contains text to be written)
			string TextToPrint = 	Global.RegisterName + "," +
									System.DateTime.Now.ToString() + "," +
				               		Global.CurrentIteration + "," + 	
							   		"Scenario: " + Global.CurrentScenario + "," +
				               		Global.TempErrorString;
			
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(Global.ErrorFileName, Global.OpenFileForAppend))
			{	file.WriteLine(TextToPrint);
			}  
			
			Global.ErrorsToday++;	// PAL Status Monitor
			
			if(Global.SwitchQuitRunningOnError)
			{
				if(Global.DoRegisterSoundAlerts)
				{
					Speech.Speak("An error has occurred, aborting the run");					
				}				
				Environment.Exit(0);
			}				

        }        
    }
}
