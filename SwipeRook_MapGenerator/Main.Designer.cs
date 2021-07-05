
namespace SwipeRook_MapGenerator
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txtXSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYSize = new System.Windows.Forms.TextBox();
            this.txtStar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWall = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnGeneration = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.MinDistanceLabel = new System.Windows.Forms.Label();
            this.lblMinDistance = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiMapGeneration = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddComponent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 11F);
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Size";
            // 
            // txtXSize
            // 
            this.txtXSize.Font = new System.Drawing.Font("굴림", 11F);
            this.txtXSize.Location = new System.Drawing.Point(55, 30);
            this.txtXSize.Multiline = true;
            this.txtXSize.Name = "txtXSize";
            this.txtXSize.Size = new System.Drawing.Size(36, 20);
            this.txtXSize.TabIndex = 2;
            this.txtXSize.Text = "10";
            this.txtXSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = ",";
            // 
            // txtYSize
            // 
            this.txtYSize.Font = new System.Drawing.Font("굴림", 11F);
            this.txtYSize.Location = new System.Drawing.Point(101, 30);
            this.txtYSize.Multiline = true;
            this.txtYSize.Name = "txtYSize";
            this.txtYSize.Size = new System.Drawing.Size(36, 20);
            this.txtYSize.TabIndex = 5;
            this.txtYSize.Text = "10";
            this.txtYSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStar
            // 
            this.txtStar.Font = new System.Drawing.Font("굴림", 11F);
            this.txtStar.Location = new System.Drawing.Point(184, 30);
            this.txtStar.Multiline = true;
            this.txtStar.Name = "txtStar";
            this.txtStar.Size = new System.Drawing.Size(36, 20);
            this.txtStar.TabIndex = 7;
            this.txtStar.Text = "1";
            this.txtStar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 11F);
            this.label3.Location = new System.Drawing.Point(147, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Star";
            // 
            // txtWall
            // 
            this.txtWall.Font = new System.Drawing.Font("굴림", 11F);
            this.txtWall.Location = new System.Drawing.Point(269, 30);
            this.txtWall.Multiline = true;
            this.txtWall.Name = "txtWall";
            this.txtWall.Size = new System.Drawing.Size(36, 20);
            this.txtWall.TabIndex = 9;
            this.txtWall.Text = "?";
            this.txtWall.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 11F);
            this.label4.Location = new System.Drawing.Point(230, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Wall";
            // 
            // btnLeft
            // 
            this.btnLeft.Enabled = false;
            this.btnLeft.Font = new System.Drawing.Font("굴림", 13F);
            this.btnLeft.Location = new System.Drawing.Point(17, 542);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(130, 50);
            this.btnLeft.TabIndex = 11;
            this.btnLeft.Text = "←";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Enabled = false;
            this.btnRight.Font = new System.Drawing.Font("굴림", 13F);
            this.btnRight.Location = new System.Drawing.Point(387, 542);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(130, 50);
            this.btnRight.TabIndex = 12;
            this.btnRight.Text = "→";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnGeneration
            // 
            this.btnGeneration.Font = new System.Drawing.Font("굴림", 13F);
            this.btnGeneration.Location = new System.Drawing.Point(202, 542);
            this.btnGeneration.Name = "btnGeneration";
            this.btnGeneration.Size = new System.Drawing.Size(130, 50);
            this.btnGeneration.TabIndex = 13;
            this.btnGeneration.Text = "Generation";
            this.btnGeneration.UseVisualStyleBackColor = true;
            this.btnGeneration.Click += new System.EventHandler(this.btnGeneration_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlMain.Location = new System.Drawing.Point(17, 55);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(500, 480);
            this.pnlMain.TabIndex = 15;
            this.pnlMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseClick);
            // 
            // MinDistanceLabel
            // 
            this.MinDistanceLabel.AutoSize = true;
            this.MinDistanceLabel.Font = new System.Drawing.Font("굴림", 11F);
            this.MinDistanceLabel.Location = new System.Drawing.Point(314, 33);
            this.MinDistanceLabel.Name = "MinDistanceLabel";
            this.MinDistanceLabel.Size = new System.Drawing.Size(102, 15);
            this.MinDistanceLabel.TabIndex = 16;
            this.MinDistanceLabel.Text = "Min Distance :";
            this.MinDistanceLabel.Visible = false;
            // 
            // lblMinDistance
            // 
            this.lblMinDistance.AutoSize = true;
            this.lblMinDistance.Font = new System.Drawing.Font("굴림", 11F);
            this.lblMinDistance.Location = new System.Drawing.Point(420, 33);
            this.lblMinDistance.Name = "lblMinDistance";
            this.lblMinDistance.Size = new System.Drawing.Size(15, 15);
            this.lblMinDistance.TabIndex = 17;
            this.lblMinDistance.Text = "0";
            this.lblMinDistance.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMapGeneration,
            this.tsmiEditor});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(534, 25);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiMapGeneration
            // 
            this.tsmiMapGeneration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddComponent});
            this.tsmiMapGeneration.Name = "tsmiMapGeneration";
            this.tsmiMapGeneration.Size = new System.Drawing.Size(113, 21);
            this.tsmiMapGeneration.Text = "MapGeneration";
            // 
            // tsmiAddComponent
            // 
            this.tsmiAddComponent.Name = "tsmiAddComponent";
            this.tsmiAddComponent.Size = new System.Drawing.Size(176, 22);
            this.tsmiAddComponent.Text = "Add Component";
            this.tsmiAddComponent.Click += new System.EventHandler(this.tsmiAddComponent_Click);
            // 
            // tsmiEditor
            // 
            this.tsmiEditor.Name = "tsmiEditor";
            this.tsmiEditor.Size = new System.Drawing.Size(55, 21);
            this.tsmiEditor.Text = "Editor";
            this.tsmiEditor.Click += new System.EventHandler(this.tsmiEditor_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(534, 606);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblMinDistance);
            this.Controls.Add(this.MinDistanceLabel);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnGeneration);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.txtWall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtYSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtXSize);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SwipeRook Map Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtXSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYSize;
        private System.Windows.Forms.TextBox txtStar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWall;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnGeneration;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label MinDistanceLabel;
        private System.Windows.Forms.Label lblMinDistance;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapGeneration;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditor;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddComponent;
    }
}

