
namespace DatabaseEditor
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songList = new System.Windows.Forms.ListBox();
            this.editButton = new System.Windows.Forms.Button();
            this.addButon = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.musicInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(384, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.songInfoMenuItem,
            this.musicInfoMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // songInfoMenuItem
            // 
            this.songInfoMenuItem.Name = "songInfoMenuItem";
            this.songInfoMenuItem.Size = new System.Drawing.Size(180, 22);
            this.songInfoMenuItem.Text = "SongInfo";
            // 
            // songList
            // 
            this.songList.FormattingEnabled = true;
            this.songList.Location = new System.Drawing.Point(13, 28);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(359, 147);
            this.songList.TabIndex = 1;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(13, 182);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Visible = false;
            // 
            // addButon
            // 
            this.addButon.Location = new System.Drawing.Point(155, 181);
            this.addButon.Name = "addButon";
            this.addButon.Size = new System.Drawing.Size(75, 23);
            this.addButon.TabIndex = 3;
            this.addButon.Text = "Add";
            this.addButon.UseVisualStyleBackColor = true;
            this.addButon.Visible = false;
            this.addButon.Click += new System.EventHandler(this.addButon_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(297, 181);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 4;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Visible = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // musicInfoMenuItem
            // 
            this.musicInfoMenuItem.Name = "musicInfoMenuItem";
            this.musicInfoMenuItem.Size = new System.Drawing.Size(180, 22);
            this.musicInfoMenuItem.Text = "MusicInfo";
            this.musicInfoMenuItem.Click += new System.EventHandler(this.musicInfoMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButon);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.songList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem songInfoMenuItem;
        private System.Windows.Forms.ListBox songList;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button addButon;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ToolStripMenuItem musicInfoMenuItem;
    }
}

