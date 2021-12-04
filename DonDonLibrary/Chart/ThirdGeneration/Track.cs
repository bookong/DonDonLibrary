using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Fumen.ThirdGeneration
{
    public class Track
    {
        public float BPM;

        // In milliseconds
        public float Time;

        public bool Gogo;
        public bool BarlineVisible;

        // Not sure what are this for? They're 6 ints in sequence
        public int[] Bunkis;

        public float ScrollSpeed;
        public float ExpertScrollSpeed;
        public float MasterScrollSpeed;

        public List<Note> Notes;
        public List<Note> ExpertNotes;
        public List<Note> MasterNotes;

        public static Track Read(EndianBinaryReader reader)
        {
            Track track = new Track();

            track.BPM = reader.ReadSingle();
            track.Time = reader.ReadSingle();

            track.Gogo = reader.ReadBoolean();
            track.BarlineVisible = reader.ReadBoolean();

            Console.WriteLine($"{track.BPM} | {track.Gogo} | {track.BarlineVisible} @ {track.Time}");

            // Padding
            reader.SeekCurrent(2);

            track.Bunkis = new int[6];
            for (int i = 0; i < 6; i++)
                track.Bunkis[i] = reader.ReadInt32();

            // More padding?
            reader.SeekCurrent(4);



            // Initializing the lists before reading data
            track.Notes = new List<Note>();
            track.ExpertNotes = new List<Note>();
            track.MasterNotes = new List<Note>();

            short noteCount = reader.ReadInt16();

            // Guess what?
            // I just really wonder why did they just use an int16 there and fill the other bytes with junk
            // Like... What
            reader.SeekCurrent(2);

            track.ScrollSpeed = reader.ReadSingle();

            for (short s = 0; s < noteCount; s++)
                track.Notes.Add(Note.Read(reader));

            // Same thing for Expert...
            short noteCountEx = reader.ReadInt16();

            reader.SeekCurrent(2);

            track.ExpertScrollSpeed = reader.ReadSingle();

            for (short s = 0; s < noteCountEx; s++)
                track.ExpertNotes.Add(Note.Read(reader));

            // ...and Master
            short noteCountMaster = reader.ReadInt16();
            reader.SeekCurrent(2);

            track.MasterScrollSpeed = reader.ReadSingle();

            for (short s = 0; s < noteCountMaster; s++)
                track.MasterNotes.Add(Note.Read(reader));

            Console.WriteLine(reader.Position);
            return track;
        }
    }
}
