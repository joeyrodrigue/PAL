/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:57 AM
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
    /// Description of fnReadPhoneNumbersFromCSVFile.
    /// </summary>
    [TestModule("11E8C7A4-191F-4899-9925-02661E72C2D8", ModuleType.UserCode, 1)]
    public class fnReadPhoneNumbersFromCSVFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnReadPhoneNumbersFromCSVFile()
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
			fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
			
    		Global.PhoneMaxOffset = 0;
    		Global.TotalNumberOfPhoneNumbers = 0;  			

            using (System.IO.StreamReader PhoneNumberFile = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\Input\PhoneNumbers.csv"))
			{
  
	        	Global.LogText = "IN fnReadPhoneNumbersFromCSVFile";
				WriteToLogFile.Run();	        	
				Global.LogFileIndentLevel++;	
			
            	int PhoneOffset = 1;
				string InputLine = "";
				InputLine = PhoneNumberFile.ReadLine(); // Skip first line it is the header
				
				for(PhoneOffset = 0; InputLine != null; PhoneOffset++)
								{	
					InputLine = PhoneNumberFile.ReadLine();
					if(InputLine != null)
					{	// Loyalty ("Pro", "Basic") Non-Loyalty ("NonMemberGenesis", "NonMemberProfile") or "" for all	
						
						string[] Numbers = InputLine.Split(',');
	 	
						Global.PhoneArrayLoyaltyPro[PhoneOffset] = Numbers[0];
						Global.PhoneArrayLoyaltyProCard[PhoneOffset] = Numbers[1];
						Global.PhoneArrayLoyaltyBasic[PhoneOffset] = Numbers[2];
						Global.PhoneArrayLoyaltyBasicCard[PhoneOffset] = Numbers[3];
						Global.PhoneArrayNonLoyaltyNonMemberGenesis[PhoneOffset] = Numbers[4];
						Global.PhoneArrayNonLoyaltyNonMemberProfile[PhoneOffset] = Numbers[5];		

			    		Global.PhoneMaxOffset = PhoneOffset;
			    		Global.TotalNumberOfPhoneNumbers = 4 * (Global.PhoneMaxOffset +1);
					}
				
				}
				PhoneNumberFile.Close();
			}	
            
            
			Global.LogFileIndentLevel--;
			Global.LogText = "OUT fnReadPhoneNumbersFromCSVFile";
			WriteToLogFile.Run();	            
        }
        
    }
}
