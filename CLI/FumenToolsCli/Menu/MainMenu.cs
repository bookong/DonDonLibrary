using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FumenToolsCli.Menu
{
    class MainMenu
    {
        public static int Run()
        {
            // Start drawing the menu
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 1. [DIVA] Custom Convert          |");
            //Console.WriteLine("| 2.                                |");
            //Console.WriteLine("| 9.                                |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("");

            string choose = Console.ReadLine();

            try
            {
                int option = int.Parse(choose);
                if(option > 0 && option < 2)
                {
                    return option;
                }
            }
            catch
            {
                return -1;
            }

            return -1;
        }
    }
}
