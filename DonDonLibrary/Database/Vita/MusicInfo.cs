using System;
using System.Collections.Generic;
using DonDonLibrary.IO;

namespace DonDonLibrary.Database.Vita
{
    public class VitaMusicInfo
    {
        public static MusicEntry[] Read(EndianBinaryReader reader)
        {
            int musicEntryCount = reader.ReadInt32();
            reader.SeekBegin(reader.ReadInt32());

            MusicEntry[] musicEntries = new MusicEntry[musicEntryCount];
            List<int> _offsets = new List<int>();

            for(int i = 0; i < musicEntryCount; i++)
            {
                MusicEntry musicEntry = new MusicEntry();

                _offsets.Add(reader.ReadInt32());
                musicEntry.previewStart = reader.ReadInt32();
                musicEntry.offset = reader.ReadInt32();

                musicEntries[i] = musicEntry;
            }

            // Looping again to get filenames
            for(int i = 0; i < _offsets.Count; i++)
            {
                reader.SeekBegin(_offsets[i]);

                musicEntries[i].name = reader.ReadString(StringBinaryFormat.NullTerminated);

                //Console.WriteLine($"[{musicEntries[i].name}, {musicEntries[i].previewStart}, {musicEntries[i].offset}]");
            }

            return musicEntries;
        }
    }
}
