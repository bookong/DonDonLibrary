using System;
using System.IO;
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

        private static Endianness GetEndianness()
        {
            using (EndiannessForm endDialog = new EndiannessForm())
            {
                if (endDialog.ShowDialog() == DialogResult.OK)
                {
                    if (endDialog.endiannessBox.SelectedIndex == 1)
                        return Endianness.BigEndian;
                }
            }
            return Endianness.LittleEndian;
        }

        private void gen3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Endianness endianness;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @".\";
                //dialog.Filter = "TaikoScript files (*.taikoscript)|*.taikoscript";
                dialog.Filter = "Taiko FUMEN files (*.bin, *.EDAT)|*.bin;*.EDAT;*.fum";
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    endianness = GetEndianness();
                    using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(dialog.FileName, FileMode.Open), endianness))
                    {
                        Gen3Fumen fumenData = Gen3.Read(reader);
                        scriptBox.Lines = UTS.FromFumen(fumenData);
                        using(EndianBinaryWriter writer = new EndianBinaryWriter(File.Open("OPENFILECACHE", FileMode.Create), Endianness.LittleEndian))
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
            Endianness endianness;
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

                        endianness = GetEndianness();

                        using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(dialog.FileName, FileMode.Create), endianness))
                        {
                            if (!File.Exists("OPENFILECACHE"))
                                MessageBox.Show("Header cache (created when loading a fumen) does not exist. This will write a set of 0x00 on the header instead, possibly causing errors.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            Gen3.Write(writer, UTS.ToFumen(scriptBox.Lines));
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

        private void openGen2MenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                Endianness endianness = Endianness.LittleEndian;
                bool isOld = false;

                dialog.InitialDirectory = @".\";
                dialog.Filter = "All Files (*.*)|*.*";
                dialog.FilterIndex = 1;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using(Gen2EndiannessForm endiannessForm = new Gen2EndiannessForm())
                    {
                        if(endiannessForm.ShowDialog() == DialogResult.OK)
                        {
                            if (endiannessForm.endiannessBox.SelectedIndex == 1)
                                endianness = Endianness.BigEndian;
                            if (endiannessForm.extraIntFlag.Checked)
                                isOld = true;
                        }
                    }
                    using (EndianBinaryReader reader = new EndianBinaryReader(File.Open(dialog.FileName, FileMode.Open), endianness))
                        scriptBox.Lines = UTS.FromFumen(DonDonLibrary.Chart.Gen2.Gen2.Read(reader, isOld));
                }
            }
        }

        private void gen2Gen3MenuItem_Click(object sender, EventArgs e)
        {
            scriptBox.Lines = UTS.ConvertGen2ToGen3(scriptBox.Lines);
        }
    }
}
