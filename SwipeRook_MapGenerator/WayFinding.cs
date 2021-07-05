using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeRook_MapGenerator
{
    class WayFinding
    {
        // 별을 먹은 모든 경로를 구함(DFS) ->
        // 별을 먹었다면 먹은 별 삭제, visited 초기화하고 남은 별들을 다시 탐색 ->
        // 각 경로마다 반복

        Queue<Point> queue;
        List<Point> way;
        List<Point[]> wayList;
        bool[,] visited;
        public int minDistance;
        public Point[] FindDirection(int[,] map)
        {
            Point[] route = null;
            way = new List<Point>();
            wayList = new List<Point[]>();
            visited = new bool[map.GetLength(0), map.GetLength(1)];

            DFS_Recursion(map, GetRookPoint(map));

            try
            {
                minDistance = wayList.Min(x => x.Length) - 1;
                for (int i = 0; i < wayList.Count; i++)
                {
                    if (wayList[i].Length - 1 == minDistance)
                    {
                        route = wayList[i];
                    }
                }
            }
            catch { return route; }
            return route;
        }

        // DFS 재귀 함수
        void DFS_Recursion(int[,] map, Point p)
        {
            // 현재 노드를 방문한 것으로 표시
            visited[p.Y, p.X] = true;
            way.Add(p);
            List<Point> stars = new List<Point>();
            if(way.Count >= 3 && way[2].Equals(new Point(0,0)))
            {
                Console.Write("경로 : ");
                foreach (var w in way)
                    Console.Write(w);
                Console.WriteLine();
            }
            // 이동경로에 별이 있었는지 확인
            if (way.Count >= 2)
            {
                stars = GetStarPoint(map, way[way.Count - 2], way[way.Count - 1]);
                if (stars.Count > 0)
                {
                    
                    RemoveStarToMap(map, stars); // 먹은 별 지우기
                    // 별을 다 먹었다면 해당 경로는 종료
                    if (GetStarNumber(map) == 0)
                    {
                        wayList.Add(way.ToArray());
                        way.RemoveAt(way.Count-1);
                        visited[p.Y, p.X] = false;
                        AddStarToMap(map, stars);
                        return;
                    }
                    // 별이 남았다면 방문 초기화하고 DFS 재시작
                    else
                    {
                        InitAndDFS(map, stars, p);
                    }
                }
            }
            // 방문한 노드와 인접한 모든 노드를 가져온다.
            List<Point> posList = GetPointToGo(map, p);
            foreach (var pos in posList)
            {
                // 방문하지 않은 노드면 해당 노드로 이동
                if (!visited[pos.Y, pos.X])
                    DFS_Recursion(map, pos); // 순환 호출
                else
                {   // 방문했어도 별이 있다면 이동
                    if(GetStarPoint(map, p, pos).Count != 0)
                        DFS_Recursion(map, pos);
                }
            }
            way.RemoveAt(way.Count - 1);
            visited[p.Y, p.X] = false;
            AddStarToMap(map, stars);
        }

        void InitAndDFS(int[,] map, List<Point> gettedStar, Point p)
        {
            bool[,] t_visited = (bool[,])visited.Clone();
            visited = new bool[map.GetLength(0), map.GetLength(1)]; // 방문 초기화
            way.RemoveAt(way.Count - 1); // DFS를 이동하지 않고 한번 더 호출하므로 겹치지 않게 삭제

            DFS_Recursion(map, p); // 방문 초기화한 상태에서 DFS 재시작
            visited = (bool[,])t_visited.Clone(); // 끝난 후 다시 불러오기
            way.Add(p); // 다시 추가
        }

        void AddStarToMap(int[,] map, List<Point> stars)
        {
            foreach (var star in stars)
                map[star.Y, star.X] = (int)ObjectCode.star;
        }
        void RemoveStarToMap(int[,] map, List<Point> stars)
        {
            foreach (var star in stars)
                map[star.Y, star.X] = (int)ObjectCode.blank;
        }

        // 이동경로에 별이 있는지 확인
        public List<Point> GetStarPoint(int[,] map, Point now, Point target)
        {
            List<Point> stars = new List<Point>();
            // 위쪽일 때
            if(target.Y - now.Y < 0)
            {
                for(int y = now.Y; y >= target.Y; y--)
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
            else if(target.Y - now.Y > 0)
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

        // 갈 수 있는 Points 반환
        public List<Point> GetPointToGo(int[,] map, Point now)
        {
            List<Point> points = new List<Point>();

            // 위쪽 탐색
            int cnt = 0;
            for(int y = now.Y - 1; y >= 0; y--)
            {
                // 아무런 장애물이 없었을 때 맨 끝 추가
                if (y == 0 && map[y, now.X] != (int)ObjectCode.wall)
                {
                    points.Add(new Point(now.X, y));
                    break;
                }

                // 장애물이 있을 때 추가
                if(map[y, now.X] == (int)ObjectCode.wall)
                {
                    if (cnt == 0) break; // 시작하자마자 있을 땐 추가X
                    else
                    {
                        points.Add(new Point(now.X, y + 1));
                        break;
                    }
                }
                else cnt++;
            }
            // 오른쪽 탐색
            cnt = 0;
            for (int x = now.X + 1; x < map.GetLength(1); x++)
            {
                if (x == map.GetLength(1) - 1 && map[now.Y, x] != (int)ObjectCode.wall)
                {
                    points.Add(new Point(x, now.Y));
                    break;
                }

                if (map[now.Y, x] == (int)ObjectCode.wall)
                {
                    if (cnt == 0) break;
                    else
                    {
                        points.Add(new Point(x - 1, now.Y));
                        break;
                    }
                }
                else cnt++;
            }
            // 아래쪽 탐색
            cnt = 0;
            for (int y = now.Y + 1; y < map.GetLength(0); y++)
            {
                if (y == map.GetLength(0) - 1 && map[y, now.X] != (int)ObjectCode.wall)
                {
                    points.Add(new Point(now.X, y));
                    break;
                }

                if (map[y, now.X] == (int)ObjectCode.wall)
                {
                    if (cnt == 0) break;
                    else
                    {
                        points.Add(new Point(now.X, y - 1));
                        break;
                    }
                }
                else cnt++;
            }
            // 왼쪽 탐색
            cnt = 0;
            for (int x = now.X - 1; x >= 0; x--)
            {
                if (x == 0 && map[now.Y, x] != (int)ObjectCode.wall)
                {
                    points.Add(new Point(x, now.Y));
                    break;
                }

                if (map[now.Y, x] == (int)ObjectCode.wall)
                {
                    if (cnt == 0) break;
                    else 
                    {
                        points.Add(new Point(x + 1, now.Y));
                        break;
                    }
                }
                else cnt++;
            }
            return points;
        }

        // Rook의 위치를 반환
        Point GetRookPoint(int[,] map)
        {
            for(int y = 0; y < map.GetLength(0); y++)
            {
                for(int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == (int)ObjectCode.rook)
                        return new Point(x, y);
                }
            }

            return new Point();
        }

        // 맵이 정상적인지 확인
        public bool IsMapOK(int[,] map, int minDistance)
        {
            int starNum = GetStarNumber(map);
            int bfs_starNum = 0, bfs_distance = 0;
            BFS_GetStarNumber(map, starNum, ref bfs_starNum, ref bfs_distance);
            if (starNum != bfs_starNum || minDistance > bfs_distance)
                return false;
            else
            {
                this.minDistance = bfs_distance;
                return true;
            }
        }

        // 맵에 존재하는 별의 개수를 반환
        int GetStarNumber(int[,] map)
        {
            int cnt = 0;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for(int x = 0; x < map.GetLength(1); x++)
                {
                    if(map[y, x] == (int)ObjectCode.star)
                        cnt++;
                }
            }
            return cnt;
        }
        // 모든 별을 먹을 수 있는지, 최단 거리(되돌아오는 거리 미포함)가 몇인지 반환
        void BFS_GetStarNumber(int[,] map, int maxStarNum, ref int starNum, ref int dist)
        {
            queue = new Queue<Point>();
            List<Point> starPoints = new List<Point>();
            int[,] distance = new int[map.GetLength(0), map.GetLength(1)];
            InitArrayToValue(distance, -1);
            Point sPos = GetRookPoint(map);
            distance[sPos.Y, sPos.X] = 0; // 방문한 것을 표시
            queue.Enqueue(sPos);

            while (queue.Count != 0)
            {
                Point p = queue.Dequeue();

                // p에서 갈 수 있는 모든 경로 가져오기
                List<Point> posList = GetPointToGo(map, p);

                foreach (var pos in posList)
                {
                    if (distance[pos.Y, pos.X] == -1)
                    {
                        distance[pos.Y, pos.X] = distance[p.Y, p.X] + 1;
                        queue.Enqueue(pos);

                        // 이동 경로에 처음 발견된 별일 때만 추가
                        List<Point> _points = GetStarPoint(map, p, pos);
                        foreach(var _p in _points)
                        {
                            if (!starPoints.Contains(_p))
                            {
                                starPoints.Add(_p);
                                starNum = starPoints.Count;
                                dist = distance[pos.Y, pos.X];
                                // 모든 별들을 먹었다면 탐색 중지
                                if (starNum == maxStarNum)
                                    return;
                            }
                        }
                    }
                }
            }
        }

        // 2차원 배열 특정 값으로 초기화
        void InitArrayToValue(int[,] array2D, int value)
        {
            for(int y = 0; y < array2D.GetLength(0); y++)
            {
                for(int x = 0; x < array2D.GetLength(1); x++)
                {
                    array2D[y, x] = value;
                }
            }
        }
    }
}
