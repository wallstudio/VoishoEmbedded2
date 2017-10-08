using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackingTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] { "bin" };
            }
            switch (args[0])
            {
                case "bin":
                    Binary(2,"D");
                    break;
                case "hex":
                    Binary(16, "X");
                    break;
                case "code":
                    Code();
                    break;
                default:
                    Binary(16, "X");
                    break;
            }
        }

        private static void Binary(int deci, string fomat)
        {
            var rand = new Random();
            for (int i = 0; i < 1100; i++)
            {
                Thread.Sleep(rand.Next(10, 60));
                for (int j = 0; j < rand.Next(1,10); j++)
                {
                    Console.Write("0x");
                    for (int k = 0; k < 12; k++)
                    {
                        if (i < 1000)
                        {
                            switch (rand.Next(0, (1000 - i) * 50) / 50)
                            {
                                case 0:
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case 2:
                                    //Console.BackgroundColor = ConsoleColor.Green;
                                    break;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        for (int l = 0; l < 4; l++)
                        {
                            Console.Write(rand.Next(0, deci).ToString(fomat));
                        }
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                    
                }
            }
        }
        private static void Code()
        {
            var rand = new Random();
            var aa = @"
                                       
                       ######          
                      ## ####          
            ##############             
       #######       ######           
     ###    |    |         ###         
    ##      |    |           ###       
    #                       ####    
    ###      ___/              ####  
      ###                      ## ###     
      ##   #### ### ####    ####       
      ##   ##          #     #         
     ##     #         ##     #         
     ##     #         ##     #         
     ###  ###         ##    ##         
     ########          #######         
      #####            #######         
     ##   #            #    #          
     #    #           ##    #=         
     #    #           ##    #          
    ##   ##           ##    #          
    ##   ##            #    #          
    ###  ##             #   ##          
     #####              ####           
                                       ";
            Line("Try hacking", ConsoleColor.Red);
            Dot(5);
            AALines(aa, rand);
            Line("Passed 1st FIREWALL");
            Dot(5);
            Line("Passed 2st FIREWALL");
            Dot(10);
            Line("Passed 3st FIREWALL");
            Dot(5);
            Thread.Sleep(100);
            AALines(aa, rand);
            Line("Attacked SECURTY SYSTEM", ConsoleColor.Red);
            Dot(10);
            Line("Destroyed security system", ConsoleColor.Green);
            Console.WriteLine();
            Thread.Sleep(100);
            Line("Hack system karnel");
            Dot(5);
            AALines(aa, rand);
            Line("Complet!");
            Thread.Sleep(5000);
        }
        private static void AALines(string aa, Random rand)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (var i in aa.Split('\n'))
            {
                Console.WriteLine(i);
                Thread.Sleep(rand.Next(100,800));
            }
            Console.ResetColor();
        }
        private static void Line(string msg, ConsoleColor fore, ConsoleColor back)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
            Console.Write(msg);
            Console.ResetColor();
        }
        private static void Line(string msg, ConsoleColor fore)
        {
            Console.ForegroundColor = fore;
            Console.Write(msg);
            Console.ResetColor();
        }
        private static void Line(string msg)
        {
            Console.Write(msg);
            Console.ResetColor();
        }
        private static void Dot(int cnt)
        {
            for (int i = 0; i < cnt; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }
            Console.WriteLine();
        }
    }
}
