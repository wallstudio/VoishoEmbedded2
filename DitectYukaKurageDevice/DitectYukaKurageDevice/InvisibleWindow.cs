using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace DitectYukaKurageDevice
{
    public partial class InvisibleWindow : Form
    {
        const uint WM_DEVICECHANGE = 0x0219;

        public InvisibleWindow()
        {
            InitializeComponent();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            Visible = false;
            notifyIcon1.BalloonTipText = "ゆかクラゲの受入れ準備が出来ています";
            notifyIcon1.BalloonTipTitle = "システム";
            notifyIcon1.ShowBalloonTip(10000);
        }

        private void YukamakiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.pixiv.net/search.php?s_mode=s_tag_full&word=%E3%82%86%E3%81%8B%E3%83%9E%E3%82%AD");
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/wallstudio/VoishoEmbedded/blob/master/Misc/Manual.md");
        }
        private void ContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/yukawallstudio");
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if ((uint)m.Msg == WM_DEVICECHANGE)
            {
                TryExecuteExtraApplication();
            }
            base.WndProc(ref m);
        }

        private bool TryExecuteExtraApplication()
        {
            var drives = Directory.GetLogicalDrives();
            foreach (var letter in drives)
            {
                if (File.Exists($"{letter}YukaKurageControl.exe"))
                {
                    var ps = Process.GetProcesses().Where(p => p.ProcessName.Contains("YukaKurageControl")).ToList();
                    if(ps.Count() > 0) { break; };
                    Thread.Sleep(500);
                    Console.WriteLine("Caught");
                    notifyIcon1.BalloonTipText = "ゆかクラゲの侵入を確認しました";
                    notifyIcon1.BalloonTipTitle = "システム";
                    notifyIcon1.ShowBalloonTip(10000);
                    Thread.Sleep(3000);
                    try
                    {
                        Process.Start($"{letter}YukaKurageControl.exe", "1");
                        Program.WriteLogLine($"Called \"{letter}YukaKurageControl.exe 1\"");
                    }
                    catch(Exception e)
                    {
                        Program.WriteLogLine($"Failed run. Error message: {e}");
                    }
                    return true;
                }
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

}
