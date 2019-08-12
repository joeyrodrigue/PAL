/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 09/10/13
 * Time: 12:55 PM
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

// for database use
using System.Data;
using System.Data.OleDb;

namespace Alpha
{
    /// <summary>
    /// Description of fnCommonScenario22and23Prerequisite.
    /// </summary>
    [TestModule("D48C90CF-FD7F-453B-B5A3-4ACD4226B795", ModuleType.UserCode, 1)]
    public class fnDataLevelPreOrder : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnDataLevelPreOrder()
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
   	// select top 1 SKU from SKU where AllowPre= true and IsDLC=false and dept in (21, 15) and qty <> 0 and ESRBCode<>"M" order by rnd(-(100000*sku)*time())
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;        	
        	
        	RanorexRepository repo = new RanorexRepository();
        	fnWriteToLogFile WriteToLogFile = new fnWriteToLogFile();  
        	fnDumpStatsQ4 DumpStatsQ4 = new fnDumpStatsQ4();
        	
			Global.LogFileIndentLevel++;		
        	Global.LogText = "IN fFnDataLevelPreOrder";
			WriteToLogFile.Run();	 

			Global.PreOrderFailed = false;			
			
			// Generates the prerequsite for scenarios 20, 22, 23, and 25  uses Global.CurrentSKU
			
			//pre requisite start
			
			// Create new stopwatch
			Stopwatch Mystopwatch = new Stopwatch();	
			Mystopwatch.Reset();
			Mystopwatch.Start();

            String strHomePhone =  "'" + Global.NextPhoneNumber + "'";	
            
			//create connection string
            String strConnect = @"Provider=Microsoft.JET.OLEDB.4.0;"
            	+ @"data source=" + Global.Register1DriveLetter + @":\pos\presell.mdb";
            
            //prep sql statements
            String strSelectCust = "SELECT TOP 1 CustomerID FROM TBLCUSTOMER WHERE HOMEPHONE = " 
            	+ strHomePhone 
            	+ " ORDER BY CustomerId DESC";
            
            //create connection
            OleDbConnection conConnection = new OleDbConnection(strConnect);
            
            //open connection
            conConnection.Open();
            //Console.WriteLine("ServerVersion: {0} \nDataSource: {1}",conConnection.ServerVersion, conConnection.DataSource);            
            
            //create dataset
            DataSet dsSelectCust = new DataSet();
            
            //create adapter and fill the dataset
            OleDbDataAdapter adpSelectCust = new OleDbDataAdapter(strSelectCust,conConnection);
            adpSelectCust.Fill(dsSelectCust);
            
            DataTable dtSelectCust = dsSelectCust.Tables[0];
            if (dtSelectCust.Rows.Count == 0)
            {
            	// no record, insert new record
            	//Console.WriteLine("No record! Creating customer...");
            	//Console.ReadKey();
            	String strInsertCust = "INSERT INTO TBLCUSTOMER "
            		+ "(LastName,FirstName,Address1,City,State,Zip,HomePhone) " 
            		+ " values ('Asberry','Travis','Po Box 1244','Blue Hill','ME','04614', " + strHomePhone + " )";
            	
            	OleDbCommand cmdInsertCust = new OleDbCommand(strInsertCust, conConnection);
            	cmdInsertCust.ExecuteNonQuery();
            }

            //select the customer created or selected from database
            OleDbDataAdapter adpSelectCustNew = new OleDbDataAdapter(strSelectCust,conConnection);
            adpSelectCustNew.Fill(dsSelectCust);
            
            DataTable dtSelectCustNew = dsSelectCust.Tables[0];
            String strCustomerId = "'" + Convert.ToString (dtSelectCustNew.Rows[0]["CustomerId"]) + "'";
            //Console.WriteLine("CustomerId: " + strCustomerId);
            //Console.ReadKey();
            
            //Insert data in tblItems
            //Console.WriteLine("Inserting reserve item...");
            //Console.ReadKey();

            String strInsertItem = "INSERT INTO TBLITEMS "
            + "(CustomerID,SKU,Qty,Type,Status) " 
            + " values ("
            + strCustomerId
            + ","
            + Global.CurrentSKU
            + ",1,1,1)";
            	
            OleDbCommand cmdInsertItem = new OleDbCommand(strInsertItem, conConnection);
            cmdInsertItem.ExecuteNonQuery();
            
            //Select max item id from tblItems
            String strSelectItem = "SELECT TOP 1 ItemID FROM TBLITEMS ORDER BY ItemId DESC";
            
            //create dataset
            DataSet dsSelectItem = new DataSet();
            
            //create adapter and fill the dataset
            OleDbDataAdapter adpSelectItem = new OleDbDataAdapter(strSelectItem,conConnection);
            adpSelectItem.Fill(dsSelectItem);
            
            DataTable dtSelectItem = dsSelectItem.Tables[0];
            String strItemId = Convert.ToString(dtSelectItem.Rows[0]["ItemId"]);
            //Console.WriteLine("ItemId: " + strItemId);
            //Console.ReadKey();
            
            //Insert data in tblDeposits using CustomerID and ItemId from previous queries
            //Console.WriteLine("Inserting deposit...");
            //Console.ReadKey();

            String strInsertDeposit = "INSERT INTO TBLDEPOSITS "
            + "(CustomerID,DepositAmount,Status,DepositType,ItemID,OrigTenderType) " 
            + " values ("
            + strCustomerId
            + ",5,1,1,"
            + strItemId
            + ",1)";
            	
            OleDbCommand cmdInsertDeposit = new OleDbCommand(strInsertDeposit, conConnection);
            cmdInsertDeposit.ExecuteNonQuery();
            
            //Close connection
            conConnection.Close();
            
            //Console.WriteLine("Done...");

			// pre requisite end
			
			Global.Q4StatLine =  ((float) Mystopwatch.ElapsedMilliseconds / 1000).ToString("R");
            Global.CurrentMetricDesciption = @"Data Level PreOrder";
            Global.Module = "Setup";                
            DumpStatsQ4.Run();    			

            Global.LogText = "OUT fFnDataLevelPreOrder";
			WriteToLogFile.Run();	
			Global.LogFileIndentLevel--;
        }           
    }
}
