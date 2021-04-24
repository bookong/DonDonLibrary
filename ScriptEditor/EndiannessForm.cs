using System;
using System.Windows.Forms;

namespace ScriptEditor
{
    public partial class EndiannessForm : Form
    {
        public EndiannessForm()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            if (endiannessBox.SelectedIndex != -1)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
