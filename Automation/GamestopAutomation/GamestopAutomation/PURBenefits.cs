/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/19/2013
 * Time: 10:21 AM
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
    /// Description of PURBenefits.
    /// </summary>
    [TestModule("3A16EE9D-21BB-4FEB-B206-286B7B6937E0", ModuleType.UserCode, 1)]
    public class PURBenefits : ITestModule
    {
        string xPathBasic = "/form[@automationid='Root']/?/?/element[@automationid='Root']/list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']//text[@automationid='ButtonContent' and @text='Enroll in Basic']";
        string xPathPro = "/form[@automationid='Root']/?/?/element[@automationid='Root']/list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']//text[@automationid='ButtonContent' and @text='Enroll in Pro']";
        string xPathNotIntrested = "/form[@automationid='Root']/?/?/element[@automationid='Root']/list[@automationid='StackItems']/listitem[@classname='ListBoxItem' and @text~'^GameStop\\.POS\\.UI\\.ViewModel' and @orientation='None' and @iscontentelement='True' and @iscontrolelement='True' and @iskeyboardfocusable='True']//text[@automationid='ButtonContent' and @text='Not interested']";
        string xPathRenew = "";
        
        Ranorex.Text txtPURSelection = null;
        
        public PURBenefits()
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
            
            string Select = Global.xelModule.Attribute("Select").Value;
            
            switch (Select)
            {
            	case "Basic":
            		break;
            		
            	case "Pro":
            		break;
            		
            	case "Not Intrested":
            		if (Host.Local.TryFindSingle<Ranorex.Text>(xPathNotIntrested, 2000, out txtPURSelection))
            		{
            			Report.Log(ReportLevel.Info, "Mouse", "Clicking Not intrested");
            			txtPURSelection.Click();
            			
            		}
            		break;
            		
            	case "Renew":
            		break;
            		
            	default:
            		break;
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
