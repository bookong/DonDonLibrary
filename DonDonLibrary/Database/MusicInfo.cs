using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Database
{
    public struct MusicEntry
    {
        public string name;
        public int unknown;
        public int previewStartTime;
        public int __nameOffset;
        public bool __isDel;
    }

    public class MusicInfo
    {
        public static MusicEntry[] Read(EndianBinaryReader reader)
        {
            long _curOffset;

            int musicEntryCount = reader.ReadInt32();
            int _dataOffset = reader.ReadInt32();
            reader.SeekBegin(_dataOffset);

            MusicEntry[] musicEntries = new MusicEntry[musicEntryCount];

            for(int i = 0; i < musicEntryCount; i++)
            {
                MusicEntry musicEntry = new MusicEntry();
                musicEntry.__nameOffset = reader.ReadInt32();
                musicEntry.previewStartTime = reader.ReadInt32();
                musicEntry.unknown = reader.ReadInt32();

                _curOffset = reader.Position;

                reader.SeekBegin(musicEntry.__nameOffset);
                musicEntry.name = reader.ReadString(StringBinaryFormat.Padded16);

                reader.SeekBegin(_curOffset);
                musicEntries[i] = musicEntry;
            }
            while (reader.Position % 16 != 0)
                reader.SeekCurrent(1);

            return musicEntries;
        }
    }
}
