using System;

namespace DonDonLibrary.Chart.Misc
{
    public class DivaNote
    {
        internal static Random random = new Random();
        
        internal static int GetRandom()
        {
            int num = random.Next(1, 8);
            while (num == 7)
                num = random.Next(1, 8);
            return num;
        }

        public static int ToTaiko(int notetype)
        {
            switch(notetype)
            {
                case 0: return 4;
                case 1: return 1;
                case 2: return 2;
                case 3: return 5;
                case 4: return 8;
                case 5: return 7;
                case 6: return 7;
                case 7: return 8;
                case 8: return 6;
                case 9: return 9;
                case 10: return 9;
                case 11: return 6;
                default: return GetRandom();
            }
        }
    }
}
