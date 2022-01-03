using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SwipeRook_MapGenerator
{
    public partial class frmMain : Form
    {
        public int desiredMinDistance = 3;
        MapGeneration mapGeneration;
        WayFinding wayFinding;
        frmMapEditor mapEditorForm = null;
        BackgroundWorker worker;
        int[,] map;
        Bitmap[] pathBitmaps = null;
        Point[] shortestRoute = null;
        int _mapIndex;
        int mapIndex
        {
            get
            {
                return _mapIndex;
            }
            set
            {
                try
                {
                    if (value <= 0)
                        _mapIndex = 0;
                    else if (value > pathBitmaps.Length - 1)
                        _mapIndex = pathBitmaps.Length - 1;
                    else _mapIndex = value;

                    if (pathBitmaps != null)
                    {
                        if (_mapIndex == 0)
                            btnLeft.Enabled = false;
                        else btnLeft.Enabled = true;

                        if (_mapIndex == pathBitmaps.Length - 1)
                            btnRight.Enabled = false;
                        else btnRight.Enabled = true;
                    }
                }
                catch { _mapIndex = 0; }
            }
        }
        int timer = 0;
        public frmMain()
        {
            InitializeComponent();
            mapGeneration = new MapGeneration();
            wayFinding = new WayFinding();
        }

        private void btnGeneration_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            btnLeft.Enabled = true;
            btnRight.Enabled = true;
            mapIndex = 0;

            // 랜덤 맵 생성, BFS로 정상적인 맵인지 확인하고 아니면 다시 만들기
            while (true)
            {
                Size mapSize = new Size(int.Parse(txtXSize.Text), int.Parse(txtYSize.Text));
                int maxValue = mapSize.Width * mapSize.Height / 2;
                int star = txtStar.Text == "?" ? rand.Next(1, 6) : int.Parse(txtStar.Text);
                int wall = txtWall.Text == "?" ? rand.Next(3, maxValue) : int.Parse(txtWall.Text);

                map = mapGeneration.CreateMap(mapSize, star, wall);
                // 맵이 정상적인지 확인
                if (wayFinding.IsMapOK(map))
                    break;
            }

            // case 1
            //map = new int[8, 9] {
            //    {0,0,2,0,1,0,2,1,0},
            //    {0,1,0,0,0,0,0,0,0},
            //    {0,0,0,0,0,0,0,0,0},
            //    {1,0,0,0,0,0,2,0,0},
            //    {0,0,0,0,0,0,1,0,0},
            //    {0,0,0,0,0,1,3,0,0},
            //    {0,0,0,0,0,0,0,0,0},
            //    {0,0,0,0,1,0,0,0,0},
            //};

            // case 2
            //map = new int[4, 4] {
            //    {2,0,0,3},
            //    {0,0,0,0},
            //    {0,0,0,0},
            //    {0,0,0,2},
            //};

            //map = new int[6, 6] {
            //    {0,0,0,0,0,2},
            //    {0,0,0,0,1,0},
            //    {1,0,0,0,0,0},
            //    {1,0,0,0,0,0},
            //    {0,1,0,0,0,0},
            //    {0,0,0,1,3,0},
            //};

            //map = new int[6, 6] {
            //    {0,0,0,0,0,0},
            //    {0,0,0,0,0,0},
            //    {0,0,0,0,0,0},
            //    {2,0,3,0,0,0},
            //    {0,0,0,0,0,0},
            //    {0,0,2,0,0,0},
            //};
            // 오래걸릴 수도 있으므로 스레드에서 동작
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(FindWay);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Show);
            worker.RunWorkerAsync();
            //workerTimeout.Start();
            btnGeneration.Enabled = false;
        }

        // 최단 경로 찾기
        void FindWay(object sender, DoWorkEventArgs e)
        {
            timer = 0;
            try
            {
                shortestRoute = wayFinding.FindDirection(map);
            }
            catch
            {
                //길찾기 재시작
                //btnGeneration_Click(sender, e);
            }
        }

        void Show(object sender, RunWorkerCompletedEventArgs e)
        {
            workerTimeout.Stop();
            btnGeneration.Enabled = true;

            // 최단 거리 출력
            MinDistanceLabel.Visible = true;
            lblMinDistance.Visible = true;
            lblMinDistance.Text = wayFinding.minDistance.ToString();
            // 최단 경로를 참고해서 비트맵 생성
            pathBitmaps = mapGeneration.DrawMapByPath(map, shortestRoute);
            // 맵 그려서 출력
            pnlMain.BackgroundImage = pathBitmaps[mapIndex];
            pnlMain.AutoScrollMinSize = pathBitmaps[mapIndex].Size;
        }

        string MapToText(int[,] map)
        {
            string text = "new int[" + map.GetLength(0) + "," + map.GetLength(1) + "] {";
            text += Environment.NewLine;

            for (int y = 0; y < map.GetLength(0); y++)
            {
                text += "    {";
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    text += x != map.GetLength(1) - 1 ? map[y, x] + "," : map[y, x] + "},";
                }
                text += Environment.NewLine;
            }
            text += "};";
            return text;
        }

        // 길찾기 타임아웃
        private void workerTimeout_Tick(object sender, EventArgs e)
        {
            timer += 1;
            if (timer >= 10)
            {
                btnGeneration.Enabled = true;
                worker.CancelAsync();
                workerTimeout.Stop();
                MessageBox.Show("맵 생성에 실패했습니다." + wayFinding.routeList.Count);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            mapIndex--;
            pnlMain.BackgroundImage = pathBitmaps[mapIndex];
            pnlMain.AutoScrollMinSize = pathBitmaps[mapIndex].Size;
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            mapIndex++;
            pnlMain.BackgroundImage = pathBitmaps[mapIndex];
            pnlMain.AutoScrollMinSize = pathBitmaps[mapIndex].Size;
        }

        // 비트맵 저장
        private void pnlMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                if (map == null) return;
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

                MenuItem m2 = new MenuItem();
                m2.Text = "map index 클립보드에 저장";
                m2.Click += new EventHandler(delegate
                {
                    Clipboard.SetText(MapToText(map));
                });

                cm.MenuItems.Add(m1);
                cm.MenuItems.Add(m2);
                cm.Show(pnlMain, new Point(e.X, e.Y));
            }
        }

        private void tsmiAddComponent_Click(object sender, EventArgs e)
        {
            frmAddComponent frmAddComponent = new frmAddComponent(this);
            frmAddComponent.ShowDialog();
        }

        private void tsmiEditor_Click(object sender, EventArgs e)
        {
            Hide();
            if (mapEditorForm == null) mapEditorForm = new frmMapEditor(this);
            mapEditorForm.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
