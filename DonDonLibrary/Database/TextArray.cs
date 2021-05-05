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
    }

    public class TextArray
    {
        public string signature;
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

        public XmlDocument ToXml()
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateElement("TextArray"));

            doc.DocumentElement.SetAttribute("Signature", this.signature);

            foreach (TextEntry entry in this.entries)
            {
                XmlElement entryNode = doc.CreateElement("Text");

                entryNode.SetAttribute("Id", entry.name);
                entryNode.InnerText = entry.displayName.Replace("\n", "\\n");

                doc.DocumentElement.AppendChild(entryNode);
            }

            return doc;
        }
    }
}
