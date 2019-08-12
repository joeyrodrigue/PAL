/*
 * Created by Ranorex
 * User: storeuser
 * Date: 04/24/14
 * Time: 6:25 AM
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

// for database use
using System.Data;
using System.Data.OleDb;

using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Configuration;
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
    /// Description of fnUpdatePALStatusMonitor.
    /// </summary>
    [TestModule("2190E68D-DC55-4D2E-B95A-2E98E8D9CD7E", ModuleType.UserCode, 1)]
    public class fnUpdatePALStatusMonitor : ITestModule
    {
		DataTable MonitorDT = new DataTable();    
		
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnUpdatePALStatusMonitor()
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
		// if PAL monitor record not exist for this specific register/test then create it - otherwise just update the record info			
			if(!Global.DBAvailable) 
				return;
			
			if(Global.RegisterRunningIPOS) CommonGetProcessInfo("POS");
			if(Global.RegisterRunningRetech) CommonGetProcessInfo("Source");			
		}

     private void CommonGetProcessInfo(string TheProcessName)
     {      	
     	fnGetPOSProcessInfo GetPOSProcessInfo = new fnGetPOSProcessInfo();
     	
     	Global.ProcessName = TheProcessName;
     	
		bool SqlReturn;     	
			
		GetPOSProcessInfo.Run();
				
			// PAL Status Monitor SQL data record
		string  dtMonitorDate,
	  		 	dtComputerName,
	  		 	dtAliveTime,
	  			dtOsVer;
		  int 	dtCurrentScenario,
		  		dtScenariosToday,
		  		dtIterationsToday,
		  		dtErrorsToday;
		  float dtCustomerLookupNow,
		  		dtEnterSKUNow,
		  		dtPayWithCashNow,
		  		dtPayWithCreditNow,
		  		dtPayWithPURCCNow;		  		
		  int 	dtPOSMemoryStart,
		 		dtPOSMemoryNow,
		  		dtPOSGDIObjectsStart,
		  		dtPOSGDIObjectsNow,
		  		dtPOSUserObjectsStart,
		  		dtPOSUserObjectsNow,
		  		dtPOSThreadCountStart,
		  		dtPOSThreadCountNow,
		  		dtPOSHandlesStart,
		  		dtPOSHandlesNow;

		  		if(!Global.MonitorStartVariablesInitialized)
	  			{
		  			Global.MonitorStartVariablesInitialized = true;
		  			
		  			Global.StartPOSMemory = Global.POSUsedMemory;
		  			Global.StartGDIObjects = Global.POSGDIObjects;
	  				Global.StartUserObjects = Global.POSUserObjects;
		  			Global.StartThreadCount = Global.POSThreads;
		  			Global.StartHandles = Global.POSHandleCount;
		  		}

		  		// Set all data in table for update
			  	dtMonitorDate = System.DateTime.Now.ToString("yyyy-MM-dd");	// Key part 1
		  		dtComputerName = Global.RegisterName; 	// Key part 2

				dtAliveTime = System.DateTime.Now.ToString("HH:mm:ss");	
		  		dtOsVer = Global.OSVersion;		
		  		dtCurrentScenario = Global.CurrentScenario;
//		  		dtScenariosToday = Global.ScenariosToday;
		  		dtScenariosToday = Global.RetechScenariosPerformed;
		  		dtIterationsToday = Global.IterationsToday;
		  		dtErrorsToday = Global.ErrorsToday;	  		
		  		
		  		dtCustomerLookupNow = Global.NowCustomerLookup;
		  		dtEnterSKUNow = Global.NowEnterSKU;
		  		dtPayWithCashNow = Global.NowPayWithCash;
		  		dtPayWithCreditNow = Global.NowPayWithCredit;
		  		dtPayWithPURCCNow = Global.NowPayWithPURCC; 			  		

				dtPOSMemoryStart = Global.StartPOSMemory;	
		  		dtPOSMemoryNow = Global.POSUsedMemory;

		  		dtPOSGDIObjectsStart = Global.StartGDIObjects;
		  		dtPOSGDIObjectsNow = Global.POSGDIObjects;
		  		
		  		dtPOSUserObjectsStart = Global.StartUserObjects;
		  		dtPOSUserObjectsNow = Global.POSUserObjects;
		  		
		  		dtPOSThreadCountStart = Global.StartThreadCount;
		  		dtPOSThreadCountNow = Global.POSThreads;
		  		
		  		dtPOSHandlesStart = Global.StartHandles;
		  		dtPOSHandlesNow = Global.POSHandleCount;


		        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString))
        		      	
		        {
		            conn.Open();
      
			        using (SqlCommand cmd =
		               new SqlCommand("SELECT * FROM MonitorRegisterAlive WHERE MonitorDate = @TheDate and ComputerName = @TheName", conn))
			        {
			            cmd.Parameters.AddWithValue("@TheDate", dtMonitorDate);
			            cmd.Parameters.AddWithValue("@TheName", dtComputerName);		            	
		            	SqlDataReader reader = cmd.ExecuteReader();
		            	SqlReturn = reader.Read();
						if(SqlReturn)
						{
							if(dtIterationsToday == 0) 
							{
								dtIterationsToday = Convert.ToInt32(reader["IterationsToday"]);
								Global.IterationsToday = dtIterationsToday;
							}
							if(dtScenariosToday == 0) 
							{
								dtScenariosToday = Convert.ToInt32(reader["ScenariosToday"]);
								Global.ScenariosToday = dtScenariosToday;
							}		  		
							if(dtErrorsToday == 0) 
							{
								dtErrorsToday = Convert.ToInt32(reader["ErrorsToday"]);
								Global.ErrorsToday = dtErrorsToday;
							}		  		
						}
			        }			            
		            conn.Close();
		            
		            if(!SqlReturn)
		            {	// No record exists so create one
		            	conn.Open();
				        using (SqlCommand cmd =
			               new SqlCommand("INSERT INTO MonitorRegisterAlive VALUES (" + 
										"@dtMonitorDate," + 
										"@dtComputerName," +
										"@dtAliveTime," +
										"@dtOsVer," +
										"@dtCurrentScenario," +
										"@dtScenariosToday," +
										"@dtIterationsToday," +
										"@dtErrorsToday," +
										"@dtCustomerLookupNow," +
										"@dtEnterSKUNow," +
										"@dtPayWithCashNow," +
										"@dtPayWithCreditNow," +
										"@dtPayWithPURCCNow," +
										"@dtPOSMemoryStart," +
										"@dtPOSMemoryNow," +
										"@dtPOSGDIObjectsStart," +
										"@dtPOSGDIObjectsNow," +
										"@dtPOSUserObjectsStart," +
										"@dtPOSUserObjectsNow," +
										"@dtPOSThreadCountStart," +
										"@dtPOSThreadCountNow," +
										"@dtPOSHandlesStart," +
										"@dtPOSHandlesNow" + 
		            	                      ")", conn))
				        {
			            	cmd.Parameters.AddWithValue("@dtMonitorDate",dtMonitorDate);
				            cmd.Parameters.AddWithValue("@dtComputerName",dtComputerName);
				            cmd.Parameters.AddWithValue("@dtAliveTime",dtAliveTime);				            
				            cmd.Parameters.AddWithValue("@dtOsVer",dtOsVer);
				            cmd.Parameters.AddWithValue("@dtCurrentScenario",dtCurrentScenario);
				            cmd.Parameters.AddWithValue("@dtScenariosToday",dtScenariosToday);
				            cmd.Parameters.AddWithValue("@dtIterationsToday",dtIterationsToday);
				            cmd.Parameters.AddWithValue("@dtErrorsToday",dtErrorsToday);
				            cmd.Parameters.AddWithValue("@dtCustomerLookupNow",dtCustomerLookupNow);
				            cmd.Parameters.AddWithValue("@dtEnterSKUNow",dtEnterSKUNow);
				            cmd.Parameters.AddWithValue("@dtPayWithCashNow",dtPayWithCashNow);
				            cmd.Parameters.AddWithValue("@dtPayWithCreditNow",dtPayWithCreditNow);
				            cmd.Parameters.AddWithValue("@dtPayWithPURCCNow",dtPayWithPURCCNow);
				            cmd.Parameters.AddWithValue("@dtPOSMemoryStart",dtPOSMemoryStart);
				            cmd.Parameters.AddWithValue("@dtPOSMemoryNow",dtPOSMemoryNow);
				            cmd.Parameters.AddWithValue("@dtPOSGDIObjectsStart",dtPOSGDIObjectsStart);
				            cmd.Parameters.AddWithValue("@dtPOSGDIObjectsNow",dtPOSGDIObjectsNow);
				            cmd.Parameters.AddWithValue("@dtPOSUserObjectsStart",dtPOSUserObjectsStart);
				            cmd.Parameters.AddWithValue("@dtPOSUserObjectsNow",dtPOSUserObjectsNow);
				            cmd.Parameters.AddWithValue("@dtPOSThreadCountStart",dtPOSThreadCountStart);
				            cmd.Parameters.AddWithValue("@dtPOSThreadCountNow",dtPOSThreadCountNow);
				            cmd.Parameters.AddWithValue("@dtPOSHandlesStart",dtPOSHandlesStart);
				            cmd.Parameters.AddWithValue("@dtPOSHandlesNow",dtPOSHandlesNow);				            
			            	SqlDataReader reader = cmd.ExecuteReader();
			            	SqlReturn = reader.Read();
				        }
		            	conn.Close();
		            }
		            else
		            {	// Record exist so just update existing record
		            	conn.Open();
					    using (SqlCommand cmd =
				           new SqlCommand("UPDATE MonitorRegisterAlive SET " + 
											"AliveTime = @dtAliveTime," +		            	                      
											"CurrentScenario = @dtCurrentScenario," +
											"ScenariosToday = @dtScenariosToday," +
											"IterationsToday = @dtIterationsToday," +
											"ErrorsToday = @dtErrorsToday," +
											"CustomerLookupNow = @dtCustomerLookupNow," +
											"EnterSKUNow = @dtEnterSKUNow," +
											"PayWithCashNow = @dtPayWithCashNow," +
											"PayWithCreditNow = @dtPayWithCreditNow," +
											"PayWithPURCCNow = @dtPayWithPURCCNow," +
											"POSMemoryNow = @dtPOSMemoryNow," +
											"POSGDIObjectsNow = @dtPOSGDIObjectsNow," +
											"POSUserObjectsNow = @dtPOSUserObjectsNow," +
											"POSThreadCountNow = @dtPOSThreadCountNow," +
											"POSHandlesNow = @dtPOSHandlesNow " + 
											"WHERE MonitorDate = @dtMonitorDate and ComputerName = @dtComputerName", conn))
					        {
				            	cmd.Parameters.AddWithValue("@dtMonitorDate",dtMonitorDate);
					            cmd.Parameters.AddWithValue("@dtComputerName",dtComputerName);
					            cmd.Parameters.AddWithValue("@dtAliveTime",dtAliveTime);					            
					            cmd.Parameters.AddWithValue("@dtCurrentScenario",dtCurrentScenario);
					            cmd.Parameters.AddWithValue("@dtScenariosToday",dtScenariosToday);
					            cmd.Parameters.AddWithValue("@dtIterationsToday",dtIterationsToday);
					            cmd.Parameters.AddWithValue("@dtErrorsToday",dtErrorsToday);
					            cmd.Parameters.AddWithValue("@dtCustomerLookupNow",dtCustomerLookupNow);
					            cmd.Parameters.AddWithValue("@dtEnterSKUNow",dtEnterSKUNow);
					            cmd.Parameters.AddWithValue("@dtPayWithCashNow",dtPayWithCashNow);
					            cmd.Parameters.AddWithValue("@dtPayWithCreditNow",dtPayWithCreditNow);
					            cmd.Parameters.AddWithValue("@dtPayWithPURCCNow",dtPayWithPURCCNow);
					            cmd.Parameters.AddWithValue("@dtPOSMemoryStart",dtPOSMemoryStart);
					            cmd.Parameters.AddWithValue("@dtPOSMemoryNow",dtPOSMemoryNow);
					            cmd.Parameters.AddWithValue("@dtPOSGDIObjectsStart",dtPOSGDIObjectsStart);
					            cmd.Parameters.AddWithValue("@dtPOSGDIObjectsNow",dtPOSGDIObjectsNow);
					            cmd.Parameters.AddWithValue("@dtPOSUserObjectsStart",dtPOSUserObjectsStart);
					            cmd.Parameters.AddWithValue("@dtPOSUserObjectsNow",dtPOSUserObjectsNow);
					            cmd.Parameters.AddWithValue("@dtPOSThreadCountStart",dtPOSThreadCountStart);
					            cmd.Parameters.AddWithValue("@dtPOSThreadCountNow",dtPOSThreadCountNow);
					            cmd.Parameters.AddWithValue("@dtPOSHandlesStart",dtPOSHandlesStart);
					            cmd.Parameters.AddWithValue("@dtPOSHandlesNow",dtPOSHandlesNow);				            
				            	SqlDataReader reader = cmd.ExecuteReader();
				            	SqlReturn = reader.Read();
					        }		            	
		        		conn.Close();				            	
		            }
		        }
		}
		

		
		
		
		
		
		
		
		
		
		
		
		
		
    }
}
