using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonDonLibrary.Chart
{
    public class Note
    {
        public int type;
        public float time;
        public int unk = 0;
        public int unk1 = 0;
        public short basePoint;
        public short addPoint;
        public float rollHoldTime = 0.0f;
        public float _absoluteTime;
        public int unk2 = 0;
        public int unk3 = 0;
    }

    public class SubTrack
    {
        internal short noteCount;
        internal short unk;
        public float scrollSpeed = 1.0f;
        public List<Note> notes = new List<Note>();
    }

    public class Track
    {
        public float bpm;
        public float time;
        public bool gogo;
        public byte trackLine;
        public short unk;
        public int[] bunkis;
        public int unk1;
        public SubTrack normalTrack = new SubTrack();
        public SubTrack professionalTrack = new SubTrack();
        public SubTrack masterTrack = new SubTrack();
    }

    public struct Header
    {
        public float[] hanteiData;
        public int[] unknownHeaderData;
        public int trackCount;
        public int unk;
    }

    public class Gen3
    {
        public Header header;
        public List<Track> tracks = new List<Track>();
    }
}
