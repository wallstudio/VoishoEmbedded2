using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageToBinary
{
    class Program
    {

        static long ROMSize = 0;

        static void Main(string[] args)
        {
            if (args.Length < 1) {
                Console.WriteLine("Param is nothing");
                Console.ReadKey();
                return;
            }
            string hStr = "";
            string str = "#include <avr/pgmspace.h>\n\n";
            foreach (string file in args)
            {
                if (!new Regex(@"png|gif").IsMatch(Path.GetExtension(file))) continue;
                Bitmap image = new Bitmap(file);
                int w = image.Width;
                int h = image.Height;
                string[] buf = new string[w * (int)Math.Ceiling(h / 8f) * 2];
                ROMSize += buf.Length;

                hStr += String.Format("extern uint8_t Bmp_{0}[];\n", Path.GetFileNameWithoutExtension(file));
                str += String.Format(@"

const uint8_t Bmp_{0}[] PROGMEM ={{
    0x{1:X2}, 0x{2:X2}, //Width {3}
    0x{4:X2}, 0x{5:X2}, //Height {6}
    0x{7:X2}, 0x{8:X2}, //Size {9} x2
    0x{10:X2}, 0x{11:X2}, //Reserve {12}

    "
                    , Path.GetFileNameWithoutExtension(file),
                    w & 0xFF, (w >> 8) & 0xFF, w,
                    h & 0xFF, (h >> 8) & 0xFF, h,
                    buf.Length / 2 & 0xFF, (buf.Length / 2 >> 8) & 0xFF, buf.Length / 2,
                    0, 0, 0
                );

                for (int i = 0; i < Math.Ceiling(h / 8f); i++) { //Each band(every8)
                    for (int j = 0; j < w; j++) { //Each poll(every4)
                        int byteColor = 0;
                        int byteAlpha = 0;
                        for (int k = 0; k < 8; k++) { //Each pxcel
                            int x = j;
                            int y = i * 8 + k;
                            Color col = y < image.Height ? image.GetPixel(x, y) : Color.FromArgb(0, 255, 255, 255);
                            int whiteness = (col.R + col.G + col.B) / 3;
                            if (whiteness < 127) {  // like blacky
                                byteColor += 1 << k;
                            }
                            if(col.A > 127) {   // like transparency
                                byteAlpha += 1 << k;
                            }
                        }
                        buf[w * i + j] = String.Format("0x{0:X2}", byteColor);
                        buf[w * i + j + buf.Length/2] = String.Format("0x{0:X2}", byteAlpha);
                    }
                }
                str += "//Color\n    " + string.Join(", ", buf, 0, buf.Length / 2) + ",\n    //Alpha\n    " + string.Join(", ", buf, buf.Length / 2, buf.Length / 2) + "\n};";
            }
            using (StreamWriter hsw = new StreamWriter(Path.GetDirectoryName(args[0]) + @"\..\Bitmap.h"))
            using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(args[0]) + @"\..\Bitmap.c")) {
                hsw.WriteLine("//Locate {0} byte.", ROMSize);
                sw.WriteLine("//Locate {0} byte.", ROMSize);
                hsw.Write(hStr);
                sw.Write(str);
            }
        }
    }
}
