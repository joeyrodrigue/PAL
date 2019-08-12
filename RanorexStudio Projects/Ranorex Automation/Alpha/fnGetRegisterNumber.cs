/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:19 AM
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
    /// Description of fnGetRegisterNumber.
    /// </summary>
    [TestModule("BFDB418A-D8F2-4780-BB26-052E4E0954C2", ModuleType.UserCode, 1)]
    public class fnGetRegisterNumber : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetRegisterNumber()
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

            // Get the register number from the register.ini file then init statfilename
			using (System.IO.StreamReader RegisterIniFile = new System.IO.StreamReader(@"C:\POS\register.ini"))
			{
				bool LineFound = false;
				string InputLine = "";
				do 
				{	
					InputLine = RegisterIniFile.ReadLine();
					if (InputLine != "")
					{
						if (InputLine.Substring(0,4) == "Num=")
						{
							LineFound = true;
							Global.RegisterNumber = InputLine.Substring(4,1);
						}						
					};
				} while (!LineFound);
			}            
        }

    }
}
