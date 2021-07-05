
namespace SwipeRook_MapGenerator
{
    partial class frmMapEditor
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtYSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetSize = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMapGeneration = new System.Windows.Forms.ToolStripMenuItem();
            this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFindRoute = new System.Windows.Forms.Button();
            this.rbtnRook = new System.Windows.Forms.RadioButton();
            this.rbtnStar = new System.Windows.Forms.RadioButton();
            this.rbtnWall = new System.Windows.Forms.RadioButton();
            this.rbtnBlank = new System.Windows.Forms.RadioButton();
            this.lblNotification = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlMain.Location = new System.Drawing.Point(17, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(500, 480);
            this.pnlMain.TabIndex = 16;
            this.pnlMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseClick);
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.pnlMain.MouseLeave += new System.EventHandler(this.pnlMain_MouseLeave);
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // txtYSize
            // 
            this.txtYSize.Font = new System.Drawing.Font("굴림", 11F);
            this.txtYSize.Location = new System.Drawing.Point(101, 30);
            this.txtYSize.Multiline = true;
            this.txtYSize.Name = "txtYSize";
            this.txtYSize.Size = new System.Drawing.Size(36, 20);
            this.txtYSize.TabIndex = 20;
            this.txtYSize.Text = "10";
            this.txtYSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = ",";
            // 
            // txtXSize
            // 
            this.txtXSize.Font = new System.Drawing.Font("굴림", 11F);
            this.txtXSize.Location = new System.Drawing.Point(55, 30);
            this.txtXSize.Multiline = true;
            this.txtXSize.Name = "txtXSize";
            this.txtXSize.Size = new System.Drawing.Size(36, 20);
            this.txtXSize.TabIndex = 18;
            this.txtXSize.Text = "10";
            this.txtXSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 11F);
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Size";
            // 
            // btnSetSize
            // 
            this.btnSetSize.Location = new System.Drawing.Point(143, 29);
            this.btnSetSize.Name = "btnSetSize";
            this.btnSetSize.Size = new System.Drawing.Size(75, 23);
            this.btnSetSize.TabIndex = 21;
            this.btnSetSize.Text = "Set Size";
            this.btnSetSize.UseVisualStyleBackColor = true;
            this.btnSetSize.Click += new System.EventHandler(this.btnSetSize_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMapGeneration,
            this.editorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(534, 25);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMapGeneration
            // 
            this.tsmiMapGeneration.Name = "tsmiMapGeneration";
            this.tsmiMapGeneration.Size = new System.Drawing.Size(113, 21);
            this.tsmiMapGeneration.Text = "MapGeneration";
            this.tsmiMapGeneration.Click += new System.EventHandler(this.tsmiMapGeneration_Click);
            // 
            // editorToolStripMenuItem
            // 
            this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            this.editorToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.editorToolStripMenuItem.Text = "Editor";
            // 
            // btnFindRoute
            // 
            this.btnFindRoute.Font = new System.Drawing.Font("굴림", 13F);
            this.btnFindRoute.Location = new System.Drawing.Point(210, 538);
            this.btnFindRoute.Name = "btnFindRoute";
            this.btnFindRoute.Size = new System.Drawing.Size(115, 64);
            this.btnFindRoute.TabIndex = 27;
            this.btnFindRoute.Text = "Save route to clipboard";
            this.btnFindRoute.UseVisualStyleBackColor = true;
            this.btnFindRoute.Click += new System.EventHandler(this.btnFindRoute_Click);
            // 
            // rbtnRook
            // 
            this.rbtnRook.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnRook.Image = global::SwipeRook_MapGenerator.Properties.Resources.Rook;
            this.rbtnRook.Location = new System.Drawing.Point(239, 26);
            this.rbtnRook.Name = "rbtnRook";
            this.rbtnRook.Size = new System.Drawing.Size(28, 28);
            this.rbtnRook.TabIndex = 28;
            this.rbtnRook.TabStop = true;
            this.rbtnRook.UseVisualStyleBackColor = true;
            // 
            // rbtnStar
            // 
            this.rbtnStar.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnStar.Image = global::SwipeRook_MapGenerator.Properties.Resources.Star;
            this.rbtnStar.Location = new System.Drawing.Point(273, 26);
            this.rbtnStar.Name = "rbtnStar";
            this.rbtnStar.Size = new System.Drawing.Size(28, 28);
            this.rbtnStar.TabIndex = 29;
            this.rbtnStar.TabStop = true;
            this.rbtnStar.UseVisualStyleBackColor = true;
            // 
            // rbtnWall
            // 
            this.rbtnWall.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnWall.Image = global::SwipeRook_MapGenerator.Properties.Resources.Wall;
            this.rbtnWall.Location = new System.Drawing.Point(307, 26);
            this.rbtnWall.Name = "rbtnWall";
            this.rbtnWall.Size = new System.Drawing.Size(28, 28);
            this.rbtnWall.TabIndex = 30;
            this.rbtnWall.TabStop = true;
            this.rbtnWall.UseVisualStyleBackColor = true;
            // 
            // rbtnBlank
            // 
            this.rbtnBlank.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnBlank.Image = global::SwipeRook_MapGenerator.Properties.Resources.Blank;
            this.rbtnBlank.Location = new System.Drawing.Point(341, 26);
            this.rbtnBlank.Name = "rbtnBlank";
            this.rbtnBlank.Size = new System.Drawing.Size(28, 28);
            this.rbtnBlank.TabIndex = 31;
            this.rbtnBlank.TabStop = true;
            this.rbtnBlank.UseVisualStyleBackColor = true;
            // 
            // lblNotification
            // 
            this.lblNotification.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblNotification.Location = new System.Drawing.Point(434, 584);
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(100, 18);
            this.lblNotification.TabIndex = 32;
            this.lblNotification.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // frmMapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(534, 606);
            this.Controls.Add(this.lblNotification);
            this.Controls.Add(this.rbtnBlank);
            this.Controls.Add(this.rbtnWall);
            this.Controls.Add(this.rbtnStar);
            this.Controls.Add(this.rbtnRook);
            this.Controls.Add(this.btnFindRoute);
            this.Controls.Add(this.btnSetSize);
            this.Controls.Add(this.txtYSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtXSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMapEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMapEditor_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtYSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetSize;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapGeneration;
        private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
        private System.Windows.Forms.Button btnFindRoute;
        private System.Windows.Forms.RadioButton rbtnRook;
        private System.Windows.Forms.RadioButton rbtnStar;
        private System.Windows.Forms.RadioButton rbtnWall;
        private System.Windows.Forms.RadioButton rbtnBlank;
        private System.Windows.Forms.Label lblNotification;
    }
}