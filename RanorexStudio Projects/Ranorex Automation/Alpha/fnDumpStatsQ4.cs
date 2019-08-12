/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 08/22/13
 * Time: 9:46 AM
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
    /// Description of fnDumpStatsQ4.
    /// </summary>
    [TestModule("D17E970F-FFD8-4B98-B5AC-E1BED37E5E0B", ModuleType.UserCode, 1)]
    public class fnDumpStatsQ4 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDumpStatsQ4()
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
	
	       	if(Global.IsPerformanceTest || Global.SwitchMetricOverRide)
        	{	// Only write out metrics if doing performance testing
				Global.Q4StatBuffer = Global.Q4StatBuffer + Global.Test_ID + "," +
												              Global.Register_ID + "," +
												              Global.AutoVer_ID  + "," +
													          Global.CurrentScenario + "," + 	
													          Global.Module + "," + 						               	
													          Global.CurrentMetricDesciption + "," +
													          Global.CurrentIteration + "," + 						                
															  Global.Q4StatLine + "," +
															  System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
															  "\r\n";
        	}

        }           
    }
}
