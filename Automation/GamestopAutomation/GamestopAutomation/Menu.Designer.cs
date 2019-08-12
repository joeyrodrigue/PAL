/*
 * Created by Ranorex
 * User: storeuser
 * Date: 09/18/13
 * Time: 11:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System.Windows.Forms;

namespace GamestopAutomation
{
	partial class Menu
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxTestType = new System.Windows.Forms.ComboBox();
            this.lblTestCategory = new System.Windows.Forms.Label();
            this.lblTestName = new System.Windows.Forms.Label();
            this.tbTestName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxTestType
            // 
            this.cbxTestType.FormattingEnabled = true;
            this.cbxTestType.Location = new System.Drawing.Point(91, 6);
            this.cbxTestType.Name = "cbxTestType";
            this.cbxTestType.Size = new System.Drawing.Size(153, 21);
            this.cbxTestType.TabIndex = 0;
            this.cbxTestType.SelectedIndexChanged += new System.EventHandler(this.cbxTestType_SelectedIndexChanged);
            // 
            // lblTestCategory
            // 
            this.lblTestCategory.AutoSize = true;
            this.lblTestCategory.Location = new System.Drawing.Point(12, 9);
            this.lblTestCategory.Name = "lblTestCategory";
            this.lblTestCategory.Size = new System.Drawing.Size(73, 13);
            this.lblTestCategory.TabIndex = 2;
            this.lblTestCategory.Text = "Test Category";
            // 
            // lblTestName
            // 
            this.lblTestName.AutoSize = true;
            this.lblTestName.Location = new System.Drawing.Point(15, 45);
            this.lblTestName.Name = "lblTestName";
            this.lblTestName.Size = new System.Drawing.Size(59, 13);
            this.lblTestName.TabIndex = 3;
            this.lblTestName.Text = "Test Name";
            this.lblTestName.Visible = false;
            // 
            // tbTestName
            // 
            this.tbTestName.Location = new System.Drawing.Point(91, 42);
            this.tbTestName.Name = "tbTestName";
            this.tbTestName.Size = new System.Drawing.Size(153, 20);
            this.tbTestName.TabIndex = 1;
            this.tbTestName.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(116, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbTestName);
            this.Controls.Add(this.lblTestName);
            this.Controls.Add(this.lblTestCategory);
            this.Controls.Add(this.cbxTestType);
            this.Controls.Add(this.btnSave);
            this.Name = "Menu";
            this.Text = "Test Information";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private Button btnSave;
        private ComboBox cbxTestType;
        private Label lblTestCategory;
        private Label lblTestName;
        private TextBox tbTestName;
        private Button btnCancel;
	}
}
