/*
 * Created by Ranorex
 * User: storeuser
 * Date: 04/21/14
 * Time: 1:20 PM
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

using System.Net;  
using Microsoft.Win32;  
using System.Linq;
using System.Runtime.InteropServices;

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Alpha
{

    /// <summary>
    /// Description of fnGetPOSProcessInfo.
    /// </summary>
    [TestModule("372C8B9B-C6F0-4346-9525-FE7B3AA7190E", ModuleType.UserCode, 1)]
    public class fnGetPOSProcessInfo : ITestModule
    {

    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetPOSProcessInfo()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        /// 

        void ITestModule.Run()
        {
        	Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
        }
        
		[DllImport("user32.dll")]	
		private static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);       
		
		public void Run()
        {	
//			// To get POS.exe stats
//			fnGetPOSProcessInfo GetPOSProcessInfo = new fnGetPOSProcessInfo();
//			GetPOSProcessInfo.Run();			

//	    	// These global variables are returned
//			public static int 		POSHandleCount = 0;
//			public static int 		POSThreads = 0;
//			public static int 		POSUsedMemory = 0;
//			public static int 		POSGDIObjects = 0;
//			public static int 		POSUserObjects = 0;
//			public static int 		POSCurrentCPUUsage = 0;       	

//			if (Global.RegisterRunningRetech) ProcessName = "source";
//			if (Global.RegisterRunningIPOS) ProcessName = "pos";
//			if (Global.RegisterRunningIPOS & Global.CurrentScenario <= 30)
//				ProcessName = "pos";
//			
//			if (Global.RegisterRunningRetech & Global.CurrentScenario > 30)
//				ProcessName = "source";			

			System.Diagnostics.Process [] localByName = System.Diagnostics.Process.GetProcessesByName(Global.ProcessName);			
			IntPtr POSHandlePtr= localByName[0].Handle;	
			Global.POSHandleCount = localByName[0].HandleCount;
			Global.POSThreads = localByName[0].Threads.Count;
			Global.POSUsedMemory = (int) localByName[0].WorkingSet64 / (1024*1024);

			Global.POSGDIObjects = (int) GetGuiResources(localByName[0].Handle, 0); // GDI
			Global.POSUserObjects = (int) GetGuiResources(localByName[0].Handle, 1); // User

			// Get Current Cpu Usage
			System.Diagnostics.PerformanceCounter CPUUsage;
			CPUUsage = new System.Diagnostics.PerformanceCounter();
			CPUUsage.CategoryName = "Processor";
			CPUUsage.CounterName = "% Processor Time";
			CPUUsage.InstanceName = "_Total";
			CPUUsage.NextValue();
			System.Threading.Thread.Sleep(1000);
			Global.POSCurrentCPUUsage = (int)CPUUsage.NextValue();     	
        }        
    }
}
