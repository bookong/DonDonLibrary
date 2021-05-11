using System;
using System.Reflection;
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
        public int offset;
        public int _nameOffset;

        public MusicEntry Read(EndianBinaryReader reader)
        {
            int nameOffset = reader.ReadInt32();

            this.previewStart = reader.ReadInt32();
            this.offset = reader.ReadInt32();

            long seek = reader.Position;
            reader.SeekBegin(nameOffset);
            this.name = reader.ReadString(StringBinaryFormat.Padded16);

            reader.SeekBegin(seek);

            return this;
        }

        public void Write(EndianBinaryWriter writer)
        {

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

        public void Write(EndianBinaryWriter writer)
        {
            int dummyLength = 3 * musicEntries.Length * 4;
            while (dummyLength % 16 != 0) dummyLength++;

            byte[] dummyData = new byte[dummyLength];

            writer.Write(musicEntries.Length);
            writer.Write(0x10);
            writer.Write((long)0x00);
            writer.Write(dummyData);

            int i = 0;
            foreach(MusicEntry entry in musicEntries)
            {
                musicEntries[i]._nameOffset = (int)writer.Position;
                writer.Write(entry.name, StringBinaryFormat.Padded16);
                i++;
            }

            writer.SeekBegin(16);
            foreach (MusicEntry entry in musicEntries)
            {
                writer.Write(entry._nameOffset);
                writer.Write(entry.previewStart);
                writer.Write(entry.offset);
            }
        }

        public XmlDocument ToXml()
        {
            XmlDocument doc = new XmlDocument();

            doc.AppendChild(doc.CreateElement("MusicArray"));
            doc.DocumentElement.SetAttribute("Format", "0_mus");

            foreach(MusicEntry entry in this.musicEntries)
            {
                XmlElement entryNode = doc.CreateElement("Music");
                XmlElement nameNode = doc.CreateElement("Name");
                XmlElement previewNode = doc.CreateElement("PreviewTime");
                XmlElement offNode = doc.CreateElement("Offset");

                nameNode.InnerText = entry.name;
                previewNode.InnerText = entry.previewStart.ToString();
                offNode.InnerText = entry.offset.ToString();

                entryNode.AppendChild(nameNode);
                entryNode.AppendChild(previewNode);
                entryNode.AppendChild(offNode);

                doc.DocumentElement.AppendChild(entryNode);
            }

            return doc;
        }

        public MusicInfo FromXml(XmlDocument doc) => Parse.FromXml<MusicInfo>(doc);
    }
}
