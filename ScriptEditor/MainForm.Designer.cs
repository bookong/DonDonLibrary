
namespace ScriptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fumenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gen3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTaikoScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDscMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTjaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTaikoScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fumenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGen3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genMeasureDivMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(669, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fumenToolStripMenuItem,
            this.openTaikoScriptToolStripMenuItem,
            this.openDscMenuItem,
            this.openTjaMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // fumenToolStripMenuItem
            // 
            this.fumenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gen3ToolStripMenuItem});
            this.fumenToolStripMenuItem.Name = "fumenToolStripMenuItem";
            this.fumenToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.fumenToolStripMenuItem.Text = "Fumen";
            // 
            // gen3ToolStripMenuItem
            // 
            this.gen3ToolStripMenuItem.Name = "gen3ToolStripMenuItem";
            this.gen3ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.gen3ToolStripMenuItem.Text = "3rd Generation";
            this.gen3ToolStripMenuItem.Click += new System.EventHandler(this.gen3ToolStripMenuItem_Click);
            // 
            // openTaikoScriptToolStripMenuItem
            // 
            this.openTaikoScriptToolStripMenuItem.Name = "openTaikoScriptToolStripMenuItem";
            this.openTaikoScriptToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openTaikoScriptToolStripMenuItem.Text = "TaikoScript";
            this.openTaikoScriptToolStripMenuItem.Click += new System.EventHandler(this.openTaikoScriptToolStripMenuItem_Click);
            // 
            // openDscMenuItem
            // 
            this.openDscMenuItem.Name = "openDscMenuItem";
            this.openDscMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openDscMenuItem.Text = "DSC";
            this.openDscMenuItem.Click += new System.EventHandler(this.openDscMenuItem_Click);
            // 
            // openTjaMenuItem
            // 
            this.openTjaMenuItem.Name = "openTjaMenuItem";
            this.openTjaMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openTjaMenuItem.Text = "TJA [Experimental]";
            this.openTjaMenuItem.Click += new System.EventHandler(this.openTjaMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveTaikoScriptToolStripMenuItem1,
            this.fumenToolStripMenuItem1});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveTaikoScriptToolStripMenuItem1
            // 
            this.saveTaikoScriptToolStripMenuItem1.Name = "saveTaikoScriptToolStripMenuItem1";
            this.saveTaikoScriptToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.saveTaikoScriptToolStripMenuItem1.Text = "UniversalTaikoScript";
            this.saveTaikoScriptToolStripMenuItem1.Click += new System.EventHandler(this.saveTaikoScriptToolStripMenuItem1_Click);
            // 
            // fumenToolStripMenuItem1
            // 
            this.fumenToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGen3MenuItem});
            this.fumenToolStripMenuItem1.Name = "fumenToolStripMenuItem1";
            this.fumenToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.fumenToolStripMenuItem1.Text = "Fumen";
            // 
            // saveGen3MenuItem
            // 
            this.saveGen3MenuItem.Name = "saveGen3MenuItem";
            this.saveGen3MenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveGen3MenuItem.Text = "3rd Generation";
            this.saveGen3MenuItem.Click += new System.EventHandler(this.saveGen3MenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genMeasureDivMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // genMeasureDivMenuItem
            // 
            this.genMeasureDivMenuItem.Name = "genMeasureDivMenuItem";
            this.genMeasureDivMenuItem.Size = new System.Drawing.Size(246, 22);
            this.genMeasureDivMenuItem.Text = "(WIP) Generate Measure Division";
            this.genMeasureDivMenuItem.ToolTipText = "Do not use this on multi-BPM songs!";
            this.genMeasureDivMenuItem.Click += new System.EventHandler(this.genMeasureDivMenuItem_Click);
            // 
            // scriptBox
            // 
            this.scriptBox.BackColor = System.Drawing.SystemColors.Window;
            this.scriptBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scriptBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.scriptBox.Location = new System.Drawing.Point(13, 28);
            this.scriptBox.Multiline = true;
            this.scriptBox.Name = "scriptBox";
            this.scriptBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scriptBox.Size = new System.Drawing.Size(644, 427);
            this.scriptBox.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 467);
            this.Controls.Add(this.scriptBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Taiko ScriptEditor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TextBox scriptBox;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fumenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gen3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTaikoScriptToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openTaikoScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fumenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveGen3MenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genMeasureDivMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDscMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTjaMenuItem;
    }
}

