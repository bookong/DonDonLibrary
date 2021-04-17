using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Chart.Gen2
{
    public class Header
    {
        public uint trackCount = 0;
        public uint noteOffset = 0;
        public uint noteCount = 0;
        public int _ = 0;
    }

    public class SubTrack
    {
        public int noteIndexSt;
        public int noteCount;
        public int pointGain;
    }

    public class Track
    {
        public float time;
        public float bpm;
        public int _unk;
        public int gogoFlag;
        public int[] bunkis = new int[6];
        public float[] scrollSpeeds = new float[6];
        public SubTrack[] subtracks = new SubTrack[6];
    }

    public class Note
    {
        public int type;
        public int measure;
        public int balloonHitCount;
        public int longNoteLength;
    }

    public class Gen2Fumen
    {
        public Header headerData = new Header();
        public List<Track> tracks = new List<Track>();
        public List<Note> notes = new List<Note>();
    }

    public class Gen2
    {
        public static Gen2Fumen Read(EndianBinaryReader reader)
        {
            Gen2Fumen fumen = new Gen2Fumen();

            fumen.headerData.trackCount = reader.ReadUInt32();
            fumen.headerData.noteOffset = reader.ReadUInt32();
            fumen.headerData.noteCount = reader.ReadUInt32();
            fumen.headerData._ = reader.ReadInt32();
            for(int i = 0; i < fumen.headerData.trackCount; i++)
            {
                Console.WriteLine(i);
                Track track = new Track();

                track.time = reader.ReadSingle();
                track.bpm = reader.ReadSingle();
                track._unk = reader.ReadInt32();
                track.gogoFlag = reader.ReadInt32();
                for (int j = 0; j < 6; j++)
                    track.bunkis[j] = reader.ReadInt32();
                for (int j = 0; j < 6; j++)
                    track.scrollSpeeds[j] = reader.ReadSingle();
                for(int j = 0; j < 6; j++)
                {
                    SubTrack subtrack = new SubTrack();
                    subtrack.noteIndexSt = reader.ReadInt32();
                    subtrack.noteCount = reader.ReadInt32();
                    subtrack.pointGain = reader.ReadInt32();
                    track.subtracks[j] = subtrack;
                }
                fumen.tracks.Add(track);
            }

            reader.SeekBegin(fumen.headerData.noteOffset);

            for(int i = 0; i < fumen.headerData.noteCount; i++)
            {
                Note note = new Note();

                note.type = reader.ReadInt32();
                note.measure = reader.ReadInt32();
                note.balloonHitCount = reader.ReadInt32();
                note.longNoteLength = reader.ReadInt32();

                fumen.notes.Add(note);
            }

            return fumen;
        }
    }
}
