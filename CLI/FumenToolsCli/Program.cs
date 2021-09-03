using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FumenToolsCli.DIVA;
using DonDonLibrary.Chart;
using DonDonLibrary.IO;
using System.IO;

namespace FumenToolsCli
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            int option;

            option = Menu.MainMenu.Run();

            if(option == -1) { Environment.Exit(1); }
            else if(option == 1)
            {
                string dscPath = string.Empty;
                string savePath = String.Empty;
                Gen3Fumen fumen = new Gen3Fumen();

                using(OpenFileDialog diag = new OpenFileDialog())
                {
                    diag.Filter = "DSC Files (*.dsc)|*.dsc";
                    diag.InitialDirectory = ".\\";
                    diag.FilterIndex = 1;

                    if(diag.ShowDialog() == DialogResult.OK)
                        dscPath = diag.FileName;
                    else
                    {
                        Console.WriteLine("User cancelled the operation");
                        Environment.Exit(1);
                    }
                }

                using(EndianBinaryReader r = new EndianBinaryReader(File.Open(dscPath, FileMode.Open), Endianness.LittleEndian))
                {
                    // 0x21090514 = Modern FT
                    if(r.ReadInt32() == 335874337) { fumen = ToFumen.ToGen3(r, Format.FT); }


                    using (SaveFileDialog diag = new SaveFileDialog())
                    {
                        diag.Filter = "FUMEN Files (*.bin)|*.bin";
                        diag.InitialDirectory = ".\\";
                        diag.FilterIndex = 1;
                        diag.AddExtension = true;

                        if (diag.ShowDialog() == DialogResult.OK)
                            savePath = diag.FileName;
                        else
                        {
                            Console.WriteLine("User cancelled the operation");
                            Environment.Exit(1);
                        }
                    }

                    using(EndianBinaryWriter w = new EndianBinaryWriter(File.Open(savePath, FileMode.Create), Endianness.LittleEndian))
                        Gen3.Write(w, fumen);
                }
                
            }
            else if(option == 9)
            {

            }
            else { Console.WriteLine(option); Console.ReadKey(); }
        }
    }
}
