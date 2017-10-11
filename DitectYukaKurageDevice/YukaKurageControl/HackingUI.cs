using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blkcatman;

namespace YukaKurageControl
{
    public partial class HackingUI : Form
    {
        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hwnd, int x, int y,int nWidth, int nHeight, int bRepaint);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("USER32.DLL")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 4;

        private string dir;
        private Process hex, code, voiceroid;
        private int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        private int showingCount = 20;

        public HackingUI()
        {
            InitializeComponent();
        }
        
        private void HackingUI_Load(object sender, EventArgs e)
        {
            dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // 起動している依存プロセス殺す
            var ps = Process.GetProcesses().Where(
                p =>  p.ProcessName.Contains("VoiceroidTServer")
                || p.ProcessName.Contains("VoiceroidTClient")
                ).ToArray();
            foreach (var i in ps)
            {
                i.Close();
            }
            // 視覚的なおまけ
            Task.Run(() =>
            {
                hex = Process.Start("cmd", $"/C {dir}\\HackingTerminal.exe hex");
                code = Process.Start("cmd", $"/C {dir}\\HackingTerminal.exe code");
                Thread.Sleep(1000);
                MoveWindow(hex.MainWindowHandle, 100, screenHeight - 600 - 10, 800, 600, 1);
                MoveWindow(code.MainWindowHandle, 10, 10, 800, 600, 1);
            });
            // ゆかりさん起動
            if (voiceroid == null)
            {
                Process[] vp = Process.GetProcesses().Where(p => p.ProcessName.Contains("VOICEROID")).ToArray();
                foreach (var i in vp)
                {
                    if (i.MainModule.FileName.Contains("Yukari"))
                    {
                        voiceroid = i;
                        break;
                    }
                }
                if (voiceroid == null)
                {
                    // 起動していない
                    var programFilesPath =
                        Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").Contains("64")
                        ? Environment.GetEnvironmentVariable("PROGRAMFILES(X86)")
                        : Environment.GetEnvironmentVariable("PROGRAMFILES");
                    var voiceroidPath = $"{programFilesPath}\\AHS\\VOICEROID+\\YukariEX\\VOICEROID.exe";
                    voiceroid = Process.Start(voiceroidPath);
                }
            }
            // ゆかりさんどいてくれ
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(1000);
                    MoveWindow(voiceroid.MainWindowHandle, screenWidth / 2, screenHeight - 20, 800, 600, 1);
                    if (hex.HasExited && code.HasExited && VoiceroidTServer.Ready) { break; }
                }
            });
            // Voiceroid talk に丸投げしてみて例外が帰ってこなければ起動完了
            Task.Run(() =>
            {
                var voiceroidExcepted = true;
                var tryCount = 0;
                while (voiceroidExcepted)
                {
                    tryCount++;
                    try
                    {
                        new VoiceroidTServer("Yukari");
                        voiceroidExcepted = false;
                    }
                    catch (Exception ve)
                    {
                        Console.WriteLine($"Excepted({tryCount}) Msg:{ve}");
                        voiceroidExcepted = true;
                    }
                }
                Console.WriteLine("VoiceroidTServer is completed.");
            });
        }
        private void voiceroidTalkerHook_Tick(object sender, EventArgs e)
        {
            if (hex.HasExited && code.HasExited && VoiceroidTServer.Ready)
            {
                //button1.BackColor = Color.Purple;
                FadeIn.Enabled = true;
                VoiceroidTalkerHook.Enabled = false;
            }
        }
        private void FadeIn_Tick(object sender, EventArgs e)
        {
            Opacity += 0.1;
            if (Opacity > 0.9)
            {
                FadeOut.Enabled = true;
                FadeIn.Enabled = false;
            }
        }
        private void FadeOut_Tick(object sender, EventArgs e)
        {
            if(showingCount > 0)
            {
                showingCount--;
            }
            else
            {
                Opacity -= 0.1;
            }

            if(Opacity < 0.1)
            {
                Opacity = 0;
                FadeOut.Enabled = false;
                Task.Run(() =>
                {
                    Application.Run(new Yukakurage(this));
                });
            }

        }

        public void Speak(string args)
        {
             new VoiceroidTClient(args);
        }
        public void SubProcessesExit()
        {
            hex.Close();
            code.Close();
            voiceroid.Close();
        }
    }
}
