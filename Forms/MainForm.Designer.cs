using System.IO;
using System;

namespace testkit
{
	partial class MainForm
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
            this.userDataListView = new System.Windows.Forms.ListView();
            this.connectBut = new System.Windows.Forms.Button();
            this.disconnectBut = new System.Windows.Forms.Button();
            this.clearList = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.portLabel = new System.Windows.Forms.Label();
            this.camPictureBox = new System.Windows.Forms.PictureBox();
            this.camBox = new System.Windows.Forms.ComboBox();
            this.webCamTextLabel = new System.Windows.Forms.Label();
            this.saveCamBut = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // userDataListView
            // 
            this.userDataListView.HideSelection = false;
            this.userDataListView.Location = new System.Drawing.Point(0, 512);
            this.userDataListView.Name = "userDataListView";
            this.userDataListView.Size = new System.Drawing.Size(768, 160);
            this.userDataListView.TabIndex = 0;
            this.userDataListView.UseCompatibleStateImageBehavior = false;
            this.userDataListView.View = System.Windows.Forms.View.Details;
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
            // camPictureBox
            // 
            this.camPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.camPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.camPictureBox.Location = new System.Drawing.Point(0, 0);
            this.camPictureBox.Name = "camPictureBox";
            this.camPictureBox.Size = new System.Drawing.Size(320, 256);
            this.camPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camPictureBox.TabIndex = 8;
            this.camPictureBox.TabStop = false;
            // 
            // camBox
            // 
            this.camBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.camBox.FormattingEnabled = true;
            this.camBox.Location = new System.Drawing.Point(800, 544);
            this.camBox.Name = "camBox";
            this.camBox.Size = new System.Drawing.Size(192, 33);
            this.camBox.TabIndex = 9;
            this.camBox.DropDown += new System.EventHandler(this.camBox_DropDown);
            this.camBox.SelectedIndexChanged += new System.EventHandler(this.camBox_SelectedIndexChanged);
            // 
            // webCamTextLabel
            // 
            this.webCamTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.webCamTextLabel.Location = new System.Drawing.Point(800, 512);
            this.webCamTextLabel.Name = "webCamTextLabel";
            this.webCamTextLabel.Size = new System.Drawing.Size(192, 32);
            this.webCamTextLabel.TabIndex = 10;
            this.webCamTextLabel.Text = "Веб камера";
            this.webCamTextLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveCamBut
            // 
            this.saveCamBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveCamBut.Location = new System.Drawing.Point(800, 576);
            this.saveCamBut.Name = "saveCamBut";
            this.saveCamBut.Size = new System.Drawing.Size(192, 64);
            this.saveCamBut.TabIndex = 11;
            this.saveCamBut.Text = "Сохранить изображение";
            this.saveCamBut.UseVisualStyleBackColor = true;
            this.saveCamBut.Click += new System.EventHandler(this.saveCamBut_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 707);
            this.Controls.Add(this.saveCamBut);
            this.Controls.Add(this.webCamTextLabel);
            this.Controls.Add(this.camBox);
            this.Controls.Add(this.camPictureBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.userDataListView);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camPictureBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView userDataListView;
		private System.Windows.Forms.Button connectBut;
		private System.Windows.Forms.Button disconnectBut;
		private System.Windows.Forms.Button clearList;
		private System.Windows.Forms.TextBox textPort;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.PictureBox camPictureBox;
        private System.Windows.Forms.ComboBox camBox;
        private System.Windows.Forms.Label webCamTextLabel;
        private System.Windows.Forms.Button saveCamBut;
    }
}

