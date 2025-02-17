using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AxFPCLOCK_SVRLib;
using System.IO;
using System.Runtime.InteropServices;
using DirectShowLib;
using System.Threading;

namespace testkit
{
	public partial class Form1 : Form
	{
		string webcamUrl;

		string stateStr = @"
        AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uV2luZG93cy5Gb3JtcywgVmVyc2lvbj00LjAuMC4w
        LCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODkFAQAAACFTeXN0
        ZW0uV2luZG93cy5Gb3Jtcy5BeEhvc3QrU3RhdGUBAAAABERhdGEHAgIAAAAJAwAAAA8DAAAAJQAAAAIB
        AAAAAQAAAAAAAAAAAAAAABAAAAAAAAEAVgoAADgEAAAAAAAACw==
";
		private AxFPCLOCK_Svr axFPCLOCK_Svr;
		private int nIndex = 0;
		private int Recordnumber = 0;

		private Dictionary<int, string> actions = new Dictionary<int, string>() 
		{
			{ 0, "Closed" },   { 1, "Opened" },
			{ 2, "HandOpen" }, { 3, "ProcOpen" },
			{ 4, "ProcClose" }, { 5, "IllegalOpen" },
			{ 6, "IlleagalRemove" }, { 7, "Alarm" },
			{ 8, "--" }
		};

		public Form1()
		{
			InitializeComponent();
			//this.vlcControl.VlcLibDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc\\win-x86"));

			InitaxFPCLOCK_Svr();
			AdjustSomeControls();
			InitListView();

			//InitUSBWEB();
		}

		private void InitUSBWEB()
		{
			vlcControl.Log += (sender, args) => Console.WriteLine(args.Message);

			webcamUrl = GetWebCamUrl();

			string[] options = new string[]
			{
				//$":dshow-vdev=\"{webcamUrl}\"",
				":dshow-vdev=USB2.0 PC CAMERA",
				":dshow-adev=none",
				":live-caching=0",
			};
			

			vlcControl.Play("dshow://", options);
			//vlcControl.Play($"dshow://{webcamUrl}", options);
			//vlcControl.Play(new Uri("C:\\Users\\user\\Desktop\\vlcvideos\\rules.mp4"));
		}

		private string GetWebCamUrl()
		{
			DsDevice[] cams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
			string webcam = "";
			
			foreach  (DsDevice cam in cams)
			{
				if (cam.Name.ToLower().Contains("camera"))
					//webcam = $"dshow:// :dshow-vdev=\"{cam.Name}\"";
					webcam = cam.Name;
				Console.WriteLine($"\n{cam.Name}");
			}
			Console.WriteLine(webcam + '\n');
			return webcam;
		}

		#region BEGIN_INITS
		private void InitaxFPCLOCK_Svr()
		{
			byte[] serializedData = Convert.FromBase64String(stateStr);

			axFPCLOCK_Svr = new AxFPCLOCK_Svr() { Name = "axFPCLOCK_Svr1" };
			using (MemoryStream ms = new MemoryStream(serializedData))
				axFPCLOCK_Svr.OcxState = new AxHost.State(ms, 1, false, null);

			axFPCLOCK_Svr.OnReceiveGLogData +=
				new AxFPCLOCK_SVRLib._DFPCLOCK_SvrEvents_OnReceiveGLogDataEventHandler(
					axFPCLOCK_Svr1_OnReceiveGLogData);

			Controls.Add(axFPCLOCK_Svr);
		}

		private void InitListView()
		{
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

		private void AdjustSomeControls()
		{
			connectBut.Enabled = true;
			disconnectBut.Enabled = false;

			textPort.Text = "7005";
		}
		#endregion
		#region BUTS
		private void button1_Click(object sender, EventArgs e)
		{
			if (int.TryParse(textPort.Text, out int port))
			{
				axFPCLOCK_Svr.OpenNetwork(port);
				connectBut.Enabled = false;
				disconnectBut.Enabled = true;
			}
			else
				MessageBox.Show("НЕВРНОЕ ЧИСЛО ДЛЯ ОТКРЫВАЕМОГО ПОРТА");
		}

		private void Disconnect_Click(object sender, EventArgs e)
		{
			if (int.TryParse(textPort.Text, out int port))
			{
				axFPCLOCK_Svr.CloseNetwork(port);
				connectBut.Enabled = true;
				disconnectBut.Enabled = false;
			}
			else
				MessageBox.Show("НЕВРНОЕ ЧИСЛО ДЛЯ ЗАКРЫВАЕМОГО ПОРТА");
		}

		private void clearList_Click(object sender, EventArgs e)
		{

			nIndex = 0;
			listView1.Items.Clear();
		}
		#endregion
		#region AX
		private void axFPCLOCK_Svr1_OnReceiveGLogData(object sender, _DFPCLOCK_SvrEvents_OnReceiveGLogDataEvent e)
		{
			string strKey = Convert.ToString(nIndex + 1);
			string str = e.anSEnrollNumber.ToString("D8");
			long dwCardNum1 = 0;
			double aTemperature;

			if (e.anSEnrollNumber > 0)
			{
				int imagelen = 0;
				int[] imagebuff = new int[200 * 1024];
				bool bRet;
				IntPtr ptrIndexFacePhoto = Marshal.AllocHGlobal(imagebuff.Length);
				bRet = axFPCLOCK_Svr.GetLogImageCS(e.linkindex, ref imagelen, ptrIndexFacePhoto);
				if (bRet && imagelen > 0)
				{
					byte[] mbytCurEnrollData = new byte[imagelen];
					Marshal.Copy(ptrIndexFacePhoto, mbytCurEnrollData, 0, imagelen);
					File.WriteAllBytes(e.anSEnrollNumber.ToString() + "_" + e.anLogDate.ToString("yy_MM_dd_HH_mm_ss") + ".jpg", mbytCurEnrollData);
				}
				Marshal.FreeHGlobal(ptrIndexFacePhoto);
			}

			//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
			listView1.BeginUpdate();

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

			listView1.Items.Add(lvi);
			//this.listView1.Items.(5, str);

			listView1.Update();

			listView1.EnsureVisible(nIndex);
			listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

			int nResult = 1;

			axFPCLOCK_Svr.SendResultandTime(e.linkindex, e.vnDeviceID, e.anSEnrollNumber, nResult);

			nIndex++;
			if (nIndex > 1000)
			{
				nIndex = 0;
				listView1.Items.Clear();
			}

		}

		public string FormString(int nVerify, int nEnrollNum) =>
			nEnrollNum == 0 ? actions[nVerify % 8] : nVerify.ToString();

		public string FormStringlong(int nVerify, long nEnrollNum) =>
			nEnrollNum == 0 ? actions[nVerify % 8] : nVerify.ToString();
		#endregion
	}
}
