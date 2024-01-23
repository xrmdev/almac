using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroSwitch
{
	public partial class ColorPicker : Form
	{

		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int MapVirtualKey(int uCode, int uMapType);
		[DllImport("user32.dll")]
		static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);
		[DllImport("User32.dll")]
		private static extern bool SetCursorPos(int X, int Y);

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public static implicit operator System.Windows.Point(POINT point)
			{
				return new System.Windows.Point(point.X, point.Y);
			}
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetCursorPos(out POINT lpPoint);


		[DllImport("user32.dll")]
		public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
		public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		[DllImport("user32.dll")]
		static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("user32.dll")]
		static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

		[DllImport("gdi32.dll")]
		static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

		public IntPtr alWindow;
		int xx = 705, yy = 23;
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

		public ColorPicker()
		{
			InitializeComponent();
			prgColor.Value = 100;
			prgColor.Style = ProgressBarStyle.Continuous;
			timer.Interval = 10;
			timer.Tick += Timer_Tick;
			timer.Start();

			timer2.Interval = 1000;
			timer2.Tick += Timer_Tick1;
			timer2.Start();
		}

		private void click_search_white_pixel(object sender, EventArgs e)
		{

			//int temp1 = MacroHelper.GetCoords1(alWindow).Width;
			//int temp2 = MacroHelper.GetCoords1(alWindow).Height;
			//label5.Text = temp1.ToString();
			//label6.Text = temp2.ToString();
			//return;
			////ClickAtPosition(xx, yy);
			//label5.Text = "searching";
			//var white_pixels = 0;
			//for (int x = 0; x < temp1; x++)
			//{
			//	for (int y = 100; y < temp2; y++)
			//	{
			//		var color1 = MacroHelper.GetPixelColor(alWindow, x, y);
			//		if (color1.R == 255 && color1.G == 255 && color1.B == 255 && color1.A == 255)
			//		{

			//			ClickAtPosition(x, y);
			//			label5.Text = $"1st white_pixels => x:{x} & y:{y}";
			//			return;
			//		}



			//	}
			//}


		}


		private void button2_Click(object sender, EventArgs e)
		{

			ClickAtPosition(xx, yy);


		}

		private void Timer_Tick1(object sender, EventArgs e)
		{
			int Width = MacroHelper.GetCoords1(alWindow).Width;
			int Middle = MacroHelper.GetCoords1(alWindow).Width / 2;
			int height = 23;
			int lowest = Middle - 107;
			int highest = Middle + 71;

			var lowest_color = MacroHelper.GetPixelColor(alWindow, lowest, height);
			var isTargetSelected = lowest_color.A == 255 && lowest_color.R > 220;

			var highest_color = MacroHelper.GetPixelColor(alWindow, highest, height);

			var isTargetMaxHP = highest_color.A == 255 && highest_color.R > 220;

			var lowestPlusOne_color = MacroHelper.GetPixelColor(alWindow, lowest + 1, height);
			var TargetIsAlive = isTargetSelected && lowestPlusOne_color.A == 255 && lowestPlusOne_color.R > 220; ;

			if (alWindow != null)
			{
				timerlen.Text = $"Target Selected= {isTargetSelected} | Target MAX HP = {isTargetMaxHP} | Target Is Dead:  {!TargetIsAlive}";
				
			}



		}


		private void Timer_Tick(object sender, EventArgs e)
		{
			Color c;
			if (alWindow != null)
			{
				c = GetPixelColor(alWindow, xx, yy);
				prgColor.ForeColor = Color.FromArgb(c.R, c.G, c.B);
				lblColor.Text = c.ToString();
			}


			POINT point;
			GetCursorPos(out point);
			ScreenToClient(alWindow, ref point);
			ScreenToClient(alWindow, ref point);
			lblDebug.Text = $"X: {point.X}    Y: {point.Y}";
		}

		static public Color GetPixelColor(IntPtr hwnd, int x, int y)
		{
			IntPtr hdc = GetDC(hwnd);
			uint pixel = GetPixel(hdc, x, y);
			ReleaseDC(hwnd, hdc);
			Color color = Color.FromArgb((int)(pixel & 0x000000FF),
							(int)(pixel & 0x0000FF00) >> 8,
							(int)(pixel & 0x00FF0000) >> 16);
			return color;
		}

		private void ColorPicker_Load(object sender, EventArgs e)
		{
			int temp1 = MacroHelper.GetCoords(alWindow).Width;
			int temp2 = MacroHelper.GetCoords(alWindow).Height;

			label3.Text = MacroHelper.GetCoords(alWindow).Width.ToString();
			label4.Text = MacroHelper.GetCoords(alWindow).Height.ToString();




		}

		private void prgColor_Click(object sender, EventArgs e)
		{

		}

		private void btnSet_Click(object sender, EventArgs e)
		{
			xx = Int32.Parse(txtX.Text);
			yy = Int32.Parse(txtY.Text);
		}







		[DllImport("User32.Dll")]
		static extern bool PostMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);


		public int MakeLParam(int LoWord, int HiWord)
		{
			return (int)((HiWord << 16) | (LoWord & 0xFFFF));
		}
		private int MAKELPARAM(int p, int p_2)
		{
			return ((p_2 << 16) | (p & 0xFFFF));
		}

		public void ClickAtPosition(int x, int y)
		{
			int WM_LBUTTONDBLCLK = 0x0203;
			//Color c = GetPixelColor(alWindow, x, y);

			PostMessage(alWindow, (uint)WM_LBUTTONDBLCLK, 1, (uint)MakeLParam(x, y));
			PostMessage(alWindow, (uint)WM_LBUTTONDBLCLK, 1, (uint)MakeLParam(x, y));

		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void label6_Click(object sender, EventArgs e)
		{

		}

		private void click_tab(object sender, EventArgs e)
		{
			GlobalHelpers.PressTab(alWindow);
		
		}
		const uint MAPVK_VK_TO_VSC = 0;

	

		public void ClickAtPosition1(int x, int y)
		{
			int WM_LBUTTONDBLCLK = 0x0203;
			//Color c = GetPixelColor(alWindow, x, y);

			PostMessage(alWindow, (uint)WM_LBUTTONDBLCLK, 1, (uint)MakeLParam(x, y));
			PostMessage(alWindow, (uint)WM_LBUTTONDBLCLK, 1, (uint)MakeLParam(x, y));

		}
	}

	public class NativeMethods
	{
		[DllImport("user32.dll")]
		public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

		public const byte VK_TAB = 0x09;
		public const int KEYEVENTF_EXTENDEDKEY = 0x0001;
		public const int KEYEVENTF_KEYUP = 0x0002;
	}
}
