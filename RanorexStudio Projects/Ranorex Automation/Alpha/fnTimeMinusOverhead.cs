/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 09/13/13
 * Time: 7:10 AM
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
    /// Description of fnTimeMinusOverhead.
    /// </summary>
    [TestModule("71CA10AA-47ED-4E3D-A771-CC01D16BBEE5", ModuleType.UserCode, 1)]
    public class fnTimeMinusOverhead : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnTimeMinusOverhead()
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
        
        public void Run(float MetricTime)
        {
            // take the given stopwatch time and subtract out the overhead time and store in Global.Q4StatLine
        	
        	Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;        	
 
			float MetricTimeDiv1000 = MetricTime / 1000;
			float OverHeadTime= (float) GlobalOverhead.Stopwatch.ElapsedMilliseconds / 1000;
			Global.AdjustedTime = MetricTimeDiv1000 - OverHeadTime;
			GlobalOverhead.Stopwatch.Reset();	// reset it after it is used        	
			Global.Q4StatLine =  (Global.AdjustedTime).ToString("R");
        }              	
    }
}
