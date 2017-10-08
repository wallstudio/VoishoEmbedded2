using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DitectYukaKurageDevice
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InvisibleWindow());
        }

        public static void WriteLogLine(string mesage)
        {
            var logPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location)}.log";
            using (var sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"{DateTime.Now.ToString("HH-mm-ss")} {mesage}");
            }
        }
    }
}
