using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DonDonLibrary.Utilities;

namespace ScriptEditor
{
    public partial class TimingEditorForm : Form
    {
        public float[] hanteiData;

        public TimingEditorForm()
        {
            InitializeComponent();
        }

        /*private string GetValue(float _val, float _val1, float _val2, float _val3)
        {
            string val = StringFormat.Stringify(_val);
            string val1 = StringFormat.Stringify(_val1);
            string val2 = StringFormat.Stringify(_val2);
            string val3 = StringFormat.Stringify(_val3);

            Console.WriteLine(36 - 3 - val.Length - val1.Length - val2.Length - val3.Length);
            return "";
        }*/

        public void GetTiming(float[] hantei)
        {
            int i = 0;
            List<string> lines = new List<string>();

            for (int j = 0; j < 27; j++)
            {
                lines.Add($"{StringFormat.Stringify(hantei[i])}|{StringFormat.Stringify(hantei[i + 1])}|{StringFormat.Stringify(hantei[i + 2])}|{StringFormat.Stringify(hantei[i + 3])}");
                i += 4;
            }
            timingBox.Lines = lines.ToArray();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            float[] hantei = new float[108];
            int i = 0;

            for (int j = 0; j < 27; j++)
            {
                string[] line = timingBox.Lines[j].Split('|');

                for(int k = 0; k < 4; k++)
                    hantei[k + i] = StringFormat.Destringify(line[k]);
                
                i += 4;
            }

            hanteiData = hantei;
            this.DialogResult = DialogResult.OK;
        }
    }
}
