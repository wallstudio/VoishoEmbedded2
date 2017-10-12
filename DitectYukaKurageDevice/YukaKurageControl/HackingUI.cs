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
        public bool InstalledVoicceroid = false;

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
                try
                {
                    hex = Process.Start("cmd", $"/C {dir}\\HackingTerminal.exe hex");
                    code = Process.Start("cmd", $"/C {dir}\\HackingTerminal.exe code");
                    Thread.Sleep(1000);
                    MoveWindow(hex.MainWindowHandle, 100, screenHeight - 600 - 10, 800, 600, 1);
                    MoveWindow(code.MainWindowHandle, 10, 10, 800, 600, 1);
                }catch (Exception ee)
                {
                    WriteErrorLog("ハッキングターミナル起動例外");
                    WriteErrorLog(ee);
                }
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
                    if(File.Exists(voiceroidPath))
                    {
                        try
                        {
                            InstalledVoicceroid = true;
                            voiceroid = Process.Start(voiceroidPath);
                        }catch (Exception ee)
                        {
                            InstalledVoicceroid = false;
                            WriteErrorLog("VOICEROID起動例外");
                            WriteErrorLog(ee);
                        }
                    }
                }
            }
            // ゆかりさんどいてくれ
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (InstalledVoicceroid && voiceroid != null)
                    {
                        Thread.Sleep(1000);
                        try
                        {
                            MoveWindow(voiceroid.MainWindowHandle, screenWidth / 2, screenHeight - 20, 800, 600, 1);
                            if (hex.HasExited && code.HasExited && VoiceroidTServer.Ready) { break; }
                        }
                        catch(Exception ee)
                        {
                            WriteErrorLog("VOICEROID Windowの移動例外");
                            WriteErrorLog(ee);
                        }
                    }
                }
            });
            // Voiceroid talk に丸投げしてみて例外が帰ってこなければ起動完了
            Task.Run(() =>
            {
                // VOICEROIDが無ければVoiceroidTalkerも起動しない．
                if (!InstalledVoicceroid)
                {
                    // InstalledVoiceroid の判定はこれ以前に同期的に済んでいる．
                    return;
                }

                var voiceroidExcepted = true;
                var tryCount = 0;
                while (voiceroidExcepted)
                {
                    tryCount++;
                    try
                    {
                        // ここは必ず通過しないとアプリケーションの意味がない
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
            // hex & code はSleepで保障, InstalledVoiceroidはLoadの同期で保障
            if (hex.HasExited && code.HasExited 
                && (!InstalledVoicceroid ||  VoiceroidTServer.Ready))
            {
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
                    try
                    {
                        Application.Run(new Yukakurage(this));
                    }
                    catch(Exception ee)
                    {
                        WriteErrorLog("ゆかクラゲフォームの起動エラー");
                        WriteErrorLog(ee);
                        MessageBox.Show("ゆかクラゲの機嫌が悪い！\r\n（開発元にお問合せください）");
                        Application.Exit();
                    }
                });
            }

        }

        public void Speak(string args)
        {
            // VTクライアントは独立しているので，VOICEROID入ってない時は例外させる
            try
            {
                new VoiceroidTClient(args);
            }
            catch (Exception ee)
            {
                WriteErrorLog("VTクライアント例外");
                if (!InstalledVoicceroid)
                {
                    WriteErrorLog("VOICEROIDがインストールされていない環境");
                }
                else if(InstalledVoicceroid && !VoiceroidTServer.Ready)
                {
                    WriteErrorLog("VTサーバーが準備中");
                }
                else if(InstalledVoicceroid && VoiceroidTServer.Ready)
                {
                    WriteErrorLog(ee);
                }
            }
        }
        public void SubProcessesExit()
        {
            try
            {
                hex.Close();
                code.Close();
                voiceroid.Close();
            }
            catch (Exception ee)
            {
                WriteErrorLog("終了処理例外");
                WriteErrorLog(ee);
            }
        }
        
        public void WriteErrorLog(object msg)
        {

            try
            {
                using (var sw = new StreamWriter("error.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine($"{DateTime.Now} {msg.ToString()}");
                }
            }
            catch
            {
                MessageBox.Show("エラーログの書き込みエラーです．\r\n例外\r\n"+msg.ToString());
            }
        }
    }
}
