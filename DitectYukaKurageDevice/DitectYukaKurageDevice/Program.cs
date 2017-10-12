using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DitectYukaKurageDevice
{
    static class Program
    {
        private static Mutex mutex;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var priority = false;
            mutex = new Mutex(
                true,
                Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location),
                out priority);
            if (!priority)
            {
                WriteLogLine("Tried double running.");
                return;
            }
            try
            {
                WriteLogLine("Healthfull");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new InvisibleWindow());
            }
            catch
            {
                mutex.ReleaseMutex();
                mutex.Close();
                WriteLogLine("Exception failed");
            }
            
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
