/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 11/06/13
 * Time: 7:51 AM
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
    /// Description of fnReadSimpleOneSKUsFromCSVFile.
    /// </summary>
    [TestModule("AE1168FE-9F03-4C08-A67C-AA94EE3543FA", ModuleType.UserCode, 1)]
    public class fnReadSimpleOneSKUsFromCSVFile : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnReadSimpleOneSKUsFromCSVFile()
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
    		Global.TotalNumberSimpleOneSKUs = 0;  			

            using (System.IO.StreamReader SimpleOneSKUFile = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\Input\SimpleOneSKUs.csv"))
			{
  
	        	Global.LogText = "IN fnReadSimpleOneSKUsFromCSVFile";
				WriteToLogFile.Run();	        	
				Global.LogFileIndentLevel++;	
			
            	int SKUOffset = 1;
				string InputLine = "";
				InputLine = SimpleOneSKUFile.ReadLine(); // Skip first line it is the header
				
				for(SKUOffset = 0; InputLine != null; SKUOffset++)
								{	
					InputLine = SimpleOneSKUFile.ReadLine();
					if(InputLine != null)
					{						
						string[] Numbers = InputLine.Split(',');
	 	
						Global.SimpleOneSku[SKUOffset] = Numbers[0];
						Global.SimpleOneSkuDescription[SKUOffset] = Numbers[1];

			    		Global.TotalNumberSimpleOneSKUs = SKUOffset;  							
					}
				
				}
				SimpleOneSKUFile.Close();
			}	
            
            // Initialize first SKU offset 
            Global.SimpleOneSKUsOffset = Convert.ToInt32(Global.RegisterNumber) - 1;
       
			Global.LogFileIndentLevel--;
			Global.LogText = "OUT fnReadSimpleOneSKUsFromCSVFile";
			WriteToLogFile.Run();	 
        }
    }
}
