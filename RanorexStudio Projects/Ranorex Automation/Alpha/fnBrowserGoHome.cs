/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 7:41 AM
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

using System.Diagnostics;

namespace Alpha
{
    /// <summary>
    /// Description of fnBrowserGoHome.
    /// </summary>
    [TestModule("C781F840-5DCB-4990-83EC-CCDC82D4C2BF", ModuleType.UserCode, 1)]
    public class fnBrowserGoHome : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnBrowserGoHome()
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
            
            RanorexRepository repo = RanorexRepository.Instance;
            
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile(); 
        	Stopwatch MystopwatchCK = new Stopwatch();	
			
        	Global.LogText = "IN fnBrowserGoHome";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel++;			
            
			repo.POSBrowserV25StorePortal.Self.Focus();
//			repo.POSBrowserV25StorePortal.POSBrowserV27RIStorePortal.Click();  // click on browser to make sure in focus
			Keyboard.Press("{F6}");

            repo.POSBrowserV25StorePortal.ExitEscInfo.SearchTimeout = 60000;
			MystopwatchCK.Reset();	
			MystopwatchCK.Start();	            
            while(!repo.StorePortal.QAPOSReCommerce.Enabled)
            {	Thread.Sleep(100);
            	
				if(MystopwatchCK.ElapsedMilliseconds > 6000)
				{
					repo.POSBrowserV25StorePortal.POSBrowserV27RIStorePortal.Click();  // click on browser to make sure in focus
					Keyboard.Press("{F6}");	
					Thread.Sleep(100);	
					MystopwatchCK.Reset();	
					MystopwatchCK.Start();	
				}                	
            }  
            repo.POSBrowserV25StorePortal.ExitEscInfo.SearchTimeout = 30000;
            
			Global.LogFileIndentLevel--;			
            Global.LogText = "OUT fnBrowserGoHome";
			WriteToLogFile.Run();	
        }
        
    }
 	
}
