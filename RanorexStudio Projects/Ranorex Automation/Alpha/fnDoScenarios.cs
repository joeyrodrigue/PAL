/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:21 AM
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

using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace Alpha
{
    /// <summary>
    /// Description of fnDoScenarios.
    /// </summary>
    [TestModule("3616AC98-B78D-4125-81F2-9D691B10365A", ModuleType.UserCode, 1)]
    public class fnDoScenarios : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDoScenarios()
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
        	if(!Global.SwitchUploadOnly)
        	{        	
	            Mouse.DefaultMoveTime = 300;
	            Keyboard.DefaultKeyPressTime = 100;
	            Delay.SpeedFactor = 1.0;
	            
				Global.PhoneNumbertype = ""; // all types	
			
				
	            // Create the needed classes
				RanorexRepository repo = new RanorexRepository();
	        	fnGetEndTime GetEndTime = new fnGetEndTime();
	        	fnGetStartTime GetStartTime = new fnGetStartTime();
	        	fnPlayWavFile PlayWavFile = new fnPlayWavFile(); 
	        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();               
	            fnDumpStats DumpStats = new fnDumpStats();
	            fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
	            SpeechSynthesizer Speech = new SpeechSynthesizer();	
	
				Global.LogFileIndentLevel++;
	 			Global.LogText = "IN fnDoScenarios";
				WriteToLogFile.Run();			
	 		 			
	 			Global.CurrentIteration = 1;
	 			
	 			if(Global.TimeToStartExecution != "")
	 			{
					Global.LogText = "Waiting for start time - IN fnDoScenarios: " + Global.TimeToStartExecution;
					WriteToLogFile.Run();	 	
	            	Report.Log(ReportLevel.Info, "fnDoScenarios", "Waiting for start time: " + Global.TimeToStartExecution, new RecordItemIndex(0));		
					
					bool TimeToStart = false;
					while(!TimeToStart)
					{
						System.DateTime DateTimeNow = System.DateTime.Now;
						int TimeUp = System.DateTime.Compare(DateTimeNow, Convert.ToDateTime(Global.TimeToStartExecution));
						if (TimeUp > 0)  {	TimeToStart = true;	}						
					}
	
	 			}
	 			
	 			Global.LoopingDone = false;
	 			while (!Global.LoopingDone)
	 			{
		 			try
		 			{
		 				DoScenarioLooping();
		 			}
		 			catch (Exception e)
		 			{
		 				string sayString = "";
						if(e.ToString().IndexOf("Thread was being aborted.") == -1)
						{
							sayString = e.ToString().Substring(0,28);
							Global.TempErrorString = "Ranorex Crashed Message: " + e.Message;
							WriteToErrorFile.Run();
							Global.WavFilePath = "RanorexCrashed.wav";
							PlayWavFile.Run();	
							
							if(e.ToString().Contains("MemoryException") )
							{
								// Write out Tasklist to file in C:\PAL\Reports String.Empty
								string TimeStampPart = System.DateTime.Now.ToString();
								TimeStampPart = Regex.Replace(TimeStampPart, @"[/]", "-");	
								TimeStampPart = Regex.Replace(TimeStampPart, @"[:]", "-");	
								TimeStampPart = Regex.Replace(TimeStampPart, @"[ ]", "_");								
								TimeStampPart = "(" + TimeStampPart + ")";
								string CsvFilename = "Tasklist_(" + Global.RegisterName + ")_" + TimeStampPart;	
	
								var proc = new Process
								{
								    StartInfo = new ProcessStartInfo
								    {
								        FileName = Global.Register1DriveLetter + @":\PAL\Reports\GetTaskList.bat",
								        Arguments = CsvFilename,	
								        UseShellExecute = false,
								        RedirectStandardOutput = true,
								        CreateNoWindow = true,
								        WorkingDirectory = Global.Register1DriveLetter + @":\PAL\Reports\"
								    }
								};	
								
							   proc.Start();								   	
							};

							//Report.Snapshot("/form[@controlname='frmWebBrowserHost']");
	
							if(Global.DoRegisterSoundAlerts)
							{
								Speech.Speak(sayString);							
							}
							Global.WavFilePath = "Error.wav";
							PlayWavFile.Run(); PlayWavFile.Run(); PlayWavFile.Run(); PlayWavFile.Run(); 
							
							string ErrorMessage;
							ErrorMessage = e.ToString();
							Global.LogText = @ErrorMessage;
							WriteToLogFile.Run();  							
							
							MessageBox.Show(e.ToString(),"Ranorex has crashed");
	
							fnTearDown Teardown = new fnTearDown();
							Report.Log(ReportLevel.Info, "Main", "Starting Teardown", new RecordItemIndex(0));            
				            TestModuleRunner.Run(Teardown);
							Report.Log(ReportLevel.Info, "Main", "Teardown Finished", new RecordItemIndex(0));  
						
							Environment.Exit(0);
						}
		 			} 				
	 			}
	
	 			Global.LogText = "OUT fnDoScenarios";
				WriteToLogFile.Run();
				Global.LogFileIndentLevel--;
        	}
        }
        
        
	     // ####################################################
	     private void InitScenarioStart()
	     {
	     	// Ranorex.Unknown element = null; 
			RanorexRepository repo = new RanorexRepository();
			
			GlobalOverhead.Stopwatch.Reset(); 	

			Global.IndirectCall = false;         // 12-3-18   
			Global.CurrentSKUOveride = false;	// 12-3-18		
			Global.DoingCollectible = false;	// 12-3/18	
			Global.ScenarioExecuted = false;			
	     }
        
      
	
        // ####################################################
	     private void EndScenarioCleanup()
	     {
	     	Ranorex.Unknown element = null; 
			RanorexRepository repo = new RanorexRepository();
			fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();   
			
	   		if(Global.DomesticRegister)  // Domestic Store
			{	// Domestic Hanging Notification Bar 9/24/18
				if(Host.Local.TryFindSingle(repo.HOPSOverlayView.InstructionsLabelInfo.AbsolutePath.ToString(), out element))
					repo.HOPSOverlayView.ExpandButton.Click();
			}
            while(File.Exists("c:\\PAL\\pause")) 
            {
	 			Global.LogText = "Pause file - pausing";
				WriteToLogFile.Run();	            	
            	Thread.Sleep(60000); 
            }
            
			if(Host.Local.TryFindSingle(repo.PickupAtStorePopupNotifier.PickupAtStoreRequestPopUpInfo.AbsolutePath.ToString(), out element))
			{
				Report.Log(ReportLevel.Info, "Mouse", "Mouse Left Down item 'PopupNotifierView' at 434;30.", repo.PickupAtStorePopupNotifier.SelfInfo, new RecordItemIndex(0));
			    repo.PickupAtStorePopupNotifier.Self.MoveTo("434;30");
			    Keyboard.Press("{ENTER}");
			    //Mouse.ButtonDown(System.Windows.Forms.MouseButtons.Left);
			    Delay.Milliseconds(200);
				// repo.PopupNotifierView.Self.Click();
				Thread.Sleep(100);
			}            
            
  			Global.IndirectCall = false;         // 12-3-18   
			Global.CurrentSKUOveride = false;	// 12-3-18
			Global.DoingCollectible = false;	// 12-3/18
			
			if(Global.ScenarioExecuted & !Global.SwitchPauseBetweenScenariosOff)
			{
				Global.LogText = "Pausing in between scenarios";
				WriteToLogFile.Run();					
				Thread.Sleep(45000);			   	
			}

	     }
	     

	     // ####################################################
	     private void DoScenarioLooping()
	     {
			RanorexRepository repo = new RanorexRepository();
        	fnGetEndTime GetEndTime = new fnGetEndTime();
        	fnGetStartTime GetStartTime = new fnGetStartTime();
        	fnPlayWavFile PlayWavFile = new fnPlayWavFile(); 
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();               
            fnDumpStats DumpStats = new fnDumpStats();
            fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
            fnUpdatePALStatusMonitor UpdatePALStatusMonitor = new fnUpdatePALStatusMonitor(); 

           // Backoffice
            fnDoScenario13 DoScenario13 = new fnDoScenario13();  
            fnDoScenario14 DoScenario14 = new fnDoScenario14();  
            fnDoScenario15 DoScenario15 = new fnDoScenario15(); 
			fnDoScenario18 DoScenario18 = new fnDoScenario18();  
			fnDoScenario19 DoScenario19 = new fnDoScenario19();
			fnDoScenario20 DoScenario20 = new fnDoScenario20();
			fnDoScenario21 DoScenario21 = new fnDoScenario21();

            // Retech
            fnDoScenario16 DoScenario16 = new fnDoScenario16();             
            fnDoScenario33 DoScenario33 = new fnDoScenario33();   
            fnDoScenario34 DoScenario34 = new fnDoScenario34();  
            fnDoScenario36 DoScenario36 = new fnDoScenario36();    
            fnDoScenario37 DoScenario37 = new fnDoScenario37(); 
            fnDoScenario38 DoScenario38 = new fnDoScenario38();
            fnDoScenario41 DoScenario41 = new fnDoScenario41();
            fnDoScenario42 DoScenario42 = new fnDoScenario42();
            fnDoScenario43 DoScenario43 = new fnDoScenario43();
            fnDoScenario47 DoScenario47 = new fnDoScenario47();


            // Other
            fnDoScenario50 DoScenario50 = new fnDoScenario50();
			fnUpdateItemMDB UpdateItemMDB = new fnUpdateItemMDB();	
                        
            
			Global.LogText = "RUN STARTED - IN fnDoScenarios Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();	 			
 			
 			// Decide if limited by number of iterations or time
 			if (Global.IterationsToDo != -1)
 			{	// not equal to zero is based on number of iterations
 				Global.StopByTime = false;
 			}
 			else
 			{	// equal to -1 means drive by stop time
 				Global.StopByTime = true;
 			}

			Global.DoDumpStats = false;
			Global.CurrentScenario = 0;
			Global.RetechScenariosPerformed = 0;
			
			if(Global.RegisterNumber == "1")
			{
				Global.LogText = "Updating SKU quantities";
				WriteToLogFile.Run();	
				Global.SQLCommand = "update sku set Qty = 999999 where Qty < 5000";
				UpdateItemMDB.Run();					
			}
		
			
			// If Scenario 37 Reserve Pickup then turn flag off for all SKUs EnforceStreetDate
			if (Global.DoScenarioFlag[37] && Global.RegisterNumber == "1")
				{
					Global.LogText = "Setting all EnforceStreetDates to false";
					WriteToLogFile.Run();	
					Global.SQLCommand = "update sku set EnforceStreetDate = false where EnforceStreetDate = true";
					UpdateItemMDB.Run();	
					Global.LogText = "Setting all IsStreetDateOverridden to true";
					WriteToLogFile.Run();						
					Global.SQLCommand = "update sku set IsStreetDateOverridden = true where IsStreetDateOverridden = false";
					UpdateItemMDB.Run();						
				}
			
			

            // ########## Start the main scenario looping here ##########
            do
            {	GetStartTime.Run();	// Get Scenario start time
            	InitScenarioStart(); DoScenario13.Run(); EndScenarioCleanup();
	            InitScenarioStart(); DoScenario14.Run(); EndScenarioCleanup();  
	            InitScenarioStart(); DoScenario15.Run(); EndScenarioCleanup();  
	            InitScenarioStart(); DoScenario16.Run(); EndScenarioCleanup();
				InitScenarioStart(); DoScenario18.Run(); EndScenarioCleanup();  
				InitScenarioStart(); DoScenario19.Run(); EndScenarioCleanup();  
				InitScenarioStart(); DoScenario20.Run(); EndScenarioCleanup();
				InitScenarioStart(); DoScenario21.Run(); EndScenarioCleanup();  
	            InitScenarioStart(); DoScenario33.Run(); EndScenarioCleanup();  
	            InitScenarioStart(); DoScenario34.Run(); EndScenarioCleanup();  
	            InitScenarioStart(); DoScenario36.Run(); EndScenarioCleanup();  
				InitScenarioStart(); DoScenario37.Run(); EndScenarioCleanup();  
				InitScenarioStart(); DoScenario38.Run(); EndScenarioCleanup();  
				InitScenarioStart(); DoScenario41.Run(); EndScenarioCleanup();
				InitScenarioStart(); DoScenario42.Run(); EndScenarioCleanup();
				InitScenarioStart(); DoScenario43.Run(); EndScenarioCleanup();
	            InitScenarioStart(); DoScenario47.Run(); EndScenarioCleanup();  
	            
	            InitScenarioStart(); DoScenario50.Run(); EndScenarioCleanup();  	            
            	GetEndTime.Run();	// Get Scenario end time
	            	
            	//  Dump stats for all scenarios to the stats file
				Global.CurrentScenario = 0;            	
            	if(Global.DoDumpStats) 
            	{ 	DumpStats.Run(); 
            	}
	        	 	
	           	Global.CurrentIteration++;

           	 	// See if time to quit
           	 	if(!Global.LoopingDone)
           	 	{
		           	 if (!Global.StopByTime)
		           	 {	if (Global.CurrentIteration > Global.IterationsToDo) {	Global.LoopingDone = true;	}
		           	 }
		           	 else
		           	 {	System.DateTime DateTimeNow = System.DateTime.Now;
		           	 	string 	TimeToStopExecution = Global.TimeToStopExecution;	
						int CommaOffset = TimeToStopExecution.IndexOf(",");
						if(CommaOffset != -1)
							TimeToStopExecution = Global.TimeToStopExecution.Substring(0,CommaOffset);		           	 	
						int TimeUp = System.DateTime.Compare(DateTimeNow, Convert.ToDateTime(TimeToStopExecution));
						if (TimeUp > 0)  {	Global.LoopingDone = true;	}
		           	 }               	 		
           	 	}
           	 	
           	 	Global.IterationsToday++;	// PAL Status Monitor
           	 	
           	 	UpdatePALStatusMonitor.Run();
           	 	
           	 	Global.LogText = "Next Iteration";
           	 	WriteToLogFile.Run();
           	 	
            } while (!Global.LoopingDone);

            Global.WavFilePath = "Register" + Global.RegisterNumber + "Finished.wav";
            PlayWavFile.Run();     

			Global.LogText = "RUN FINISHED - OUT fnDoScenarios Iteration: " + Global.CurrentIteration;
			WriteToLogFile.Run();		     	
	     }
	     

    
    }
    
    
   

}
