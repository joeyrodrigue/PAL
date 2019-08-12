/*
 * Created by Ranorex
 * User: storeuser
 * Date: 8/6/2013
 * Time: 4:04 PM
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


using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using GSLogger;
using System.Reflection;


namespace GamestopAutomation
{
    /// <summary>
    /// Description of Root.
    /// </summary>
    [TestModule("3D31BD85-196B-41AC-B353-DB940A0CE609", ModuleType.UserCode, 1)]
    public class Root : ITestModule
    {
		GamestopAutomationRepository repo = GamestopAutomationRepository.Instance;
    	List<string> lsScenarios = new List<string>();
		List<string> lsSequence = new List<string>();
		XDocument xdocTest = XDocument.Load(@"c:\PAL\Automation\test.config");
		string strPOSArch = "ipos";
		
		// setup modules

			
        public Root()
        {
            // Do not delete - a parameterless constructor is required!
        }

        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
           
			// Get the scenarios to run and the weight
			GetScenerios();
			
			// Execute the scenarios ensuring the modules are executed according to POS Architecure. 
			ExecuteScenarios();
			
      
   
        }
        
        public void GetScenerios()
        {
        	//Get the scenarios that do not have a weight of 0
        	IEnumerable<XElement> scElements = from sc in xdocTest.Descendants("scenarios")
				where sc.Descendants().Attributes("weight").ToString() != "0"
				select sc;			
			
        	//Run through each of the scenarios
        	foreach (XElement sc in scElements.Descendants())
			{
				//Add the scenerio id to the list of scenarios to run based on weight.
				//Example weight of 1: add to list once, weight of 4: add to list four times.
        		for (int i = 1; i <= (Convert.ToInt32(sc.Attribute("weight").Value.ToString())); i++)
				     {
					lsScenarios.Add(sc.Attribute("id").Value.ToString());
				     }
			}
        }    

        public void ExecuteScenarios()
        {

        	var rnd = new Random ();
        	//Loop through the scenario list while the list still has scenarios
        	while (lsScenarios.Count !=0)
        	{
        		var index = rnd.Next(0, lsScenarios.Count);
        		string strScenarioId = lsScenarios[index];

        		XDocument xdocSc = XDocument.Load(@"c:\PAL\Automation\Scenarios\" + strScenarioId + ".config");
    		
        		TestReport.BeginTestCase("Scenario " + strScenarioId,"");
        		Report.Log(ReportLevel.Info,"Scenario", "Starting Scenario " + strScenarioId);
        		
				//Query xml for the scenario matching the random selected scenario index
				var lv1s = from lv1 in xdocSc.Descendants("module") 
					select lv1;
				
				//Loop through results
					foreach (var lv1 in lv1s)
					{
						if (Global.Proceed)
						{
							Global.xelModule = lv1;
							switch (lv1.Attribute("name").Value)
							{
								case "login":
									Login mLogin = new Login();
									TestModuleRunner.Run(mLogin);
									break;
								case "AddItems":
									AddItems mAddItems = new AddItems();
									TestModuleRunner.Run(mAddItems);
									break;
								case "GPG":
									GPG mGPG = new GPG();
									TestModuleRunner.Run(mGPG);
									break;
								case "IncludeCustomer":
									IncludeCustomer mIncludeCustomer = new IncludeCustomer();
									TestModuleRunner.Run(mIncludeCustomer);
									break;
								case "PURBenefits":
									PURBenefits mPURBenefits = new PURBenefits();
									TestModuleRunner.Run(mPURBenefits);
									break;
								case "Checkout":
									Checkout mCheckout = new Checkout();
									TestModuleRunner.Run(mCheckout);
									break;
								case "Cash":
									Cash mCash = new Cash();
									TestModuleRunner.Run(mCash);
									break;								
								default:
									break;
							}
						}

					}
    		
        		lsScenarios.RemoveAt(index);
        		TestReport.EndTestCase();
        		Global.Proceed = true;
        		
        	}
        }
        
    }
}
