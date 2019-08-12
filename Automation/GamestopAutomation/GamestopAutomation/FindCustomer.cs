/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/19/2013
 * Time: 8:40 AM
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

namespace GamestopAutomation
{
    /// <summary>
    /// Description of FindCustomer.
    /// </summary>
    [TestModule("86E20FE1-5F55-4297-A6E6-66135AFA99C6", ModuleType.UserCode, 1)]
    public class FindCustomer : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FindCustomer()
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
    }
}
