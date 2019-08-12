/*
 * Created by Ranorex
 * User: storeuser
 * Date: 09/26/13
 * Time: 12:22 PM
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

using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using System.Management;
using Ini;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;



namespace Alpha
{
    /// <summary>
    /// Description of fnTearDown.
    /// </summary>
    [TestModule("7615B2D7-046E-4DEB-A525-03A71BA18B1D", ModuleType.UserCode, 1)]
    public class fnTearDown : ITestModule
    {
        	public DataTable dtPerfData = new DataTable("Performance");
        	public DataTable dtAutoData = new DataTable("Automation");
        	
        public fnTearDown()
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
            
			Report.Log(ReportLevel.Info, "fnTearDown", "Uploading Data", new RecordItemIndex(0));            
            
            if(!Global.IsPerformanceTest)
            {
            	//This is not a performance test. Close application.
            	Environment.Exit(0);
            }
            else
            {
            	Process[] processes = Process.GetProcessesByName("wscript");
            	foreach (Process proc in processes)
            	{
            		proc.Kill();
            	}
            	Delay.Milliseconds(5000);
            	
            	Report.Log(ReportLevel.Info, "Metric Collection", "Uploading Test Data", new RecordItemIndex(0));
            	string boottime = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Gamestop", "BootTime", null);
            	
            	GatherPerfData(boottime);
            	if (dtPerfData.Rows.Count != 0)
            	{
            		fnSQLBulkInsert(dtPerfData,"PerfMetrics");
            	}
            	
            	GatherQTPData();
            	if (dtAutoData.Rows.Count != 0)
            	{
            		fnSQLBulkInsert(dtAutoData,"AutomationMetrics");
            	}
            }
        }
        public void GatherPerfData(string boottime)
        {
          	dtPerfData.Columns.Add("PerfMetrics_ID", typeof(Int32));
            dtPerfData.Columns.Add("EventTime", typeof(System.DateTime));
            dtPerfData.Columns.Add("Test_ID", typeof(Int32));
            dtPerfData.Columns.Add("Register_ID", typeof(String));
            dtPerfData.Columns.Add("Process", typeof(String));
            dtPerfData.Columns.Add("CPU", typeof(Int32));
            dtPerfData.Columns.Add("Memory", typeof(Int32));
            dtPerfData.Columns.Add("threads", typeof(Int32));
            dtPerfData.Columns.Add("handles", typeof(Int32));
            dtPerfData.Columns.Add("gdiobjects", typeof(Int32));
            dtPerfData.Columns.Add("userobjects", typeof(Int32));
            dtPerfData.Columns.Add("PercentDiskTime", typeof(Int32));
            dtPerfData.Columns.Add("AverageDiskQueueLength", typeof(Int32));
            dtPerfData.Columns.Add("BootTime", typeof(Int32));
            dtPerfData.Columns.Add("DiskIdleTime", typeof(Int32));
            dtPerfData.Columns.Add("PagesOutputPersec", typeof(Int32));

           	var filez = Directory.GetFiles(@"c:\pal\performance","PerformanceMetrics*.csv",SearchOption.TopDirectoryOnly);
			foreach (string currentfile in filez)
			{
			   string cline = "";
			   StreamReader sr = new StreamReader(currentfile);
			   sr.ReadLine();
			

			
			   while (!sr.EndOfStream)
			   {
			       DataRow newRow = dtPerfData.NewRow();
			       cline = sr.ReadLine();
			       string[] values = cline.Split(',');
			       
			       newRow["EventTime"] = Convert.ToDateTime(values[0]);
			       newRow["Test_ID"] = Global.Test_ID; 
			       newRow["Register_ID"] = Global.Register_ID;
			       newRow["Process"] = values[2];
			       if (values[3] == "")
			       {
			       	newRow["CPU"] = DBNull.Value;
			       }
			       else
			       {
			           newRow["CPU"] = Convert.ToInt32(values[3]);
			       }
			       if (values[2] == "Total")
			       {
			           if (Convert.ToInt32(values[4]) < 10)
			           {
			               newRow["Memory"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["Memory"] = Convert.ToInt32(values[4]);
			           }
			           if (Convert.ToInt32(values[5]) < 10)
			           {
			               newRow["Threads"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["Threads"] = Convert.ToInt32(values[5]);
			           }
			           if (Convert.ToInt32(values[6]) < 100)
			           {
			               newRow["Handles"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["Handles"] = Convert.ToInt32(values[6]);
			           }
			
			       }
			       else
			       {
			           newRow["Memory"] = Convert.ToInt32(values[4]);
			           newRow["Threads"] = Convert.ToInt32(values[5]);
			           newRow["Handles"] = Convert.ToInt32(values[6]);
			       }
			
			       if (values[2] != "Total")
			       {
			           newRow["PercentDiskTime"] = DBNull.Value;
			           newRow["AverageDiskQueueLength"] = DBNull.Value;
			           newRow["DiskIdleTime"] = DBNull.Value;
			           newRow["PagesOutputPersec"] = DBNull.Value;
			           if (values[7] == "")
			           {
			               newRow["gdiobjects"] = DBNull.Value;
			           }
			           else
			           {
			
			               newRow["gdiobjects"] = Convert.ToInt32(values[7]);
			
			           }
			           if (values[8] == "")
			           {
			               newRow["userobjects"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["userobjects"] = Convert.ToInt32(values[8]);
			           }
			
			       }
			       else
			       {
			           if (values[7] == "")
			           {
			               newRow["PercentDiskTime"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["PercentDiskTime"] = Convert.ToInt32(values[7]);
			           }
			           if (values[8] == "")
			           {
			               newRow["AverageDiskQueueLength"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["AverageDiskQueueLength"] = Convert.ToInt32(values[8]);
			           }
			           if (values[9] == "")
			           {
			               newRow["DiskIdleTime"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["DiskIdleTIme"] = Convert.ToInt32(values[9]);
			           }
			           if (values[10] == "")
			           {
			               newRow["PagesOutputPersec"] = DBNull.Value;
			           }
			           else
			           {
			               newRow["PagesOutputPersec"] = Convert.ToInt32(values[10]);
			           }			           
			           newRow["gdiobjects"] = DBNull.Value;
			           newRow["userobjects"] = DBNull.Value;
			       }
			
			
			
			
			       if (boottime == "")
			       {
			           newRow["BootTime"] = DBNull.Value;
			       }
			       else
			       {
			           newRow["BootTime"] = Convert.ToInt32(boottime);
			       }
			
			       dtPerfData.Rows.Add(newRow);
	
			       
			   }
			   sr.Close();
			
			   File.Move(currentfile, currentfile.Replace(".csv", "." + System.DateTime.Now.ToString("yyyymmdd-HHmm") + ".bak"));
			
           }
          
        }
        
        public void GatherQTPData()
        {
            dtAutoData.Columns.Add("[Test_ID]", typeof(int));
            dtAutoData.Columns.Add("[Register_ID]", typeof(int));
            dtAutoData.Columns.Add("[AutoVer_ID]", typeof(int));
            dtAutoData.Columns.Add("[Scenario]", typeof(int));
            dtAutoData.Columns.Add("[Module]", typeof(string));
            dtAutoData.Columns.Add("[MetricDescription]", typeof(string));
            dtAutoData.Columns.Add("[Iteration]", typeof(int));
            dtAutoData.Columns.Add("[Duration]", typeof(decimal));
            dtAutoData.Columns.Add("[EventTime]", typeof(System.DateTime));

            var filez = Directory.GetFiles(@"c:\" + Global.StatsFileDirectory, "AutomationMetrics.csv",SearchOption.TopDirectoryOnly);
           foreach (string currentfile in filez)
           {
			   string cline = "";
               StreamReader sr = new StreamReader(currentfile);
               sr.ReadLine();
               while (!sr.EndOfStream)
               {
                   
                   DataRow newRow = dtAutoData.NewRow();
                   cline = sr.ReadLine();
                   string[] values = cline.Split(',');

                   for (int i = 0; i < values.Length; i++)
                   {
                       try
                       {
                           newRow[i] = values[i];
                           
                           
                       }
                       catch
                       {
                           newRow[i] = DBNull.Value;
                           
                           
                       }
                       
                   }
                   
                   dtAutoData.Rows.Add(newRow);
               }
               sr.Close();

               File.Move(currentfile, currentfile.Replace(".csv", "." + System.DateTime.Now.ToString("yyyyMMdd-HHmm") + ".bak"));
			dtAutoData.Columns.Add("[AutomationMetrics_ID]", typeof(int));
			dtAutoData.Columns["[AutomationMetrics_ID]"].SetOrdinal(0);

           }
        }
        
        public void fnSQLBulkInsert(DataTable dt, string SQLtable)
        {
            SqlBulkCopy bulkcopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString);
            bulkcopy.BatchSize = 1000;

            Report.Log(ReportLevel.Info, "Metric Collection", "Start Upload of " + dt.TableName + " data", new RecordItemIndex(0));
            
            bulkcopy.DestinationTableName = SQLtable;
            bulkcopy.WriteToServer(dt);
            bulkcopy.Close();
            
            Report.Log(ReportLevel.Info, "Metric Collection", "Uploaded " + dt.Rows.Count.ToString(), new RecordItemIndex(0));
            Thread.Sleep(3000);
        }

    }
}
