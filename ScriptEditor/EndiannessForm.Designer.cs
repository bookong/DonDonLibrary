
namespace ScriptEditor
{
    partial class EndiannessForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndiannessForm));
            this.openFileButton = new System.Windows.Forms.Button();
            this.endiannessBox = new System.Windows.Forms.ComboBox();
            this.endSelText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(183, 75);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(82, 32);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.Text = "Open";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // endiannessBox
            // 
            this.endiannessBox.FormattingEnabled = true;
            this.endiannessBox.Items.AddRange(new object[] {
            "Little Endian",
            "Big Endian"});
            this.endiannessBox.Location = new System.Drawing.Point(55, 37);
            this.endiannessBox.Name = "endiannessBox";
            this.endiannessBox.Size = new System.Drawing.Size(142, 21);
            this.endiannessBox.TabIndex = 1;
            // 
            // endSelText
            // 
            this.endSelText.AutoSize = true;
            this.endSelText.Location = new System.Drawing.Point(52, 21);
            this.endSelText.Name = "endSelText";
            this.endSelText.Size = new System.Drawing.Size(145, 13);
            this.endSelText.TabIndex = 2;
            this.endSelText.Text = "Please select the endianness";
            // 
            // EndiannessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 110);
            this.Controls.Add(this.endSelText);
            this.Controls.Add(this.endiannessBox);
            this.Controls.Add(this.openFileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EndiannessForm";
            this.Text = "Endianness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button openFileButton;
        public System.Windows.Forms.ComboBox endiannessBox;
        private System.Windows.Forms.Label endSelText;
    }
}