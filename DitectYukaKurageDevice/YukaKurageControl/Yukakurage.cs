using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YukaKurageControl
{
    public partial class Yukakurage : Form
    {

        private int _animationIndex = 0;
        private int animationIndex { get => _animationIndex; set => _animationIndex = value % images.Length; }
        private Image[] images;
        private Point baseLocation;
        private Point downLocation;
        private bool mouseDowned = false;
        private HackingUI mainUi;
        public static Dictionary<string, string> ManualTalk;

        public Yukakurage(HackingUI hackingUI)
        {
            InitializeComponent();
            mainUi = hackingUI;
            images = new Image[] { Properties.Resources.im0, Properties.Resources.im1 };
        }

        #region Design
        private void Form2_Load(object sender, EventArgs e)
        {
            var x = Screen.PrimaryScreen.Bounds.Width - Bounds.Width - 100;
            var y = Screen.PrimaryScreen.Bounds.Height - Bounds.Height - 100;
            baseLocation = new Point(x, y);
            Location = baseLocation;
            Activate();
            VoiceMix(yukaKurageMessage.Text.Replace("ゆかクラゲ： ", ""));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TopMost = true;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = images[animationIndex++];
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            downLocation = new Point(e.X, e.Y);
            mouseDowned = true;
        }
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDowned)
            {
                Left += e.X - downLocation.X;
                Top += e.Y - downLocation.Y;
            }
        }
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDowned = false;
        }
        #endregion

        #region Logic
        public static void LoadManualTalk()
        {
            try
            {
                if (!File.Exists("manual.txt"))
                {
                    using (var sw = new StreamWriter("manual.txt", false, Encoding.UTF8))
                    {
                        sw.Write("肉じゃが:肉じゃが肉じゃが肉じゃが肉じゃが肉じゃが肉じゃが肉じゃが肉じゃが；\r\nけだマキマキ：大好き！；");
                    }
                }
                using(var sr = new StreamReader("manual.txt", Encoding.UTF8))
                {
                    var buff = sr.ReadToEnd()
                        .Replace("：", ":")
                        .Replace("；", ";")
                        .Replace("\n", "")
                        .Replace("\r", "")
                        .Split(';')
                        .Select(x => x.Split(':')
                            .Select(y => y.Trim()).ToArray()
                        ).ToList();
                    ManualTalk = new Dictionary<string, string>();
                    foreach (var i in buff)
                    {
                        if (i.Length == 2 && i[0] != "" && !ManualTalk.ContainsKey(i[0]))
                        {
                            ManualTalk.Add(i[0], i[1]);
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                HackingUI.WriteErrorLog("マニュアルトークテーブルの読み込み例外");
                HackingUI.WriteErrorLog(ee);
            }
        }
        private void playerMessage_MouseClick(object sender, MouseEventArgs e)
        {
            if(playerMessage.Text == "あなたのメッセージを入力してね")
            {
                playerMessage.Text = "";
            }
        }
        private void sendMessage_Click(object sender, EventArgs e)
        {
            var playerText = playerMessage.Text.Replace("\r\n", "").Trim();
            if(playerText == "")
            {
                return;
            }
            var chatLog = yukaKurageMessage.Text.Split('\n').Select(str=>str.Trim('\r')).ToList();
            Console.WriteLine(playerText);
            WriteTalkLog($"あなた： {playerText}");
            chatLog.Add($"あなた： {playerText.Substring(0,Math.Min(playerText.Length, 27))}");
            playerMessage.Text = "";
            var respons = GetBotTalk(playerText);
            VoiceMix(respons);
            WriteTalkLog($"ゆかクラゲ： {respons}");
            chatLog.Add($"ゆかクラゲ： {respons.Substring(0, Math.Min(respons.Length, 25))}");
            while(chatLog.Count() > 9)
            {
                chatLog.RemoveAt(0);
            }
            yukaKurageMessage.Text = String.Join("\r\n",chatLog);
        }
        private void playerMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && playerMessage.Text != "") {
                sendMessage_Click(null, null);
            }
        }
        private void playerMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ビープ音を消す
            e.Handled = true;
        }

        private string GetBotTalk(string message)
        {
            if (ManualTalk != null && ManualTalk.ContainsKey(message))
            {
                return ManualTalk[message];
            }

            try
            {
                var apiKey = "5669512e684c616a6a4f764551313148583153345a6a466c792e2f645233774b423567645246564f394332";
                var request = WebRequest.Create($"https://api.apigw.smt.docomo.ne.jp/dialogue/v1/dialogue?APIKEY={apiKey}") as HttpWebRequest;
                request.ContentType = "application/json;charset=UTF-8";
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;
                using (var s = request.GetRequestStream())
                {
                    var json = "{ \"utt\":\"" + message + "\"}";
                    s.Write(Encoding.UTF8.GetBytes(json), 0, Encoding.UTF8.GetBytes(json).Length);
                }
                var respons = request.GetResponse() as HttpWebResponse;
                var result = "";
                using (var sr = new StreamReader(respons.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
                result = new Regex("\"utt\":\"([^\"]*)\"").Match(result).Groups[1].Value;
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return "オハナシスルー？";
            }
        }
        private void VoiceMix(string text)
        {
            // Yukari [メッセージ];[音量(0.0-2.0)];[話速(0.5-4.0)];[高さ(0.5-2.0)];[抑揚(0.0-2.0)]
            mainUi.Speak($"Yukari {text};1.5;1.0;1.9;0.2".Replace("。", "？"));
        }
        public static void WriteTalkLog(string msg)
        {
            try
            {
                using (var sw = new StreamWriter("talk.log", true, Encoding.UTF8))
                {
                    sw.WriteLine($"{DateTime.Now} {msg.ToString()}");
                }
            }
            catch
            {

            }
        }
        #endregion
    }

}
