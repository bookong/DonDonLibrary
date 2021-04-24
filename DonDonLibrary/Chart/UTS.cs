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
            string[] cmd_spl = cmd.Split('(');
            cmd_spl[1] = cmd_spl[1].Trim();

            return cmd_spl[1].Split(',');
        }

        public static string[] FromFumen(Gen3Fumen fumenData)
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
                        uts.Add($"NOTE({note.type}, {StringFormat.Stringify(note.rollHoldTime)}, {note.balloonHitCount});");
                    }

                    uts.Add("SUBTRACK_END();");
                }

                if (firstTrack)
                    firstTrack = false;
            }
            uts.Add("TRACK_END();");

            return uts.ToArray();
        }

        public static string[] FromFumen(Gen2.Gen2Fumen fumenData)
        {
            List<String> uts = new List<string>();
            bool firstTrack = true;
            // -1000 to assure that the time will always be greater than
            int point = -1;
            float measure = 0.0f;
            SubTrack curSubTrack = new SubTrack();
            string[] subTrackLabels = { "NORMAL", "PROFESSIONAL", "MASTER" };

            foreach (Gen2.Track track in fumenData.tracks)
            {
                measure = 60 / track.bpm * 4 / 48;

                if (!firstTrack)
                    uts.Add("TRACK_END();");

                uts.Add($"TRACK2_START({StringFormat.Stringify(track.bpm)}, {StringFormat.Stringify(track.time)}, {track.gogoFlag}, {track.trackLine});");

                for (int i = 0; i < 6; i++)
                {
                    uts.Add($"SUBTRACK2_START({i}, {StringFormat.Stringify(track.scrollSpeeds[i])});");
                    int noteSt = track.subtracks[i].noteIndexSt;
                    int noteCount = track.subtracks[i].noteCount;
                    int pointAdd = track.subtracks[i].pointGain;
                    if (pointAdd != point)
                    {
                        uts.Add($"SET_POINT_BASE({pointAdd});");
                        uts.Add($"SET_POINT_INCREASE(0);");
                    }

                    for (int k = noteSt; k < (noteSt + noteCount); k++)
                    {
                        int rollHoldTime = fumenData.notes[k].longNoteLength;
                        int balloonHitCount = fumenData.notes[k].balloonHitCount;
                        int type = fumenData.notes[k].type;

                        uts.Add($"TIME({StringFormat.Stringify(measure * fumenData.notes[k].measure * 1000)});");
                        uts.Add($"NOTE2({type}, {rollHoldTime}, {balloonHitCount});");
                    }
                    uts.Add("SUBTRACK_END();");
                }

                if (firstTrack)
                    firstTrack = false;
            }
            uts.Add("TRACK_END();");

            return uts.ToArray();
        }

        public static Gen3Fumen ToFumen(string[] uts)
        {
            Gen3Fumen fumenData = new Gen3Fumen();
            
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
                    note.balloonHitCount = short.Parse(args[2]);
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

        public static string[] ConvertGen2ToGen3(string[] _lines)
        {
            List<string> lines = new List<string>();
            string curSubtrack = "";
            string[] args;
            string noteType = "";

            foreach(string line in _lines)
            {
                args = GetCommandArgs(line);
                for(int i = 0; i < args.Length; i++) { args[i] = args[i].Replace(" ", "");  }

                if (line.StartsWith("TRACK2_START"))
                    lines.Add($"TRACK_START({args[0]}, {args[1]}, {args[2]}, {args[3]});");
                else if (line.StartsWith("TRACK_END"))
                    lines.Add(line);
                else if (line.StartsWith("SUBTRACK2_START"))
                {
                    if (args[0] == "0")
                        lines.Add($"SUBTRACK_START(NORMAL, {args[1]});");
                    else if (args[0] == "1")
                        lines.Add($"SUBTRACK_START(PROFESSIONAL, {args[1]});");
                    else if (args[0] == "2")
                        lines.Add($"SUBTRACK_START(MASTER, {args[1]});");
                    else
                    {
                        curSubtrack = "IGNORE";
                        continue;
                    }

                    curSubtrack = "NO_IGNORE";
                }
                else if (line.StartsWith("SUBTRACK_END") && curSubtrack == "NO_IGNORE")
                    lines.Add(line);
                else if (line.StartsWith("SET_POINT") && curSubtrack == "NO_IGNORE")
                    lines.Add(line);
                else if (line.StartsWith("TIME") && curSubtrack == "NO_IGNORE")
                    lines.Add(line);
                else if (line.StartsWith("NOTE") && curSubtrack == "NO_IGNORE")
                {
                    if (args[1] == "-1")
                        args[1] = "0";
                    if (args[2] == "-1")
                        args[2] = "0";

                    lines.Add($"NOTE({args[0]}, {args[1]}, {args[2]});");
                }
            }

            return lines.ToArray();
        }
    }
}
