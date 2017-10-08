namespace YukaKurageControl
{
    partial class Yukakurage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yukakurage));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sendMessage = new System.Windows.Forms.Button();
            this.talkPanel = new System.Windows.Forms.PictureBox();
            this.yukaKurageMessage = new System.Windows.Forms.Label();
            this.playerMessage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.talkPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::YukaKurageControl.Properties.Resources.im0;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(597, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 320);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            // 
            // sendMessage
            // 
            this.sendMessage.Font = new System.Drawing.Font("メイリオ", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendMessage.Location = new System.Drawing.Point(482, 299);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(74, 39);
            this.sendMessage.TabIndex = 2;
            this.sendMessage.Text = "送信";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // talkPanel
            // 
            this.talkPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.talkPanel.Location = new System.Drawing.Point(31, 63);
            this.talkPanel.Name = "talkPanel";
            this.talkPanel.Size = new System.Drawing.Size(543, 285);
            this.talkPanel.TabIndex = 3;
            this.talkPanel.TabStop = false;
            this.talkPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.talkPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.talkPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            // 
            // yukaKurageMessage
            // 
            this.yukaKurageMessage.AutoSize = true;
            this.yukaKurageMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.yukaKurageMessage.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.yukaKurageMessage.Location = new System.Drawing.Point(43, 77);
            this.yukaKurageMessage.Name = "yukaKurageMessage";
            this.yukaKurageMessage.Size = new System.Drawing.Size(239, 24);
            this.yukaKurageMessage.TabIndex = 4;
            this.yukaKurageMessage.Text = "ゆかクラゲ： オハナシスルー？";
            // 
            // playerMessage
            // 
            this.playerMessage.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playerMessage.Location = new System.Drawing.Point(47, 299);
            this.playerMessage.Multiline = true;
            this.playerMessage.Name = "playerMessage";
            this.playerMessage.Size = new System.Drawing.Size(424, 39);
            this.playerMessage.TabIndex = 5;
            this.playerMessage.Text = "あなたのメッセージを入力してね";
            this.playerMessage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.playerMessage_MouseClick);
            this.playerMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.playerMessage_KeyDown);
            // 
            // Yukakurage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(938, 360);
            this.Controls.Add(this.playerMessage);
            this.Controls.Add(this.yukaKurageMessage);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.talkPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1500, 600);
            this.Name = "Yukakurage";
            this.Opacity = 0.85D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Load += new System.EventHandler(this.Form2_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.talkPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.PictureBox talkPanel;
        private System.Windows.Forms.Label yukaKurageMessage;
        private System.Windows.Forms.TextBox playerMessage;
    }
}