/*
 * Created by Ranorex
 * User: storeuser
 * Date: 12/18/2013
 * Time: 9:08 AM
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

using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Windows.Forms;

namespace GamestopAutomation
{
    /// <summary>
    /// Description of Validation.
    /// </summary>
    [TestModule("A08349E5-ABAF-4172-9BCB-F72D9C0D75E1", ModuleType.UserCode, 1)]
    public class Verify : ITestModule
    {

        Ranorex.Unknown rUnknown = null;
        
        
        public Verify()
        {

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

            Global.Criteria = from c in Global.xdocModule.Descendants("Criteria")
				select c;	


            foreach (XElement lv1 in Global.Criteria.Descendants(Global.CriteriaType))
            {
                
            	string xPath = lv1.Attribute("XPath").Value;
            	bool rExists = Convert.ToBoolean(lv1.Attribute("Exists").Value);
         	
            	bool found = Host.Local.TryFindSingle<Ranorex.Unknown>(xPath, 2000, out rUnknown);
            	
            	if(rExists)
            	{
            		if(found)
            		   {
            			Validate.Exists(xPath,2000,"Valid " + Global.CriteriaType + " Criteria Found",false);
            		   	Global.Proceed = true;
            		   	return;
            		   }
            	}
            	else
            	{
            		if(!found)
            		   {
            			Validate.NotExists(xPath,500,"Valid " + Global.CriteriaType + " Criteria Found",false);
            		   	Global.Proceed = true;
            		   	return;
             		   }
            	}
            	
            }
            
            if (!Global.Proceed)
            {
            	Report.Log(ReportLevel.Failure, "Criteria not met", Global.CriteriaType + " validation failed");
            }
        }
        public void Item()
        {
        	
        }

    }
}
