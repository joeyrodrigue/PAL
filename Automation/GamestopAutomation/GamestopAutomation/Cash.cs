/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/19/2013
 * Time: 11:46 AM
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

using Ranorex.Core.Repository;
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
    /// Description of Cash.
    /// </summary>
    [TestModule("11C824F2-4EB2-4585-BB6E-4BD1A0DBBBF1", ModuleType.UserCode, 1)]
    public class Cash : ITestModule
    {

    	string xPathAmountPaid = "/form[@processname='Source']/text[@automationid='txtAmountPaid']";
        Ranorex.Text txtAmountPaid= null;
        
        string xPathBalanceDue = "/form[@processname='Source']/?/?/text[@automationid='BalanceDue']";
        Ranorex.Text txtBalanceDue = null;
                     
        string xPathCash = "/form[@processname='Source']/?/?/element[@automationid='PaymentControl']/?/?/text[@automationid='ButtonContent' and @text='Cash' and visible='True']";
        Ranorex.Text txtCash = null;
        
        string xPathChangeDue = "/form[@processname='Source']/?/?/text[@name='Change Due']";
        Ranorex.Text txtChangeDue = null;

        
        public Cash()
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
            
            string TenderAmount = "";
            string BalanceDue = "";
            string Tender = Global.xelModule.Attribute("Tender").Value;    
            
            if (Host.Local.TryFindSingle<Ranorex.Text>(xPathBalanceDue, 2000, out txtBalanceDue))
            {
            	BalanceDue = txtBalanceDue.TextValue.Replace("$","");
            }

            if (Host.Local.TryFindSingle<Ranorex.Text>(xPathCash, 2000, out txtCash))
            {
            	Report.Log(ReportLevel.Info, "Mouse", "Clicking Cash");
            	txtCash.Click();
            }
            
            if (Host.Local.TryFindSingle<Ranorex.Text>(xPathAmountPaid, 2000, out txtAmountPaid))
            {
            	   
	            switch (Tender)
	            {
	            	case "Exact":
	            		TenderAmount = BalanceDue;
	            		break;
	            		
	            	case "Split w/ Credit Prompt":
	            		
	            		break;
	            		
	            	case "Split":
	            		
	            		break;
	            		
	            	case "Over":
	            		
	            		break;
	            		
	            	default:
	            		break;
	            } 
	            
		        Report.Log(ReportLevel.Info, "Keyboard", "Typing " + TenderAmount +" and a Return in AmountPaid");
	            txtAmountPaid.PressKeys(TenderAmount + "{Return}");
            }

                        
            Validate.Exists(xPathChangeDue,5000,"Change Due Prompt Displayed",false);
            
            GamestopAutomationRepository repo = GamestopAutomationRepository.Instance;
            
            RepoItemInfo ChangeDueInfo = new RepoItemInfo(repo,"ChangeDue", xPathChangeDue,30000,null);
            ChangeDueInfo.WaitForNotExists(30000);
            	
            //Validate.NotExists(xPathChangeDue,10000,"Change Due Prompt Closed",false);
            
 
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
