using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SwipeRook_MapGenerator
{
    class MapGeneration
    {
        List<Point> points;
        int[,] map;
        Random rand = new Random();

        public int[,] CreateMap(Size size, int star, int wall)
        {
            points = new List<Point>();
            map = new int[size.Height, size.Width];

            // 맵의 모든 위치를 리스트에 저장 (랜덤을 효율적으로 하기 위해)
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    points.Add(new Point(x, y));
                }
            }

            // wall이 0이면 개수를 랜덤으로 설정
            if (wall == 0) wall = new Random().Next(1, map.GetLength(0) * map.GetLength(1) / 2);

            // wall 생성
            CreateWallToMap(wall);
            // star 생성
            CreateStarToMap(star);
            // rook 생성
            CreateRookToMap();

            return map;
        }

        // 맵에 wall 생성
        private void CreateWallToMap(int wall)
        {
            // wall 개수만큼 map에 wall 생성
            for (int i = wall; i > 0; i--)
            {
                // 랜덤으로 위치 얻기
                int index = rand.Next(0, points.Count);
                // 맵에 wall 생성
                map[points[index].Y, points[index].X] = (int)ObjectCode.wall;
                points.RemoveAt(index);
            }
        }

        // 맵에 star 생성
        private void CreateStarToMap(int star)
        {
            for (int i = star; i > 0; i--)
            {
                int index = rand.Next(0, points.Count);
                map[points[index].Y, points[index].X] = (int)ObjectCode.star;
                points.RemoveAt(index);
            }
        }

        // 맵에 rook 생성
        private void CreateRookToMap()
        {
            int index = rand.Next(0, points.Count);
            map[points[index].Y, points[index].X] = (int)ObjectCode.rook;
            points.RemoveAt(index);
        }

        public void PrintMap()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.Write(map[y, x] + " ");
                }
                Console.WriteLine();
            }
        }

        public Bitmap DrawMap()
        {
            Bitmap bitmap = new Bitmap(map.GetLength(1) * 25, map.GetLength(0) * 25);
            Graphics g = Graphics.FromImage(bitmap);

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == (int)ObjectCode.blank)
                    {
                        g.DrawImage(Properties.Resources.Blank, x * 25, y * 25);
                    }
                    else if (map[y, x] == (int)ObjectCode.wall)
                    {
                        g.DrawImage(Properties.Resources.Wall, x * 25, y * 25);
                    }
                    else if (map[y, x] == (int)ObjectCode.star)
                    {
                        g.DrawImage(Properties.Resources.Star, x * 25, y * 25);
                    }
                    else if (map[y, x] == (int)ObjectCode.rook)
                    {
                        g.DrawImage(Properties.Resources.Rook, x * 25, y * 25);
                    }
                }
            }
            g.Dispose();
            return bitmap;
        }

        public Bitmap DrawMap(int[,] map)
        {
            Bitmap bitmap = new Bitmap(map.GetLength(1) * 25, map.GetLength(0) * 25);
            Graphics g = Graphics.FromImage(bitmap);

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == (int)ObjectCode.blank)
                    {
                        g.DrawImage(Properties.Resources.Blank, x * 25, y * 25);
                    }
                    else if (map[y, x] == (int)ObjectCode.wall)
                    {
                        g.DrawImage(Properties.Resources.Wall, x * 25, y * 25);
                    }
                    else if (map[y, x] == (int)ObjectCode.star)
                    {
                        g.DrawImage(Properties.Resources.Star, x * 25, y * 25);
                    }
                    else if (map[y, x] == (int)ObjectCode.rook)
                    {
                        g.DrawImage(Properties.Resources.Rook, x * 25, y * 25);
                    }
                }
            }
            g.Dispose();
            return bitmap;
        }

        // 실행 흐름
        // 진행 중 별을 먹었다면 맵에서 별 삭제
        // 최단 경로에 따라 맵에 룩을 배치
        // 비트맵에 저장
        public Bitmap[] DrawMapByPath(int[,] map, Point[] path)
        {
            Bitmap[] pathBitmaps = new Bitmap[path.Length];
            int[,] c_map = (int[,])map.Clone();
            
            for (int i = 0; i < path.Length; i++)
            {
                if (i >= 1)
                {
                    List<Point> stars = GetStarPoint(c_map, path[i - 1], path[i]);
                    if (stars.Count > 0)
                        RemoveStarToMap(c_map, stars);
                }
                MoveRook(c_map, path[i]);
                pathBitmaps[i] = DrawMap(c_map);
            }
            return pathBitmaps;
        }

        void MoveRook(int[,] map, Point target) {
            // 맵의 룩 삭제
            for(int y = 0; y < map.GetLength(0); y++)
            {
                for(int x = 0; x < map.GetLength(1); x++)
                {
                    if(map[y, x] == (int)ObjectCode.rook)
                        map[y, x] = (int)ObjectCode.blank;
                }
            }
            AddRook(map, target);
        }

        void AddRook(int[,] map, Point rookPoint)
        {
            map[rookPoint.Y, rookPoint.X] = (int)ObjectCode.rook;
        }

        // 이동 경로의 별들 반환
        public List<Point> GetStarPoint(int[,] map, Point now, Point target)
        {
            List<Point> stars = new List<Point>();
            // 위쪽일 때
            if (target.Y - now.Y < 0)
            {
                for (int y = now.Y; y >= target.Y; y--)
                {
                    if (map[y, now.X] == (int)ObjectCode.star)
                        stars.Add(new Point(now.X, y));
                }
            }
            // 오른쪽일 때
            else if (target.X - now.X > 0)
            {
                for (int x = now.X; x <= target.X; x++)
                {
                    if (map[now.Y, x] == (int)ObjectCode.star)
                        stars.Add(new Point(x, now.Y));
                }
            }
            // 아래쪽일 때
            else if (target.Y - now.Y > 0)
            {
                for (int y = now.Y; y <= target.Y; y++)
                {
                    if (map[y, now.X] == (int)ObjectCode.star)
                        stars.Add(new Point(now.X, y));
                }
            }
            // 왼쪽일 때
            else
            {
                for (int x = now.X; x >= target.X; x--)
                {
                    if (map[now.Y, x] == (int)ObjectCode.star)
                        stars.Add(new Point(x, now.Y));
                }
            }
            return stars;
        }

        // 맵에서 특정 별 삭제
        void RemoveStarToMap(int[,] map, List<Point> stars)
        {
            foreach (var star in stars)
                map[star.Y, star.X] = (int)ObjectCode.blank;
        }
    }
}
