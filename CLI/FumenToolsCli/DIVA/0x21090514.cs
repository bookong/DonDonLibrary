namespace FumenToolsCli.DIVA
{
    public class FT
    {
        public static int GetLength(int opcode)
        {
            switch(opcode)
            {
                case 0: return 0;
                case 1: return 1;
                case 4: return 2;
                case 6: return 7;
                case 14: return 1;
                case 25: return 0;
                case 32: return 0;
                case 58: return 1;
                case 67: return 1;
                case 68: return 1;
                default: return -1;
            }
        }
    }
}
