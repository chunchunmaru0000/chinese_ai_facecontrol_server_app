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
			this.listView1 = new System.Windows.Forms.ListView();
			this.connect = new System.Windows.Forms.Button();
			this.Disconnect = new System.Windows.Forms.Button();
			this.ClearList = new System.Windows.Forms.Button();
			this.textPort = new System.Windows.Forms.TextBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(992, 352);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// connect
			// 
			this.connect.Location = new System.Drawing.Point(256, 384);
			this.connect.Name = "connect";
			this.connect.Size = new System.Drawing.Size(160, 32);
			this.connect.TabIndex = 1;
			this.connect.Text = "connect";
			this.connect.UseVisualStyleBackColor = true;
			this.connect.Click += new System.EventHandler(this.button1_Click);
			// 
			// Disconnect
			// 
			this.Disconnect.Location = new System.Drawing.Point(256, 416);
			this.Disconnect.Name = "Disconnect";
			this.Disconnect.Size = new System.Drawing.Size(160, 32);
			this.Disconnect.TabIndex = 2;
			this.Disconnect.Text = "disconnect";
			this.Disconnect.UseVisualStyleBackColor = true;
			this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
			// 
			// ClearList
			// 
			this.ClearList.Location = new System.Drawing.Point(256, 448);
			this.ClearList.Name = "ClearList";
			this.ClearList.Size = new System.Drawing.Size(160, 32);
			this.ClearList.TabIndex = 3;
			this.ClearList.Text = "ClearList";
			this.ClearList.UseVisualStyleBackColor = true;
			this.ClearList.Click += new System.EventHandler(this.clearList_Click);
			// 
			// textPort
			// 
			this.textPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textPort.Location = new System.Drawing.Point(256, 352);
			this.textPort.Name = "textPort";
			this.textPort.Size = new System.Drawing.Size(160, 31);
			this.textPort.TabIndex = 4;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(448, 352);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(544, 160);
			this.richTextBox1.TabIndex = 5;
			this.richTextBox1.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 516);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.textPort);
			this.Controls.Add(this.ClearList);
			this.Controls.Add(this.Disconnect);
			this.Controls.Add(this.connect);
			this.Controls.Add(this.listView1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button connect;
		private System.Windows.Forms.Button Disconnect;
		private System.Windows.Forms.Button ClearList;
		private System.Windows.Forms.TextBox textPort;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.RichTextBox richTextBox1;
	}
}

