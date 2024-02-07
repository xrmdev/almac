using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchlordMacro
{





	public partial class MSettings : Form
	{

		public Form1 form1; // Set by the property
		public IntPtr alWindow;

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

		private void Click_PressTabEverySec(object sender, EventArgs e)
		{
			form1.tabkenEverySec = !form1.tabkenEverySec;
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");

		}

		private void checkBox4_CheckedChanged(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");

		}

		private void Click_SetCastTime(object sender, EventArgs e)
		{
			// do nothing when checkbox is false
			if (checkBox_CastTime.Checked == false)
			{
				return;
			}

			//check for alWindow
			if (alWindow == IntPtr.Zero)
			{
				MessageBox.Show("Archlord window is not focused yet. Activate macro first.");
				checkBox_CastTime.Checked = false;
				return;
			}

			var text_cast = text_CastTimeValue.Text;
			int casttime;

			// validate empty cast time
			if (string.IsNullOrEmpty(text_cast))
			{

				MessageBox.Show("Cast Time not set");
				checkBox_CastTime.Checked = false;
				return;
			}


			if (!int.TryParse(text_cast, out casttime))
			{
				MessageBox.Show("Cast Time is not a number");
				checkBox_CastTime.Checked = false;
				return;
			}


			int[] offsets = { 0xBD0, 0x4, 0x10, 0x3F8, 0x2C, 0xC };


			this.SetArchlordMemory(0x007E72F8, offsets, casttime);



		}

		private void SetArchlordMemory(Int32 pointer, int[] offsets, Int32 value)
		{
			var process = "alefclient";
			var processes = Process.GetProcessesByName(process).Where(x => x.MainWindowHandle == alWindow).ToList();
			if (!processes.Any()) { return; }
			Process gameProcess = processes.FirstOrDefault();
			IntPtr baseAddress = gameProcess.MainModule.BaseAddress + pointer;
			
			var VAM = new VAMemory(process);

			for (int offset = 0; offset < offsets.Length; offset++)
			{
				// we add the offsets here one by one to the base address to get the target address on which your value is stored
				baseAddress = IntPtr.Add((IntPtr)VAM.ReadInt32(baseAddress), offsets[offset]);
			}

			VAM.WriteInt32(baseAddress, value);
		}



	}
}
