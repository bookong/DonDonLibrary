using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.Utilities;
using DonDonLibrary.IO;

namespace DonDonLibrary.Chart
{
    /* UTS stands for "Universal Taiko Script format"
     * Heavily based on DIVAScript from the Project DIVA games.
     * Created by me */
    public class UTS
    {
        public static string[] GetCommandArgs(string cmd)
        {
            cmd = cmd.Trim(';');
            cmd = cmd.Trim(')');
            Console.WriteLine(cmd);
            string[] cmd_spl = cmd.Split('(');
            cmd_spl[1] = cmd_spl[1].Trim();

            return cmd_spl[1].Split(',');
        }

        public static string[] FromFumen(Gen3 fumenData)
        {
            List<String> uts = new List<string>();
            bool firstTrack = true;
            // -1000 to assure that the time will always be greater than
            float time = -1000;
            int basePnt = -1;
            int addPnt = -1;
            SubTrack curSubTrack = new SubTrack();
            string[] subTrackLabels = { "NORMAL", "PROFESSIONAL", "MASTER" };

            foreach (Track track in fumenData.tracks)
            {
                time = -1000;

                if (!firstTrack)
                    uts.Add("TRACK_END();");

                int gogo_flag = track.gogo ? 1 : 0;
                uts.Add($"TRACK_START({StringFormat.Stringify(track.bpm)}, {StringFormat.Stringify(track.time)}, {gogo_flag}, {track.trackLine});");

                for (int i = 0; i < 3; i++)
                {
                    time = -1000;
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

                    uts.Add($"SUBTRACK_START({subTrackLabels[i]}, {StringFormat.Stringify(curSubTrack.scrollSpeed)});");

                    foreach (Note note in curSubTrack.notes)
                    {
                        if (note.time > time)
                        {
                            uts.Add($"TIME({StringFormat.Stringify(note.time)});");
                            time = note.time;
                        }
                        if (note.basePoint != basePnt)
                        {
                            uts.Add($"SET_POINT_BASE({note.basePoint});");
                            basePnt = note.basePoint;
                        }
                        if (note.addPoint != addPnt)
                        {
                            uts.Add($"SET_POINT_INCREASE({note.addPoint});");
                            addPnt = note.addPoint;
                        }
                        uts.Add($"NOTE({note.type}, {StringFormat.Stringify(note.rollHoldTime)});");
                    }

                    uts.Add("SUBTRACK_END();");
                }

                if (firstTrack)
                    firstTrack = false;
            }
            uts.Add("TRACK_END();");

            return uts.ToArray();
        }

        public static Gen3 ToFumen(string[] uts)
        {
            Gen3 fumenData = new Gen3();
            
            Track curTrack = new Track();
            SubTrack curSubTrack = new SubTrack();
            List<Note> notes = new List<Note>();
            string curSubTrackType = String.Empty;
            float time = 0;
            short pointBase = 0;
            short pointAdd = 0;

            foreach(string rawcmd in uts)
            {
                string cmd = rawcmd.ToUpper();
                string[] args = GetCommandArgs(cmd);

                if (cmd.StartsWith("TRACK_START"))
                {
                    Track track = new Track();
                    track.bpm = StringFormat.Destringify(args[0]);
                    track.time = StringFormat.Destringify(args[1]);
                    track.gogo = args[2] == "1";
                    track.trackLine = byte.Parse(args[3]);
                    track.unk = 0;
                    track.unk1 = 0;
                    track.bunkis = new int[6];
                    for (int i = 0; i < 6; i++)
                    {
                        track.bunkis[i] = -1;
                    }

                    curTrack = track;
                }
                else if (cmd.StartsWith("SUBTRACK_START"))
                {
                    curSubTrack = new SubTrack();
                    curSubTrackType = args[0].ToUpper();
                    curSubTrack.scrollSpeed = StringFormat.Destringify(args[1]);
                    if (args[0] != "NORMAL" && args[0] != "PROFESSIONAL" && args[0] != "MASTER")
                    {
                        throw new Exception("Unknown SubTrack type.");
                    }
                }
                else if (cmd.StartsWith("TIME"))
                {
                    time = StringFormat.Destringify(args[0]);
                }
                else if (cmd.StartsWith("SET_POINT_BASE"))
                {
                    pointBase = short.Parse(args[0]);
                }
                else if (cmd.StartsWith("SET_POINT_INCREASE"))
                {
                    pointAdd = short.Parse(args[0]);
                }
                else if (cmd.StartsWith("NOTE"))
                {
                    Note note = new Note();
                    note.type = int.Parse(args[0]);
                    note.time = time;
                    note.rollHoldTime = StringFormat.Destringify(args[1]);
                    note.basePoint = pointBase;
                    note.addPoint = pointAdd;
                    note.unk = 0;
                    note.unk1 = 0;
                    note.unk2 = 0;
                    note.unk3 = 0;

                    note._absoluteTime = note.time + curTrack.time;

                    notes.Add(note);
                }
                else if (cmd.StartsWith("SUBTRACK_END"))
                {
                    curSubTrack.notes = notes;
                    if (curSubTrackType == "NORMAL")
                        curTrack.normalTrack = curSubTrack;
                    else if (curSubTrackType == "PROFESSIONAL")
                        curTrack.professionalTrack = curSubTrack;
                    else if (curSubTrackType == "MASTER")
                        curTrack.masterTrack = curSubTrack;
                    notes = new List<Note>();
                }
                else if (cmd.StartsWith("TRACK_END"))
                {
                    fumenData.tracks.Add(curTrack);
                }
                else
                    throw new Exception($"Unknown opcode");
            }
            Console.WriteLine(fumenData.tracks[0].normalTrack.notes.Count);
            return fumenData;
        }
    }
}
