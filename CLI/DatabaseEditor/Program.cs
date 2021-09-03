using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary;
using DonDonLibrary.Database;
using DonDonLibrary.Database.Vita;
using DonDonLibrary.IO;

namespace DatabaseEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseFormat fmt = DatabaseFormat.MusicInfo;
            GameFormat game = GameFormat.Vita;
            string filename = String.Empty;

            bool isFromJson = false;


            foreach(string arg in args)
            {
                if(arg.StartsWith("-gf") || arg.StartsWith("--game-format"))
                {
                    switch(arg.Split('=')[0].ToLower())
                    {
                        case "vita":
                            game = GameFormat.Vita;
                            break;
                        default:
                            Console.WriteLine("Unknown game format!");
                            break;
                    }
                }
                else if(filename == String.Empty) { filename = arg; if (filename.EndsWith(".json")) isFromJson = true; }
            }

            if(fmt == DatabaseFormat.MusicInfo)
            {
                EndianBinaryReader r = new EndianBinaryReader(File.Open(filename, FileMode.Open), Endianness.LittleEndian);
                MusicEntry[] musicEntries;

                if (isFromJson)
                {
                    JsonConverter.FromJson(filename);
                    Console.ReadKey();
                }

                if (game == GameFormat.Vita)
                {
                    if (!isFromJson)
                    {
                        musicEntries = VitaMusicInfo.Read(r);
                        JsonConverter.ToJson(musicEntries, Path.ChangeExtension(filename, "json"));
                    }
                }
            }


        }
    }
}
