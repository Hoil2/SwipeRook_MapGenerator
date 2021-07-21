using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SwipeRook_MapGenerator
{
    class WayFinding
    {
        // 별을 먹은 모든 경로를 구함(DFS) ->
        // 별을 먹었다면 먹은 별 삭제, visited 초기화하고 남은 별들을 다시 탐색 ->
        // 각 경로마다 반복
        // 개선점
        // 모든 경로를 구하는건 엄청난 메모리와 시간을 필요하는 것 같음
        // BFS로 최단 경로 구해서 각각 최단경로를 합치는 방식으로 전환
        // BFS는 모든 경로를 구하는 데에 적합하지 않음
        Queue<Point> queue;
        List<Point> way;
        public List<Point[]> wayList;
        bool[,] visited;
        public int minDistance;
        int bfsCnt;
        public Point[] FindDirection(int[,] map)
        {
            wayList = new List<Point[]>();
            BFS(map, new Point[] { GetRookPoint(map) });

            // 테스트 출력
            //foreach (var w in wayList)
            //{
            //    foreach (var v in w)
            //        Console.Write(v);
            //    Console.WriteLine();
            //}
            minDistance = wayList.Min(x => x.Length)-1;
            return wayList.Find(x => x.Length-1 == minDistance);
        }
        void BFS(int[,] map, Point[] route)
        {
            queue = new Queue<Point>();
            List<Point> endPoints = new List<Point>();
            List<Point> starPoints = new List<Point>();
            int[,] distance = new int[map.GetLength(0), map.GetLength(1)];
            Point sPoint = route[route.Length - 1];
            InitArrayToValue(distance, -1);
            int[,] c_map = (int[,])map.Clone();
            int[,] starMinNum = new int[map.GetLength(0), map.GetLength(1)];
            List<Point>[,] parentsPoints = new List<Point>[map.GetLength(0), map.GetLength(1)];
            InitArrayToValue(starMinNum, int.MaxValue);
            RemoveStarByRoute(c_map, route); // 지나온 별들을 삭제
            distance[sPoint.Y, sPoint.X] = 0; // 방문한 것을 표시
            queue.Enqueue(sPoint);
            bfsCnt = 0;

            while (queue.Count != 0)
            {
                Point p = queue.Dequeue();
                bfsCnt++;
                // p에서 갈 수 있는 모든 경로 가져오기
                List<Point> posList = GetPointToGo(c_map, p);

                foreach (var pos in posList)
                {   // 처음 가본 곳이거나 가봤지만 가야 하는 경우
                    if (distance[pos.Y, pos.X] == -1 || distance[p.Y, p.X] == distance[pos.Y, pos.X] - 1 ) 
                    {
                        distance[pos.Y, pos.X] = distance[p.Y, p.X] + 1;
                        parentsPoints[pos.Y, pos.X] = new List<Point>();
                        parentsPoints[pos.Y, pos.X].Add(p);
                        queue.Enqueue(pos);

                        // 별을 먹을 때 bfsCnt가 저장된 카운트보다 작거나 같을 때만 먹은 별 endPoints에 추가하고 각 별에 카운트 갱신
                        starPoints = GetTargetPoint(c_map, p, pos, (int)ObjectCode.star);
                        if(starPoints.Count > 0)
                        {
                            if (!endPoints.Contains(pos))
                                endPoints.Add(pos); // 별을 먹은 경로의 마지막 Point 저장
                        }
                    }
                }
            }

            // 먹은 마지막 Points를 바탕으로 _routes 경로 생성
            List<List<Point>> routes = CreateRoutesByEndPoints(route, endPoints, parentsPoints);
            foreach (var r in routes)
            {
                //Console.WriteLine("현재 루트 개수 : " + routes.Count);
                //foreach (var w in r)
                    //Console.Write(w);
                //Console.WriteLine();
                c_map = (int[,])map.Clone();
                RemoveStarByRoute(c_map, r.ToArray());
                if (GetStarNumber(c_map) > 0)
                    BFS(map, r.ToArray());
                else
                {
                    wayList.Add(r.ToArray());
                }
            }
        }

        bool CheckStarMinNum(List<Point> starPoints, int[,] starMinNum, int bfsCnt)
        {
            foreach (var sp in starPoints)
            {
                if (starMinNum[sp.Y, sp.X] < bfsCnt)
                { 
                    return false;
                }
            }
            return true;
        }
        void RemoveStarByRoute(int[,] map, Point[] route)
        {
            for(int i = 0; i < route.Length-1; i++)
            {
                List<Point> stars = GetTargetPoint(map, route[i], route[i+1], (int)ObjectCode.star);
                foreach(var s in stars)
                {
                    map[s.Y, s.X] = (int)ObjectCode.blank;
                }
            }
        }

        // routes : 저장되고 있는 route들
        // endPoints : 마지막으로 별을 먹은 위치들
        // parentsPoints : 각 지점마다 부모의 point를 저장한 리스트
        List<List<Point>> CreateRoutesByEndPoints(Point[] routes, List<Point> endPoints, List<Point>[,] parentsPoints)
        {
            List<List<Point>> resultRoutes = new List<List<Point>>();

            // 경로를 추적해 생성
            foreach (var endPoint in endPoints)
            {
                List<Point> route = new List<Point>();
                route.Add(endPoint);
                resultRoutes.Add(route);
                DFS_Recursion(resultRoutes, route, endPoint, parentsPoints);
            }

            // 기존의 경로와 추적한 경로를 합침
            foreach (var resultRoute in resultRoutes)
            {
                resultRoute.Reverse();
                resultRoute.RemoveAt(0);
                resultRoute.InsertRange(0, routes);
            }
            
            return resultRoutes;
        }

        // 부모의 경로를 따라가며 모든 경로를 저장하는 재귀함수
        void DFS_Recursion(List<List<Point>> resultRoutes, List<Point> nowRoute, Point p, List<Point>[,] parentsPoints)
        {
            if (parentsPoints[p.Y, p.X] == null) return;
            // 해당 포인트에 분기가 있다면
            for (int i = 1; i < parentsPoints[p.Y, p.X].Count; i++)
            {
                List<Point> copyRoute = nowRoute.ToList();
                copyRoute.Add(parentsPoints[p.Y, p.X][i]);
                resultRoutes.Add(copyRoute);
                DFS_Recursion(resultRoutes, copyRoute, parentsPoints[p.Y, p.X][i], parentsPoints);
            }
            nowRoute.Add(parentsPoints[p.Y, p.X][0]);
            DFS_Recursion(resultRoutes, nowRoute, parentsPoints[p.Y, p.X][0], parentsPoints); // 순환 호출
        }

        public List<Point> GetPointToGoByDistance(int[,] map, int[,] distance, Point now)
        {
            List<Point> points = GetPointToGo(map, now);
            List<Point> resultPoints = new List<Point>();
            int value = distance[now.Y, now.X];
            for(int i = 0; i < points.Count; i++)
            {
                List<Point> targetPoint = GetTargetPoint(distance, now, points[i], value - 1);
                if (targetPoint.Count > 0) 
                    resultPoints.Add(targetPoint[0]);
                //if (targetPoint.Count > 1)
                    //Console.WriteLine("2가지 경우도 있음");
            }
            
            return resultPoints;
        }

        /*
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

            foreach (var w in way)
                Console.Write(w);
            Console.WriteLine();
            Console.WriteLine();
            List<Point> stars = new List<Point>();
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
        */

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

        // 이동경로에 특정 타겟이 있는지 확인
        public List<Point> GetTargetPoint(int[,] map, Point start, Point end, int target)
        {
            List<Point> points = new List<Point>();
            // 위쪽일 때
            if(end.Y - start.Y < 0)
            {
                for(int y = start.Y; y >= end.Y; y--)
                {
                    if (map[y, start.X] == target)
                        points.Add(new Point(start.X, y));
                }
            }
            // 오른쪽일 때
            else if (end.X - start.X > 0)
            {
                for (int x = start.X; x <= end.X; x++)
                {
                    if (map[start.Y, x] == target)
                        points.Add(new Point(x, start.Y));
                }
            }
            // 아래쪽일 때
            else if(end.Y - start.Y > 0)
            {
                for (int y = start.Y; y <= end.Y; y++)
                {
                    if (map[y, start.X] == target)
                        points.Add(new Point(start.X, y));
                }
            }
            // 왼쪽일 때
            else
            {
                for (int x = start.X; x >= end.X; x--)
                {
                    if (map[start.Y, x] == target)
                        points.Add(new Point(x, start.Y));
                }
            }
            return points;
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
                        List<Point> _points = GetTargetPoint(map, p, pos, (int)ObjectCode.star);
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

        void Print2DArray(int[,] array2D)
        {
            for (int y = 0; y < array2D.GetLength(0); y++)
            {
                for (int x = 0; x < array2D.GetLength(1); x++)
                {
                    Console.Write(array2D[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
