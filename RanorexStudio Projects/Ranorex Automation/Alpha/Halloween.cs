/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 11/04/13
 * Time: 6:06 AM
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
    /// Description of Halloween.
    /// </summary>
    [TestModule("529EDEEA-DD53-4FCA-94B1-84DBA1F413AA", ModuleType.UserCode, 1)]
    public class Halloween : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Halloween()
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
        {	Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
        	RanorexRepository repo = new RanorexRepository();
        	
        	fnPlayWavFile PlayWavFile = new fnPlayWavFile(); 
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
            System.Media.SoundPlayer PlaySound = new System.Media.SoundPlayer();        	
        	Random random = new Random();
        	
        //*****************Start  Scenario 3 - halloween sounds ******************
			Global.CurrentScenario = 3;
			if (!Global.DoScenarioFlag[Global.CurrentScenario])	
			{ 	return;
			}			
			
			int NumberOfWavFiles = 0;
			string[] WavFileNames = new string[50];
            string WavSetStr, Prompt, DelayStrA, DelayStrB;
            int DelayA, DelayB;
            
			
			// Read in names of .wav files
            using (System.IO.StreamReader HalloweenSoundsFile = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\HalloweenSounds.csv"))
			{
            	int Offset = 1;
				string InputLine = "";
				
				for(Offset = 0; InputLine != null; Offset++)
								{	
					InputLine = HalloweenSoundsFile.ReadLine();
					if(InputLine != null)
					{	WavFileNames[Offset] = InputLine;
			    		NumberOfWavFiles++;  							
					}
				
				}
				HalloweenSoundsFile.Close();
			}			
            
            Prompt = "Enter Set number 1 to 6 to use\n" +
                     "    Set 1: Haunted sounds\n" +
                     "    Set 2: Heartbeat\n" +
                     "    Set 3: Storms\n" +
                     "    Set 4: Bubles\n" +
                     "    Set 5: Sceams & Howling\n" +
                     "    Set 6: Door & Moaning";
			InputBoxResult BoxInput = InputBox.Show(Prompt,"Wav set to play", "");
			if(BoxInput.ReturnCode == DialogResult.Cancel) { Environment.Exit(0); }	// Exit if cancel pressed
			WavSetStr = BoxInput.Text.ToUpper();
            
            Prompt = "Enter A for Thread.Sleep(random.Next(A,B)) if format A,B";
			InputBoxResult BoxInput2 = InputBox.Show(Prompt,"Random Play String", "");
			if(BoxInput2.ReturnCode == DialogResult.Cancel) { Environment.Exit(0); }	// Exit if cancel pressed
			DelayStrA = BoxInput2.Text.ToUpper(); 
			DelayA = Convert.ToInt32(DelayStrA);
            
            Prompt = "Enter B for Thread.Sleep(random.Next(A,B)) if format A,B";
			InputBoxResult BoxInput3 = InputBox.Show(Prompt,"Random Play String", "");
			if(BoxInput3.ReturnCode == DialogResult.Cancel) { Environment.Exit(0); }	// Exit if cancel pressed
			DelayStrB = BoxInput3.Text.ToUpper(); 
			DelayB = Convert.ToInt32(DelayStrB);

            while(1 != 2)
            {	
			    // Process the list of files found in the directory. 
				string sourceDir = Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\HalloweenSounds\" + "Set" + WavSetStr;
			    string [] fileEntries = Directory.GetFiles(sourceDir);
			    foreach(string fileName in fileEntries)
			    {
			       	Console.WriteLine(fileName);
			       	PlaySound.SoundLocation = fileName;
			       	PlaySound.PlaySync();
					Thread.Sleep(random.Next(DelayA,DelayB));			       
			    }            				
            }
			

			
		// ***********End Scenario 3*****************        	
        }        
    }
}
