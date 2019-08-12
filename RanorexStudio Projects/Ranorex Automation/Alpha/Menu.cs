/*
 * Created by Ranorex
 * User: storeuser
 * Date: 09/18/13
 * Time: 11:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;


namespace Alpha
{
	/// <summary>
	/// Description of Menu.
	/// </summary>
	public partial class Menu : System.Windows.Forms.Form
	{
		string SelectedTest;
       	int SelectedTest_ID;
       	int Test_ID;
       	int AutoVer_ID;
       	DataTable AutomationIDs = new DataTable();
		XDocument xdoc = XDocument.Load("test.config");
		IEnumerable<XElement> TestParameters;
		SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MetricsRepository"].ConnectionString);		
		
		public Menu()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			string PALFolder = @"c:\PAL\Performance";
			if (!Directory.Exists(PALFolder))
                {
                	Directory.CreateDirectory(PALFolder);
                }

			TestParameters = from TestParameter in xdoc.Descendants("testParameters")
        		select TestParameter;

			DataTable TestTypes = new DataTable();
			
			SqlCommand SelectTestType = new SqlCommand("Select * from TestTypeLK",conn);
			
			try
			{
				conn.Open();
				SqlDataAdapter da = new SqlDataAdapter(SelectTestType);
				da.Fill(TestTypes);
				conn.Close();
				da.Dispose();
				cbxTestType.DataSource = TestTypes.DefaultView;
				cbxTestType.DisplayMember = "TypeName";
			}
			catch
			{
				foreach (var TestParameter in TestParameters.Descendants())
				{
	        		if (TestParameter.Name == "type")
	        		{
	        			TestParameter.Value = "standard";
	        		}
	        		if (TestParameter.Name == "test_ID")
	        		{
	        			TestParameter.Value = "0";
	        		}
				}
				xdoc.Save("test.config");
				Global.IsPerformanceTest = false;
				this.Close();
				
			}
			
		}

        public void btnSave_Click(object sender, EventArgs e)
        {
        	if (tbTestName.Text == "")
        	{
        		MessageBox.Show("You must enter a test name");
        	}
        	else
        	{
        		string AutomationVersion = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).FileVersion;
        		
        		SqlCommand SaveTest = new SqlCommand("INSERT INTO TestCase (Name,Type_ID) Values(@Name,@Type_ID); SELECT SCOPE_IDENTITY();",conn);
	        	SaveTest.Parameters.AddWithValue("@Name",tbTestName.Text);
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
	        		        	
	        	DataRowView vSelectedRow  = (DataRowView)cbxTestType.SelectedItem;
	        	DataRow SelectedRow = vSelectedRow.Row;
	        	
       	
	        	
	        	this.Close();
        	}
        }

        private void cbxTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
        	DataRowView vSelectedRow  = (DataRowView)cbxTestType.SelectedItem;
        	DataRow SelectedRow = vSelectedRow.Row;
        	SelectedTest = SelectedRow[1].ToString();
        	SelectedTest_ID = Convert.ToInt32(SelectedRow[0].ToString());
        	
            if (SelectedTest != "OS Patches" && SelectedTest != "Baseline")
            {
                lblTestName.Visible = true;
                tbTestName.Text = "";
                tbTestName.Visible = true;
                
            }
            else
            {
                lblTestName.Visible = false;
                tbTestName.Visible = false;
                
                if (SelectedTest == "OS Patches")
                {
                	System.DateTime now = System.DateTime.Now;
                	tbTestName.Text = (System.DateTime.Now.ToString("yyyy-MMMM"));
                }
                else
                {
                	tbTestName.Text = "BaseLine Collected:" + System.DateTime.Now.ToShortDateString();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {	btnQuit_Cancel_Click(sender, e);
        }
        
        private void btnQuit_Click(object sender, EventArgs e)
        {	btnQuit_Cancel_Click(sender, e);
			Environment.Exit(0);
        }    
        
        private void btnQuit_Cancel_Click(object sender, EventArgs e) 
        {
        	foreach (var TestParameter in TestParameters.Descendants())
        	{
        		if (TestParameter.Name == "type")
        		{
        			TestParameter.Value = "standard";
        		}
        		if (TestParameter.Name == "test_ID")
        		{
        			TestParameter.Value = "0";
        		}
        	}
        	xdoc.Save("test.config");
        	Global.IsPerformanceTest = false;
        	this.Close();        	
        }
        
	}
}
