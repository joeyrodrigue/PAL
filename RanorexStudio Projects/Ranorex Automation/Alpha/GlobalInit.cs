/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:25 AM
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
    /// Description of fnGlobalInit.
    /// </summary>
    [TestModule("A26B7581-4149-439D-9EC1-669870AA5134", ModuleType.UserCode, 1)]
    public class fnGlobalInit : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGlobalInit()
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
			
			// Give audio feedback on VM store for develompent
//			if( Global.RegisterName == "USA01020-1" || Global.RegisterName == "USA01020-2" )
//				Global.DoRegisterSoundAlerts = true;
			
			if(File.Exists("c:\\Program Files\\GameStop\\Source\\Source.exe"))
				Global.DomesticRegister = true;
			else
				Global.DomesticRegister = false;

			Global.UsePhoneNumberType = 1;
	    	Global.PhoneNumberOffset = Convert.ToInt32(Global.RegisterNumber) - 1;
	    	
	    	Global.Scenario9ExtraSkusOffset = 1;

	    	Global.OverridePhoneNumber = "";
	    	Global.OverrideSKU = "";	    	

    		Global.CreditCardNumber = "3566007770019474";
    		Global.CreditCardMonth = "09";
    		Global.CreditCardYear = "49"; 
    		Global.CreditCardZip = "33606";   
    		
//    		if (File.Exists(@"c:\POS\POS.exe"))
//    			Global.RegisterRunningIPOS = true;
//			else
//    			Global.RegisterRunningIPOS = false;	    		
//    		if (File.Exists(@"c:\Program Files\GameStop\Source\Source.exe"))
//    			Global.RegisterRunningRetech = true;
//			else
//    			Global.RegisterRunningRetech = false;	    		
    		
    		// Set flag for IPOS and Retech running
    		Process[] CKiPOS = Process.GetProcessesByName("POS");
    		if(CKiPOS.Length == 0) Global.RegisterRunningIPOS = false;
    		else Global.RegisterRunningIPOS = true;
    		
     		Process[] CKRetech = Process.GetProcessesByName("SOURCE");
    		if(CKRetech.Length == 0) Global.RegisterRunningRetech = false;
    		else Global.RegisterRunningRetech = true;   
    		
    		if(Global.RegisterRunningIPOS && Global.RegisterRunningRetech)
    		{
    			Global.CombinedIPOS = true;
    		}
  
			string OStest = Environment.OSVersion.ToString();
			if(OStest.Contains("5.1")) Global.OSVersion = "XP";
				else if(OStest.Contains("6.1")) Global.OSVersion = "W7";
					 else Global.OSVersion = "??";    		
    		
    		Global.LogFileIndentLevel = -1;
    		
			// Set all switches to default
		    Global.SwitchPhoneNumbersNonLoyalty = true;
		    Global.SwitchPhoneNumbersLoyalty = true;
		    Global.SwitchAllRegistersUseAllPhoneNumbers = false;
		    Global.SwitchScenario9Use40SKUs = false;	
		    Global.SwitchSkipCustomerLookup = false;	
		    Global.SwitchMetricOverRide = false;	
		    Global.SwitchQuitRunningOnError = false;
		    Global.SwitchUploadOnly = false;
		    Global.SwitchPauseBetweenScenariosOff = false;

	    	// Read in default data from InputData.csv
	    	// Format Title (column A) then Value (column B)
 	
            using (System.IO.StreamReader InputDataFile = new System.IO.StreamReader(Global.Register1DriveLetter + @":\" + Global.AutomationFileDirectory + @"\Input\InputData.csv"))
			{	
				Global.S4Sku1 = InputDataFile.ReadLine().Split(',')[1];					
				
				Global.S5Sku1 = InputDataFile.ReadLine().Split(',')[1];   
				Global.S5Sku2 = InputDataFile.ReadLine().Split(',')[1];   	
				Global.S8Sku = InputDataFile.ReadLine().Split(',')[1];   				
				    	
				Global.S9SKU1 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU2 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU3 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU4 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU5 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU6 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU7 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU8 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU9 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU10 = InputDataFile.ReadLine().Split(',')[1];     	
				Global.S9SKU11 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU12 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU13 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU14 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU15 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU16 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU17 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU18 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU19 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU20 = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S9SKU21 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU22 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU23 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU24 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU25 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU26 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU27 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU28 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU29 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU30 = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S9SKU31 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU32 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU33 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU34 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU35 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU36 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU37 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU38 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU39 = InputDataFile.ReadLine().Split(',')[1];
				Global.S9SKU40 = InputDataFile.ReadLine().Split(',')[1];    
				    	
				Global.S10SKU1 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU2 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU3 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU4 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU5 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU6 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU7 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU8 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU9 = InputDataFile.ReadLine().Split(',')[1];
				Global.S10SKU10 = InputDataFile.ReadLine().Split(',')[1];   
				
				Global.S11Sku1 = InputDataFile.ReadLine().Split(',')[1];
				Global.S11Zip = InputDataFile.ReadLine().Split(',')[1];   	

// 	Q4 variables	
				Global.S14SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S15SKU = InputDataFile.ReadLine().Split(',')[1];
				Global.S15PostalCode = InputDataFile.ReadLine().Split(',')[1];   	
				
				Global.S16SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S17SKU = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S17PowerUpNumber = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S17Trade1 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade2 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade3 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade4 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade5 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade6 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade7 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade8 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade9 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade10 = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S17Trade11 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade12 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade13 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade14 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade15 = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S17Trade16 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade17 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade18 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade19 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade20 = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S17Trade21 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade22 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade23 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade24 = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S17Trade25 = InputDataFile.ReadLine().Split(',')[1]; 				

				Global.S18SKU = InputDataFile.ReadLine().Split(',')[1]; 	
				Global.S18SKUSerial = InputDataFile.ReadLine().Split(',')[1]; 	

				Global.S19SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S20SKUa = InputDataFile.ReadLine().Split(',')[1]; 
				Global.S20SKUb = InputDataFile.ReadLine().Split(',')[1]; 				

 				Global.S21SKU = InputDataFile.ReadLine().Split(',')[1];
				
 				Global.S22SKU = InputDataFile.ReadLine().Split(',')[1];
				
 				Global.S23SKU = InputDataFile.ReadLine().Split(',')[1];
				
 				Global.S24SKU = InputDataFile.ReadLine().Split(',')[1];
				
 				Global.S25SKUa = InputDataFile.ReadLine().Split(',')[1];
 				Global.S25SKUb = InputDataFile.ReadLine().Split(',')[1]; 				

 				Global.S26SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S27SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S28SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S29SKU = InputDataFile.ReadLine().Split(',')[1];
				
				Global.S30SKU = InputDataFile.ReadLine().Split(',')[1];

				InputDataFile.Close();				
			}	            
        }
      
    }
}
