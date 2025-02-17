using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using AxFPCLOCK_SVRLib;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Linq;

namespace testkit
{
	public partial class MainForm : Form
	{
		private AxFPCLOCK_Svr axFPCLOCK_Svr;
		private int nIndex = 0;

		private readonly Dictionary<int, string> actions = new Dictionary<int, string>() 
		{
			{ 0, "Closed" },   { 1, "Opened" },
			{ 2, "HandOpen" }, { 3, "ProcOpen" },
			{ 4, "ProcClose" }, { 5, "IllegalOpen" },
			{ 6, "IlleagalRemove" }, { 7, "Alarm" },
			{ 8, "--" }
		};

		public MainForm()
		{
			InitializeComponent();
			InitaxFPCLOCK_Svr();
			AdjustSomeControls();
			InitListView();
        }

		#region WEB_CAM

		private FilterInfoCollection videoDevices { get; set; }
		private VideoCaptureDevice videoCaptureDevice { get; set; }

        private void camBox_DropDown(object sender, EventArgs e)
        {
			videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			camBox.Items.Clear();

			for (int i = 0; i < videoDevices.Count; i++)
				camBox.Items.Add($"{i} {videoDevices[i].Name}");
        }

        private void camBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			string camName = string.Join(" ", camBox.SelectedItem.ToString().Split(' ').Skip(1));
            for (int i = 0; i < videoDevices.Count; i++)
			{
				if (videoDevices[i].Name == camName)
				{
					videoCaptureDevice?.Stop();

                    videoCaptureDevice = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    videoCaptureDevice.NewFrame += WebCamNewFrame;
                    videoCaptureDevice.Start();
                }
			}
        }

		private void WebCamNewFrame(object sender, NewFrameEventArgs e)
		{
			camPictureBox.Image?.Dispose();
			camPictureBox.Image = (Bitmap)e.Frame.Clone();
		}

        private void takePhotoBut_Click(object sender, EventArgs e)
        {
            if (camPictureBox.Image == null)
                return;

			takenPhotoPictureBox.Image = (Bitmap)camPictureBox.Image.Clone();
        }

        private void saveCamBut_Click(object sender, EventArgs e)
        {
			if (takenPhotoPictureBox.Image == null)
				return;

			string imageName = $"img_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png";
            takenPhotoPictureBox.Image.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
        }

        #endregion WEB_CAM

        #region BEGIN_INITS
        private void InitaxFPCLOCK_Svr()
		{
            string stateStr = @"
        AAEAAAD/////AQAAAAAAAAAMAgAAAFdTeXN0ZW0uV2luZG93cy5Gb3JtcywgVmVyc2lvbj00LjAuMC4w
        LCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODkFAQAAACFTeXN0
        ZW0uV2luZG93cy5Gb3Jtcy5BeEhvc3QrU3RhdGUBAAAABERhdGEHAgIAAAAJAwAAAA8DAAAAJQAAAAIB
        AAAAAQAAAAAAAAAAAAAAABAAAAAAAAEAVgoAADgEAAAAAAAACw==
";

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
			userDataListView.Columns.Add(" ", 40, HorizontalAlignment.Left);          //一步添加
			userDataListView.Columns.Add("EnrollNo", 100, HorizontalAlignment.Left);
			userDataListView.Columns.Add("VerifyMode", 100, HorizontalAlignment.Left);
			userDataListView.Columns.Add("InOut", 60, HorizontalAlignment.Left);
			userDataListView.Columns.Add("DateTime", 140, HorizontalAlignment.Left);
			userDataListView.Columns.Add("IP", 130, HorizontalAlignment.Left);
			userDataListView.Columns.Add("Port", 60, HorizontalAlignment.Left);
			userDataListView.Columns.Add("DevID", 60, HorizontalAlignment.Left);
			userDataListView.Columns.Add("SerialNo", 60, HorizontalAlignment.Left);
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
			userDataListView.Items.Clear();
		}
		#endregion
		#region AX

		public string FormString(int nVerify, int nEnrollNum) =>
			nEnrollNum == 0 ? actions[nVerify % 8] : nVerify.ToString();

		public string FormStringlong(int nVerify, long nEnrollNum) =>
			nEnrollNum == 0 ? actions[nVerify % 8] : nVerify.ToString();

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

                    string imageName = e.anSEnrollNumber.ToString() + "_" + e.anLogDate.ToString("yy_MM_dd_HH_mm_ss") + ".png";
					try
					{
						byte[] imageBytes = new byte[imagelen];
						mbytCurEnrollData.CopyTo(imageBytes, 0);

						aiPictureBox.Image = new Bitmap(new MemoryStream(imageBytes)).Clone() as Bitmap;
					} catch { }

					File.WriteAllBytes(imageName, mbytCurEnrollData);
				}
				Marshal.FreeHGlobal(ptrIndexFacePhoto);
			}

			//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
			userDataListView.BeginUpdate();
            #region LIST_ADD_DATA
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
				lvi.SubItems.Add(str);


			idLabel.Text = e.anSEnrollNumber < 0 ? idLabel.Text : $"ПОСЛЕДНИЙ ID: {e.anSEnrollNumber}";


			if (e.anVerifyMode > 40)
			{
				aTemperature = e.anVerifyMode;
				aTemperature = (250 + aTemperature) / 10;
				str = aTemperature.ToString("#0.0");
			}
			else
				str = FormString(e.anVerifyMode, e.anSEnrollNumber);

			if (e.anSEnrollNumber < 0)
			{
				dwCardNum1 = e.anSEnrollNumber + 4294967296;
				str = FormStringlong(e.anVerifyMode, dwCardNum1);
				lvi.SubItems.Add(str);
			}
			else
				lvi.SubItems.Add(str);

			if (e.anInOutMode == 1)
				str = "OUT";
			else if (0 == e.anInOutMode)
				str = "IN";
			else
				str = "--";

			lvi.SubItems.Add(str);

			str = Convert.ToString(e.anLogDate.ToString("yyyy/MM/dd HH:mm:ss"));
			lvi.SubItems.Add(str);

			lvi.SubItems.Add(e.astrDeviceIP);

			str = Convert.ToString(e.anDevicePort);
			lvi.SubItems.Add(str);

			str = Convert.ToString(e.vnDeviceID);
			lvi.SubItems.Add(str);

			str = Convert.ToString(e.anSN);
			lvi.SubItems.Add(str);

			userDataListView.Items.Add(lvi);
            #endregion LIST_ADD_DATA
            userDataListView.Update();

			userDataListView.EnsureVisible(nIndex);
			userDataListView.EndUpdate();  //结束数据处理，UI界面一次性绘制。

			int nResult = 1;

			axFPCLOCK_Svr.SendResultandTime(e.linkindex, e.vnDeviceID, e.anSEnrollNumber, nResult);

			nIndex++;
			if (nIndex > 1000)
			{
				nIndex = 0;
				userDataListView.Items.Clear();
			}

		}

        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
			videoCaptureDevice?.Stop();
			Environment.Exit(0);
        }
    }
}
