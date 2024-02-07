namespace ArchlordMacro
{
	partial class MSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox_CastTime = new System.Windows.Forms.CheckBox();
			this.text_CastTimeValue = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.BackColor = System.Drawing.Color.Transparent;
			this.checkBox1.ForeColor = System.Drawing.Color.White;
			this.checkBox1.Location = new System.Drawing.Point(36, 103);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(152, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Press TAB after target dies";
			this.checkBox1.UseVisualStyleBackColor = false;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkbox_tabontargetdie);
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.BackColor = System.Drawing.Color.Transparent;
			this.checkBox2.ForeColor = System.Drawing.Color.White;
			this.checkBox2.Location = new System.Drawing.Point(36, 126);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(77, 17);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Spam TAB";
			this.checkBox2.UseVisualStyleBackColor = false;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.BackColor = System.Drawing.Color.Transparent;
			this.checkBox3.Enabled = false;
			this.checkBox3.ForeColor = System.Drawing.Color.White;
			this.checkBox3.Location = new System.Drawing.Point(36, 316);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(144, 17);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "Close Archlord on 5% HP";
			this.checkBox3.UseVisualStyleBackColor = false;
			this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.BackColor = System.Drawing.Color.Transparent;
			this.checkBox4.Enabled = false;
			this.checkBox4.ForeColor = System.Drawing.Color.White;
			this.checkBox4.Location = new System.Drawing.Point(36, 293);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(192, 17);
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Text = "Close Archlord when closing Macro";
			this.checkBox4.UseVisualStyleBackColor = false;
			this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
			// 
			// checkBox_CastTime
			// 
			this.checkBox_CastTime.AutoSize = true;
			this.checkBox_CastTime.BackColor = System.Drawing.Color.Transparent;
			this.checkBox_CastTime.ForeColor = System.Drawing.Color.White;
			this.checkBox_CastTime.Location = new System.Drawing.Point(299, 204);
			this.checkBox_CastTime.Name = "checkBox_CastTime";
			this.checkBox_CastTime.Size = new System.Drawing.Size(122, 17);
			this.checkBox_CastTime.TabIndex = 4;
			this.checkBox_CastTime.Text = "Set Cast Time Value";
			this.checkBox_CastTime.UseVisualStyleBackColor = false;
			this.checkBox_CastTime.CheckedChanged += new System.EventHandler(this.click_SetCastTime);
			// 
			// text_CastTimeValue
			// 
			this.text_CastTimeValue.Location = new System.Drawing.Point(427, 202);
			this.text_CastTimeValue.Name = "text_CastTimeValue";
			this.text_CastTimeValue.Size = new System.Drawing.Size(78, 20);
			this.text_CastTimeValue.TabIndex = 5;
			// 
			// MSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackgroundImage = global::ArchlordMacro.Properties.Resources.zever;
			this.ClientSize = new System.Drawing.Size(552, 498);
			this.Controls.Add(this.text_CastTimeValue);
			this.Controls.Add(this.checkBox_CastTime);
			this.Controls.Add(this.checkBox4);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MSettings";
			this.Text = "Settings";
			this.Load += new System.EventHandler(this.Settings_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox_CastTime;
		private System.Windows.Forms.TextBox text_CastTimeValue;
	}
}