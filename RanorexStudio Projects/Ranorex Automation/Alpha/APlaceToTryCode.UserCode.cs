﻿///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// Your custom recording code should go in this file.
// The designer will only add methods to this file, so your custom code won't be overwritten.
// http://www.ranorex.com
// 
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WinForms = System.Windows.Forms;
using Microsoft.Win32;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace Alpha
{
    public partial class APlaceToTryCode
    {
        /// <summary>
        /// This method gets called right after the recording has been started.
        /// It can be used to execute recording specific initialization code.
        /// </summary>
        /// 
        	
        private void Init()
        {
            //Ranorex.Report.Info ("Information Message");
			//Ranorex.Report.Warn ("Warning Message");
			//Ranorex.Report.Error ("Error Message");
			//Ranorex.Report.Success("Success Message");
			//Ranorex.Report.Failure("Failure Message");
			
			// How to make code module so can drag and drop it - this will be done later
			// var mLogin = new fnLogin();
			// TestModuleRunner.Run(mLogin);
			
			// How to do text to speech
//			 SpeechSynthesizer Speech = new SpeechSynthesizer();
//			 Speech.Speak("This is a test of the speech synthesis");				

            //////////////////////////////////////////////////////////////////////////////////
        	
            RanorexRepository repo = new RanorexRepository();
            // PUT ANY CODE YOU WANT TO TEST HERE IT EXECUTES BEFORE ANY OTHER CODE
			
			Ranorex.Unknown element = null; 




			

			
			
			


//int i;
//i = 0;
			
	
 // Below are sample uses of the MessageBox * 
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms; 
/*
	    //
	    // The simplest overload of MessageBox.Show. [1]
	    //
	    MessageBox.Show("Dot Net Perls is awesome.");
	    //
	    // Dialog box with text and a title. [2]
	    //
	    MessageBox.Show("Dot Net Perls is awesome.",
		"Important Message");
	    //
	    // Dialog box with two buttons: yes and no. [3]
	    //
	    DialogResult result1 = MessageBox.Show("Is Dot Net Perls awesome?",
		"Important Question",
		MessageBoxButtons.YesNo);
	    //
	    // Dialog box with question icon. [4]
	    //
	    DialogResult result2 = MessageBox.Show("Is Dot Net Perls awesome?",
		"Important Query",
		MessageBoxButtons.YesNoCancel,
		MessageBoxIcon.Question);
	    //
	    // Dialog box with question icon and default button. [5]
	    //
	    DialogResult result3 = MessageBox.Show("Is Visual Basic awesome?",
		"The Question",
		MessageBoxButtons.YesNoCancel,
		MessageBoxIcon.Question,
		MessageBoxDefaultButton.Button2);
	    //
	    // Test the results of the previous three dialogs. [6]
	    //
	    if (result1 == DialogResult.Yes &&
		result2 == DialogResult.Yes &&
		result3 == DialogResult.No)
	    {
		MessageBox.Show("You answered yes, yes and no.");
	    }
	    //
	    // Dialog box that is right-aligned (not useful). [7]
	    //
	    MessageBox.Show("Dot Net Perls is the best.",
		"Critical Warning",
		MessageBoxButtons.OKCancel,
		MessageBoxIcon.Warning,
		MessageBoxDefaultButton.Button1,
		MessageBoxOptions.RightAlign,
		true);
	    //
	    // Dialog box with exclamation icon. [8]
	    //
	    MessageBox.Show("Dot Net Perls is super.",
		"Important Note",
		MessageBoxButtons.OK,
		MessageBoxIcon.Exclamation,
		MessageBoxDefaultButton.Button1);
*/



 			

            //////////////////////////////////////////////////////////////////////////////////            
        }
        
        
        
        
  

    }
    

    
    
    public class HWndCounter
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("user32.dll")]
        private static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

        public static int GetWindowHandlesForCurrentProcess(IntPtr hWnd)
        {
            IntPtr processHandle = GetCurrentProcess();
            uint gdiObjects = GetGuiResources(processHandle, 0); // GDI
            uint userObjects = GetGuiResources(processHandle, 1); // User

            return Convert.ToInt32(gdiObjects + userObjects);
        }
    }
    
}
