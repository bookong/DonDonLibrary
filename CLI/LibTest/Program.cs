using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;
using DonDonLibrary.Database.Vita;

namespace LibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using(EndianBinaryReader r = new EndianBinaryReader(File.Open(args[0], FileMode.Open), Endianness.LittleEndian))
            {
                VitaMusicInfo.Read(r);
                Console.ReadKey();
            }
        }
    }
}
