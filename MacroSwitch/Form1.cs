namespace MacroSwitch
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Drawing;
	using ExtensionMethods;
	using System.Threading;
	using System.Diagnostics;
	using System.Windows.Forms;
	using System.Threading.Tasks;
	using System.Xml.Serialization;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

	public partial class Form1 : Form
	{
		[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
		static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

		[DllImport("user32.dll")]
		static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		//730 1036
		int baseX = 730;
		int YHeight = 1036;

		List<int> row1Indexes = new List<int>();
		List<int> row2Indexes = new List<int>();
		List<int> row1XOffsets = new List<int>();
		List<int> row1YOffsets = new List<int>();
		List<int> row2XOffsets = new List<int>();
		List<int> row2YOffsets = new List<int>();

		public IntPtr targetProcess = IntPtr.Zero;
		public bool tabken = false;

		int MacroProgress;
		bool bRepeat = false;
		int CurrentBar;
		System.Windows.Forms.Timer targetTimer = new System.Windows.Forms.Timer();


		List<Color> row1PixelList = new List<Color>();
		List<Color> row2PixelList = new List<Color>();
		bool bPrepared = false;
		bool init = false;

		ColorPicker picker;
		SwitchTool switcher;
		MSettings mSettings;
		SaveState state;

		public Form1()
		{
			InitializeComponent();
			SetWindowTitle();

			state = new SaveState();

			int FirstHotkeyId = 1;
			int FirstHotKeyKey = (int)Keys.F2;
			Boolean F2Registered = RegisterHotKey(
				this.Handle, FirstHotkeyId, 0x0000, FirstHotKeyKey
			);

			if (!F2Registered)
			{
				MessageBox.Show("Global Hotkey '[F2]' couldn't be registered !");
			}
		}

		private void doTabken()
		{
			this.tabken = !this.tabken;

		}

		//protected override void OnPaint(PaintEventArgs e)
		//{
		//	ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.White, ButtonBorderStyle.None);
		//}
		 private const int WM_NCHITTEST = 0x84;
     private const int HTCLIENT = 0x1;
     private const int HTCAPTION = 0x2;

     ///
     /// Handling the window messages 
     ///
   
		private protected string txtXCoord_Text = "0";
		private protected string txtYCoord_Text = "0";
		private protected string txtDelay_Text = "1";
		private protected string txtStartBar_Text = "0";
		private protected int Row1BarNum_Value = 1;
		private protected int Row2BarNum_Value = 2;
		private protected bool cbNewBar_Checked = true;
		private protected bool cbSwitch_Checked = false;

		private void Form1_Load(object sender, EventArgs e)
		{


			target_progressbar.Value = 0;
			target_progressbar.Maximum = 100;
			target_progressbar.Minimum = 0;

			if (File.Exists("config.xml"))
			{
				loadConfig();
			}

			baseX = 0;
			YHeight = 0;

			Row1Skill1.Checked = state.Row1 != null && state.Row1.FirstOrDefault();
			Row1Skill2.Checked = state.Row1 != null && state.Row1[1];
			Row1Skill3.Checked = state.Row1 != null && state.Row1[2];
			Row1Skill4.Checked = state.Row1 != null && state.Row1[3];
			Row1Skill5.Checked = state.Row1 != null && state.Row1[4];
			Row1Skill6.Checked = state.Row1 != null && state.Row1[5];
			Row1Skill7.Checked = state.Row1 != null && state.Row1[6];
			Row1Skill8.Checked = state.Row1 != null && state.Row1[7];
			Row1Skill9.Checked = state.Row1 != null && state.Row1[8];
			Row1Skill10.Checked = state.Row1 != null && state.Row1[9];
			Row2Skill1.Checked = state.Row1 != null && state.Row2.FirstOrDefault();
			Row2Skill2.Checked = state.Row1 != null && state.Row2[1];
			Row2Skill3.Checked = state.Row1 != null && state.Row2[2];
			Row2Skill4.Checked = state.Row1 != null && state.Row2[3];
			Row2Skill5.Checked = state.Row1 != null && state.Row2[4];
			Row2Skill6.Checked = state.Row1 != null && state.Row2[5];
			Row2Skill7.Checked = state.Row1 != null && state.Row2[6];
			Row2Skill8.Checked = state.Row1 != null && state.Row2[7];
			Row2Skill9.Checked = state.Row1 != null && state.Row2[8];
			Row2Skill10.Checked = state.Row1 != null && state.Row2[9];


			this.LoadAdditional();
		}
		private void loadConfig()
		{
			XmlSerializer ser = new XmlSerializer(typeof(SaveState));
			using (FileStream fs = File.OpenRead("config.xml"))
			{
				state = (SaveState)ser.Deserialize(fs);
			}
		}

		private void LoadAdditional()
		{
			//this.Cursor = new Cursor(GetType(), "cursor_m1.cur");

			//this.Cursor = new Cursor("cursor_m1.cur");

			Task.Run(() => Task_CheckIfArchlordIsRunningOnStart());
		}

		private async Task Task_CheckIfArchlordIsRunningOnStart()
		{
			var counter = 1;

			var archlordRunning = Process.GetProcessesByName("alefclient").ToList().Count == 0;

			while (archlordRunning)
			{

				LogBox.InvokeIfRequired(() =>
				{
					LogBox.Text = "Archlord is not running.\n Can not find process 'alefclient.exe'";
				});
				Thread.Sleep(1000);
				counter++;
				archlordRunning = Process.GetProcessesByName("alefclient").ToList().Count == 0;
			}

			LogBox.InvokeIfRequired(() =>
			{
				LogBox.Text = "Archlord running. Focus your window and sta F2";
			});
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			writeConfig();
		}

		private void writeConfig()
		{
			using (StreamWriter sw = new StreamWriter("config.xml"))
			{
				state.Row1 = new List<bool>() { Row1Skill1.Checked, Row1Skill2.Checked, Row1Skill3.Checked, Row1Skill4.Checked, Row1Skill5.Checked, Row1Skill6.Checked, Row1Skill7.Checked, Row1Skill8.Checked, Row1Skill9.Checked, Row1Skill10.Checked };
				state.Row2 = new List<bool>() { Row2Skill1.Checked, Row2Skill2.Checked, Row2Skill3.Checked, Row2Skill4.Checked, Row2Skill5.Checked, Row2Skill6.Checked, Row2Skill7.Checked, Row2Skill8.Checked, Row2Skill9.Checked, Row2Skill10.Checked };
				var ser = new XmlSerializer(typeof(SaveState));
				ser.Serialize(sw, state);
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x0312)
			{
				int id = m.WParam.ToInt32();
				switch (id)
				{
					case 1:
						if (bRepeat)
						{

							LogBox.Text = "OFF";
							status_bar.BackColor = Color.Red;
							bRepeat = false;
							MacroProgress = 999;
							if (!cbNewBar_Checked)
							{
								while (CurrentBar != Convert.ToInt32(txtStartBar_Text))
								{
									MacroHelper.SendBarUp(targetProcess);
									IncrementCurrBar();
								}
							}
							//if (cbSwitch_Checked) switcher?.MakeSwitch();
						}
						else
						{
							LogBox.Text = "ON";
							if (!init)
							{
								init = true;
								targetTimer.Interval = 1000;
								targetTimer.Tick += Timer_Tick1;
								targetTimer.Start();
							}

							Macro();
						}
						break;
				}
				//Thread.Sleep(200);
			}

			base.WndProc(ref m);

		}

		private void Timer_Tick1(object sender, EventArgs e)
		{
			int Width = MacroHelper.GetCoords1(targetProcess).Width;
			int Middle = MacroHelper.GetCoords1(targetProcess).Width / 2;
			int height = 23;
			int lowest = Middle - 107;
			int highest = Middle + 71;

			var lowest_color = MacroHelper.GetPixelColor(targetProcess, lowest, height);
			var isTargetSelected = lowest_color.A == 255 && lowest_color.R > 220;

			var highest_color = MacroHelper.GetPixelColor(targetProcess, highest, height);

			var isTargetMaxHP = highest_color.A == 255 && highest_color.R > 220;

			var lowestPlusOne_color = MacroHelper.GetPixelColor(targetProcess, lowest + 1, height);
			var TargetIsAlive = isTargetSelected && lowestPlusOne_color.A == 255 && lowestPlusOne_color.R > 220; ;
			target_percentage.Text = tabken.ToString();
			if (isTargetMaxHP)
			{
				target_progressbar.Value = 100;
				target_percentage.Text = $"{100} %";

				return;
			}

			if (!TargetIsAlive)
			{
				target_progressbar.Value = 0;
				target_percentage.Text = $"{0} %";
				if (this.tabken)
				{
					GlobalHelpers.PressTab(targetProcess);

				}


			}

			for (int i = 1; i <= 20; i++)
			{

				var minus = i * 9;
				if (i == 19) { minus--; }
				if (i == 20) { minus -= 2; }
				var color = MacroHelper.GetPixelColor(targetProcess, highest - minus, height);
				var isredish = color.A == 255 && color.R > 220;
				if (isredish)
				{
					var xxx = (20 - i) * 5;
					target_progressbar.Value = (20 - i) * 5;
					target_percentage.Text = $"{xxx} %";

					return;
				}
			}

			//var highest_color_95_color = MacroHelper.GetPixelColor(targetProcess, highest - 18, height);
			//var highest_color_95 = highest_color_95_color.A == 255 && highest_color_95_color.R > 220;
			//if (highest_color_95)
			//{
			//	target_progressbar.Value = 95;
			//	return;
			//}
			//var highest_color_90_color = MacroHelper.GetPixelColor(targetProcess, highest - 36, height);
			//var highest_color_90 = highest_color_90_color.A == 255 && highest_color_90_color.R > 220;
			//if (highest_color_90)
			//{
			//	target_progressbar.Value = 90;
			//	return;
			//}
			//var highest_color_85_color = MacroHelper.GetPixelColor(targetProcess, highest - 53, height);
			//var highest_color_85 = highest_color_85_color.A == 255 && highest_color_85_color.R > 220;
			//if (highest_color_85)
			//{
			//	target_progressbar.Value = 85;
			//	return;
			//}
			//var highest_color_80_color = MacroHelper.GetPixelColor(targetProcess, highest - 71, height);
			//var highest_color_80 = highest_color_80_color.A == 255 && highest_color_80_color.R > 220;
			//if (highest_color_80)
			//{
			//	target_progressbar.Value = 80;
			//	return;
			//}


			////if (isTargetMaxHP)
			////{
			////	target_progressbar.Value = 100;

			////}
			////else if (TargetIsAlive)
			////{
			////	target_progressbar.Value = 50;

			////}
			////else
			////{
			////	target_progressbar.Value = 0;

			////}


		}

		private void Macro()
		{
			InitMacroDefaults();
			Prepare();

			var alefclients = Process.GetProcessesByName("alefclient").ToList();
			if (alefclients.Count == 0)
			{
				LogBox.Text = "No archlord clients found named 'alefclient.exe'.";

				return;
			}

			var temp = alefclients.Where(x => x.MainWindowHandle == targetProcess).ToList();
			if (!temp.Any())
			{
				LogBox.Text = "alefclient not found";
				return;
			}
			IntPtr procHandle = temp.FirstOrDefault().MainWindowHandle;

			if (procHandle == targetProcess)
			{
				this.Invoke(new ThreadStart(() => ExecuteMacro()));
			}
		}

		private void IncrementCurrBar()
		{
			_ = CurrentBar >= 4 ? CurrentBar = 1 : CurrentBar += 1;
		}

		public Color GetPixelColorSimplified(IntPtr hwnd, int x, int y)
		{
			return MacroHelper.GetPixelColor(hwnd, x, y);
		}

		private void Prepare()
		{
			baseX = MacroHelper.GetCoords(targetProcess).Width;
			YHeight = MacroHelper.GetCoords(targetProcess).Height;

			if (!bPrepared)
			{
				row1PixelList.Clear();
				row2PixelList.Clear();

				CurrentBar = Convert.ToInt32(txtStartBar_Text);

				if (row1Indexes.Count > 0)
				{
					for (int i = 0; i < row1Indexes.Count; i++)
					{
						int offset = 0;
						if ((int)Row1BarNum_Value > 1 && row1Indexes[i] >= 5) offset = -1;
						int currX = baseX + row1XOffsets[i] + (row1Indexes[i] * 50) + (row1Indexes[i] * 2) + offset;
						int currY = YHeight + row1YOffsets[i] + MacroHelper.GetYOffsetFromBar((int)Row1BarNum_Value);
						row1PixelList.Add(GetPixelColorSimplified(targetProcess, currX, currY));
					}
				}

				if (row2Indexes.Count > 0)
				{
					for (int i = 0; i < row2Indexes.Count; i++)
					{
						int offset = 0;
						if ((int)Row2BarNum_Value > 1 && row2Indexes[i] >= 5) offset = -1;
						int currX = baseX + row2XOffsets[i] + (row2Indexes[i] * 50) + (row2Indexes[i] * 2) + offset;
						int currY = YHeight + row2YOffsets[i] + MacroHelper.GetYOffsetFromBar((int)Row2BarNum_Value);
						row2PixelList.Add(GetPixelColorSimplified(targetProcess, currX, currY));
					}
				}

				while (CurrentBar != Row1BarNum_Value && !cbNewBar_Checked)
				{
					MacroHelper.SendBarUp(targetProcess);
					IncrementCurrBar();
				}

				bPrepared = true;
			}
		}
		// I literally cannot be asked to make those long static functions shorter.

		private void InitMacroDefaults()
		{
			row1Indexes.Clear();
			row1XOffsets.Clear();
			row1YOffsets.Clear();
			if (Row1Skill1.Checked)
			{
				row1Indexes.Add(0);
				row1XOffsets.Add(28);
				row1YOffsets.Add(8);
			}
			if (Row1Skill2.Checked)
			{
				row1Indexes.Add(1);
				row1XOffsets.Add(28);
				row1YOffsets.Add(8);
			}
			if (Row1Skill3.Checked)
			{
				row1Indexes.Add(2);
				row1XOffsets.Add(28);
				row1YOffsets.Add(8);
			}
			if (Row1Skill4.Checked)
			{
				row1Indexes.Add(3);
				row1XOffsets.Add(28);
				row1YOffsets.Add(8);
			}
			if (Row1Skill5.Checked)
			{
				row1Indexes.Add(4);
				row1XOffsets.Add(28);
				row1YOffsets.Add(8);
			}
			if (Row1Skill6.Checked)
			{
				row1Indexes.Add(5);
				row1XOffsets.Add(27);
				row1YOffsets.Add(8);
			}
			if (Row1Skill7.Checked)
			{
				row1Indexes.Add(6);
				row1XOffsets.Add(26);
				row1YOffsets.Add(8);
			}
			if (Row1Skill8.Checked)
			{
				row1Indexes.Add(7);
				row1XOffsets.Add(25);
				row1YOffsets.Add(8);
			}
			if (Row1Skill9.Checked)
			{
				row1Indexes.Add(8);
				row1XOffsets.Add(24);
				row1YOffsets.Add(8);
			}
			if (Row1Skill10.Checked)
			{
				row1Indexes.Add(9);
				row1XOffsets.Add(23);
				row1YOffsets.Add(8);
			}

			row2Indexes.Clear();
			row2XOffsets.Clear();
			row2YOffsets.Clear();
			if (Row2Skill1.Checked)
			{
				row2Indexes.Add(0);
				row2XOffsets.Add(28);
				row2YOffsets.Add(8);
			}
			if (Row2Skill2.Checked)
			{
				row2Indexes.Add(1);
				row2XOffsets.Add(28);
				row2YOffsets.Add(8);
			}
			if (Row2Skill3.Checked)
			{
				row2Indexes.Add(2);
				row2XOffsets.Add(28);
				row2YOffsets.Add(8);
			}
			if (Row2Skill4.Checked)
			{
				row2Indexes.Add(3);
				row2XOffsets.Add(28);
				row2YOffsets.Add(8);
			}
			if (Row2Skill5.Checked)
			{
				row2Indexes.Add(4);
				row2XOffsets.Add(28);
				row2YOffsets.Add(8);
			}
			if (Row2Skill6.Checked)
			{
				row2Indexes.Add(5);
				row2XOffsets.Add(27);
				row2YOffsets.Add(8);
			}
			if (Row2Skill7.Checked)
			{
				row2Indexes.Add(6);
				row2XOffsets.Add(26);
				row2YOffsets.Add(8);
			}
			if (Row2Skill8.Checked)
			{
				row2Indexes.Add(7);
				row2XOffsets.Add(25);
				row2YOffsets.Add(8);
			}
			if (Row2Skill9.Checked)
			{
				row2Indexes.Add(8);
				row2XOffsets.Add(24);
				row2YOffsets.Add(8);
			}
			if (Row2Skill10.Checked)
			{
				row2Indexes.Add(9);
				row2XOffsets.Add(23);
				row2YOffsets.Add(8);
			}

			try
			{
				IntPtr currHandle = targetProcess;
				targetProcess = GetForegroundWindow();
				if (currHandle != targetProcess)
				{
					bPrepared = false;
					Prepare();
				}
			}
			catch (Exception)
			{
				// Should never happen but fuck it amirite?
				MessageBox.Show("No foreground window found. Try again.", "An error has occured.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		private void ProcessMacroEntryBar1(List<int> list, int MacroProgress, int delay)
		{
			if (MacroProgress == 999)
			{
				return;
			}
			var key = list[MacroProgress];

			var color = Row1Skill1_Label.ForeColor;
			//switch (key)
			//{
			//	case 1:
			//		Row1Skill1_Label.ForeColor = Color.Red;
			//		LogBox.Text = "pressed 1 init";
			//		break;
			//	default:
			//		break;
			//}

			int val = (int)Row1BarNum_Value;
			if (val == 1) MacroHelper.PressBtnNormal(key, targetProcess);
			else if (val == 2) MacroHelper.PressBtnShift(key, targetProcess);
			else if (val == 3) MacroHelper.PressBtnCtrl(key, targetProcess);

			status_bar.BackColor = Color.Green;

			//LogBox.Text = "pressed init over " + key;

			Thread.Sleep(delay);
			Row1Skill1_Label.ForeColor = color;

		}
		private void ProcessMacroEntryBar2(int key, int delay)
		{
			int val = (int)Row2BarNum_Value;
			if (val == 1) MacroHelper.PressBtnNormal(key, targetProcess);
			else if (val == 2) MacroHelper.PressBtnShift(key, targetProcess);
			else if (val == 3) MacroHelper.PressBtnCtrl(key, targetProcess);

			Thread.Sleep(delay);
		}

		private void ExecuteMacro()
		{
			CurrentBar = Convert.ToInt32(txtStartBar_Text);
			int delay = Int32.Parse(txtDelay_Text);

			bRepeat = true;
			if (bRepeat)
			{
				Task.Factory.StartNew(() =>
				{
					//if (cbSwitch_Checked) switcher?.MakeSwitch();
					while (bRepeat)
					{
						if (row1Indexes.Count > 0)
						{
							if (!cbNewBar_Checked)
							{
								while (CurrentBar != Row1BarNum_Value)
								{
									MacroHelper.SendBarUp(targetProcess);
									IncrementCurrBar();
								}
								Thread.Sleep(30);
							}
							for (MacroProgress = 0; MacroProgress < row1Indexes.Count; MacroProgress++)
							{
								if (!cbNewBar_Checked)
								{
									while (MacroProgress != 999 && MacroHelper.IsColorsEqual(row1PixelList[MacroProgress], GetPixelColorSimplified(targetProcess, baseX + row1XOffsets[MacroProgress] + (row1Indexes[MacroProgress] * 50) + (row1Indexes[MacroProgress] * 2), YHeight + row1YOffsets[MacroProgress])))
									{
										if (MacroProgress != 999)
										{
											this.Invoke(new ThreadStart(() => ProcessMacroEntryBar1(row1Indexes, MacroProgress, delay)));
										}
									}
								}
								else
								{
									int offset = 0;
									if ((int)Row1BarNum_Value > 1 && row1Indexes[MacroProgress] >= 5) offset = -1;
									while (MacroProgress != 999 && MacroHelper.IsColorsEqual(row1PixelList[MacroProgress], GetPixelColorSimplified(targetProcess, baseX + row1XOffsets[MacroProgress] + (row1Indexes[MacroProgress] * 50) + (row1Indexes[MacroProgress] * 2) + offset, YHeight + row1YOffsets[MacroProgress] + MacroHelper.GetYOffsetFromBar((int)Row1BarNum_Value))))
									{
										if (MacroProgress != 999)
										{
											this.Invoke(new ThreadStart(() => ProcessMacroEntryBar1(row1Indexes, MacroProgress, delay)));

										}
									}
								}
							}
						}
						if (row2Indexes.Count > 0)
						{
							if (!cbNewBar_Checked)
							{
								while (CurrentBar != Row2BarNum_Value)
								{
									MacroHelper.SendBarUp(targetProcess);
									IncrementCurrBar();
								}
								Thread.Sleep(30);
							}
							for (MacroProgress = 0; MacroProgress < row2Indexes.Count; MacroProgress++)
							{
								if (!cbNewBar_Checked)
								{
									while (MacroHelper.IsColorsEqual(row2PixelList[MacroProgress], GetPixelColorSimplified(targetProcess, baseX + row2XOffsets[MacroProgress] + (row2Indexes[MacroProgress] * 50) + (row2Indexes[MacroProgress] * 2), YHeight + row2YOffsets[MacroProgress])))
									{
										this.Invoke(new ThreadStart(() => ProcessMacroEntryBar2(row2Indexes[MacroProgress], delay)));
									}
								}
								else
								{
									int offset = 0;
									if ((int)Row2BarNum_Value > 1 && row2Indexes[MacroProgress] >= 5) offset = -1;
									while (MacroHelper.IsColorsEqual(row2PixelList[MacroProgress], GetPixelColorSimplified(targetProcess, baseX + row2XOffsets[MacroProgress] + (row2Indexes[MacroProgress] * 50) + (row2Indexes[MacroProgress] * 2) + offset, YHeight + row2YOffsets[MacroProgress] + MacroHelper.GetYOffsetFromBar((int)Row2BarNum_Value))))
									{
										this.Invoke(new ThreadStart(() => ProcessMacroEntryBar2(row2Indexes[MacroProgress], delay)));
									}
								}
							}
						}
					}
				});
			}

			//LogBox.Text = "Macro STARTED";
			status_bar.ForeColor = Color.Green;
		}

		private void Row1Skill1X_TextChanged(object sender, EventArgs e)
		{
			bPrepared = false;
		}

		private void Row2Skill1_CheckedChanged(object sender, EventArgs e)
		{
			bPrepared = false;
		}

		private void btnDebug_Click(object sender, EventArgs e)
		{
			MessageBox.Show("This feature is disabled.");
			return;
			picker?.Close();
			picker = new ColorPicker();

			if (targetProcess == IntPtr.Zero)
			{

				picker.alWindow = GetForegroundWindow();

			}
			else
			{
				picker.alWindow = targetProcess;

			}

			picker.Show();
		}

		private void btnSwitchOpen_Click(object sender, EventArgs e)
		{

			MessageBox.Show("This feature is disabled.");
			return;

			switcher?.Close();
			switcher = new SwitchTool();
			switcher.alWindow = targetProcess;
			switcher.Show();
		}

		private void cbNewBar_CheckedChanged(object sender, EventArgs e)
		{
			bPrepared = false;
		}

		private void SetWindowTitle()
		{
			Random rnd = new Random();
			this.Text = "ALMacro.";
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void LogBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			mSettings?.Close();
			mSettings = new MSettings();
			mSettings.form1 = this;
			mSettings.Show();
		}

		private void button2_Click(object sender, EventArgs e)
		{

			Process.Start("C:\\Users\\yerl02\\Documents\\AL\\Custom-Archlord\\Custom-Archlord");

		}

		private void button_closeclients_Click(object sender, EventArgs e)
		{
			foreach (var process in Process.GetProcessesByName("alefclient"))
			{
				process.Kill();
			}
		}

		private void label4_Click(object sender, EventArgs e)
		{

		}

		private void label_status_Click(object sender, EventArgs e)
		{

		}

		private void target_progressbar_Click(object sender, EventArgs e)
		{

		}

		public const int WM_NCLBUTTONDOWN = 0xA1;
		public const int HT_CAPTION = 0x2;

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern bool ReleaseCapture();

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
			}
		}

		private void status_bar_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click_1(object sender, EventArgs e)
		{

			Application.Exit();
		}

		private void label21_Click(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;

		}
	}
}

namespace ExtensionMethods
{
	using System.Windows.Forms;

	public static class MyExtensions
	{
		public static void InvokeIfRequired(this Control control, MethodInvoker action)
		{
			if (control.InvokeRequired)
			{
				control.Invoke(action);
			}
			else
			{
				action();
			}
		}
	}
}

//using System.Runtime.Remoting.Channels;
//using MacroSwitch.Properties;
//using System.Windows;
//using System.Xml.Linq;
//using System.Xml.Schema;
//using System.Net;
//using System.Net.Sockets;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
