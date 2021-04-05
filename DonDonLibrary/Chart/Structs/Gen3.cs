using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonDonLibrary.Chart
{
    public struct Note
    {
        public int type;
        public float time;
        public int unk;
        public int unk1;
        public short basePoint;
        public short addPoint;
        public float rollHoldTime;
        public float _absoluteTime;
        public int unk2;
        public int unk3;
    }

    public struct SubTrack
    {
        internal short noteCount;
        internal short unk;
        public float scrollSpeed;
        public List<Note> notes;
    }

    public struct Track
    {
        public float bpm;
        public float time;
        public bool gogo;
        public byte trackLine;
        public short unk;
        public int[] bunkis;
        public int unk1;
        public SubTrack normalTrack;
        public SubTrack professionalTrack;
        public SubTrack masterTrack;
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
