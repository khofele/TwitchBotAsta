using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBotAsta
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            File.Delete(FileManager.Path);
            Console.ReadLine();
        }
    }
}
