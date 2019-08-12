/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 07/18/13
 * Time: 8:21 AM
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

namespace Alpha
{
    /// <summary>
    /// Description of fnWaitForItemSearchToFinish.
    /// </summary>
    [TestModule("0D89488A-1C06-4AE3-8959-C1ECA54AD2A5", ModuleType.UserCode, 1)]
    public class fnWaitForItemSearchToFinish : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnWaitForItemSearchToFinish()
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
        {	// Wait for xx of yy to be all numeric, whick indicates that search is complete
			// Wait for xx of yy to only show yy then return yy in Global.RecordsFoundString and Global.RecordsFound
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
            
            Global.LogText = "IN fnWaitForItemSearchToFinish";
			WriteToLogFile.Run();	            
        	Global.LogFileIndentLevel++;			
            
            RanorexRepository repo = new RanorexRepository();
            
			// Wait for xx of yy to be all numeric, whick indicates that search is complete
			while(!Regex.IsMatch(repo.ItemSearch.RawTextXXofYY.RawTextValue,@"^\d+$"))
			{	Thread.Sleep(100);
			}				

			Global.RecordsFoundString = repo.ItemSearch.RawTextXXofYY.RawTextValue; 				
			Global.RecordsFound = Convert.ToInt32(repo.ItemSearch.RawTextXXofYY.RawTextValue);
			
			Global.LogFileIndentLevel--;
			Global.LogText = "OUT fnWaitForItemSearchToFinish";
			WriteToLogFile.Run();	
        }         
    }
}
