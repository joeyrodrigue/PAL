/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:18 AM
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
using System.Diagnostics;
using System.IO;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ini;

namespace Alpha
{
    /// <summary>
    /// Description of fnGetRegisterInfo.
    /// </summary>
    [TestModule("B81ADE97-C50E-4A41-8E80-557F8BF406E8", ModuleType.UserCode, 1)]
    public class fnGetRegisterInfo : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnGetRegisterInfo()
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
            
            if(File.Exists("c:\\Program Files\\GameStop\\Source\\Source.exe"))
            	Global.RetechVersion = FileVersionInfo.GetVersionInfo(@"c:\Program Files\GameStop\Source\Source.exe").FileVersion;
            else
            	Global.RetechVersion = "";
            
			Global.IPOSVersion = FileVersionInfo.GetVersionInfo(@"c:\POS\pos.exe").ProductName;

			// Read Values from Register.ini
			IniFile RegisterIniFile = new IniFile(Global.RegisterIni);
            Global.IsMaster = Convert.ToBoolean(RegisterIniFile.IniReadValue("Terminal","IsMaster"));
            Global.RegisterNumber = RegisterIniFile.IniReadValue("Terminal", "Num");

            // Use Register.ini to find location of Store.ini
            string StoreIni = RegisterIniFile.IniReadValue("INI", "Store").ToString().Replace(@"\",@"\\");
            IniFile StoreIniFile = new IniFile(StoreIni);
            
            // Read Values of Store.ini
            Global.NumberOfRegisters = Convert.ToInt32(StoreIniFile.IniReadValue("System","NumRegisters"));
            Global.RegisterName = StoreIniFile.IniReadValue("Registers","RegisterName" + Convert.ToString(Global.RegisterNumber));
 
            //If register is master use "c" drive else use the mapped "d" drive
            if (Global.IsMaster)
            {
            	Global.Register1DriveLetter = "C";			//	Store stats on C drive for Register 1
			}
            else
            {
				Global.Register1DriveLetter = "D";			//	Store stats on D drive (shared) Register 1
			}   
			
        }
      
    }
}
