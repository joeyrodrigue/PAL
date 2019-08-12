/*
 * Created by Ranorex
 * User: storeuser
 * Date: 06/02/15
 * Time: 6:18 AM
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
    /// Description of FnCommonScenario14and15.
    /// </summary>
    [TestModule("8ADAA63D-642B-49E8-8AFB-2EFDDAC50873", ModuleType.UserCode, 1)]
    public class FnCommon14and15 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FnCommon14and15()
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
        
	     private void GoHome()
	     {
	     	Ranorex.Unknown element = null; 
			RanorexRepository repo = new RanorexRepository();
			fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
			
			Thread.Sleep(1000);
			Global.LogText = @"Waiting for Performance Dashboard Menu to display";
			WriteToLogFile.Run();	
			while(!repo.ShellRoot.DashboardSHomeButton.Enabled)
			{ Thread.Sleep(100); }
			repo.ShellRoot.DashboardSHomeButton.Click();
			while(!Host.Local.TryFindSingle(repo.ShellRoot.MyLogoRootInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.MyLogoRoot.Enabled) 
            	{ Thread.Sleep(100); }  		
	     }                
        
        public void Run()
        {	
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;    
            
			Ranorex.Unknown element = null;             
			
        	RanorexRepository repo = new RanorexRepository();
        	fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor();  
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();
        	FnWriteOutStatsQ4Buffer WriteOutStatsQ4Buffer = new FnWriteOutStatsQ4Buffer();
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();
        	fnTimeMinusOverhead TimeMinusOverhead = new fnTimeMinusOverhead(); 
			fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();  			
			
			Stopwatch MystopwatchQ4 = new Stopwatch();	
			Stopwatch MystopwatchTT = new Stopwatch();	
			
           	MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start(); 
			Global.LogText = @"Waiting for Performance Dashboard Menu to display";
			WriteToLogFile.Run();	
			while(!Host.Local.TryFindSingle(repo.ShellRoot.DashBoardF11Info.AbsolutePath.ToString(), out element)) 
				{	Thread.Sleep(100);	 }
            while(!repo.ShellRoot.DashBoardF11.Enabled)
            	{	Thread.Sleep(100);	}
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Display Dashboard Menu";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();   	           
 
            //
            // Sales F1 Report
            // 
			Global.LogText = @"Waiting for Sales F1 Report to display";
			WriteToLogFile.Run();
			while(!Host.Local.TryFindSingle(repo.ShellRoot.SalesButtonF1Info.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
			while(!repo.ShellRoot.SalesButtonF1.Enabled) 
				{ Thread.Sleep(100);   }
			Thread.Sleep(100);
			repo.ShellRoot.SalesButtonF1.Click("184;26");
			Thread.Sleep(100);
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start(); 
			while(!Host.Local.TryFindSingle(repo.ShellRoot.SalesTitleBarInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.SalesTitleBar.Enabled) 
            	{ Thread.Sleep(100); }
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Display Sales F1 Report";
            Global.Module = "Dashboard:";
            DumpStatsQ4.Run();    
			GoHome();

            
            //
            // Five To Drive F2 Report
            // 
			Global.LogText = @"Waiting for Five To Drive F2 Report to display";
			WriteToLogFile.Run();	
			while(!Host.Local.TryFindSingle(repo.ShellRoot.FiveToDriveButtonF2Info.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.FiveToDriveButtonF2.Enabled) 
            	{ Thread.Sleep(100);	}
			Thread.Sleep(100);            
			repo.ShellRoot.FiveToDriveButtonF2.Click("104;13");
			Thread.Sleep(100);
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();  
			while(!Host.Local.TryFindSingle(repo.ShellRoot.FiveToDriveTitleBarInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.FiveToDriveTitleBar.Enabled) 
            	{ Thread.Sleep(100);	}
   			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Display Five To Drive F2 Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run(); 
			GoHome();
            
            
            //
            // Guest F3 Report
            // 
			Global.LogText = @"Waiting for Guest F3 Report to display";
			WriteToLogFile.Run();	
			while(!Host.Local.TryFindSingle(repo.ShellRoot.GuestButtonF3Info.AbsolutePath.ToString(), out element)) 
				{	Thread.Sleep(100);	 }
			while(!repo.ShellRoot.GuestButtonF3.Enabled) 
				{ Thread.Sleep(100); }
			Thread.Sleep(100);			
			repo.ShellRoot.GuestButtonF3.Click("160;9");			
            Thread.Sleep(100);
            MystopwatchQ4.Reset();	                
			MystopwatchQ4.Start();  
			while(!Host.Local.TryFindSingle(repo.ShellRoot.GuestTitleBarInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
			while(!repo.ShellRoot.GuestTitleBar.Enabled) 
				{ Thread.Sleep(100); }
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Display Guest F3 Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run(); 
			GoHome(); 

            //
            // Circle of Life F4 Report
            //             
			Global.LogText = @"Waiting for Circle of Life F4 Report to display";
			WriteToLogFile.Run();
			while(!Host.Local.TryFindSingle(repo.ShellRoot.CircleOfLifeButtonF4Info.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
			while(!repo.ShellRoot.CircleOfLifeButtonF4.Enabled) 
				{ Thread.Sleep(100); }
			Thread.Sleep(100);
			repo.ShellRoot.CircleOfLifeButtonF4.Click("198;9");
			Thread.Sleep(100);
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();
			while(!Host.Local.TryFindSingle(repo.ShellRoot.CircleOfLifeTaskBarInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.CircleOfLifeTaskBar.Enabled) 
            	{ Thread.Sleep(100);	}
			Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = "Display Circle of Life F4 Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();      
	        // Sub Circle of Life Reports            
	        // F1 Reservations
				Global.LogText = @"Waiting for F1 Reservations Report to display";
				WriteToLogFile.Run();
				while(!repo.ShellRoot.SalesButtonF1.Enabled) 
					{ Thread.Sleep(100); }
				Thread.Sleep(100);
				repo.ShellRoot.SalesButtonF1.Click();
				Thread.Sleep(100);
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();
				while(!Host.Local.TryFindSingle(repo.ShellRoot.ReservationsTitleBarInfo.AbsolutePath.ToString(), out element))
					{ Thread.Sleep(100); }
	            while(!repo.ShellRoot.ReservationsTitleBar.Enabled) 
	            	{ Thread.Sleep(100);	}
				Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.CurrentMetricDesciption = "Display F1 Reservations Report";
	            Global.Module = "Dashboard:";                
	            DumpStatsQ4.Run();   
	            Thread.Sleep(1000);
				repo.ShellRoot.Self.PressKeys("{Escape}"); 	            
				Global.LogText = @"Waiting for Circle of Life to display";
				WriteToLogFile.Run();	
				while(!Host.Local.TryFindSingle(repo.ShellRoot.CircleOfLifeTaskBarInfo.AbsolutePath.ToString(), out element))
					{ Thread.Sleep(100); }
	            while(!repo.ShellRoot.CircleOfLifeTaskBar.Enabled) 
	            	{ Thread.Sleep(100);	}
	        // F2 Power Up Rewards
	            Global.LogText = @"Waiting for F2 Power Up Rewards Report to display";
				WriteToLogFile.Run();
				while(!repo.ShellRoot.FiveToDriveButtonF2.Enabled) 
					{ Thread.Sleep(100); }
				Thread.Sleep(100);
				repo.ShellRoot.FiveToDriveButtonF2.Click();
				Thread.Sleep(100);
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();
				while(!Host.Local.TryFindSingle(repo.ShellRoot.PowerUpRewardsTitleBarInfo.AbsolutePath.ToString(), out element))
					{ Thread.Sleep(100); }
	            while(!repo.ShellRoot.PowerUpRewardsTitleBar.Enabled) 
	            	{ Thread.Sleep(100);	}
				Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.CurrentMetricDesciption = "Display F2 Power Up Rewards Report";
	            Global.Module = "Dashboard:";                
	            DumpStatsQ4.Run(); 
				Thread.Sleep(1000);	            
				repo.ShellRoot.Self.PressKeys("{Escape}"); 	            
				Global.LogText = @"Waiting for Circle of Life to display";
				WriteToLogFile.Run();	
				while(!Host.Local.TryFindSingle(repo.ShellRoot.CircleOfLifeTaskBarInfo.AbsolutePath.ToString(), out element))
					{ Thread.Sleep(100); }
	            while(!repo.ShellRoot.CircleOfLifeTaskBar.Enabled) 
	            	{ Thread.Sleep(100);	}
            // Exit Circle of Life Report
			GoHome();
			
            //
            // Team F5 Report
            //             
			Global.LogText = @"Waiting for Team F5 Report to display";
			WriteToLogFile.Run();	
			while(!Host.Local.TryFindSingle(repo.ShellRoot.TeamF5Info.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
			while(!repo.ShellRoot.TeamF5.Enabled) 
				{ Thread.Sleep(100); }
			Thread.Sleep(100);
			repo.ShellRoot.TeamF5.Click("53;11");
			Thread.Sleep(100);
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();  
			while(!Host.Local.TryFindSingle(repo.ShellRoot.TeamTitleBarInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.TeamTitleBar.Enabled) 
            	{ Thread.Sleep(100); }
      		Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Display Team F5 Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();              
			Global.LogText = @"Waiting for Performance Dashboard Menu to display";
			WriteToLogFile.Run();	
            Thread.Sleep(100);	
            	// Sub report 
            	// F1 Collectible Sales
				Global.LogText = @"Waiting for F1 Collectible Sales Report to display";
				WriteToLogFile.Run();
				repo.ShellRoot.Self.PressKeys("{F1}"); 
	            MystopwatchQ4.Reset();	    
				MystopwatchQ4.Start();
				while(!Host.Local.TryFindSingle(repo.ShellRoot.CollectiblesSalesDollarInfo.AbsolutePath.ToString(), out element))
					{ Thread.Sleep(100); }
	            while(!repo.ShellRoot.CollectiblesSalesDollar.Enabled) 
	            	{ Thread.Sleep(100);	}
				Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
				TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
	            Global.CurrentMetricDesciption = "Display F1 Collectible Sales Report";
	            Global.Module = "Dashboard:";                
	            DumpStatsQ4.Run(); 
				Thread.Sleep(1000);	            
				repo.ShellRoot.Self.PressKeys("{Escape}"); 	            
				Global.LogText = @"Waiting for Circle of Life to display";
				WriteToLogFile.Run();	
				while(!Host.Local.TryFindSingle(repo.ShellRoot.TeamTitleBarInfo.AbsolutePath.ToString(), out element))
					{ Thread.Sleep(100); }
	            while(!repo.ShellRoot.TeamTitleBar.Enabled) 
	            	{ Thread.Sleep(100); }
            // Exit Team Report
			GoHome();     
            
            // My Report
			Global.LogText = @"Waiting for My Report Report to display";
			WriteToLogFile.Run();	            
            repo.ShellRoot.Icon.Click("1;22");
            Delay.Milliseconds(200);
            repo.ShellRoot.MyREPORT.Click("127;23");
            Delay.Milliseconds(200);     
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();  
			while(!Host.Local.TryFindSingle(repo.ShellRoot.EmployeeComparisonCtrlPlusTInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.EmployeeComparisonCtrlPlusT.Enabled) 
            	{ Thread.Sleep(100); }
      		Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Display My Report Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();       
			GoHome();
			while(Host.Local.TryFindSingle(repo.ShellRoot.EmployeeComparisonCtrlPlusTInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }

            // My Goals
			Global.LogText = @"Waiting for My Goals Report to display";
			WriteToLogFile.Run();	            
            repo.ShellRoot.Icon.Click("1;22");
            Delay.Milliseconds(200);
            repo.ShellRoot.Text.Click("38;9");
            Delay.Milliseconds(200);         
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();  
			while(!Host.Local.TryFindSingle(repo.ShellRoot.EntireStoreInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.EntireStore.Enabled) 
            	{ Thread.Sleep(100); }
      		Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Display My Goals Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();              
			GoHome();
            
            // My Schedule Planner
			Global.LogText = @"Waiting for My Schedule Planner Report to display";
			WriteToLogFile.Run();	            
            repo.ShellRoot.Icon.Click("1;22");
            Delay.Milliseconds(200);
            repo.ShellRoot.Text1.Click("87;6");
            Delay.Milliseconds(200);            
            MystopwatchQ4.Reset();	    
			MystopwatchQ4.Start();  
			while(!Host.Local.TryFindSingle(repo.ShellRoot.SchedulePlannerHeaderInfo.AbsolutePath.ToString(), out element))
				{ Thread.Sleep(100); }
            while(!repo.ShellRoot.SchedulePlannerHeader.Enabled) 
            	{ Thread.Sleep(100); }
      		Global.TempFloat = (float) MystopwatchQ4.ElapsedMilliseconds / 1000;
			TimeMinusOverhead.Run((float) MystopwatchQ4.ElapsedMilliseconds);  // Subtract overhead and store in Global.Q4StatLine
            Global.CurrentMetricDesciption = @"Display My Schedule Planner Report";
            Global.Module = "Dashboard:";                
            DumpStatsQ4.Run();              
			GoHome(); 
        }
    }
}
