namespace YukaKurageControl
{
    partial class HackingUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HackingUI));
            this.VoiceroidTalkerHook = new System.Windows.Forms.Timer(this.components);
            this.FadeIn = new System.Windows.Forms.Timer(this.components);
            this.FadeOut = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // VoiceroidTalkerHook
            // 
            this.VoiceroidTalkerHook.Enabled = true;
            this.VoiceroidTalkerHook.Tick += new System.EventHandler(this.voiceroidTalkerHook_Tick);
            // 
            // FadeIn
            // 
            this.FadeIn.Tick += new System.EventHandler(this.FadeIn_Tick);
            // 
            // FadeOut
            // 
            this.FadeOut.Tick += new System.EventHandler(this.FadeOut_Tick);
            // 
            // HackingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BackgroundImage = global::YukaKurageControl.Properties.Resources.Splash;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HackingUI";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HackingUI";
            this.Load += new System.EventHandler(this.HackingUI_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer VoiceroidTalkerHook;
        private System.Windows.Forms.Timer FadeIn;
        private System.Windows.Forms.Timer FadeOut;
    }
}