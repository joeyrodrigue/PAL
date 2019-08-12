/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:26 AM
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
    /// Description of fnEnterCustomerInformation.
    /// </summary>
    [TestModule("41DF8B5B-CE9E-444B-94C4-59CBA13F3DDF", ModuleType.UserCode, 1)]
    public class fnEnterCustomerInformation : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnEnterCustomerInformation()
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
            Mouse.DefaultMoveTime = 0;
            Keyboard.DefaultKeyPressTime = 0;
            Delay.SpeedFactor = 0.0;
        }
        
        public void Run()
        {	
            Mouse.DefaultMoveTime = 0;
            Keyboard.DefaultKeyPressTime = 0;
            Delay.SpeedFactor = 0.0;        	
        	
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile(); 
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	
        	Global.LogText = "IN fnEnterCustomerInformation";
			WriteToLogFile.Run();	        	
			Global.LogFileIndentLevel++;			

			// Create new stopwatch
			Stopwatch Mystopwatch = new Stopwatch();
            Mystopwatch.Reset();	    
			Mystopwatch.Start();			
			
			Ranorex.Unknown element = null;  
			string MyXPATH;
            
            // Will only fill in fields that are enabled and blank
            
            //repo.TransactionCustomerInformation.SearchTimeout = 100;
            while(!repo.TransactionCustomerInformation.PnlContent.FirstName.Enabled) 
            { Thread.Sleep(100); }

            // First Name
            if(	repo.TransactionCustomerInformation.PnlContent.FirstName.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.FirstName.TextValue == "")
            {	
            	repo.TransactionCustomerInformation.PnlContent.FirstName.TextValue = "PALFirst";
            }

			// Middle Name
            if(	repo.TransactionCustomerInformation.PnlContent.MiddleName.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.MiddleName.TextValue == "")
            {	
            	repo.TransactionCustomerInformation.PnlContent.MiddleName.TextValue = "PALMiddle";
            }			

           	// Last Name
            if(	repo.TransactionCustomerInformation.PnlContent.LastName.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.LastName.TextValue == "")
            {	
           		repo.TransactionCustomerInformation.PnlContent.LastName.TextValue = "PALLast";
           	}
            
            // Address 1
           	if(	repo.TransactionCustomerInformation.PnlContent.Address1.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.Address1.TextValue == "")
            {	repo.TransactionCustomerInformation.PnlContent.Address1.TextValue = "301 Bronco Street";
           	}
                        
            // Primary Phone
            if(	repo.TransactionCustomerInformation.PnlContent.PrimaryPhone.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.PrimaryPhone.TextValue == "")
            {	repo.TransactionCustomerInformation.PnlContent.PrimaryPhone.TextValue = "9727651234";
           	}           
            
            // Address 2
           	if(	repo.TransactionCustomerInformation.PnlContent.Address2.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.Address2.TextValue == "")
            {	repo.TransactionCustomerInformation.PnlContent.Address2.TextValue = "301 Bronco Street";
           	}
                               
            // Mobile Phone
            if(	repo.TransactionCustomerInformation.PnlContent.MobilePhone.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.MobilePhone.TextValue == "")
            {	repo.TransactionCustomerInformation.PnlContent.MobilePhone.TextValue = "9727654321";
           	}                  
            
            // Email
//            if(	repo.TransactionCustomerInformation.PnlContent.Email.Enabled &&
//            	repo.TransactionCustomerInformation.PnlContent.Email.TextValue == "")
            if(	repo.EmailValue.Enabled &&
            	repo.EmailValue.TextValue == "")            	
            {	
				// Create a unique name consisting of time down to millisecond
				System.DateTime DateTimeNow = System.DateTime.Now;
				System.TimeSpan TimeNow = DateTimeNow.TimeOfDay;
				string UniqueName = TimeNow.ToString().Replace(":","");
				UniqueName = UniqueName.Replace(".","");            	
            	repo.EmailValue.TextValue = "PAL" + UniqueName + "@PALMail.com";
           	}              
            
            // City
            if(	repo.TransactionCustomerInformation.PnlContent.City.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.City.TextValue == "")
            {	
            	repo.TransactionCustomerInformation.PnlContent.City.TextValue = "Little Elm";
           	}                   
            
            // Select State from drop down
             if(repo.TransactionCustomerInformation.PnlContent.StateDropDown.SelectedItemIndex == 0 &&
              	repo.TransactionCustomerInformation.PnlContent.StateDropDown.Enabled )
            {	
	            repo.TransactionCustomerInformation.RawText6.Click("6;5");
	            Delay.Milliseconds(200);
	            
	            repo.TransactionCustomerInformation.RawText6.Click("6;5");
	            Delay.Milliseconds(200);
	            
	            repo.ListItemsValues.ListItemMB.Click("55;7");
	            Delay.Milliseconds(200);		        
            }

            // Zip
            if(	repo.TransactionCustomerInformation.PnlContent.ZipCode.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.ZipCode.TextValue == "")
            {	
            	repo.TransactionCustomerInformation.PnlContent.ZipCode.TextValue = "75068";
           	}                  
            
            // Date of Birth
            if(	repo.TransactionCustomerInformation.TradeInformation.DateOfBirth.Enabled &&
            	repo.TransactionCustomerInformation.TradeInformation.DateOfBirth.TextValue == "  /  /")
            {	
            	repo.TransactionCustomerInformation.TradeInformation.DateOfBirth.TextValue = "09111952";
            }

            // Male/Female
			if(repo.TransactionCustomerInformation.TradeInformation.MaleFemaleDropDownArrow.TextColor != "#FFFFFF")	// Is "#FFFFFF" when shaded out - not enabled
			{	
				repo.TransactionCustomerInformation.TradeInformation.MaleFemaleDropDownArrow.Click("6;8");	// NOTE must be pressed twice
		        repo.TransactionCustomerInformation.TradeInformation.MaleFemaleDropDownArrow.Click("6;8"); 
		        repo.ListItemsValues.Female.Click("99;7");   				
			}

            // Eye Color
			if(repo.TransactionCustomerInformation.TradeInformation.EyeColorDropDownArrow.TextColor != "#FFFFFF") // Is "#FFFFFF" when shaded out - not enabled
			{	
				repo.TransactionCustomerInformation.TradeInformation.EyeColorDropDownArrow.Click("5;7");	// NOTE must be pressed twice
		        repo.TransactionCustomerInformation.TradeInformation.EyeColorDropDownArrow.Click("5;7");    
		        repo.ListItemsValues.Blue.Click("115;6"); 				
			}
            
            // Hair Color
			if(repo.TransactionCustomerInformation.TradeInformation.HairColorDropDownArrow.TextColor != "#FFFFFF")  // Is "#FFFFFF" when shaded out - not enabled
			{	
				repo.TransactionCustomerInformation.TradeInformation.HairColorDropDownArrow.Click("8;8");	// NOTE must be pressed twice
		       	repo.TransactionCustomerInformation.TradeInformation.HairColorDropDownArrow.Click("8;8"); 
		        repo.ListItemsValues.Black.Click("99;2");  				
			}

            // Tax ID
			MyXPATH  = repo.TransactionCustomerInformation.PnlContent.TaxIDInfo.AbsolutePath.ToString();
			if(Host.Local.TryFindSingle(MyXPATH, out element) )
            {	
				repo.TransactionCustomerInformation.PnlContent.TaxID.TextValue = "46464645645645";
           	}               
			
            // Organization
            MyXPATH = repo.TransactionCustomerInformation.PnlContent.OrganizationNameInfo.AbsolutePath.ToString();
		    if(	Host.Local.TryFindSingle(MyXPATH, out element) )
            {	
		    	repo.TransactionCustomerInformation.PnlContent.OrganizationName.TextValue = "My Ogranization";
           	}              

            // Select "Other" for Reason for Exemption
            MyXPATH = repo.TransactionCustomerInformation.PnlContent.ReasonForExemptionDropDownArrowInfo.AbsolutePath.ToString();
            if( Host.Local.TryFindSingle(MyXPATH, out element) )
            {	
            	repo.TransactionCustomerInformation.PnlContent.ReasonForExemptionDropDownArrow.Click("6;5");
	        	//Thread.Sleep(50);                 	
            	repo.ListItemsValues.Other.Click("211;10");              	
            }

            // Country
            if(	repo.TransactionCustomerInformation.PnlContent.Country.Enabled &&
            	repo.TransactionCustomerInformation.PnlContent.Country.TextValue == "")
            {	
            	repo.TransactionCustomerInformation.PnlContent.Country.TextValue = "US";
           	}   
 
            Keyboard.Press("{F5}");
            Thread.Sleep(100);
			
			// Check for Refund Warning pop-up if yes press F5 for cash
			if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.WarningPowerUpMemberInfo.AbsolutePath.ToString(), out element) )
			{	
				Global.LogText = "Popup - Warning Power Up Member";
				WriteToLogFile.Run();	
				Keyboard.Press("{F5}");
				Thread.Sleep(100);
			}			

			// Check for "If the customer also want to update their GI subscription ..."
			// NOTE: this has same xpath as no customer found by phone number popup
			if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.WarningPowerUpMemberInfo.AbsolutePath.ToString(), out element) )
			{	
				Global.LogText = "Popup - If the customer also want to update their GI subscription ...";
				WriteToLogFile.Run();	
				Keyboard.Press("{F5}");
				Thread.Sleep(100);
				Keyboard.Press("{F5}");
				Thread.Sleep(100);				
			}	

			// Check for if customer want to update GI subscription
			if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.PopUpIfWantToUpdateGIInfo.AbsolutePath.ToString(), out element) )
			{	
				Global.LogText = "Popup - if customer want to update GI subscription";
				WriteToLogFile.Run();	
				Keyboard.Press("{F5}");
				Thread.Sleep(100);
			}	
			
			while(Host.Local.TryFindSingle(repo.TransactionCustomerInformation.PnlContent.FirstNameInfo.AbsolutePath.ToString(), out element))
			{
				Thread.Sleep(100);
					
				// Check for Invalid email address
				if(Host.Local.TryFindSingle(repo.InvalidFieldDialog.InvalidEmailAddressInfo.AbsolutePath.ToString(), out element) )
				{	
					Global.LogText = "Popup - Invalid email address";
					WriteToLogFile.Run();	
					repo.InvalidFieldDialog.Skip.Click("4;15");
				}	
				// Check for Invalid email address Windows 7
				if(Host.Local.TryFindSingle(repo.InvalidFieldDialog.VerifyTheEmailAddressWithTheCustomInfo.AbsolutePath.ToString(), out element) )
				{	
					Global.LogText = "Popup - Invalid email address";
					WriteToLogFile.Run();	
					Keyboard.Press("{Escape}");
				}					

				// Check for Because the customer is a PowerUp member...
				if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.BecauseTheCustomerIsAPowerUpMemberInfo.AbsolutePath.ToString(), out element) )
				{	
					Global.LogText = "Popup - Because the customer is a PowerUp member..";
					WriteToLogFile.Run();	
					Keyboard.Press("{F5}");
					Thread.Sleep(100);
				}	

				// Check for Customer communication email address...
				if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.CustomersCommunicationEmailAddressHaInfo.AbsolutePath.ToString(), out element) )
				{	
					Global.LogText = "Popup - Customer communication email address..";
					WriteToLogFile.Run();	
					Keyboard.Press("{F5}");
					Thread.Sleep(100);
				}
				// Check for Customer communication email address...
				if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.CustomersCommunicationEmailAddressInfo.AbsolutePath.ToString(), out element) )
				{	
					Global.LogText = "Popup - Customer communication email address..";
					WriteToLogFile.Run();	
					Keyboard.Press("{F5}");
					Thread.Sleep(100);
				}

				// Check for Unable to Validate Customer
				if(Host.Local.TryFindSingle(repo.PopUpCustomerLookupUnavailable.WeAreUnableToValidateTheCustomersInfo.AbsolutePath.ToString(), out element) )
				{	
					Global.LogText = "Popup - Unable to Validate Customer..";
					WriteToLogFile.Run();	
					Keyboard.Press("{F5}");
					Thread.Sleep(100);
				}				
			}


			TimeMinusOverhead.Run((float) Mystopwatch.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine          
	        Global.CurrentMetricDesciption = "Enter Customer Information";
	        Global.Module = "fnEnterCustomerInformation";            
	        DumpStatsQ4.Run();  

			Global.LogFileIndentLevel--;
	        Global.LogText = "OUT fnEnterCustomerInformation";
			WriteToLogFile.Run();
        }        
    }
}
