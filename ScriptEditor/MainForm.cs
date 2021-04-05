using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DonDonLibrary.IO;
using DonDonLibrary.Chart;
using DonDonLibrary.Chart.Misc.DIVA;

namespace ScriptEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void gen3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @".\";
                //dialog.Filter = "TaikoScript files (*.taikoscript)|*.taikoscript";
                dialog.Filter = "Taiko FUMEN files (*.bin, *.EDAT)|*.bin;*.EDAT;*.fum";
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var reader = new EndianBinaryReader(File.Open(dialog.FileName, FileMode.Open), Endianness.LittleEndian))
                    {
                        Gen3 fumenData = Fumen.Read(reader);
                        scriptBox.Lines = UTS.FromFumen(fumenData);
                        using(var writer = new EndianBinaryWriter(File.Open("OPENFILECACHE", FileMode.Create), Endianness.LittleEndian))
                        {
                            writer.Write(fumenData.header.hanteiData);
                            writer.Write(fumenData.header.unknownHeaderData);
                        }
                    }
                }
            }
        }

        private void saveTaikoScriptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = @".\";
                dialog.Title = "Save";
                dialog.Filter = "TaikoScript files (*.txt)|*.txt";
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if(dialog.FileName.Length > 0)
                    {
                        if (!dialog.FileName.EndsWith(".txt"))
                            dialog.FileName += ".txt";
                        File.WriteAllText(dialog.FileName, String.Join("\n", scriptBox.Lines));
                    }
                }
            }
            //using(EndianBinaryWriter writer = new EndianBinaryWriter())
        }

        private void openTaikoScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @".\";
                dialog.Filter = "TaikoScript file (*.txt)|*.txt";
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    scriptBox.Lines = File.ReadAllText(dialog.FileName).Split('\n');
                }
            }
        }

        private void saveGen3MenuItem_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = @".\";
                dialog.Title = "Save";
                dialog.Filter = "Modern FUMEN file (*.bin)|*.bin|PSP FUMEN file (*.EDAT)|*.EDAT";
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.FileName.Length > 0)
                    {
                        if (!dialog.FileName.ToLower().EndsWith(".edat"))
                            if (!dialog.FileName.ToLower().EndsWith(".bin"))
                                dialog.FileName += ".bin";

                        using (var writer = new EndianBinaryWriter(File.Open(dialog.FileName, FileMode.Create), Endianness.LittleEndian))
                        {
                            if (!File.Exists("OPENFILECACHE"))
                                MessageBox.Show("Header cache (created when loading a fumen) does not exist. This will write a set of 0x00 on the header instead, possibly causing errors.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            Fumen.Write(writer, UTS.ToFumen(scriptBox.Lines));
                        }
                    }
                }
            }
        }

        private void openDscMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @".\";
                dialog.Filter = "DSC file (*.dsc)|*.dsc";
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(dialog.FileName, FileMode.Open), Endianness.LittleEndian))
                        scriptBox.Lines = UTS.FromFumen(DSC.ToFumen(reader));
                }
            }
        }

        private void genMeasureDivMenuItem_Click(object sender, EventArgs e)
        {
            scriptBox.Lines = UTS.FromFumen(Fumen.GenMeasureDivision(UTS.ToFumen(scriptBox.Lines)));
        }
    }
}
