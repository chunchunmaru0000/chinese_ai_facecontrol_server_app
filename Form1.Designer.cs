using System.IO;
using System;

namespace testkit
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.listView1 = new System.Windows.Forms.ListView();
			this.connectBut = new System.Windows.Forms.Button();
			this.disconnectBut = new System.Windows.Forms.Button();
			this.clearList = new System.Windows.Forms.Button();
			this.textPort = new System.Windows.Forms.TextBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.panel1 = new System.Windows.Forms.Panel();
			this.portLabel = new System.Windows.Forms.Label();
			this.vlcControl = new Vlc.DotNet.Forms.VlcControl();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.vlcControl)).BeginInit();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 512);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(768, 160);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// connectBut
			// 
			this.connectBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.connectBut.Location = new System.Drawing.Point(32, 32);
			this.connectBut.Name = "connectBut";
			this.connectBut.Size = new System.Drawing.Size(192, 32);
			this.connectBut.TabIndex = 1;
			this.connectBut.Text = "Подключиться";
			this.connectBut.UseVisualStyleBackColor = true;
			this.connectBut.Click += new System.EventHandler(this.button1_Click);
			// 
			// disconnectBut
			// 
			this.disconnectBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.disconnectBut.Location = new System.Drawing.Point(32, 64);
			this.disconnectBut.Name = "disconnectBut";
			this.disconnectBut.Size = new System.Drawing.Size(192, 32);
			this.disconnectBut.TabIndex = 2;
			this.disconnectBut.Text = "Отключиться";
			this.disconnectBut.UseVisualStyleBackColor = true;
			this.disconnectBut.Click += new System.EventHandler(this.Disconnect_Click);
			// 
			// clearList
			// 
			this.clearList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.clearList.Location = new System.Drawing.Point(32, 96);
			this.clearList.Name = "clearList";
			this.clearList.Size = new System.Drawing.Size(192, 32);
			this.clearList.TabIndex = 3;
			this.clearList.Text = "Очистить список";
			this.clearList.UseVisualStyleBackColor = true;
			this.clearList.Click += new System.EventHandler(this.clearList_Click);
			// 
			// textPort
			// 
			this.textPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textPort.Location = new System.Drawing.Point(128, 0);
			this.textPort.Name = "textPort";
			this.textPort.Size = new System.Drawing.Size(95, 31);
			this.textPort.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.portLabel);
			this.panel1.Controls.Add(this.disconnectBut);
			this.panel1.Controls.Add(this.connectBut);
			this.panel1.Controls.Add(this.clearList);
			this.panel1.Controls.Add(this.textPort);
			this.panel1.Location = new System.Drawing.Point(1056, 512);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(256, 160);
			this.panel1.TabIndex = 6;
			// 
			// portLabel
			// 
			this.portLabel.AutoSize = true;
			this.portLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.portLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.portLabel.Location = new System.Drawing.Point(32, 0);
			this.portLabel.Name = "portLabel";
			this.portLabel.Size = new System.Drawing.Size(76, 31);
			this.portLabel.TabIndex = 5;
			this.portLabel.Text = "Порт";
			// 
			// vlcControl
			// 
			this.vlcControl.BackColor = System.Drawing.Color.Black;
			this.vlcControl.Location = new System.Drawing.Point(0, 0);
			this.vlcControl.Name = "vlcControl";
			this.vlcControl.Size = new System.Drawing.Size(288, 224);
			this.vlcControl.Spu = -1;
			this.vlcControl.TabIndex = 7;
			this.vlcControl.Text = "vlcControl1";
			this.vlcControl.VlcLibDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc\\win-x86")); 
			this.vlcControl.VlcMediaplayerOptions = null;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1324, 707);
			this.Controls.Add(this.vlcControl);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.listView1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.vlcControl)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button connectBut;
		private System.Windows.Forms.Button disconnectBut;
		private System.Windows.Forms.Button clearList;
		private System.Windows.Forms.TextBox textPort;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label portLabel;
		private Vlc.DotNet.Forms.VlcControl vlcControl;
	}
}

