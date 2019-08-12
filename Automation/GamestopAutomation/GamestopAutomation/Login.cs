/*
 * Created by Ranorex
 * User: storeuser
 * Date: 7/15/2013
 * Time: 11:04 AM
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
    ///  The Logon module is should be used anytime the user credentials is required by the POS.
    /// </summary>
    [TestModule("A82337F6-97C6-4D55-9D48-8B3E9408C127", ModuleType.UserCode, 1)]
    public class Login : ITestModule
    {
        GamestopAutomationRepository repo = GamestopAutomationRepository.Instance;
        
        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// 
        bool found = false;
        
        string xPathTxtUserID = "/form[@processname='Source']/*/text[@automationid='txtUserId']";
        Ranorex.Text txtUserId= null;
        
        string xPathTxtPassword = "/form[@processname='Source']/*/text[@automationid='txtPassword']";
        Ranorex.Text txtPassword = null;
                     
        string xPathPressF5ToStartANewTransaction = "/form[@processname='Source']/element[@classname='DashboardView']/button[@name~'Press F5 to start a new transaction' and @enabled='True']";
        Ranorex.Button btnNewTransaction = null;
        
        public Login()
        { 

        }

        void ITestModule.Run()
        {
        	Mouse.DefaultMoveTime = 100;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
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
        	
            string strUsername = "";
        	string strPassword = "";
			       	
        	try
        	{
        		strUsername = Global.xelModule.Attribute("username").Value;
        		strPassword = Global.xelModule.Attribute("password").Value;
        	}
        	catch
        	{}
        		
        	//string strRegisterIni = "c:\\pos\\register.ini";
        	IniFile iniRegister = new IniFile(Global.strRegisterIni);
        	string strStatus = iniRegister.IniReadValue("Terminal", "Status");
        	strStatus = strStatus.ToLower();

        	Global.stwStepStopWatch = new Stopwatch();
        	Global.stwIterationStopWatch = new Stopwatch();

            //Start the time
            Global.stwStepStopWatch.Start();
            Global.stwIterationStopWatch.Start();

                     
            
        	if (strStatus == "open")
        	{
        		

        		if (!Host.Local.TryFindSingle<Ranorex.Text>(xPathTxtUserID, 100, out txtUserId))
        		{
        			Host.Local.TryFindSingle<Ranorex.Button>(xPathPressF5ToStartANewTransaction, 2000, out btnNewTransaction); 
        			Report.Log(ReportLevel.Info, "Mouse", "Click on 'PressF5ToStartANewTransaction'.");
        			btnNewTransaction.Click();
        		}
        	}
            

            if (Host.Local.TryFindSingle<Ranorex.Text>(xPathTxtUserID, 2000, out txtUserId))
            {
            	Report.Log(ReportLevel.Info, "Keyboard", "Key sequence 'psu' with focus on 'TxtUserId'.");
            	txtUserId.PressKeys(strUsername);
            }
            

            if (Host.Local.TryFindSingle<Ranorex.Text>(xPathTxtPassword, 2000, out txtPassword))
            {
            	Report.Log(ReportLevel.Info, "Keyboard", "Key sequence 'advanced' with focus on 'TxtPassword'.");
            	txtPassword.PressKeys(strPassword);
            	
            	Report.Log(ReportLevel.Info, "Keyboard", "Key sequence '{Return}' with focus on 'TxtPassword'.");
            	txtPassword.PressKeys("{Return}");
            }
            
            Global.CriteriaType = "Exit";
            Global.Proceed = false;

            TestModuleRunner.Run(V);
            
            Global.stwStepStopWatch.Stop();
            Global.logger.Add(1, Global.intIteration, Global.strID,  this.GetType().Name, Global.stwStepStopWatch.Elapsed);
            if (Global.Proceed)
            
               	
          
            TestReport.EndTestModule();
            if (!Global.Proceed)
            {
            	TestReport.EndTestCase(TestResult.Failed);
            }
            
        } 
        
        public void Run()
        {

        		
                	
        	
        }
    }
}
