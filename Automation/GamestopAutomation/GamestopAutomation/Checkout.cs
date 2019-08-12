/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/19/2013
 * Time: 11:08 AM
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

using Ranorex.Core.Reporting;
using Ini;
using System.Windows.Forms;
using System.Diagnostics;
using GSLogger;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

namespace GamestopAutomation
{
    /// <summary>
    /// Description of Checkout.
    /// </summary>
    [TestModule("AB86286A-BEAC-48F0-BE04-76405B81D336", ModuleType.UserCode, 1)]
    public class Checkout : ITestModule
    {

    	string xPathCheckout = "/form[@processname='Source']/?/?/button[@automationid='CheckOutButton' and @enabled='True']";
        
        Ranorex.Button btnCheckout = null;
    	
        public Checkout()
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
            
            TestReport.BeginTestModule(this.GetType().Name);
            Global.xdocModule = XDocument.Load(@"C:\PAL\Automation\Modules\" + this.GetType().Name + ".config");
            Global.CriteriaType = "Entrance";
             
            Global.Proceed = false;
            Verify V = new Verify();
            TestModuleRunner.Run(V);            
            
            
            if (Host.Local.TryFindSingle<Ranorex.Button>(xPathCheckout, 2000, out btnCheckout))
        	{
				Report.Log(ReportLevel.Info, "Mouse", "Clicking Checkout");
				btnCheckout.Click();
    		}
            
            
            
            
            
            
            
            
            Global.CriteriaType = "Exit";
             
            Global.Proceed = false;
            TestModuleRunner.Run(V);
            
            TestReport.EndTestModule();
            if (!Global.Proceed)
            {
            	TestReport.EndTestCase(TestResult.Failed);
            }
        }
    }
}
