using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroSwitch
{





	public partial class MSettings : Form
	{

		public Form1 form1; // Set by the property

		public MSettings()
		{
			InitializeComponent();
		}

		private void Settings_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			

		}

		private void checkbox_tabontargetdie(object sender, EventArgs e)
		{
			form1.tabken = !form1.tabken;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");

		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");

		}

		private void checkBox5_CheckedChanged(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");

		}
	}
}
