using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;

namespace DonDonLibrary.Chart
{
    public class Fumen
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
                if(note.type == 6 || note.type == 9)
                {
                    reader.SeekCurrent(8);
                }

                note._absoluteTime = note.time + _trackTime;
            }
            return _notes;
        }

        public static Gen3 Read(EndianBinaryReader reader)
        {
            Gen3 fumenData = new Gen3();

            // reading the header data
            fumenData.header.hanteiData = reader.ReadSingles(108);
            fumenData.header.unknownHeaderData = reader.ReadInt32s(20);
            //reader.SeekBegin(0);
            //reader.ReadBytes(512).ToString();
            int trackCount = reader.ReadInt32();
            reader.SeekCurrent(4);

            // reading the actual track data

            for(int i = 0; i < trackCount; i++)
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

        public static void Write(EndianBinaryWriter writer, Gen3 fumenData)
        {
            SubTrack curSubTrack = new SubTrack();

            if (File.Exists("OPENFILECACHE"))
            {
                using (var reader = new EndianBinaryReader(File.Open("OPENFILECACHE", FileMode.Open), Endianness.LittleEndian))
                {
                    writer.Write(reader.ReadBytes(512));
                }
            }
            else
            {
                for (int i = 0; i < 512; i++)
                {
                    writer.Write((byte)0x00);
                }
            }

            writer.Write(fumenData.tracks.Count);
            writer.Write(0x563740);
            foreach(Track track in fumenData.tracks)
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

                for(int i = 0; i < 3; i++)
                {
                    switch(i)
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
                    foreach(Note note in curSubTrack.notes)
                    {
                        writer.Write(note.type);
                        writer.Write(note.time);
                        writer.Write((double)0x00);
                        writer.Write(note.basePoint);
                        writer.Write(note.addPoint);
                        writer.Write(note.rollHoldTime);
                        if (note.type == 6 || note.type == 9)
                            writer.Write((double)0x00);
                    }
                }
            }
        }

        internal static Track NewTrack(float bpm)
        {
            Track newTrack = new Track();
            newTrack.normalTrack = new SubTrack();
            newTrack.professionalTrack = new SubTrack();
            newTrack.masterTrack = new SubTrack();
            newTrack.normalTrack.notes = new List<Note>();
            newTrack.professionalTrack.notes = new List<Note>();
            newTrack.masterTrack.notes = new List<Note>();
            newTrack.bpm = bpm;

            return newTrack;
        }

        public static Gen3 GenMeasureDivision(Gen3 fumenData)
        {
            float measureLength = (60 / fumenData.tracks[0].bpm) * 4;
            float curMeasure = measureLength;

            // process all notes into other lists
            Gen3 divFumen = new Gen3();
            Track curTrack = new Track();
            curTrack.bpm = fumenData.tracks[0].bpm;
            
            List<Note> normalNotes = new List<Note>();
            List<Note> profNotes = new List<Note>();
            List<Note> masterNotes = new List<Note>();
            foreach(Track track in fumenData.tracks)
            {
                foreach(Note note in track.normalTrack.notes) { normalNotes.Add(note); }
                foreach(Note note in track.professionalTrack.notes) { profNotes.Add(note); }
                foreach(Note note in track.masterTrack.notes) { masterNotes.Add(note); }
            }
            // divide
            foreach(Note note in normalNotes)
            {
                Note _note = note;
                if(note.time > curMeasure)
                {
                    divFumen.tracks.Add(curTrack);
                    curTrack = new Track();
                    curTrack.bpm = fumenData.tracks[0].bpm;
                    curMeasure += measureLength;
                    curTrack.time = curMeasure;
                }

                _note.time = note._absoluteTime - curTrack.time;
                curTrack.normalTrack.notes.Add(_note);
            }

            return divFumen;
        }

        public static Gen3 FromTja(string path)
        {
            char[] stNote = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Gen3 fumenData = new Gen3();
            Track curTrack = new Track();

            float bpm = 0.0f;
            string[] lines = File.ReadAllLines(path);
            string[] split;
            string[] split1;
            bool metaSection = true;
            int measure = 4;
            float milliPerMeasure = 0.0f;
            float time = 0.0f;
            float measureDivision = 0.0f;
            float delta = 0.0f;

            int basePoint = 0;
            int addPoint = 0;

            foreach (string line in lines)
            {
                if (metaSection) { split = line.Split(':'); }
                else { split = line.Split(','); }

                //foreach(string item in split) { Console.Write(item + " "); }

                if (metaSection)
                {
                    if (line.StartsWith("BPM"))
                        bpm = float.Parse(split[1]);
                    else if (line.StartsWith("SCOREINIT"))
                        basePoint = int.Parse(split[1]);
                    else if (line.StartsWith("SCOREDIFF"))
                        addPoint = int.Parse(split[1]);
                    else if (line.StartsWith("#START"))
                        metaSection = false;

                    curTrack.bpm = bpm;
                    curTrack.trackLine = 1;
                }
                else
                {
                    if (line.Length == 0) continue;

                    if (line.StartsWith("#END"))
                    {
                        fumenData.tracks.Add(curTrack);
                        break;
                    }
                    else if (line.StartsWith("#BPMCHANGE"))
                    {
                        fumenData.tracks.Add(curTrack);
                        bpm = float.Parse(line.Split(' ')[1]);
                        curTrack = new Track();
                        curTrack.bpm = bpm;
                        curTrack.trackLine = 1;
                        curTrack.time = time;
                        delta = 0.0f;
                    }
                    else if (line.StartsWith("#MEASURE"))
                        measure = int.Parse(line.Split(' ')[1].Split('/')[0]);
                    else if (stNote.Contains(line[0]))
                    {
                        milliPerMeasure = 60000 * measure * 4 / bpm;
                        split = line.Split(',');
                        foreach(string notes in split)
                        {
                            if (notes.Length == 0) continue;
                            if (!notes.StartsWith("//"))
                            {
                                measureDivision = milliPerMeasure / notes.Length;
                                for (int i = 0; i < notes.Length; i++)
                                {
                                    if (notes[i] == '0') continue;
                                    else
                                    {
                                        Note note = new Note();
                                        note.addPoint = (short)addPoint;
                                        note.basePoint = (short)basePoint;
                                        note.time = (measureDivision * i) + delta;
                                        note.type = 1;
                                        curTrack.normalTrack.notes.Add(note);
                                    }
                                }
                            }
                            delta += milliPerMeasure;
                        }

                        time += milliPerMeasure;
                    }
                    else
                        continue;
                }
            }

            return fumenData;
        }
    }
}
