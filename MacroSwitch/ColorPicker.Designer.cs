namespace ArchlordMacro
{
    partial class ColorPicker
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
			this.prgColor = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtX = new System.Windows.Forms.TextBox();
			this.txtY = new System.Windows.Forms.TextBox();
			this.btnSet = new System.Windows.Forms.Button();
			this.lblColor = new System.Windows.Forms.Label();
			this.lblDebug = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.timerlen = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// prgColor
			// 
			this.prgColor.Location = new System.Drawing.Point(399, 59);
			this.prgColor.Name = "prgColor";
			this.prgColor.Size = new System.Drawing.Size(151, 270);
			this.prgColor.TabIndex = 0;
			this.prgColor.Click += new System.EventHandler(this.prgColor_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 141);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "X";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(93, 141);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(14, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y";
			// 
			// txtX
			// 
			this.txtX.Location = new System.Drawing.Point(12, 157);
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(33, 20);
			this.txtX.TabIndex = 3;
			// 
			// txtY
			// 
			this.txtY.Location = new System.Drawing.Point(83, 157);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(33, 20);
			this.txtY.TabIndex = 4;
			// 
			// btnSet
			// 
			this.btnSet.Location = new System.Drawing.Point(12, 183);
			this.btnSet.Name = "btnSet";
			this.btnSet.Size = new System.Drawing.Size(104, 23);
			this.btnSet.TabIndex = 5;
			this.btnSet.Text = "Set";
			this.btnSet.UseVisualStyleBackColor = true;
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// lblColor
			// 
			this.lblColor.AutoSize = true;
			this.lblColor.Location = new System.Drawing.Point(12, 59);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(0, 13);
			this.lblColor.TabIndex = 6;
			// 
			// lblDebug
			// 
			this.lblDebug.AutoSize = true;
			this.lblDebug.Location = new System.Drawing.Point(12, 263);
			this.lblDebug.Name = "lblDebug";
			this.lblDebug.Size = new System.Drawing.Size(0, 13);
			this.lblDebug.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(263, 316);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "label3";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(322, 316);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "label4";
			// 
			// timerlen
			// 
			this.timerlen.AutoSize = true;
			this.timerlen.Location = new System.Drawing.Point(15, 329);
			this.timerlen.Name = "timerlen";
			this.timerlen.Size = new System.Drawing.Size(47, 13);
			this.timerlen.TabIndex = 10;
			this.timerlen.Text = "timerken";
			this.timerlen.Click += new System.EventHandler(this.timerlen_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(106, 285);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "Move cursor to X and Y";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.click_search_white_pixel);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(15, 285);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 12;
			this.button2.Text = "TAB";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 5);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(12, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "x";
			this.label5.Click += new System.EventHandler(this.label5_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 34);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(12, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "y";
			this.label6.Click += new System.EventHandler(this.label6_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(106, 253);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 15;
			this.button3.Text = "Send G";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.click_tab);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(65, 12);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 16;
			this.button4.Text = "TAB";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Tab_click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(146, 12);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 17;
			this.button5.Text = "PRESS_Z";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Press_z_click);
			// 
			// ColorPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::ArchlordMacro.Properties.Resources.Screenshot_2;
			this.ClientSize = new System.Drawing.Size(592, 555);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.timerlen);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblDebug);
			this.Controls.Add(this.lblColor);
			this.Controls.Add(this.btnSet);
			this.Controls.Add(this.txtY);
			this.Controls.Add(this.txtX);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.prgColor);
			this.Name = "ColorPicker";
			this.Text = "ColorPicker";
			this.Load += new System.EventHandler(this.ColorPicker_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblDebug;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label timerlen;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
	}
}