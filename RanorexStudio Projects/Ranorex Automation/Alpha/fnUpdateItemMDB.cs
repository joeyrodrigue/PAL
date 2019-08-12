/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 9:24 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

using System.Data.OleDb;

namespace Alpha
{
    /// <summary>
    /// Description of fnUpdateItemMDB.
    /// </summary>
    [TestModule("BBA78117-9FBB-4D85-8CF2-1F56B5D46D7B", ModuleType.UserCode, 1)]
    public class fnUpdateItemMDB : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public fnUpdateItemMDB()
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

        	
        	// Sample call
			//	fnUpdateItemMDB UpdateItemMDB = new fnUpdateItemMDB();	
			//	Global.SQLCommand = "update sku set EnforceStreetDate = false where EnforceStreetDate = true";
			//	UpdateItemMDB.Run();
	
			string myConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
			                           "Data Source=" + Global.Register1DriveLetter + ":/pos/item.mdb;" +                                    
			                           "Persist Security Info=True;" +
			                           "Jet OLEDB:Database Password=myPassword;";
			try
			{
			    // Open OleDb Connection
			    OleDbConnection myConnection = new OleDbConnection();
			    myConnection.ConnectionString = myConnectionString;
			    myConnection.Open();
			
			    // Execute Queries
			    OleDbCommand cmd = myConnection.CreateCommand();
			    cmd.CommandText = Global.SQLCommand;
			    OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // close conn after complete
			
			    // Load the result into a DataTable
			    //DataTable myDataTable = new DataTable();
			    //myDataTable.Load(reader);
			}
			catch (Exception ex)
			{
			    Console.WriteLine("OLEDB Connection FAILED: " + ex.Message);
			}

        }        
    }
}
