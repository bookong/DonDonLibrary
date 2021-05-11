using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonDonLibrary.Database;
using DonDonLibrary.IO;

namespace DatabaseEditor
{
    class Program
    {
        private static string ReadType()
        {
            Console.WriteLine("+----------------------------+\n| [1]. Text                  |\n| [2]. MusicInfo             |\n+----------------------------+");
            Console.Write("> ");
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            string originFilename = null;
            string destFilename = null;
            DatabaseType type = DatabaseType._;
            bool isWriteMode = false;

            // Argument parse
            foreach(string arg in args)
            {
                if (arg.ToLower() == "-m")
                    type = DatabaseType.Music;
                else if (arg.ToLower() == "-t")
                    type = DatabaseType.Text;
                else
                {
                    if (originFilename == null) originFilename = arg;
                    else if (destFilename == null) originFilename = arg;
                }
            }

            Console.Clear();

            if (originFilename == null)
            {
                Console.WriteLine("==============================");
                Console.WriteLine("Usage:\n");
                Console.WriteLine("DatabaseEditor.exe input.dat [TYPE] [output.xml]");
                Console.WriteLine("Or");
                Console.WriteLine("DatabaseEditor.exe input.xml [input.dat]");
                Console.WriteLine("\nPlease, DO NOT edit the \"Signature\" element within the XML. It affects how the program writes the data.");
                Console.WriteLine("\n* Arguments between square brackets are optional.");
                Console.WriteLine("==============================");
                Environment.Exit(-1);
            }
            else if (destFilename == null)
                if (originFilename.ToLower().EndsWith("dat"))
                    destFilename = Path.ChangeExtension(originFilename, "xml");
                else if (originFilename.ToLower().EndsWith("xml"))
                {
                    destFilename = Path.ChangeExtension(originFilename, "dat");
                    isWriteMode = true;
                }

            if (!isWriteMode)
            {
                if (type == DatabaseType._)
                {
                    string _type = ReadType();

                    while (_type != "1" && _type != "2")
                    {
                        Console.Clear();
                        _type = ReadType();
                    }

                    if (_type == "1")
                        type = DatabaseType.Text;
                    else if (_type == "2")
                        type = DatabaseType.Music;
                }
            }

            if (!isWriteMode)
            {
                using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(originFilename, FileMode.Open), Encoding.UTF8, Endianness.LittleEndian))
                {
                    if (type == DatabaseType.Text)
                    {
                        TextArray data = new TextArray();
                        data.Read(reader);
                        using (TextWriter tw = new StreamWriter(destFilename, false, Encoding.UTF8))
                            data.ToXml().Save(tw);
                    }
                    else if (type == DatabaseType.Music)
                    {
                        MusicInfo data = new MusicInfo();
                        data.Read(reader);
                        using (TextWriter tw = new StreamWriter(destFilename, false, Encoding.UTF8))
                            data.ToXml().Save(tw);
                    }
                }
            }
            else
            {
                using(EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(destFilename, FileMode.Create), Encoding.UTF8, Endianness.LittleEndian))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(originFilename);

                    if (doc.DocumentElement.GetAttribute("Format") == "0_text")
                    {
                        TextArray data = new TextArray().FromXml(doc);
                        data.Write(writer);
                    }
                    else if(doc.DocumentElement.GetAttribute("Format") == "0_mus")
                    {
                        MusicInfo data = new MusicInfo().FromXml(doc);
                        data.Write(writer);
                    }
                }
            }
        }
    }
}
