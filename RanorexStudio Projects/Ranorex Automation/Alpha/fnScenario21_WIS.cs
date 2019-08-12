/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:03 AM
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
    /// Description of fnDoScenario3.
    /// </summary>
    [TestModule("5372A447-AB16-4A86-8BD0-976B858B269C", ModuleType.UserCode, 1)]
    public class fnDoScenario21 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenario21()
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
        {	Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
        	RanorexRepository repo = new RanorexRepository();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor();  
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead();   
        	
			Ranorex.Unknown element = null;
			Global.AbortScenario = false;
        	
        //*****************Start  Scenario 21 - WIS Web-In-Store ******************
			Global.CurrentScenario = 21;

        	if(!Global.IndirectCall)			
				if (!Global.DoScenarioFlag[Global.CurrentScenario] )	
	//			if(Global.CurrentScenario != 9999)
				{ 	
					return;
				}
        	
        	Global.ScenarioExecuted = true;
		
			Global.RetechScenariosPerformed++;
			UpdatePALStatusMonitor.Run();
			
          	// Create new stopwatch
			Stopwatch MystopwatchTT = new Stopwatch();	
			MystopwatchTT.Reset();	
			MystopwatchTT.Start();	
			Stopwatch MystopwatchModuleTotal = new Stopwatch();		
			MystopwatchModuleTotal.Reset();
			MystopwatchModuleTotal.Start();	 			
			
			Stopwatch MystopwatchQ4 = new Stopwatch();				
			
			Global.LogText = @"---> fnDoScenario21 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	        	
            Report.Log(ReportLevel.Info, "Scenario 21 IN", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));			

            // Click on back office link
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();    
			Global.LogText = @"Clicking on Back Office link";
			WriteToLogFile.Run(); 				
            repo.Retech.Self.Click();
            repo.Retech.Self.PressKeys("{LShiftKey down}{F3}{LShiftKey up}");
 			Global.LogText = @"Waiting for Back Office home screen";
			WriteToLogFile.Run();               
            while(!repo.BackOffice275111HomeScreen.BackOffice275111HomeScreen.Enabled)
            	{	Thread.Sleep(100);   }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Load Back Office";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run();  
	        Thread.Sleep(1000);
		
	        // Press Shift-F2 for WIS
	        MystopwatchQ4.Reset();
			MystopwatchQ4.Start();	
			Global.LogText = @"Pressing Shift-F2 for WIS";
			WriteToLogFile.Run();  			
			Keyboard.Press("{LShiftKey down}{F2}{LShiftKey up}");
			Global.LogText = @"Waiting for Browser to load";
			WriteToLogFile.Run();
			Thread.Sleep(3000);
			while(!Host.Local.TryFindSingle(repo.WebInStoreProductMozillaFirefox.FireFoxTextInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(200); }
			while(!repo.WebInStoreProductMozillaFirefox.FireFoxText.Enabled) 
				{ Thread.Sleep(200); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Load Browser";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run();             	
	        Thread.Sleep(200);

	        // Enter SKU
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Enter SKU"; 
			WriteToLogFile.Run(); 
			repo.WebInStoreProductMozillaFirefox.FireFoxText.PressKeys("100800{Return}");  // ##### E N T E R    S K U ###
            Thread.Sleep(100);
            Global.LogText = @"Waiting for SKU link to display";
			WriteToLogFile.Run();
			while(!repo.MozillaFirefoxGameStopEdition.ProductDetails.Enabled)
				{ Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Search for SKU";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 

	        // Click on Product Details
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Click on Product Details"; 
			WriteToLogFile.Run();  
			repo.MozillaFirefoxGameStopEdition.ProductDetails.Click();
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Product Details to display";
			WriteToLogFile.Run();
			while(!repo.WebInStoreProductMozillaFirefox.ProductDetailsText.Enabled) { Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Get Product Details";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 

	        // Click on Add to Cart
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Click on Add to Cart"; 
			WriteToLogFile.Run();  
			repo.WebInStoreProductMozillaFirefox.Grouping.AddToCart.Click();
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Item(s) added to cart";
			WriteToLogFile.Run();
			while(!repo.WebInStoreProductMozillaFirefox.Grouping.TextItemSAddedToCart.Enabled)
				{ Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Add to Cart";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 	        

	      	// Click on Cart Continue
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Click on Cart Continue"; 
			WriteToLogFile.Run(); 
			repo.WebInStoreProductMozillaFirefox.Grouping.Continue.Click();
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Shipping Address";
			WriteToLogFile.Run();
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddress1.Click();
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Load Shipping Address";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 	   

	       	// Fill in shipping address and click continue
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Fill in shipping address and click continue"; 
			WriteToLogFile.Run(); 
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressFirstName.PressKeys("PAL");
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressLastName.PressKeys("Last");
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressAddress.PressKeys("301 Tower");
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressCity.PressKeys("MyCity");
			//state start
            repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ComboBox.Click("214;9");
            Delay.Milliseconds(200);
            repo.Dropdown.Self.Click("169;23");
            Delay.Milliseconds(200);
            repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ComboBox.PressKeys("{LControlKey down}{LMenu}{LControlKey up}");
			// State end
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressZipPostal.PressKeys("76401");
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressPhone.PressKeys("9727651234");
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressEmail.PressKeys("x@y.com");
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressReTypeEmail.PressKeys("x@y.com");
			// Press continue
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressContinue.Click();	
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Shipping Address Confirm";
			WriteToLogFile.Run();
			while(!repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressConfirmButton.Enabled)
				{ Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Fill in Shipping Address";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 	

	      	// Click on Shipping Address Confirmed
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Click on Shipping Address Confirmed"; 
			WriteToLogFile.Run();  
			repo.ShippingAddressMozillaFirefoxGame.ShippingAddress.ShippingAddressConfirmButton.Click();
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Shipping Method";
			WriteToLogFile.Run();
			while(!repo.ShippingAddressMozillaFirefoxGame.Grouping.ShippingMethod.Enabled)
				{ Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Shipping Address Confirmed";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 	

	        // Click on Shipping Method Continue
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Click on Shipping Method Continue"; 
			WriteToLogFile.Run();
			repo.ShippingAddressMozillaFirefoxGame.Grouping.ShippingMethodContinue.Click();
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Order Review";
			WriteToLogFile.Run();
			while(!repo.ShippingAddressMozillaFirefoxGame.Grouping.OrderReview.Enabled) 
				{ Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Shipping Method Confirmed";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 	

	        // Click on Order Review Continue
	    	MystopwatchQ4.Reset();
			MystopwatchQ4.Start();
			Global.LogText = @"Click on Order Review Continue"; 
			WriteToLogFile.Run();  
			repo.ShippingAddressMozillaFirefoxGame.Grouping.OrderReviewSubmit.Click();
            Thread.Sleep(100);
            Global.LogText = @"Waiting for Order Confirmation";
			WriteToLogFile.Run();
			while(!repo.ShippingAddressMozillaFirefoxGame.Grouping.GameStopWebOrderConfirmation.Enabled)
				{ Thread.Sleep(100); }
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine            
	        Global.CurrentMetricDesciption = "Order Confirmation";
	        Global.Module = "WIS:";                
	        DumpStatsQ4.Run(); 		        

			Global.TempFloat = (float) MystopwatchModuleTotal.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchModuleTotal.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Module Total Time";
            Global.Module = "WIS:";                
            DumpStatsQ4.Run();    
	        
            // Press Escape to exit
            repo.ShippingAddressMozillaFirefoxGame.FireFoxExit.Click();

            TimeMinusOverhead.Run((float) MystopwatchTT.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Scenario 21";
            Global.Module = "Total Time";                
            DumpStatsQ4.Run();  	
            
			// Write out metrics buffer
			WriteOutStatsQ4Buffer.Run();              

			Global.LogText = "<--- fnDoScenario21 Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	      			
            Report.Log(ReportLevel.Info, "Scenario 21 OUT", "Iteration: " + Global.CurrentIteration, new RecordItemIndex(0));	

			return;
			
		// ***********End Scenario 21 *****************        	
        }
    }
}
