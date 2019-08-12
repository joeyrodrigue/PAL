/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:16 AM
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
    /// Description of fnGetRegister1DriveLetter.
    /// </summary>
    [TestModule("B4633708-A9CC-4503-9C89-1017F5B2561F", ModuleType.UserCode, 1)]
    public class fnGetRegister1DriveLetter : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetRegister1DriveLetter()
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
            
			RanorexRepository repo = new RanorexRepository();

			if (Global.RegisterNumber == "1") {
				Global.Register1DriveLetter = "C";			//	Store stats on C drive for Register 1
			} else {
				Global.Register1DriveLetter = "D";			//	Store stats on D drive (shared) Register 1
			}            
        }
    
    }
}
