/*
 * Created by Ranorex
 * User: storeuser
 * Date: 02/19/15
 * Time: 3:15 PM
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

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Alpha
{
    /// <summary>
    /// Description of FnEnterSKU.
    /// </summary>
    [TestModule("6971DA69-43DE-43EF-9D26-A74D02346DB6", ModuleType.UserCode, 1)]
    public class FnEnterSKU : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnEnterSKU()
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
        }
        
        public void Run()
        {	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;    
            
            Ranorex.Unknown element = null;

        	RanorexRepository repo = new RanorexRepository(); 
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4(); 
        	fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();   
        	
           	// Create new stopwatch
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	

			Stopwatch MystopwatchF1 = new Stopwatch();	
			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();
       	
			
			// Enter SKU
			MystopwatchModuleTotal.Reset();			
			MystopwatchModuleTotal.Start();			
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();		
			if(Global.DomesticRegister)
			{
				while(!repo.AddItemText.Enabled)
					{ Thread.Sleep(100);  }
				
				if(Global.CurrentSKUOveride)	// 12-3-18
					repo.AddItemText.TextValue = Global.CurrentSKUOverideValue;
				else
					repo.AddItemText.TextValue = Global.CurrentSKU;
				
				repo.RetechQuickEntryView.TxtWatermark.PressKeys("{Enter}");
				Global.LogText = @"Item added";
				WriteToLogFile.Run(); 	
				if(!Global.DoingCollectible)
				{
					while(!repo.ContinueButtonCommand.Enabled)
						{ Thread.Sleep(100);  }		
					Thread.Sleep(100);				
		            repo.Retech.ButtonContent2.Click("32;11");
				}
			}
			else
			{
				// International SKU OBS Mass Effect
				//repo.IPOSScreen.InternationalOutsideSKUField.PressKeys("611547{Enter}");
				repo.IPOSInternationalEnterSKUField.Text.PressKeys("611547{Enter}");  // 3/25/19
				WriteToLogFile.Run(); 	 
				Thread.Sleep(100);
//				while(!repo.IPOS20167172.OBSMASSEFFECTEDGECARDInfo.Exists())
//				{	Thread.Sleep(100);	} 					
				if(Host.Local.TryFindSingle(repo.ReservationDeposit.RawTextESCCloseInfo.AbsolutePath.ToString(), out element)) 
					repo.ReservationDeposit.RawTextESCClose.Click();
			}

			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
            Global.CurrentMetricDesciption = "Enter SKU";
            Global.Module = "Add SKU";                
            DumpStatsQ4.Run();   			

//            if(Global.DomesticRegister)
//            {
//				// Click continue 
//	            MystopwatchQ4.Reset();	    
//				MystopwatchQ4.Start();				
//				switch (Global.RetechVersion.Substring(0,3))
//				{
//					case "5.6":
//	           			while(!repo.Retech.ContinueF5_5_6_0_103.Enabled)
//						{	Thread.Sleep(100);	}  	
//	           			repo.Retech.ContinueF5_5_6_0_103.Click();
//						break;
//					case "5.7":
//	           			while(!repo.ContinueButtonCommand.Enabled)
//						{	Thread.Sleep(100);	}  	
//	           			repo.ContinueButtonCommand.Click();			
//						break;	
//					default:
//	           			while(!repo.ContinueButtonCommand.Enabled)
//						{	
//							if(Host.Local.TryFindSingle(repo.ContinueButtonCommandInfo.AbsolutePath.ToString(), out element))
//							{
//								repo.ContinueButtonCommand.Click();
//							}		
//	           				Thread.Sleep(100);
//	           			}
//	           			Thread.Sleep(100);
//	           			repo.ContinueButtonCommand.Click();				
//						break;						
//				}				
//
//				Global.LogText = @"Waiting for CheckOut F12";
//				WriteToLogFile.Run(); 	
//				while(!repo.Retech.ButtonCheckout.Enabled)
//				{	Thread.Sleep(100);	}	
//				
//				if(Global.CurrentScenario != 39)
//				{
//					TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
//			        Global.CurrentMetricDesciption = "Click Continue";
//			        Global.Module = "Add SKU";                
//			        DumpStatsQ4.Run();  					
//				}
//			}
				
          
			if(Global.CurrentScenario != 39)
			{
		        TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
		        Global.NowCustomerLookup = Global.AdjustedTime;    
		        Global.NowEnterSKU = Global.AdjustedTime;    
		        Global.CurrentMetricDesciption = "Module Total Time";
		        Global.Module = "Add SKU";                   
		        DumpStatsQ4.Run();  				
			}
				
        }        
    }
}
