/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:22 AM
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
using System.Configuration;
using System.Data.SqlClient;

namespace Alpha
{
    /// <summary>
    /// Description of fnGetScenariosToRun.
    /// </summary>
    [TestModule("823B70A5-E4A5-4333-BABE-F461C61FF1AC", ModuleType.UserCode, 1)]
    public class fnGetScenariosToRun : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetScenariosToRun()
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
        	if(!Global.SwitchUploadOnly)
        	{
	            Mouse.DefaultMoveTime = 300;
	            Keyboard.DefaultKeyPressTime = 100;
	            Delay.SpeedFactor = 1.0;
	            
				RanorexRepository repo = new RanorexRepository();
				fnParseSwitches ParseSwitches = new fnParseSwitches();
				fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
				fnPlayWavFile PlayWavFile = new fnPlayWavFile();

				// Get default scenario list 
				//	- use DefaultScenarioList.txt for all but last register
				// 	- use DefaultScenarioListLastRegister.txt for the last register in a store
				string ListToUse = "";
				string DefaultScenarios = "13,16,18,19,20,33,34,36,37,41,42,43,47";
//				if( Global.RegisterNumber == "4" || ( Global.RegisterName == "USA04285-3" )
//				  )
//					ListToUse = "DefaultScenarioListLastRegister.txt";
//				else
					ListToUse = "DefaultScenarioList.txt";

	            try
	            {
		            // Read in the default Scenario List from Register 1 \Ranorex Automation\DefaultScenarioList.txt
					using (System.IO.StreamReader RegisterScenarioFileGet = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + ListToUse))
					{
						DefaultScenarios = RegisterScenarioFileGet.ReadLine();
						RegisterScenarioFileGet.Close();
					}	            	
	            }
	            catch
	            {
		            // Write out default Scenario List to Register 1 \Ranorex Automation\DefaultScenarioList.txt
					using (System.IO.StreamWriter  RegisterScenarioFilePut = new System.IO.StreamWriter(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + ListToUse))
					{
						RegisterScenarioFilePut.WriteLine(DefaultScenarios);
						RegisterScenarioFilePut.Close();
					}	            	
	            }
	               
	            
	            string TextInput;
	            string Prompt = "Enter list of scenarios like 3,5-7,/LX\n\n" +
	            	
								"Back Office ---------------------------------------------------\n" +
								"Scenario 13: eMail\n" +    
	            	 	        "Scenario 14: Performance Dashboard\n" +    
	            				"Scenario 15: Performance Dashboard Loop without exiting\n" +	
	            				"Scenario 18: Transaction Journal\n" +  
	            				"Scenario 19: Cover Art\n" + 
	            				"Scenario 20: HR Workday Employee List\n" + 
	            				"Scenario 21: WIS Web-In-Store\n\n" +

								"RETECH --------------------------------------------------------\n" +   
	            				"Scenario 16: Returns\n" +	            	
	            				"Scenario 33: Simple One SKU Cash\n" +
								"Scenario 34: Simple One SKU Credit\n" +
								"Scenario 36: Item Search\n" +     
	            				"Scenario 37: Reserve Pickup\n" +
	            				"Scenario 38: Suspend Resume\n" +	         
								"Scenario 41: GPG\n" +		
								"Scenario 42: PRP\n" +		
								"Scenario 43: Card Balance\n" +			            	
								"Scenario 47: Purchase 1 SKU with 5 trades and cach back\n\n" +   
	            	
								"Other --------------------------------------------------------\n" +   
	            				"Scenario 50: Dashboard data init (about 20 minutes)\n\n" +		            	
	            	
								"Switches: put comma in front of switches 2-4,7,/NXS\n" +
								"    N=Phone Numbers NonLoyalty\n" +
	            				"    L=Phone Numbers Loyalty\n" +
								"    A=All Registers use all Phone Numbers\n" +
								"    X=Skip Customer Lookup (NO 4,7,17,20-23,25)\n" +  
								"    M=Metrics file create when no save them\n" +
	            				"    Q=Quit running when get error\n" +
								"    S=Scenario 9 use 40 SKUs"; 
	            
	            if(Global.AutoRun)
	            {
	            	if(Global.CommandLineArg3 != "")
	            		TextInput = Global.CommandLineArg3;
	            	else
	            		TextInput = DefaultScenarios;            	
	            }
	            else
	            {
					InputBoxResult BoxInput = InputBox.Show(Prompt, "Scenarios", DefaultScenarios);
					if(BoxInput.ReturnCode == DialogResult.Cancel) { Environment.Exit(0); }	// Exit if cancel pressed
					TextInput = BoxInput.Text.ToUpper();	            	
	            }
            
	            // Write out default Scenario List to Register 1 \Ranorex Automation\DefaultScenarioList.txt
				using (System.IO.StreamWriter  RegisterScenarioFilePut = new System.IO.StreamWriter(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\" + ListToUse))
				{
					RegisterScenarioFilePut.WriteLine(TextInput);
					RegisterScenarioFilePut.Close();
				}				
				
	
				// Remove all spaces from the input
				TextInput = TextInput.Replace(" ", System.String.Empty);
				
				// Set flag if just Scenario 32 is selected - it consumes card numbers - so cannot run as part of normal run
				if(TextInput == "32")
				{
					Global.JustScenario32 = true;
				}
	
				// Clear the DoScenarioFlag array
				for (int Offset = 0; Offset <= Global.MaxScenarioNumber; Offset++) {	Global.DoScenarioFlag[Offset] = false;	}
	
				string[] PromptItems = TextInput.Split(',');
				string TempText;
				int PromptItemsCount = PromptItems.Length;
				int ParseOffset;
				for (ParseOffset = 0; ParseOffset <= PromptItemsCount -1; ParseOffset++)
				{
					TempText = PromptItems[ParseOffset];
					ParseSwitches.Run(TempText);
				}    			
							
				// If you can connect to the PALDB then provide the option to upload the test.
				var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString);
				try
	    		{
	    			Report.Log(ReportLevel.Info, "fnGetScenariosToRun", "Open connection to DB", new RecordItemIndex(0));	
					connection.Open();
	    			connection.Close();
				    var mSystemInfo = new FnSystemInfo();
					TestModuleRunner.Run(mSystemInfo);
	    		}
	    		catch (Exception e)
	    		{
	    			Global.IsPerformanceTest = false;
	    			Global.DBAvailable = false;
	    			
		 			string sayString = "";
					if(e.ToString().IndexOf("Thread was being aborted.") == -1)
					{
						sayString = e.ToString().Substring(0,28);
						Global.TempErrorString = "Cannot connect to Metric DB: " + e.Message;
						WriteToErrorFile.Run();
						//MessageBox.Show(e.ToString(),Global.TempErrorString);
					}
	    		}
        	}
        }
        
        
        
    }
}
