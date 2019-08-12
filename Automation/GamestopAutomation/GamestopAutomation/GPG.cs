/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/18/2013
 * Time: 2:53 PM
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
    /// Description of GPG.
    /// </summary>
    [TestModule("853A53BB-14E8-4481-9915-6B445ABA3C68", ModuleType.UserCode, 1)]
    public class GPG : ITestModule
    {
        string xPathAddGPGAll = "/form[@automationid='Root']//list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']/?/?/button[@automationid='CheckAllButton']";
    	Ranorex.Button btnAddGPGAll = null;
        
    	string xPathGPGContinue = "/form[@automationid='Root']/?/?/element[@automationid='Root']/list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']//text[@name='Continue']/../";
    	Ranorex.Button btnGPGContinue = null;
    	
        public GPG()
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
            
            string GPGSelect = Global.xelModule.Attribute("Select").Value;
            
            if (GPGSelect.ToLower() == "all")
            {
    			if (Host.Local.TryFindSingle<Ranorex.Button>(xPathAddGPGAll, 2000, out btnAddGPGAll))
        		{
    				Report.Log(ReportLevel.Info, "Mouse", "Clicking Add GPG to all items button");
    				btnAddGPGAll.Click();
    			}
            }
            
            if (Host.Local.TryFindSingle<Ranorex.Button>(xPathGPGContinue, 2000, out btnGPGContinue))
            {
            	Report.Log(ReportLevel.Info, "Mouse", "Clicking C ontinueAdd GPG to all items button");
            	btnGPGContinue.Click();
            }
            
                      
            if (!Global.Proceed)
            {
            	TestReport.EndTestModule();
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
