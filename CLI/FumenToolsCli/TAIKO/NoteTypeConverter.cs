using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FumenToolsCli.TAIKO
{
    public class NoteTypeConverter
    {
        public static int GetTaikoType(int divaType)
        {
            switch(divaType)
            {
                case 0:
                    return 4;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 8;
                case 5:
                    return 7;
                case 6:
                    return 5;
                case 7:
                    return 9;
                case 12:
                    return 10;
                default:
                    return 4;
            }
        }
    }
}
