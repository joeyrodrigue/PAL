/*
 * Created by Ranorex
 * User: storeuser
 * Date: 7/16/2013
 * Time: 11:08 AM
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
    /// Description of SystemInfo.
    /// </summary>
    [TestModule("F91F8CF5-0A70-497B-AF50-299EEA924147", ModuleType.UserCode, 1)]
    public class FnSystemInfo : ITestModule
    {
    	string  POSArch,
                POSVer,
                BootupState,
                Model,
                ComputerName,
                Workgroup,
                OSVersion,
                IPAddress,
                SerialNum,
                BIOSver,
                StoreServerVer,
                KioskVer,
                Terminal,
                MemoryPart,
                MemorySpeed,
                HDDModel,
                ProcName,
               	BuildDate,
               	BuildVer,
               	PinPad,
               	ReceiptPrinter,
               	Hash;
        
        int     NumOfLogicalProc,
                TotalPhysicalMemory,
                MemoryModuleCount,
                RegisterID,
                StoreID;
        
        bool	StoreServerStatus;

				
        double  PhysicalMemory;
        
		System.DateTime InstalledDate;
		
		DataTable SystemDT = new DataTable();
        
        public string RetechEXE = "c:\\Program Files\\GameStop\\Source\\Source.exe",
			   RetechDisableFLG = "c:\\DisableRetech.flg",
               IPOSExe = "c:\\POS\\pos.exe",
               StoreServerExe = "c:\\posserver\\GameStop.Store.Server.Service.exe",
               KioskExe = "c:\\kioskapps\\Kiosk\\EdenKiosk.exe",
               GameStopRegKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\GameStop\\Build";
 
    	
        public FnSystemInfo()
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
            
            Report.Log(ReportLevel.Info, "FnSystemInfo", "Getting System Information: ", new RecordItemIndex(0));	       
            
			GatherSystemData();
			
			if (Global.IsMaster)
			{
				if(Global.AutoRun)
            	{
					PerformanceRunSetupWithoutMenu();
				}
				else
				{
					System.Windows.Forms.Form test = new Menu();
	            	test.ShowDialog();					
				}
			}
			
			XDocument xdoc = XDocument.Load("test.config");
			IEnumerable<XElement> TestParameters = TestParameters = from TestParameter in xdoc.Descendants("testParameters")
        		select TestParameter;
			foreach (var TestParameter in TestParameters.Descendants())
			{
	        		if (TestParameter.Name == "type")
	        		{
	        			Global.TestType = TestParameter.Value;
	        			if (Global.TestType == "performance")
	        			{
	        				Global.IsPerformanceTest = true;
	        			}
	        			else
	        			{
	        				Global.IsPerformanceTest = false;
	        			}
	        		}
	        		if (TestParameter.Name == "test_ID")
	        		{
	        			Global.Test_ID = Convert.ToInt32(TestParameter.Value);
	        		}
	        		if (TestParameter.Name == "autoVer_ID")
	        		{
	        			Global.AutoVer_ID = Convert.ToInt32(TestParameter.Value);
	        		}
			}
			
			if(Global.IsPerformanceTest)
			{
				initSystemDT();
	            
	            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString);			
	            
	            SQLStore(conn);
	           
				SystemDT.Rows.Add(RegisterID,
	            	StoreID,
	            	Global.RegisterNumber,
	            	ComputerName,
					Model,
					SerialNum,
					BIOSver,
					ProcName,
					NumOfLogicalProc,
					TotalPhysicalMemory,
					MemoryPart,
					MemorySpeed,
					HDDModel,
					OSVersion,
					InstalledDate,
					BuildVer,
					IPAddress,
					POSArch,
					POSVer,
					StoreServerVer,
					StoreServerStatus,
					KioskVer,
					PinPad,
					ReceiptPrinter);
	            
				// Serialize the table
				DataContractSerializer serializer = new DataContractSerializer(typeof(DataTable));
				MemoryStream memoryStream = new MemoryStream();
				XmlWriter writer = XmlDictionaryWriter.CreateBinaryWriter(memoryStream);
				serializer.WriteObject(memoryStream, SystemDT);
				byte[] serializedData = memoryStream.ToArray();
				
				// Calculte the serialized data's hash value
				SHA1CryptoServiceProvider SHA = new SHA1CryptoServiceProvider();
				byte[] sha = SHA.ComputeHash(serializedData);
				
				// Convert the hash to a base 64 string
				Hash = Convert.ToBase64String(sha);
	            
	            
	            //Hash = result.ToString();
	            
	            SQLRegister(conn);
	            
	          	Process[] processes = Process.GetProcessesByName("wscript");
	          	if (processes.Length == 0)
	          	{
	          		if(Global.Windows10)
	          			Host.Local.RunApplication("C:\\PAL\\Tools\\benchqa10.vbs", "", "", false);
	          		else
	          			Host.Local.RunApplication("C:\\PAL\\Tools\\benchqa.vbs", "", "", false);
	          	}
            }
        }
        
        public void PerformanceRunSetupWithoutMenu()
        {
	       	int SelectedTest_ID = 4;
	       	int Test_ID;
	       	int AutoVer_ID;
	       	DataTable AutomationIDs = new DataTable();
			XDocument xdoc = XDocument.Load("test.config");
			IEnumerable<XElement> TestParameters;
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString);			
			
			TestParameters = from TestParameter in xdoc.Descendants("testParameters")
        		select TestParameter;

			DataTable TestTypes = new DataTable();    	
        	
        	string tbTestName = "BaseLine Collected:" + System.DateTime.Now.ToShortDateString();
        	
        	string AutomationVersion = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).FileVersion;
        		
        	SqlCommand SaveTest = new SqlCommand("INSERT INTO TestCase (Name,Type_ID) Values(@Name,@Type_ID); SELECT SCOPE_IDENTITY();",conn);
	        SaveTest.Parameters.AddWithValue("@Name",tbTestName);
	        SaveTest.Parameters.AddWithValue("@Type_ID",SelectedTest_ID);
	        	
	        SqlCommand AutoVersion = new SqlCommand("SELECT AutoVer_ID FROM AutoVersion WHERE AutomationVersion = @FileVersion",conn);
	        AutoVersion.Parameters.AddWithValue("@FileVersion",AutomationVersion);
	        	
	        conn.Open();
	        Test_ID = Convert.ToInt32(SaveTest.ExecuteScalar());
	        SqlDataAdapter AutoDA = new SqlDataAdapter(AutoVersion);
	        AutoDA.Fill(AutomationIDs);
	        if (AutomationIDs.Rows.Count == 0)
	        {
	        	SqlCommand AddAutoVersion = new SqlCommand("INSERT INTO AutoVersion (AutomationVersion) VALUES(@FileVersion); SELECT SCOPE_IDENTITY();",conn);
	        	AddAutoVersion.Parameters.AddWithValue("@FileVersion",AutomationVersion);
	        	AutoVer_ID = Convert.ToInt32(AddAutoVersion.ExecuteScalar());
	        }
	        else
	        {
	        	AutoVer_ID = Convert.ToInt32(AutomationIDs.Rows[0][0]);
	        }
	        conn.Close();

	        foreach (var TestParameter in TestParameters.Descendants())
	        {
	        	if (TestParameter.Name == "type")
	        	{
	        		TestParameter.Value = "performance";
	        	}
	        	if (TestParameter.Name == "test_ID")
	        	{
	        		TestParameter.Value = Test_ID.ToString();
	        	}
	        	if (TestParameter.Name == "autoVer_ID")
	        	{
	        		TestParameter.Value = AutoVer_ID.ToString();
	        	}
	        }
	        	
	        xdoc.Save("test.config");
        }
        
        
        public void initSystemDT()
        {
        	SystemDT.TableName = "Register";
        	SystemDT.Columns.Add("[Register_ID]", typeof(int));
        	SystemDT.Columns.Add("[Store_ID]", typeof(int));
        	SystemDT.Columns.Add("[RegisterNumber]", typeof(string));
			SystemDT.Columns.Add("[ComputerName]", typeof(string));
			SystemDT.Columns.Add("[ComputerModel]", typeof(string));
			SystemDT.Columns.Add("[SerialNum]", typeof(string));
			SystemDT.Columns.Add("[BiosVer]", typeof(string));
			SystemDT.Columns.Add("[ProcessorName]", typeof(string));
			SystemDT.Columns.Add("[LocicalProcessors]", typeof(string));
			SystemDT.Columns.Add("[Memory]", typeof(int));
			SystemDT.Columns.Add("[MemoryPart]", typeof(string));
			SystemDT.Columns.Add("[MemorySpeed]", typeof(string));
			SystemDT.Columns.Add("[HdModel]", typeof(string));
			SystemDT.Columns.Add("[OsVer]", typeof(string));
			SystemDT.Columns.Add("[BuildDate]", typeof(System.DateTime));
			SystemDT.Columns.Add("[BuildVer]", typeof(string));
			SystemDT.Columns.Add("[IPAddress]", typeof(string));
			SystemDT.Columns.Add("[PosArch]", typeof(string));
			SystemDT.Columns.Add("[POSVer]", typeof(string));
			SystemDT.Columns.Add("[StoreServerVer]", typeof(string));
			SystemDT.Columns.Add("[StoreServerStatus]", typeof(bool));
			SystemDT.Columns.Add("[KioskVer]", typeof(string));
			SystemDT.Columns.Add("[PinPad]", typeof(string));
			SystemDT.Columns.Add("[ReceiptPrinter]", typeof(string));
			SystemDT.Columns.Add("[Hash]",typeof(string));
			

			
//			DataRow row = SystemDT.Rows[0];
//			foreach (object item in row.ItemArray)
//			{
//				MessageBox.Show(item.ToString() +", "+ item.GetType());
//			}
        }
             
        public void SQLRegister(SqlConnection conn)
        {
        	DataTable RegisterData = new DataTable();
        	
        	SqlCommand SelectRegister = new SqlCommand("Select * From Register Where Hash = @Hash",conn);
        	SelectRegister.Parameters.AddWithValue("@Hash", Hash);

        	try
			{
				conn.Open();
				SqlDataAdapter RegisterSQL = new SqlDataAdapter(SelectRegister);
				RegisterSQL.Fill(RegisterData);
				conn.Close();

				
				if (RegisterData.Rows.Count != 0)
				{
					RegisterID = Convert.ToInt32(RegisterData.Rows[0][0]);
					//MessageBox.Show("Register " + RegisterID.ToString());
				}
				else
				{
					SystemDT.Rows[0][24] = Hash;

					SqlBulkCopy RegisterInsert = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString);
					RegisterInsert.DestinationTableName = "Register";
					RegisterInsert.WriteToServer(SystemDT);
					RegisterInsert.Close();
					
					conn.Open();
					RegisterSQL.Fill(RegisterData);
					conn.Close();
					RegisterID = Convert.ToInt32(RegisterData.Rows[0][0]);
					//MessageBox.Show("added Register " + StoreID.ToString());
				}
				RegisterSQL.Dispose();
				Global.Register_ID = RegisterID;
        	}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}

        	
        }
        
        public void SQLStore(SqlConnection conn)
        {
        	DataTable StoreData = new DataTable();

        	SqlCommand SelectStore = new SqlCommand("Select * From Store Where StoreNumber = @Store and RegisterCount = @Registers",conn);
        	SelectStore.Parameters.AddWithValue("@Store", Workgroup);
        	SelectStore.Parameters.AddWithValue("@Registers", Global.NumberOfRegisters);
        	
        	try
			{
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(SelectStore);
				da.Fill(StoreData);
				conn.Close();

				
				if (StoreData.Rows.Count != 0)
				{
					StoreID = Convert.ToInt32(StoreData.Rows[0][0]);
				}
				else
				{
					SqlCommand AddStore = new SqlCommand("Insert Into Store (StoreNumber,RegisterCount) Values (@Store,@Registers)",conn);
					AddStore.Parameters.AddWithValue("@Store",Workgroup);
					AddStore.Parameters.AddWithValue("@Registers",Global.NumberOfRegisters);
					conn.Open();
					AddStore.ExecuteNonQuery();
					SqlDataAdapter ad = new SqlDataAdapter(SelectStore);
					ad.Fill(StoreData);
					ad.Dispose();
					conn.Close();
					StoreID = Convert.ToInt32(StoreData.Rows[0][0]);
					MessageBox.Show("added store " + StoreID.ToString());
				}
				da.Dispose();
				
			}
			catch(Exception e)
			{
				MessageBox.Show(e.ToString());
			}
        	
        }
        
        public void GatherSystemData()
        {            
            
            IniFile RegisterIniFile = new IniFile(Global.RegisterIni);
            
            //Set variables available using Environment.
            OSVersion = Environment.OSVersion.ToString();
            NumOfLogicalProc = Convert.ToInt32(Environment.ProcessorCount);
            ComputerName = Environment.MachineName.ToString();
            
            if (OSVersion == "Microsoft Windows NT 6.2.9200.0")
            {
            	Global.Windows10 = true;
            	GameStopRegKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\GameStop\\Build";
            }
            
            //Read Build Date and Build Version from Registry
            try
            {
            	BuildDate = Registry.GetValue(GameStopRegKey,"CREATED","N/A").ToString();
            	BuildVer = Registry.GetValue(GameStopRegKey,"BuildVersion","N/A").ToString();
            }
            catch{}
            
            
            // Set POS related variables
            if (File.Exists(RetechEXE))
            {
                 POSArch = "ReTech";
                 FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(RetechEXE);
                 POSVer = myFileVersionInfo.FileVersion.ToString();
                 PinPad = "Verifone MX";
                 ReceiptPrinter = "Epson Thermal";
            }
            else
            {
                POSArch = "IPOS";
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(IPOSExe);
                POSVer = myFileVersionInfo.ProductName.ToString();

                string PinPadType = RegisterIniFile.IniReadValue("DeviceType","PinPad");
                switch (PinPadType)
                {
                	case "0":{ PinPad = "None";break;}
                 	case "1":{ PinPad = "Verifone";break;}
                 	case "5":{ PinPad = "Ingenico eN1000";break;}
                 	case "6":{ PinPad = "Ingenico i6550";break;}
                 	case "8":{ PinPad = "Verifone MX";break;}
                 	default: {break;}
                }
                string ReceiptPrinterType = RegisterIniFile.IniReadValue("DeviceType","ReceiptPrinter");
                switch (ReceiptPrinterType)
                {
                	case "0":{ ReceiptPrinter = "None";break;}
                 	case "1":{ ReceiptPrinter = "Epson Impact";break;}
                 	case "2":{ ReceiptPrinter = "Star Thermal";break;}
                 	case "3":{ ReceiptPrinter = "Epson Thermal";break;}
                 	default: {break;}
                }
            }
            
            if (File.Exists(StoreServerExe))
            {
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(StoreServerExe);
                StoreServerVer = myFileVersionInfo.FileVersion.ToString();
                
                Process [] pname = Process.GetProcessesByName("GameStop.Store.Server.Service.exe");
                if (pname.Length == 0)
                {
                	StoreServerStatus = false;
                }
                else
                {
                	StoreServerStatus = true;
                }
            }

            if (File.Exists(KioskExe))
            {
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(KioskExe);
                KioskVer = myFileVersionInfo.FileVersion.ToString();
                Terminal = ComputerName.Substring(ComputerName.Length - 3);
            }

            //Set variables using WIM query
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_ComputerSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    BootupState = queryObj["BootupState"].ToString();
                    //Manufacturer = queryObj["Manufacturer"].ToString();
                    Model = queryObj["Model"].ToString();
                    Workgroup = queryObj["Domain"].ToString();
                    TotalPhysicalMemory = Convert.ToInt32(queryObj["TotalPhysicalMemory"].ToString().Remove((queryObj["TotalPhysicalMemory"].ToString().Length) - 3, 3));
                    PhysicalMemory = (Double)TotalPhysicalMemory / 1073741;
                    TotalPhysicalMemory = Convert.ToInt32(Math.Round(PhysicalMemory));
                }
            }
            catch { }

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "Select IPAddress from Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'True'");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["IPAddress"] != null)
                    {
                        String[] arrIPAddress = (String[])(queryObj["IPAddress"]);
                        Array.Sort(arrIPAddress);
                        foreach (String arrValue in arrIPAddress)
                        {
                            if (!arrValue.Contains(":"))
                            {
                                if (IPAddress == null)
                                {
                                    IPAddress = arrValue.ToString();
                                }
                                else 
                                { 
                                    IPAddress = IPAddress + ";" + arrValue.ToString(); 
                                }
                            }
                        }
                    }
                }
           }
           catch { }

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_BIOS");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    SerialNum = queryObj["SerialNumber"].ToString();
                    BIOSver = queryObj["Name"].ToString();
                }
            }
            catch  { }

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    InstalledDate = ManagementDateTimeConverter.ToDateTime(queryObj["InstallDate"].ToString());
                }
            }
            catch { }

            try
            {
                ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_DiskDrive WHERE MediaType = 'Fixed hard disk media'");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (HDDModel == null)
                    {
                        HDDModel = queryObj["Model"].ToString();
                    }
                    else 
                    {
                        HDDModel = HDDModel + ";" + queryObj["Model"].ToString();
                    }
                }
            }
            catch { }

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    ProcName = queryObj["Name"].ToString();
                }
            }
            catch { }

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    MemoryModuleCount = MemoryModuleCount + 1;
                    if (MemoryPart == null)
                    {
                        MemoryPart = queryObj["PartNumber"].ToString();
                    }

                    if (MemoryPart != queryObj["PartNumber"].ToString())
                    {
                        MemoryPart = MemoryPart + ";" + queryObj["PartNumber"].ToString();
                    }

                    if (MemorySpeed == null)
                    {
                        MemorySpeed = queryObj["Speed"].ToString();
                    }
                    if (MemorySpeed != queryObj["Speed"].ToString())
                    {
                        MemorySpeed = MemorySpeed + ";" + queryObj["Speed"].ToString();
                    }
                    
                }
            }
            catch { }

        }
    }
}
