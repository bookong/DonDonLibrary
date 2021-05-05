using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Database
{
    public class MusicEntry
    {
        public string name;
        public int previewStart; // menu preview start time in milliseconds
        public int unknown;

        public MusicEntry Read(EndianBinaryReader reader)
        {
            int nameOffset = reader.ReadInt32();

            this.previewStart = reader.ReadInt32();
            this.unknown = reader.ReadInt32();

            long seek = reader.Position;
            reader.SeekBegin(nameOffset);
            this.name = reader.ReadString(StringBinaryFormat.Padded16);

            reader.SeekBegin(seek);

            return this;
        }
    }

    public class MusicInfo
    {
        public MusicEntry[] musicEntries;

        public void Read(EndianBinaryReader reader)
        {
            int dataCount = reader.ReadInt32();
            int dataOffset = reader.ReadInt32();

            reader.SeekBegin(dataOffset);
            this.musicEntries = new MusicEntry[dataCount];

            for(int i = 0; i < dataCount; i++)
                this.musicEntries[i] = new MusicEntry().Read(reader);

            reader.Align(16);
        }

        public XmlDocument ToXml()
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateElement("MusicArray"));

            foreach(MusicEntry entry in this.musicEntries)
            {
                XmlElement entryNode = doc.CreateElement("Music");
                XmlElement nameNode = doc.CreateElement("Name");
                XmlElement previewNode = doc.CreateElement("PreviewTime");
                XmlElement unkNode = doc.CreateElement("Unknown");

                nameNode.InnerText = entry.name;
                previewNode.InnerText = entry.previewStart.ToString();
                unkNode.InnerText = entry.unknown.ToString();

                entryNode.AppendChild(nameNode);
                entryNode.AppendChild(previewNode);
                entryNode.AppendChild(unkNode);

                doc.DocumentElement.AppendChild(entryNode);
            }

            return doc;
        }
    }
}
