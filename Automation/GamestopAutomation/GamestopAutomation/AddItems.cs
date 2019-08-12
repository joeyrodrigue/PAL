/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/17/2013
 * Time: 5:38 PM
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
    /// Description of Purchases.
    /// </summary>
    [TestModule("1C21268F-42F2-4C6F-A15B-148B2BC1CFD1", ModuleType.UserCode, 1)]
    public class AddItems : ITestModule
    {
        bool found = false;
                
        string xPathPurchase = "/form[@processname='Source']/element[@classname='OrderWorkspaceView']/element[@classname='PurchaseView']/button[@automationid='PurchaseButton' and @enabled='true']";
        Ranorex.Button btnPurchase = null;
        
        string xpathAddItem = "/form[@processname='Source']/text[@automationid='TxtSearch']";
        Ranorex.Text txtAddItem = null;
        
        public AddItems()
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
            
            string SKU = "";
            string DEPT = "";
            string ESRB = "";
            int QTY = 0;
            bool GPG;
            
            
            TestReport.BeginTestModule(this.GetType().Name);
            Global.xdocModule = XDocument.Load(@"C:\PAL\Automation\Modules\" + this.GetType().Name + ".config");
            Global.CriteriaType = "Entrance";
             
            Global.Proceed = false;
            Verify V = new Verify();
            TestModuleRunner.Run(V);
            
                      
            if (!Global.Proceed)
            {
            	TestReport.EndTestModule();
            }
            
           try
           {
           		SKU = Global.xelModule.Attribute("SKU").Value;
           }
           catch{}
                       
           try
           {
           		DEPT = Global.xelModule.Attribute("DEPT").Value;
           }
           catch{}            
           try
           {
           		ESRB = Global.xelModule.Attribute("ESRB").Value;
           }
           catch{}            
           try
           {
           		QTY = Convert.ToInt32(Global.xelModule.Attribute("QTY").Value);
           }
           catch{}            
           try
           {
           		GPG = Convert.ToBoolean(Global.xelModule.Attribute("QTY").Value);
           }
           catch{}
        		

           for (int i = 0; i < QTY ; i++ )
           {

	            if (Host.Local.TryFindSingle<Ranorex.Button>(xPathPurchase, 2000, out btnPurchase))
	            {
	            	Report.Log(ReportLevel.Info, "Mouse", "Click on 'Purchases'.");
	            	btnPurchase.Click();
	            	
	            	if (Host.Local.TryFindSingle<Ranorex.Text>(xpathAddItem, 2000, out txtAddItem))
	            	{
	            		Report.Log(ReportLevel.Info, "Keyboard", "Enter SKU followed by the Enter key.");
	            		txtAddItem.PressKeys(SKU + "{Return}");
	            		Delay.Milliseconds(1000);
	            	}
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
