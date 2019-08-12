/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:14 AM
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
    /// Description of fnGetNextPhoneNumber.
    /// </summary>
    [TestModule("88016846-162F-4DA1-B33F-26B4000EB451", ModuleType.UserCode, 1)]
    public class fnGetNextPhoneNumber : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetNextPhoneNumber()
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
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile(); 
        	
			Global.LogFileIndentLevel++;
        	Global.LogText = "IN fnGetNextPhoneNumber";
			WriteToLogFile.Run();	        	

			string PhoneNumber = "";
			string MatchingCard = "";
			while (PhoneNumber == "")
			{
				switch (Global.UsePhoneNumberType)
				{	// Loyalty ("Pro", "Basic") Non-Loyalty ("NonMemberGenesis", "NonMemberProfile") or "" for all	

//					Global.PhoneArrayLoyaltyPro[PhoneOffset] = Numbers[0];
//					Global.PhoneArrayLoyaltyProCard[PhoneOffset] = Numbers[1];
//					Global.PhoneArrayLoyaltyBasic[PhoneOffset] = Numbers[2];
//					Global.PhoneArrayLoyaltyBasicCard[PhoneOffset] = Numbers[3];
//					Global.PhoneArrayNonLoyaltyNonMemberGenesis[PhoneOffset] = Numbers[4];
//					Global.PhoneArrayNonLoyaltyNonMemberProfile[PhoneOffset] = Numbers[5];	
//    				Global.PhoneNumberMatchingCard; 						
						
						
					case 1:
						//  Loyalty - Pro
						if ( Global.SwitchAllRegistersUseAllPhoneNumbers || ( Global.SwitchPhoneNumbersLoyalty && ( Global.PhoneNumbertype == "" || Global.PhoneNumbertype == "Pro" ) ) )
						{
							PhoneNumber = Global.PhoneArrayLoyaltyPro[Global.PhoneNumberOffset];
							MatchingCard = Global.PhoneArrayLoyaltyProCard[Global.PhoneNumberOffset];
							Global.LoyaltyNonLoyaltyFlag = "Loyalty";
						}
						Global.UsePhoneNumberType = 2;
						break;
						
					case 2:
						//  Loyalty - Basic (or Free)
						if ( Global.SwitchAllRegistersUseAllPhoneNumbers || ( Global.SwitchPhoneNumbersLoyalty && ( Global.PhoneNumbertype == "" || Global.PhoneNumbertype == "Basic" ) ) )						
						{
							PhoneNumber = Global.PhoneArrayLoyaltyBasic[Global.PhoneNumberOffset];
							MatchingCard = Global.PhoneArrayLoyaltyBasicCard[Global.PhoneNumberOffset];
							Global.LoyaltyNonLoyaltyFlag = "Loyalty";
						}
						Global.UsePhoneNumberType = 3;
						break;						
					case 3:
						//  NonLoyalty - Non Member Genesis
						if ( Global.SwitchAllRegistersUseAllPhoneNumbers || ( Global.SwitchPhoneNumbersLoyalty && ( Global.PhoneNumbertype == "" || Global.PhoneNumbertype == "NonMemberGenesis" ) ) )								
						{
							PhoneNumber = Global.PhoneArrayNonLoyaltyNonMemberGenesis[Global.PhoneNumberOffset];
							MatchingCard = "";
							Global.LoyaltyNonLoyaltyFlag = "NonLoyalty";
						}
						Global.UsePhoneNumberType = 4;
						break;		
					case 4:
						//  NonLoyalty - Non Member Genesis
						if ( Global.SwitchAllRegistersUseAllPhoneNumbers || ( Global.SwitchPhoneNumbersLoyalty && ( Global.PhoneNumbertype == "" || Global.PhoneNumbertype == "NonMemberProfile" ) ) )							
						{
							PhoneNumber = Global.PhoneArrayNonLoyaltyNonMemberProfile[Global.PhoneNumberOffset];
							MatchingCard = "";
							Global.LoyaltyNonLoyaltyFlag = "NonLoyalty";
						}
						Global.UsePhoneNumberType = 1;
						
						if(Global.SwitchAllRegistersUseAllPhoneNumbers)
						{
							if(Global.PhoneNumberOffset  >= Global.PhoneMaxOffset) 
							{	Global.PhoneNumberOffset = 0;
							}
							else
							{	Global.PhoneNumberOffset++;
							}
						}
						else if((Global.PhoneNumberOffset + Global.NumberOfRegisters) >= Global.PhoneMaxOffset) 
								{
							Global.PhoneNumberOffset = Convert.ToInt32(Global.RegisterNumber) - 1;
								} else 
								{
									Global.PhoneNumberOffset = Global.PhoneNumberOffset + Global.NumberOfRegisters;
								}						
						break;
				}
			}
			
			if(Global.OverridePhoneNumber != "")
			{
				PhoneNumber = Global.OverridePhoneNumber;
				Global.OverridePhoneNumber = "";				
			}
//PhoneNumber = "3038530673";	// for debugging  ####  D O    N O T   F O R G E T   T O  C O M M E N T   O U T   W H E N   D O N E #####
			Global.NextPhoneNumber = PhoneNumber;
			Global.PhoneNumberMatchingCard = MatchingCard;
			Global.LogText = "Next Phone: " + PhoneNumber + "  Matchig Card: " + Global.PhoneNumberMatchingCard;
			WriteToLogFile.Run();	
			Global.LogText = "OUT fnGetNextPhoneNumber";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;
        }        
    }
}
