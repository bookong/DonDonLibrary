using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Fumen.ThirdGeneration
{
    public class Note
    {
        public NoteType Type;

        // This time is relative to the time of the track where the note is.
        // So if this time is, for instance, 3000ms and the track time is 13400ms, then the absolute time where the note
        // will appear on screen is 16400ms (12400 + 3000) from the start of the song
        public float Time;

        // In the same place where this value is stored, the ballon hit count is also stored if the note is a balloon
        // And I just ask myself... WHY. Seriously. Be normal N*mco. Get a psychiatrist.
        public short BaseScoreGain;
        public short ScoreDifference;

        // Length of the note (in milliseconds) for drumroll and baloon
        public float Length;

        public static Note Read(EndianBinaryReader reader)
        {
            Note note = new Note();

            note.Type = (NoteType)reader.ReadInt32();
            note.Time = reader.ReadSingle();

            reader.SeekCurrent(4);

            note.BaseScoreGain = reader.ReadInt16();
            note.ScoreDifference = reader.ReadInt16();

            reader.SeekCurrent(4);

            note.Length = reader.ReadSingle();

            // Drumroll has 8 extra bytes after the length
            // they're always 0 though, not sure for what are they used (probably nothing)
            if(note.Type == NoteType.Drumroll || note.Type == NoteType.LargeDrumroll)
                reader.SeekCurrent(8);

            return note;
        }
    }
}
