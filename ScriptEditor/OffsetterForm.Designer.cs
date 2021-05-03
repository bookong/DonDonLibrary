
namespace ScriptEditor
{
    partial class OffsetterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OffsetterForm));
            this.offsetLbl = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.applyOnLbl = new System.Windows.Forms.Label();
            this.applyOn = new System.Windows.Forms.ComboBox();
            this.offsetBox = new System.Windows.Forms.TextBox();
            this.applyToTrack = new System.Windows.Forms.RadioButton();
            this.applyToNote = new System.Windows.Forms.RadioButton();
            this.applyToLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // offsetLbl
            // 
            this.offsetLbl.AutoSize = true;
            this.offsetLbl.Location = new System.Drawing.Point(6, 26);
            this.offsetLbl.Name = "offsetLbl";
            this.offsetLbl.Size = new System.Drawing.Size(35, 13);
            this.offsetLbl.TabIndex = 0;
            this.offsetLbl.Text = "Offset";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(136, 119);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(95, 46);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.applyToLbl);
            this.groupBox1.Controls.Add(this.applyToNote);
            this.groupBox1.Controls.Add(this.applyToTrack);
            this.groupBox1.Controls.Add(this.applyOnLbl);
            this.groupBox1.Controls.Add(this.applyOn);
            this.groupBox1.Controls.Add(this.offsetBox);
            this.groupBox1.Controls.Add(this.offsetLbl);
            this.groupBox1.Controls.Add(this.applyButton);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 171);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Apply Offset";
            // 
            // applyOnLbl
            // 
            this.applyOnLbl.AutoSize = true;
            this.applyOnLbl.Location = new System.Drawing.Point(5, 72);
            this.applyOnLbl.Name = "applyOnLbl";
            this.applyOnLbl.Size = new System.Drawing.Size(48, 13);
            this.applyOnLbl.TabIndex = 4;
            this.applyOnLbl.Text = "Apply on";
            // 
            // applyOn
            // 
            this.applyOn.FormattingEnabled = true;
            this.applyOn.Items.AddRange(new object[] {
            "All tracks",
            "----------"});
            this.applyOn.Location = new System.Drawing.Point(63, 69);
            this.applyOn.Name = "applyOn";
            this.applyOn.Size = new System.Drawing.Size(159, 21);
            this.applyOn.TabIndex = 3;
            // 
            // offsetBox
            // 
            this.offsetBox.Location = new System.Drawing.Point(63, 23);
            this.offsetBox.Name = "offsetBox";
            this.offsetBox.Size = new System.Drawing.Size(159, 20);
            this.offsetBox.TabIndex = 2;
            // 
            // applyToTrack
            // 
            this.applyToTrack.AutoSize = true;
            this.applyToTrack.Checked = true;
            this.applyToTrack.Location = new System.Drawing.Point(63, 47);
            this.applyToTrack.Name = "applyToTrack";
            this.applyToTrack.Size = new System.Drawing.Size(79, 17);
            this.applyToTrack.TabIndex = 5;
            this.applyToTrack.TabStop = true;
            this.applyToTrack.Text = "Track Time";
            this.applyToTrack.UseVisualStyleBackColor = true;
            // 
            // applyToNote
            // 
            this.applyToNote.AutoSize = true;
            this.applyToNote.Location = new System.Drawing.Point(148, 47);
            this.applyToNote.Name = "applyToNote";
            this.applyToNote.Size = new System.Drawing.Size(74, 17);
            this.applyToNote.TabIndex = 6;
            this.applyToNote.Text = "Note Time";
            this.applyToNote.UseVisualStyleBackColor = true;
            // 
            // applyToLbl
            // 
            this.applyToLbl.AutoSize = true;
            this.applyToLbl.Location = new System.Drawing.Point(5, 49);
            this.applyToLbl.Name = "applyToLbl";
            this.applyToLbl.Size = new System.Drawing.Size(45, 13);
            this.applyToLbl.TabIndex = 7;
            this.applyToLbl.Text = "Apply to";
            // 
            // OffsetterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 184);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OffsetterForm";
            this.Text = "Offsetter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label offsetLbl;
        public System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label applyOnLbl;
        public System.Windows.Forms.ComboBox applyOn;
        public System.Windows.Forms.TextBox offsetBox;
        public System.Windows.Forms.RadioButton applyToNote;
        public System.Windows.Forms.RadioButton applyToTrack;
        private System.Windows.Forms.Label applyToLbl;
    }
}