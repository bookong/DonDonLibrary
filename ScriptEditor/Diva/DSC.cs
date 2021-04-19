using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;
using DonDonLibrary.Chart;

namespace DonDonLibrary.Chart.Misc.DIVA
{
    public class DSC
    {
        public static Gen3Fumen ToFumen(EndianBinaryReader reader)
        {
            int[] formats = { 302121504 };
            int signature = reader.ReadInt32();
            int command;
            int paramCount;
            int[] commandArgs;
            bool firstTempoSet = true;
            bool isNewTrack = true;

            Gen3Fumen fumenData = new Gen3Fumen();
            Track track = new Track();
            float time = 0;
            float bpm = 0;


            if (!formats.Contains(signature))
                throw new Exception("DSC format not suported!");

            while (true)
            {
                command = reader.ReadInt32();
                if (signature == formats[0])
                {
                    paramCount = Diva.F.Script.GetParameterCount((Diva.F.Opcode)command);
                    Console.WriteLine($"{command} -> {paramCount} <- {reader.Position}");
                    commandArgs = reader.ReadInt32s(paramCount);

                    if ((Diva.F.Opcode)command == Diva.F.Opcode.TIME)
                        time = (float)commandArgs[0] / 100;
                    else if ((Diva.F.Opcode)command == Diva.F.Opcode.BAR_TIME_SET)
                    {
                        if (firstTempoSet)
                        {
                            track.bpm = commandArgs[0];
                            firstTempoSet = false;
                        }
                        else
                        {
                            fumenData.tracks.Add(track);
                            track = new Track();
                            track.bpm = commandArgs[0];
                        }
                        track.gogo = false;
                        track.time = time;
                        track.trackLine = 1;
                        isNewTrack = true;
                    }
                    else if ((Diva.F.Opcode)command == Diva.F.Opcode.TARGET_FLYING_TIME)
                    {
                        if (firstTempoSet)
                        {
                            track.bpm = 240000 / commandArgs[0];
                            firstTempoSet = false;
                        }
                        else
                        {
                            fumenData.tracks.Add(track);
                            track = new Track();
                            track.bpm = 240000 / commandArgs[0];
                        }
                        track.gogo = false;
                        track.time = time;
                        track.trackLine = 1;
                        isNewTrack = true;
                    }
                    else if ((Diva.F.Opcode)command == Diva.F.Opcode.END)
                        break;
                    else if((Diva.F.Opcode)command == Diva.F.Opcode.TARGET)
                    {
                        Note note = new Note();
                        note.time = time - track.time;
                        note.type = DivaNote.ToTaiko(commandArgs[0]);
                        note.basePoint = 275;
                        note.addPoint = 135;
                        note.unk = 0;
                        note.unk1 = 0;
                        note.unk2 = 0;
                        note.unk3 = 0;
                        if (note.type == 6 || note.type == 9)
                        {
                            if (commandArgs[1] != -1)
                                note.rollHoldTime = commandArgs[1] / 100;
                            else if (commandArgs[1] == -1 && commandArgs[2] == 1)
                                continue;   
                        }
                        track.normalTrack.notes.Add(note);
                    }
                }
            }
            fumenData.tracks.Add(track);

            return fumenData;
        }
    }
}
