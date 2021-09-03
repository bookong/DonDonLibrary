using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;
using System.IO;

namespace DonDonLibrary.Chart
{
    public class Note
    {
        public int type;
        public float time;
        public int unk = 0;
        public int unk1 = 0;
        public short basePoint = 600;
        public short addPoint = 600;
        public float rollHoldTime = 0.0f;
        public short balloonHitCount = -1;
        public float _absoluteTime;
        public int unk2 = 0;
        public int unk3 = 0;
    }

    public class SubTrack
    {
        internal short noteCount;
        internal short unk = 0;
        public float scrollSpeed = 1.0f;
        public List<Note> notes = new List<Note>();
    }

    public class Track
    {
        public float bpm = 160f;
        public float time = 0f;
        public bool gogo = false;
        public byte trackLine = 0x01;
        public short unk = 0;
        public int[] bunkis = new int[] { -1, -1, -1, -1, -1, -1 };
        public int unk1 = 0;
        public SubTrack normalTrack = new SubTrack();
        public SubTrack professionalTrack = new SubTrack();
        public SubTrack masterTrack = new SubTrack();
    }

    public struct Header
    {
        public float[] hanteiData;
        public int[] headerData;
        public int trackCount;
        public int unk;
    }

    public class Gen3Fumen
    {
        public Header header;
        public List<Track> tracks = new List<Track>();
    }

    public class Gen3
    {
        internal static List<Note> ReadNote(EndianBinaryReader reader, ushort _noteCount, float _trackTime)
        {
            List<Note> _notes = new List<Note>();
            for (int j = 0; j < _noteCount; j++)
            {
                Note note = new Note();
                note.type = reader.ReadInt32();
                note.time = reader.ReadSingle();
                reader.SeekCurrent(8);
                note.basePoint = reader.ReadInt16();
                note.addPoint = reader.ReadInt16();
                note.rollHoldTime = reader.ReadSingle();
                _notes.Add(note);

                if (note.type == 6 || note.type == 9)
                    reader.SeekCurrent(8);
                else if (note.type == 10)
                    note.balloonHitCount = note.basePoint;

                note._absoluteTime = note.time + _trackTime;
            }
            return _notes;
        }

        public static Gen3Fumen Read(EndianBinaryReader reader)
        {
            Gen3Fumen fumenData = new Gen3Fumen();

            // reading the header data
            fumenData.header.hanteiData = reader.ReadSingles(108);
            fumenData.header.headerData = reader.ReadInt32s(20);
            //reader.SeekBegin(0);
            //reader.ReadBytes(512).ToString();
            int trackCount = reader.ReadInt32();
            reader.SeekCurrent(4);

            // reading the actual track data

            for (int i = 0; i < trackCount; i++)
            {
                Track track = new Track();
                track.bpm = reader.ReadSingle();
                track.time = reader.ReadSingle();
                track.gogo = reader.ReadByte() == 0x01;
                track.trackLine = reader.ReadByte();
                track.unk = reader.ReadInt16();
                track.bunkis = reader.ReadInt32s(6);
                track.unk1 = reader.ReadInt32();

                // read subtracks
                ushort _noteCountNormal = reader.ReadUInt16();
                reader.SeekCurrent(2);
                track.normalTrack.scrollSpeed = reader.ReadSingle();
                track.normalTrack.notes = ReadNote(reader, _noteCountNormal, track.time);

                ushort _noteCountProf = reader.ReadUInt16();
                reader.SeekCurrent(2);
                track.professionalTrack.scrollSpeed = reader.ReadSingle();
                track.professionalTrack.notes = ReadNote(reader, _noteCountProf, track.time);

                ushort _noteCountMaster = reader.ReadUInt16();
                reader.SeekCurrent(2);
                track.masterTrack.scrollSpeed = reader.ReadSingle();
                track.masterTrack.notes = ReadNote(reader, _noteCountMaster, track.time);

                fumenData.tracks.Add(track);
            }

            //Console.WriteLine(reader.Position);

            return fumenData;
        }

        public static void Write(EndianBinaryWriter writer, Gen3Fumen fumenData)
        {
            SubTrack curSubTrack = new SubTrack();

            for (int i = 0; i < 108; i++)
                writer.Write(fumenData.header.hanteiData[i]);
            foreach (int i in fumenData.header.headerData)
                writer.Write(i);

            writer.Write(fumenData.tracks.Count);
            writer.Write(0x563740);
            foreach (Track track in fumenData.tracks)
            {
                writer.Write(track.bpm);
                writer.Write(track.time);
                if (track.gogo)
                    writer.Write((byte)0x01);
                else
                    writer.Write((byte)0x00);
                writer.Write(track.trackLine);
                writer.Write((short)0x00);
                for (int i = 0; i < 6; i++)
                    writer.Write(-1);
                writer.Write(0xF4FC12);

                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            curSubTrack = track.normalTrack;
                            break;
                        case 1:
                            curSubTrack = track.professionalTrack;
                            break;
                        case 2:
                            curSubTrack = track.masterTrack;
                            break;
                    }

                    writer.Write((short)curSubTrack.notes.Count);
                    writer.Write((short)0x00);
                    writer.Write(curSubTrack.scrollSpeed);
                    foreach (Note note in curSubTrack.notes)
                    {
                        writer.Write(note.type);
                        writer.Write(note.time);
                        writer.Write((double)0x00);
                        if (note.type == 10)
                        {
                            writer.Write(note.balloonHitCount);
                            writer.Write((short)0x00);
                        }
                        else
                        {
                            writer.Write(note.basePoint);
                            writer.Write(note.addPoint);
                        }
                        writer.Write(note.rollHoldTime);
                        if (note.type == 6 || note.type == 9)
                            writer.Write((double)0x00);
                    }
                }
            }
        }
    }
}
