/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:57 AM
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
    /// Description of fnGetInterationsToRun.
    /// </summary>
    [TestModule("A8DB5EF5-D057-4930-9B88-35226C98592F", ModuleType.UserCode, 1)]
    public class fnGetInterationsToRun : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetInterationsToRun()
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
			fnParseSwitches ParseSwitches = new fnParseSwitches();			
        	
			// Get default scenario list 
			//	- use DefaultScenarioList.txt for all but last register
			// 	- use DefaultIterationsLastRegister.txt for the last register in a store
     		string DefaultIterations = "1";
    		string NumberIterations = "";
//			if( Global.RegisterNumber == "4" || ( Global.RegisterName == "USA04285-3" )
//			  )
//				NumberIterations = "DefaultIterationsLastRegister.txt";
//			else
				NumberIterations = "DefaultIterations.txt";        	

        	try
        	{
	            // Read in the default iterations from Register 1 \Ranorex Automation\DefaultInterations.txt
				using (System.IO.StreamReader RegisterIniFileGet = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + NumberIterations))
				{
					DefaultIterations = RegisterIniFileGet.ReadLine();
					RegisterIniFileGet.Close();
				}	        		
        	}
        	catch
        	{
	            // Write out default iterations to Register 1 \Ranorex Automation\DefaultInterations.txt
				using (System.IO.StreamWriter  RegisterIniFilePut = new System.IO.StreamWriter(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + NumberIterations))
				{
					RegisterIniFilePut.WriteLine("1");
					RegisterIniFilePut.Close();
				}	        		
        	}
            

            string TextInput;
            
            string Prompt = "Enter number of iterations\n" +
							"    or time to stop like HH:MM PM\n" +
							"    or time to start & stop like HH:MM AM to HH:MM PM\n\n" +            	
            				"Or /U for metric upload only\n\n" +
            	
							"Switches: put comma in front of switches #,/Q\n" +
	            			"    P=Pause between scenarios off\n" +            	
							"    N=Phone Numbers NonLoyalty\n" +
            				"    L=Phone Numbers Loyalty\n" +
							"    A=All Registers use all Phone Numbers\n" +
							"    M=Metrics file create when no save them\n" +
            				"    Q=Quit running when get error";

            string IDLine = " (TestID: " + Global.Test_ID + ", " + "RegID: " + Global.Register_ID + ", " + "VerID: " + Global.AutoVer_ID + ") ";
            
            if(Global.AutoRun)
            {
            	if(Global.CommandLineArg2 != "")
            		TextInput = Global.CommandLineArg2;
            	else
            		TextInput = DefaultIterations;
            }
            else
            {
	            InputBoxResult BoxInput = InputBox.Show(Prompt, "Iterations" + IDLine, DefaultIterations);
				if(BoxInput.ReturnCode == DialogResult.Cancel) { Environment.Exit(0); }	// Exit if cancel pressed    
				TextInput = BoxInput.Text.ToUpper(); 	
				Global.IterationsText = TextInput;				
            }
            

			Global.TimeToStartExecution = "";	
			Global.TimeToStopExecution = "";	
			
			
			
			// Check for Upload Metrics Only
			string[] PromptItems = TextInput.Split(',');
			if(PromptItems[0].Substring(0,1) == "/")
			{
				if(PromptItems[0].Substring(0,2) == "/U")
				{
					ParseSwitches.Run(PromptItems[0]);
				}			
			}
				else
			{					    
				if (TextInput.Contains(":"))
				{
					if (TextInput.Contains(" TO "))
					{	int ToOffset = TextInput.IndexOf(" TO ");
						Global.TimeToStartExecution = TextInput.Substring(0,ToOffset);
						Global.TimeToStopExecution = TextInput.Substring(ToOffset + 4);	
					}
					else
					{	Global.TimeToStopExecution = TextInput;
					}
					Global.IterationsToDo = -1;						
				}
				else
				{
					Global.IterationsToDo = Convert.ToInt32(PromptItems[0]);							
				}
				
				// Check for given switches
				if(PromptItems.Length > 1)
				{
					if(PromptItems[1].Substring(0,1) == "/")
					{
						ParseSwitches.Run(PromptItems[1]);
					}
				}
     				
		        // Write out default iterations to Register 1 \Ranorex Automation\DefaultInterations.txt
				using (System.IO.StreamWriter  RegisterIniFilePut = new System.IO.StreamWriter(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + NumberIterations))
				{
					RegisterIniFilePut.WriteLine(TextInput);
					RegisterIniFilePut.Close();
				}	
			}
			
			
        }
    }
}
