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
using DonDonLibrary.Database;

namespace DatabaseEditor
{
    public partial class MainForm : Form
    {
        MusicEntry[] currentlyEditing;

        public MainForm()
        {
            InitializeComponent();
        }

        private void addButon_Click(object sender, EventArgs e)
        {

        }

        private void musicInfoMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @".\";
                dialog.Filter = "Taiko Database (*.dat)|*.dat";
                dialog.FilterIndex = 1;

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!File.Exists(dialog.FileName))
                        throw new Exception("File does not exist");
                    using(EndianBinaryReader reader = new EndianBinaryReader(File.Open(dialog.FileName, FileMode.Open), Endianness.LittleEndian))
                    {
                        currentlyEditing = MusicInfo.Read(reader);
                        foreach (MusicEntry entry in currentlyEditing)
                            songList.Items.Add(entry.name);

                        editButton.Visible = true;
                        addButon.Visible = true;
                        removeButton.Visible = true;
                    }
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            MusicEntry musicEntry = new MusicEntry();
            musicEntry.__isDel = true;
            currentlyEditing[songList.SelectedIndex] = musicEntry;
            songList.Items[songList.SelectedIndex] = "[DELETED] " + songList.Items[songList.SelectedIndex];
        }
    }
}
