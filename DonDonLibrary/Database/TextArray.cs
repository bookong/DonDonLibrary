using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Database
{
    public class TextEntry
    {
        public string name;
        public string displayName;

        public void Read(EndianBinaryReader reader, uint nameOff, uint dispOff)
        {
            long seek = reader.Position;

            reader.SeekBegin(nameOff);
            this.name = reader.ReadString(StringBinaryFormat.Padded16);
            reader.SeekBegin(dispOff);
            this.displayName = reader.ReadString(StringBinaryFormat.Padded16);
            reader.SeekBegin(seek);
        }

        public void Write(EndianBinaryWriter writer, int index)
        {
            long offset = 32 + (8 * index);

            long nameOffset = writer.Position;
            writer.Write(name, StringBinaryFormat.Padded16);
            long displayOffset = writer.Position;
            writer.Write(displayName, StringBinaryFormat.Padded16);
            long curOffset = writer.Position;

            writer.SeekBegin(offset);
            writer.Write((uint)nameOffset);
            writer.Write((uint)displayOffset);
            writer.SeekBegin(curOffset);
        }
    }

    public class TextArray
    {
        public string signature = "dokodon!!";
        public TextEntry[] entries;

        public void Read(EndianBinaryReader reader)
        {
            int dataCount = reader.ReadInt32();
            int signatureOffset = reader.ReadInt32();
            int dataOffset = reader.ReadInt32();

            entries = new TextEntry[dataCount];

            reader.SeekBegin(signatureOffset);
            this.signature = reader.ReadString(StringBinaryFormat.Padded16);
            reader.SeekBegin(dataOffset);

            for (int i = 0; i < dataCount; i++)
            {
                TextEntry entry = new TextEntry();
                entry.Read(reader, reader.ReadUInt32(), reader.ReadUInt32());

                this.entries[i] = entry;
            }
        }

        public void Write(EndianBinaryWriter writer)
        {
            int dummyLength = this.entries.Length * 2 * 4;
            while (dummyLength % 16 != 0)
                dummyLength += 4;

            int[] dummy = new int[dummyLength / 4];
            writer.Write(this.entries.Length);
            writer.Write(0x10);
            writer.Write(0x20);
            writer.Write(0x00);
            writer.Write(this.signature, StringBinaryFormat.Padded16);
            writer.Write(dummy);
            for (int i = 0; i < this.entries.Length; i++)
                this.entries[i].Write(writer, i);
        }

        public XmlDocument ToXml()
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateElement("TextArray"));

            doc.DocumentElement.SetAttribute("Signature", this.signature);
            doc.DocumentElement.SetAttribute("Format", "0_text");

            foreach (TextEntry entry in this.entries)
            {
                XmlElement entryNode = doc.CreateElement("Text");

                entryNode.SetAttribute("Id", entry.name);
                entryNode.InnerText = entry.displayName.Replace("\n", "\\n");

                doc.DocumentElement.AppendChild(entryNode);
            }

            return doc;
        }

        public TextArray FromXml(XmlDocument doc) { return Parse.FromXml<TextArray>(doc); }
    }
}
