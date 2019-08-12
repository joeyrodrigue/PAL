/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 10:58 AM
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

using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace Alpha
{
    /// <summary>
    /// Description of fnWaitForBrowserToLoad.
    /// </summary>
    [TestModule("4C69F64A-33F5-4863-8DD0-C256F1B592DA", ModuleType.UserCode, 1)]
    public class fnWaitForBrowserToLoad : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnWaitForBrowserToLoad()
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
			fnBrowserGoHome BrowserGoHome = new fnBrowserGoHome(); 
			fnWriteToErrorFile WriteToErrorFile = new fnWriteToErrorFile();
			fnPlayWavFile PlayWavFile = new fnPlayWavFile();
			SpeechSynthesizer Speech = new SpeechSynthesizer();			

        	Global.LogFileIndentLevel++;
			Global.LogText = "IN fnWaitForBrowserToLoad";
			WriteToLogFile.Run();	 

			Global.POSBrowserFlush = false;

			Thread.Sleep(5000);

			int AttemptsCounter = 0;
			while(   ! repo.POSBrowserV25StorePortal.HomeF6.Enabled 
			      || ! repo.StorePortal.QAPOSReCommerce.Enabled
			      || !(repo.POSBrowserV25StorePortal.ProgressBar.Value == 100) )
            {	Thread.Sleep(100);
				
			double aaaa = repo.POSBrowserV25StorePortal.ProgressBar.Value;				
				
                // Check for Oops Game Over
                Report.Log(ReportLevel.Info, "WaitStatus", "Ck Oops");
				if(Host.Local.TryFindSingle(repo.RecommerceTradeLink.OopsGameOverWhatYouWereAttemptingToDoInfo.AbsolutePath.ToString(), out element))
				{	AttemptsCounter++;
					GlobalOverhead.Stopwatch.Start();
					Global.TempErrorString = "Browser Oops Game Over - pressing home and retrying";
					if(Global.DoRegisterSoundAlerts)
					{
						Speech.Speak(Global.TempErrorString + " try number " + AttemptsCounter.ToString());						
					}
					WriteToErrorFile.Run();
					Global.LogText = Global.TempErrorString;
					WriteToLogFile.Run();	
					Global.WavFilePath = "BrowserOopsGameOver.wav	";
					PlayWavFile.Run();  						
					BrowserGoHome.Run();	
					GlobalOverhead.Stopwatch.Stop();
				}       				

				// Check for browser unavailable
				Report.Log(ReportLevel.Info, "WaitStatus", "Ck browser unavailable");
				if(Host.Local.TryFindSingle(repo.StorePortal.POSBrowsingIsCurrentlyUnavailableInfo.AbsolutePath.ToString(), out element))
				{	AttemptsCounter++;
					GlobalOverhead.Stopwatch.Start();
					Global.TempErrorString = "Browser Unavailable - try go home";
					if(Global.DoRegisterSoundAlerts)
					{
						Speech.Speak(Global.TempErrorString + " try number " + AttemptsCounter.ToString());						
					}					
					WriteToErrorFile.Run();
					Global.LogText = Global.TempErrorString;
					WriteToLogFile.Run();	
					BrowserGoHome.Run();
					GlobalOverhead.Stopwatch.Stop();
				}       

				// Check for server error
				Report.Log(ReportLevel.Info, "WaitStatus", "Ck browser server error");
				if(Host.Local.TryFindSingle(repo.RecommerceTradeLink.ServerErrorInApplicationInfo.AbsolutePath.ToString(), out element))
				{	AttemptsCounter++;
					GlobalOverhead.Stopwatch.Start();
					Global.TempErrorString = "Browser Server Error In Application - try go home";
					if(Global.DoRegisterSoundAlerts)
					{
						Speech.Speak(Global.TempErrorString + " try number " + AttemptsCounter.ToString());						
					}					
					WriteToErrorFile.Run();
					Global.LogText = Global.TempErrorString;
					WriteToLogFile.Run();	
					BrowserGoHome.Run();
					GlobalOverhead.Stopwatch.Stop();
				}    

				Report.Log(ReportLevel.Info, "WaitStatus", "look for recommerce link");
				if(!Host.Local.TryFindSingle(repo.StorePortal.QAPOSReCommerceInfo.AbsolutePath.ToString(), out element))
				{
					if(AttemptsCounter > Global.MaxRetries)
					{
						
						Global.LogText = "Flushing wait for browser to load";
						WriteToLogFile.Run();
						Global.POSBrowserFlush = true;
					}
					else
					{

					}
				}
            }

        	Global.LogText = "OUT fnWaitForBrowserToLoad";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;
        }        
    }
}
