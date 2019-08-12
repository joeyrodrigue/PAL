/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/19/2013
 * Time: 8:39 AM
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
    /// Description of IncludeCustomer.
    /// </summary>
    [TestModule("F3F978CA-4A9E-4CB6-A008-078C028A47DB", ModuleType.UserCode, 1)]
    public class IncludeCustomer : ITestModule
    {
        string xPathFindCustomer = "/form[@automationid='Root']/?/?/element[@automationid='Root']/list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']//text[@name='Find existing customer']";
        Ranorex.Text txtFindCustomer= null;
        
        string xPathDoNotInclude = "/form[@processname='Source']//list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']/element/button[@automationid='DoNotIncludeCustomer']";
        Ranorex.Button btnDoNotInclude = null;
                     
 	    	
        public IncludeCustomer()
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
            
            bool AddCustomer = Convert.ToBoolean(Global.xelModule.Attribute("AddCustomer").Value);
            
            if (!AddCustomer)
            {
            	if(Host.Local.TryFindSingle<Ranorex.Button>(xPathDoNotInclude, 2000, out btnDoNotInclude))
            	{
            		Report.Log(ReportLevel.Info, "Mouse", "Clicking Do Not Include");
            		btnDoNotInclude.Click();
            	}
            }
            else
            {
            	if(Host.Local.TryFindSingle<Ranorex.Text>(xPathFindCustomer, 2000, out txtFindCustomer))
            	{
            		txtFindCustomer.Click();
            		FindCustomer mFindCustomer = new FindCustomer();
            		TestModuleRunner.Run(mFindCustomer);
            	}
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
