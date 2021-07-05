using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwipeRook_MapGenerator
{
    public partial class frmMapEditor : Form
    {
        MapGeneration mapGeneration;
        WayFinding wayFinding;
        int[,] map;
        frmMain mainForm;
        bool mousePressed;

        public frmMapEditor(frmMain mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            mapGeneration = new MapGeneration();
            wayFinding = new WayFinding();
            InitMap(int.Parse(txtXSize.Text), int.Parse(txtYSize.Text));
        }

        // 맵 크기 재설정
        private void btnSetSize_Click(object sender, EventArgs e)
        {
            InitMap(int.Parse(txtXSize.Text), int.Parse(txtYSize.Text));
        }

        // 최단 경로 찾아서 클립보드에 저장
        private void btnFindRoute_Click(object sender, EventArgs e)
        {
            lblNotification.Text = "Finding";
            Point[] route = wayFinding.FindDirection(map);
            if (route == null)
            {
                lblNotification.Text = "Not found";
                return;
            }
            string text = "= new Point[" + (route.Length-1) + "];" + Environment.NewLine;
            for(int i = 1; i < route.Length; i++)
            {
                text += "= new Point(" + route[i].X + ", " + route[i].Y + ");" + Environment.NewLine;
            }
            Clipboard.SetText(text);
            lblNotification.Text = "Complete";
        }

        // 맵에 그리기
        private void pnlMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                if(e.Button.Equals(MouseButtons.Left))
                    DrawImageToMap(e);
            }
        }

        // 맵에 그리기
        private void pnlMain_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button.Equals(MouseButtons.Left)) 
                DrawImageToMap(e);
            else if (e.Button.Equals(MouseButtons.Right))
            {
                ContextMenu cm = new ContextMenu(); //메뉴에 들어갈 아이템을 만듭니다
                MenuItem m1 = new MenuItem();
                m1.Text = "이미지 저장";
                m1.Click += new EventHandler(delegate
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.InitialDirectory = "C:";
                    saveFile.Title = "이미지 저장";
                    saveFile.DefaultExt = "jpg";
                    saveFile.Filter = "jpg files(*.jpg)|*.jpg";
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        pnlMain.BackgroundImage.Save(saveFile.FileName);
                    }
                });
                cm.MenuItems.Add(m1);
                cm.Show(pnlMain, new Point(e.X, e.Y));
            }
        }

        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
        }

        private void pnlMain_MouseLeave(object sender, EventArgs e)
        {
            mousePressed = false;
        }

        // Map Generation 폼 열기
        private void tsmiMapGeneration_Click(object sender, EventArgs e)
        {
            mainForm.Show();
            Hide();
        }

        // 종료 시
        private void frmMapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        void InitMap(int xSize, int ySize)
        {
            map = new int[ySize, xSize];
            pnlMain.BackgroundImage = mapGeneration.DrawMap(map);
            pnlMain.Size = new Size(xSize * 25, ySize * 25);
            pnlMain.Location = new Point(Size.Width / 2 - pnlMain.Size.Width / 2, Size.Height / 2 - pnlMain.Size.Height / 2 - 25);
        }

        Point PointToIndex(Point p)
        {
            return new Point(p.X / 25, p.Y / 25);
        }

        void DrawImageToMap(MouseEventArgs e)
        {
            try
            {
                Point index = PointToIndex(e.Location);
                if (rbtnRook.Checked)
                {
                    map[index.Y, index.X] = (int)ObjectCode.rook;
                }
                else if (rbtnStar.Checked)
                {
                    map[index.Y, index.X] = (int)ObjectCode.star;
                }
                else if (rbtnWall.Checked)
                {
                    map[index.Y, index.X] = (int)ObjectCode.wall;
                }
                else if (rbtnBlank.Checked)
                {
                    map[index.Y, index.X] = (int)ObjectCode.blank;
                }
                pnlMain.BackgroundImage = mapGeneration.DrawMap(map);
            }
            catch { }
        }
    }
}
