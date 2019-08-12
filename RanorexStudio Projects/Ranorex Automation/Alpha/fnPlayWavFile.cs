/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:51 AM
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
    /// Description of fnPlayWavFile.
    /// </summary>
    [TestModule("D55FF869-3621-4D96-B149-F3A3A4180CA4", ModuleType.UserCode, 1)]
    public class fnPlayWavFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnPlayWavFile()
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
            System.Media.SoundPlayer PlaySound = new System.Media.SoundPlayer();          	

            // To generate the sound files go to  AT&T Labs Natural Voice Text-To-Speech site
            //		URL: http://www2.research.att.com/~ttsweb/tts/demo.php
            // Type in what you want it to say
            // Slect voice Mike US English
            // Press the download button to save an a .wav file and put file in Ranorex Automation directory
  
           	string MyWavFilePath = Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + Global.WavFilePath;            
			PlaySound.SoundLocation = MyWavFilePath;            	

			if(Global.DoRegisterSoundAlerts)
			{
				PlaySound.PlaySync();
			}

        }
    }
}
