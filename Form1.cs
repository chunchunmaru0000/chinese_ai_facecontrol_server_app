using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using FPCLOCK_SVRLib;
using AxFPCLOCK_SVRLib;
using System.Net.Http.Headers;
using System.IO;
using System.Runtime.InteropServices;

namespace testkit
{
	public partial class Form1 : Form
	{
		string stateStr = @"
        AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uV2luZG93cy5Gb3JtcywgVmVyc2lvbj00LjAuMC4w
        LCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODkFAQAAACFTeXN0
        ZW0uV2luZG93cy5Gb3Jtcy5BeEhvc3QrU3RhdGUBAAAABERhdGEHAgIAAAAJAwAAAA8DAAAAJQAAAAIB
        AAAAAQAAAAAAAAAAAAAAABAAAAAAAAEAVgoAADgEAAAAAAAACw==
";
		private AxFPCLOCK_Svr axFPCLOCK_Svr1;
		private int nIndex = 0;
		private int Recordnumber = 0;

		public Form1()
		{
			InitializeComponent();

			byte[] serializedData = Convert.FromBase64String(stateStr);

			axFPCLOCK_Svr1 = new AxFPCLOCK_Svr() { Name = "axFPCLOCK_Svr1" };
			using (MemoryStream ms = new MemoryStream(serializedData))
				axFPCLOCK_Svr1.OcxState = new AxHost.State(ms, 1, false, null);
			/*
			axFPCLOCK_Svr1.OcxState = 
				((System.Windows.Forms.AxHost.State)
				(new System.ComponentModel.ComponentResourceManager(typeof(Form1))
				.GetObject("axFPCLOCK_Svr1.OcxState")));
*/
			axFPCLOCK_Svr1.OnReceiveGLogData += 
				new AxFPCLOCK_SVRLib._DFPCLOCK_SvrEvents_OnReceiveGLogDataEventHandler(
					axFPCLOCK_Svr1_OnReceiveGLogData);

			Controls.Add(axFPCLOCK_Svr1);

			connect.Enabled = true;
			Disconnect.Enabled = false;

			textPort.Text = "7005";

			listView1.Columns.Add(" ", 40, HorizontalAlignment.Left);          //一步添加
			listView1.Columns.Add("EnrollNo", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("VerifyMode", 100, HorizontalAlignment.Left);
			listView1.Columns.Add("InOut", 60, HorizontalAlignment.Left);
			listView1.Columns.Add("DateTime", 140, HorizontalAlignment.Left);
			listView1.Columns.Add("IP", 130, HorizontalAlignment.Left);
			listView1.Columns.Add("Port", 60, HorizontalAlignment.Left);
			listView1.Columns.Add("DevID", 60, HorizontalAlignment.Left);
			listView1.Columns.Add("SerialNo", 60, HorizontalAlignment.Left);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			String str = this.textPort.Text;
			int nPort = Convert.ToInt32(str);
			axFPCLOCK_Svr1.OpenNetwork(nPort);
			connect.Enabled = false;
			this.Disconnect.Enabled = true;
		}

		private void axFPCLOCK_Svr1_OnReceiveGLogData(object sender, AxFPCLOCK_SVRLib._DFPCLOCK_SvrEvents_OnReceiveGLogDataEvent e)
		{
			String strKey = Convert.ToString(nIndex + 1);
			String str = e.anSEnrollNumber.ToString("D8");
			long dwCardNum1 = 0;
			double aTemperature;

			richTextBox1.Text += $"{e.anSEnrollNumber}\n";

			if (e.anSEnrollNumber > 0)
			{
				int imagelen = 0;
				int[] imagebuff = new int[200 * 1024];
				bool bRet;
				IntPtr ptrIndexFacePhoto = Marshal.AllocHGlobal(imagebuff.Length);
				bRet = this.axFPCLOCK_Svr1.GetLogImageCS(e.linkindex, ref imagelen, ptrIndexFacePhoto);
				if (bRet && imagelen > 0)
				{
					byte[] mbytCurEnrollData = new byte[imagelen];
					Marshal.Copy(ptrIndexFacePhoto, mbytCurEnrollData, 0, imagelen);
					System.IO.File.WriteAllBytes(e.anSEnrollNumber.ToString() + "_" + e.anLogDate.ToString("yy_MM_dd_HH_mm_ss") + ".jpg", mbytCurEnrollData);
				}
				Marshal.FreeHGlobal(ptrIndexFacePhoto);
			}

			//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
			this.listView1.BeginUpdate();

			//this.listView1.Focus();
			ListViewItem lvi = new ListViewItem();
			lvi.Text = strKey;

			if (e.anSEnrollNumber < 0)
			{
				dwCardNum1 = e.anSEnrollNumber + 4294967296;
				str = dwCardNum1.ToString();
				lvi.SubItems.Add(str);
			}
			else
			{
				lvi.SubItems.Add(str);
			}

			if (e.anVerifyMode > 40)
			{
				aTemperature = e.anVerifyMode;
				aTemperature = (250 + aTemperature) / 10;
				str = aTemperature.ToString("#0.0");
			}
			else
			{

				str = FormString(e.anVerifyMode, e.anSEnrollNumber);
			}
			if (e.anSEnrollNumber < 0)
			{
				dwCardNum1 = e.anSEnrollNumber + 4294967296;
				str = FormStringlong(e.anVerifyMode, dwCardNum1);
				lvi.SubItems.Add(str);
			}
			else
			{
				lvi.SubItems.Add(str);
			}

			if (e.anInOutMode == 1)
			{
				str = "OUT";
			}
			else if (0 == e.anInOutMode)
			{
				str = "IN";
			}
			else
			{
				str = "--";
			}
			lvi.SubItems.Add(str);

			str = Convert.ToString(e.anLogDate.ToString("yyyy/MM/dd HH:mm:ss"));
			//str = Convert.ToString(e.anLogDate.ToString("yyyy/MM/dd HH:mm"));
			lvi.SubItems.Add(str);

			//str = Convert.ToString(e.astrDeviceIP);
			lvi.SubItems.Add(e.astrDeviceIP);

			str = Convert.ToString(e.anDevicePort);
			lvi.SubItems.Add(str);

			str = Convert.ToString(e.vnDeviceID);
			lvi.SubItems.Add(str);

			str = Convert.ToString(e.anSN);
			lvi.SubItems.Add(str);

			this.listView1.Items.Add(lvi);
			//this.listView1.Items.(5, str);

			this.listView1.Update();

			this.listView1.EnsureVisible(nIndex);
			this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

			int nResult = 1;

			this.axFPCLOCK_Svr1.SendResultandTime(e.linkindex, e.vnDeviceID, e.anSEnrollNumber, nResult);

			nIndex++;
			if (nIndex > 1000)
			{
				this.nIndex = 0;
				this.listView1.Items.Clear();
			}

		}

		private void Disconnect_Click(object sender, EventArgs e)
		{
			String str = this.textPort.Text;
			int nPort = Convert.ToInt32(str);
			this.axFPCLOCK_Svr1.CloseNetwork(nPort);

			this.connect.Enabled = true;
			this.Disconnect.Enabled = false;
		}

		public String FormString(int nVerify, int nEnrollNum)
		{
			int nAction = nVerify % 8;
			if (nEnrollNum == 0)
			{
				switch (nAction)
				{
					case 0:
						return "Closed";
					//break;
					case 1:
						return "Opened";
					// break;
					case 2:
						return "HandOpen";
					// break;
					case 3:
						return "ProcOpen";
					// break;
					case 4:
						return "ProcClose";
					// break;
					case 5:
						return "IllegalOpen";
					//break;
					case 6:
						return "IlleagalRemove";
					//break;
					case 7:
						return "Alarm";
					//break;
					case 8:
						return "--";
						//break;
				}
			}
			else
			{
				return nVerify.ToString();

			}

			return "Not my fault";
		}
		public String FormStringlong(int nVerify, long nEnrollNum)
		{
			int nAction = nVerify % 8;
			if (nEnrollNum == 0)
			{
				switch (nAction)
				{
					case 0:
						return "Closed";
					//break;
					case 1:
						return "Opened";
					// break;
					case 2:
						return "HandOpen";
					// break;
					case 3:
						return "ProcOpen";
					// break;
					case 4:
						return "ProcClose";
					// break;
					case 5:
						return "IllegalOpen";
					//break;
					case 6:
						return "IlleagalRemove";
					//break;
					case 7:
						return "Alarm";
					//break;
					case 8:
						return "--";
						//break;
				}
			}
			else
			{
				return nVerify.ToString();

			}

			return "Not my fault";
		}
		private void clearList_Click(object sender, EventArgs e)
		{

			this.nIndex = 0;
			this.listView1.Items.Clear();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
