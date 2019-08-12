/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 03/13/13
 * Time: 1:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;

using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace Alpha
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // Uncomment the following 2 lines if you want to automate Windows apps
            // by starting the test executable directly
            //if (Util.IsRestartRequiredForWinAppAccess)
            //    return Util.RestartWithUiAccess();
            
        	fnPlayWavFile PlayWavFile = new fnPlayWavFile();  
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
			SpeechSynthesizer Speech = new SpeechSynthesizer();        	

            Keyboard.AbortKey = System.Windows.Forms.Keys.Escape;
            int error = 0;
            
            // To autorun Scripts in directory <C: on reg1, D: on reg 2-n>/PAL/RanorexStudio Projects\Ranorex Automation\Alpha\bin\Debug
            //	NOTE: autorun is assumed to be type Baseline
            // 		RanorexAutomation.exe auto [<iteratins-time> [<Scenario-list>] ]
            //			<iteratins-time> = number iterations
            //			<iteratins-time> = time to stop "6:00 am"
            //			<iteratins-time> = time to start and stop "6:00 am to 3:00 PM"
            //			<Scenario-list> =  list if scenarios to run comma delited "3,4,6-20"
            //		If neither <iteratins-time> or <Scenario-list> supplied will run using same paramaters as last run
            //		Just auto <iterations-time> can be given, will ust last run scenario list

            // Get command line argumets
    		Global.CommandLineArg0 = "";
    		Global.CommandLineArg1 = "";
	   		Global.CommandLineArg2 = "";  	
	   		Global.CommandLineArg3 = "";  			   		
			string[] RanorexCmdLine = Environment.GetCommandLineArgs();
			int Arguments = args.Length;
			switch (Arguments) {
				case 1:
					Global.CommandLineArg0 = RanorexCmdLine[0].ToUpper();
					Global.CommandLineArg1 = RanorexCmdLine[1].ToUpper();					
					break;
				case 2:
					Global.CommandLineArg0 = RanorexCmdLine[0].ToUpper();
					Global.CommandLineArg1 = RanorexCmdLine[1].ToUpper();
					Global.CommandLineArg2 = RanorexCmdLine[2].ToUpper();						
					break;
				case 3:
					Global.CommandLineArg0 = RanorexCmdLine[0].ToUpper();
					Global.CommandLineArg1 = RanorexCmdLine[1].ToUpper();
					Global.CommandLineArg2 = RanorexCmdLine[2].ToUpper();	
					Global.CommandLineArg3 = RanorexCmdLine[3].ToUpper();						
					break;
			}

			// For debugging auto run
//			Global.CommandLineArg1 = "AUTO";
//			Global.CommandLineArg2 = "1";
//			Global.CommandLineArg3 = "3";
			
//			Report.Log(ReportLevel.Info, "CommandLineStuff", 
//			           "\n" + "Arglen: " + Arguments + "\n" +
//			           "Zero: " + Global.CommandLineArg0 + "\n" +
//			           "One: " + Global.CommandLineArg1 + "\n" +
//			           "Two: " + Global.CommandLineArg2 + "\n" +
//			           "Three: " + Global.CommandLineArg3 + "\n"			           
//			           , new RecordItemIndex(0));
//			Thread.Sleep(10000);
//			Environment.Exit(0);	

			// Onetime delay for Ranorex to setup instrumentation
			Thread.Sleep(5000);
			
			if(Global.CommandLineArg1 == "AUTO")
			{
				Global.AutoRun = true;
			}

            try
            {
                error = TestSuiteRunner.Run(typeof(Program), Environment.CommandLine);
            }
            catch (Exception e)
            {
                Report.Error("Unexpected exception occurred: " + e.ToString());
                error = -1;
                
				Global.LogText = "Unexpected exception occurred: " + e.ToString();
				WriteToLogFile.Run();	
				Global.TempErrorString = Global.LogText;
				WriteToErrorFile.Run();                
				Global.WavFilePath = "UnexpectedException.wav";
				PlayWavFile.Run();
				if(Global.DoRegisterSoundAlerts)
				{
					Speech.Speak("Register " + Global.RegisterNumber);  					
				}
            }
            
            fnTearDown Teardown = new fnTearDown();
			Report.Log(ReportLevel.Info, "Main", "Starting Teardown", new RecordItemIndex(0));            
            TestModuleRunner.Run(Teardown);
			Report.Log(ReportLevel.Info, "Main", "Teardown Finished", new RecordItemIndex(0));              
            
            return error;
        }
    }
}
