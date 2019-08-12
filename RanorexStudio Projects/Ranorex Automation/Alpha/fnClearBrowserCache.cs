/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 10/12/13
 * Time: 7:27 AM
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
    /// Description of fnClearBrowserCache.
    /// </summary>
    [TestModule("E51C90A5-8DB5-4F89-B3B3-E5C0CCE25377", ModuleType.UserCode, 1)]
    public class fnClearBrowserCache : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnClearBrowserCache()
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
        	
        	RanorexRepository repo = new RanorexRepository();
        	
        	Ranorex.Unknown element = null; 
        	
			Report.Log(ReportLevel.Info, "IN fnClearBrowserCache", "IN fnClearBrowserCache", new RecordItemIndex(0));        	

			// The following will clear the cache for IE and the POS Browser. You can copy and paste it into the Ranorex code.

			Host.Local.RunApplication("C:\\Windows\\System32\\rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255", "", false);
			Thread.Sleep(1000);

			// NOTE store 4285 register 2 has takes very long time to clear cache and times out
			try
			{	repo.DeleteBrowsingHistory.PleaseWaitWhileClearingHistoryInfo.WaitForNotExists(90000);
			}
			catch
			{	repo.DeleteBrowsingHistory.PleaseWaitWhileClearingHistoryInfo.WaitForNotExists(90000);
			}
			try
			{	repo.DeleteBrowsingHistory.SelfInfo.WaitForNotExists(90000);
			}
			catch
			{	
				try
				{
					repo.DeleteBrowsingHistory.SelfInfo.WaitForNotExists(90000);
				}
				catch
				{
					try
					{
						repo.DeleteBrowsingHistory.SelfInfo.WaitForNotExists(90000);
					}
					catch
					{
						repo.DeleteBrowsingHistory.SelfInfo.WaitForNotExists(90000);
					}					
				}

			}			
			
			while(	Host.Local.TryFindSingle(repo.DeleteBrowsingHistory.PleaseWaitWhileClearingHistoryInfo.AbsolutePath.ToString(), out element)
			  	|| 	Host.Local.TryFindSingle(repo.DeleteBrowsingHistory.SelfInfo.AbsolutePath.ToString(), out element)
			  
			  )
			{
				Thread.Sleep(2000);				
			}
			
			Report.Log(ReportLevel.Info, "OUT fnClearBrowserCache", "OUT fnClearBrowserCache", new RecordItemIndex(0));  			

        }        
    }
}
