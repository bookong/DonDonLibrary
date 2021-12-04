using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DonDonLibrary.IO;
using DonDonLibrary.Fumen.ThirdGeneration;

namespace DonDonLibrary.Fumen
{
    public class ThirdGenFumen
    {
        // Timing judgment (hantei) data
        public Vector3[] JudgementData;

        // Speculated to be a flag for divergent paths but by the looks of it, it can have many values
        // so I don't really trust it... but let's keep record of it internally
        private int mUnknown = 0;

        public int MaxPoints;
        public int ClearPoints;

        public int PointsEarnedPerGood;
        public int PointsEarnedPerFine;
        public int PointsLostPerBad;

        public int MaxCombo;

        public int PointIncreaseRateExpert;
        public int PointIncreaseRateMaster;

        public int GoodDivergePoints;
        public int FineDivergePoints;
        public int BadDivergePoints;
        public int RendaDivergePoints;

        // For the large notes
        public int LargeGoodDivergePoints;
        public int LargeFineDivergePoints;
        public int LargeRendaDivergePoints;

        public int BalloonDivergePoints;
        public int BellDivergePoints;

        private int mUnknown1 = 0;

        // This is apparently not used anymore but it's still there, hanging
        public int MaxScore;

        public List<Track> Tracks = new List<Track>();

        public static ThirdGenFumen Read(EndianBinaryReader reader)
        {
            ThirdGenFumen fumen = new ThirdGenFumen();

            // Hantei data
            fumen.JudgementData = reader.ReadVector3s(36);

            Console.WriteLine(reader.Position);

            fumen.mUnknown = reader.ReadInt32();

            fumen.MaxPoints = reader.ReadInt32();
            fumen.ClearPoints = reader.ReadInt32();

            fumen.PointsEarnedPerGood = reader.ReadInt32();
            fumen.PointsEarnedPerFine = reader.ReadInt32();
            fumen.PointsLostPerBad = reader.ReadInt32();

            fumen.MaxCombo = reader.ReadInt32();

            fumen.PointIncreaseRateExpert = reader.ReadInt32();
            fumen.PointIncreaseRateMaster = reader.ReadInt32();

            fumen.GoodDivergePoints = reader.ReadInt32();
            fumen.FineDivergePoints = reader.ReadInt32();
            fumen.BadDivergePoints = reader.ReadInt32();
            fumen.RendaDivergePoints = reader.ReadInt32();

            fumen.LargeGoodDivergePoints = reader.ReadInt32();
            fumen.LargeFineDivergePoints = reader.ReadInt32();
            fumen.LargeRendaDivergePoints = reader.ReadInt32();

            fumen.BalloonDivergePoints = reader.ReadInt32();
            fumen.BellDivergePoints = reader.ReadInt32();

            fumen.mUnknown1 = reader.ReadInt32();

            fumen.MaxScore = reader.ReadInt32();

            // Phew. That was quite a few ReadInt32's, don't you think?
            // Now we get to the notes part

            Console.WriteLine(reader.Position);

            int trackCount = reader.ReadInt32();

            Console.WriteLine(trackCount);

            // Apparently padding/leftover data?
            reader.SeekCurrent(4);

            // Read the tracks now
            // (If you're wondering why I'm calling these tracks, not measures, that's because "measure" is innacurate
            //  in this context since a track in fumen doesn't necessarily need to change every measure)

            fumen.Tracks = new List<Track>();
            for (int i = 0; i < trackCount; i++)
                fumen.Tracks.Add(Track.Read(reader));

            return fumen;
        }
    }
}
