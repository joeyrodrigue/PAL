/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 03/26/14
 * Time: 9:30 AM
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

using System.Diagnostics;
using System.IO;

using System.ComponentModel;
using System.Data;
using System.Windows.Forms; 

using System.Collections;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace Alpha
{
    /// <summary>
    /// Description of fnCalculateEnrollmentCheckDigit.
    /// </summary>
    [TestModule("920BD14A-E142-46CE-9B1A-EF8F41623C83", ModuleType.UserCode, 1)]
    public class fnCalculateEnrollmentCheckDigit : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnCalculateEnrollmentCheckDigit()
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
            
			// Given Card Number
			//	    Global.EnrollmentCardNumber = "387595191230";			
			// Return Card Number plus check digit (EnrollmentFullCardNumber)
			//      Global.EnrollmentFullCardNumber return plus check digit "3875951912304"
			
//			fnCalculateEnrollmentCheckDigit CalculateEnrollmentCheckDigit = new fnCalculateEnrollmentCheckDigit();
//			Global.EnrollmentCardNumber = "387595191230";
//			CalculateEnrollmentCheckDigit.Run();
//			string a = Global.EnrollmentFullCardNumber;

			int[] EnrollmentPositionInCardNum = new int[12];
			int[] EnrollmentCorrespondingDigit = new int[12];
			int[] EnrollmentWeightMultiplier = new int[12];
			int[] EnrollmentResult = new int[12];
			int[] EnrollmentAdditiveResult = new int[12];
			int EnrollmentSum = 0;
			int EnrollmentCheckDigitSum = 0;
			
			// Calculate check digit and full card number
			for (int COff = 0; COff <= Global.EnrollmentCardNumber.Length -1 ; COff++ ) 
			{	
				// Fill in EnrollmentPositionInCardNum and EnrollmentCorrespondingDigit arrays
				EnrollmentPositionInCardNum[COff] = COff + 1;
				EnrollmentCorrespondingDigit[COff] = Convert.ToInt32(Global.EnrollmentCardNumber.Substring(COff,1));				
				
				// Fill in EnrollmentPositionInCardNum array
				int d = 0;
				if(Global.EnrollmentCardNumber.Length >= 1)
				{
					if( (Global.EnrollmentCardNumber.Length + 1) % 2 == 0)
					{
						if( (EnrollmentPositionInCardNum[COff] % 2) == 0) d = 1; else d = 2;	
					}
					else
					{
						if( (EnrollmentPositionInCardNum[COff] % 2) == 0) d = 2; else d = 1;
					}						
				}
				EnrollmentWeightMultiplier[COff] = d;
				
				// Fill in EnrollmentResult array
				if(Global.EnrollmentCardNumber.Length >= COff + 1)
				{
					EnrollmentResult[COff] = EnrollmentCorrespondingDigit[COff] * EnrollmentWeightMultiplier[COff];
				}	
				
				// Fill in EnrollmentAdditiveResult array and computer EnrollmentSum
				if(Global.EnrollmentCardNumber.Length >= 1)
				{
					if( EnrollmentResult[COff] < 10)
					{
						d = EnrollmentResult[COff];	
					}
					else
					{
						d = (int) (EnrollmentResult[COff] / 10) + (EnrollmentResult[COff] % 10);
					}						
				}
				EnrollmentAdditiveResult[COff] = d;
				EnrollmentSum = EnrollmentSum + d;	

			}	
			
			// Compute Check Digit
			if( (EnrollmentSum % 10) > 0)
				 EnrollmentCheckDigitSum = 10 - (EnrollmentSum % 10);
			else EnrollmentCheckDigitSum = 0;
	
			Global.EnrollmentFullCardNumber = Global.EnrollmentCardNumber + EnrollmentCheckDigitSum.ToString();
        }
    }
}
