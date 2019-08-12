/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:23 AM
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
    /// Description of fnGetStartTime.
    /// </summary>
    [TestModule("F7BCA839-D7CC-4397-AA70-3CCF6209C4DE", ModuleType.UserCode, 1)]
    public class fnGetStartTime : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetStartTime()
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

            //System.DateTime DateTimeNow = System.DateTime.Now;
			//System.TimeSpan TimeNow = DateTimeNow.TimeOfDay;
			Global.ScenarioStartTime = System.DateTime.Now.ToString();
        }        
    }
}
