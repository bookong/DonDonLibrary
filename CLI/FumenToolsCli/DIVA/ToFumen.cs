using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.Chart;
using DonDonLibrary.IO;
using FumenToolsCli.TAIKO;


namespace FumenToolsCli.DIVA
{
    public class ToFumen
    {
        public static Gen3Fumen ToGen3(EndianBinaryReader dsc, Format fmt)
        {
            Gen3Fumen fumen = new Gen3Fumen();
            int cmd;
            int len;
            int[] args = new int[20];

            float glTime = 0f;
            float mBpm = -1f;
            float mTft = -1f;
            int mTrackIdx = 0;
            bool isFirstTrack = true;

            fumen.header.hanteiData = HanteiTemplate.tNiEasy;
            fumen.header.headerData = new int[] { 0, 10000, 6000, 298, 223, -149, 65536, 65536, 65536, 20, 10, 0, 1, 20, 10, 1, 30, 30, 20, 797380};

            fumen.tracks.Add(new Track());
            fumen.tracks[0].time = 0f;

            while(true)
            {
                cmd = dsc.ReadInt32();
                len = FT.GetLength(cmd);

                if (len != -1)
                {
                    args = dsc.ReadInt32s(len);
                }
                else
                {
                    Console.WriteLine($"Unknown upcode (ID: {cmd}) while reading DSC.");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                if(cmd == 0) { break; }
                else if(cmd == 1) { glTime = args[0]; }
                else if(cmd == 6)
                {
                    if( new int[] { 0, 1, 2, 3, 4, 5, 6 }.Contains(args[0]) )
                    {
                        Note n = new Note();
                        n.time = glTime - fumen.tracks[mTrackIdx].time;
                        n.type = NoteTypeConverter.GetTaikoType(args[0]);
                        n.addPoint = 600;
                        n.basePoint = 1200;
                        fumen.tracks[mTrackIdx].normalTrack.notes.Add(n);
                    }
                }
                else if(cmd == 58)
                {
                    if (args[0] != mTft)
                    {
                        mTft = args[0];
                        mBpm = 240000.0f / args[0];

                        if (!isFirstTrack)
                        {
                            mTrackIdx++;
                            fumen.tracks.Add(new Track());
                            fumen.tracks[mTrackIdx].bpm = mBpm;
                            fumen.tracks[mTrackIdx].time = 0f;
                        }
                        else
                            fumen.tracks[0].bpm = mBpm;
                    }
                }
            }

            return fumen;
        }
    }
}
