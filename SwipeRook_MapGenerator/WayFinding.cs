using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace SwipeRook_MapGenerator
{
    class WayFinding
    {
        Queue<Point> queue;
        public List<List<Point>> routeList;
        public int minDistance;
        public List<List<Point>> completeRouteList;

        // 최단 경로 찾기
        public Point[] FindDirection(int[,] map)
        {
            completeRouteList = new List<List<Point>>();
            routeList = BFS(map, GetRookPoint(map));

            while (routeList.Count > 0) {
                var route = routeList[0];
                Console.WriteLine("루트 : ");
                foreach(var r in route)
                {
                    Console.Write(r);
                }
                Console.WriteLine();
                var t_map = RemoveStarByRoute(map, route);
                //Console.WriteLine("removestarByRoute");
                //Print2DArray(t_map);
                if (GetStarNumber(t_map) == 0) {
                    completeRouteList.Add(route);
                    routeList.Remove(route);
                    continue;
                }

                var bfsRoutes = BFS(t_map, route[route.Count - 1]);
                Console.WriteLine("bfs루트 : " + route[route.Count - 1] + "시작");
                foreach (var rs in bfsRoutes)
                {
                    foreach(var r in rs)
                        Console.Write(r);
                    Console.WriteLine();
                }
                PrependToBFSRoutes(route, bfsRoutes);


                routeList.AddRange(bfsRoutes);
                routeList.Remove(route);
                
            }
            // 테스트 출력
            Console.WriteLine("최종 경로들");
            foreach (var w in completeRouteList)
            {
                foreach (var v in w)
                    Console.Write(v);
                Console.WriteLine();
                Console.WriteLine();
            }
            minDistance = completeRouteList.Min(x => x.Count)-1;
            return completeRouteList.Find(x => x.Count-1 == minDistance).ToArray();
        }

        // BFSRoutes 앞에 앞서 탐색한 route 추가
        void PrependToBFSRoutes(List<Point> route, List<List<Point>> bfsRoutes)
        {
            route.RemoveAt(route.Count - 1);
            foreach(var bfsRoute in bfsRoutes)
            {
                bfsRoute.InsertRange(0, route);
            }
        }

        // BFS로 탐색
        List<List<Point>> BFS(int[,] map, Point sPoint)
        {
            List<List<Point>> routes = new List<List<Point>>();
            queue = new Queue<Point>();
            int[,] distance = new int[map.GetLength(0), map.GetLength(1)];

            // 탐색을 위한 distance 배열 초기화
            for(int y = 0; y < distance.GetLength(0); y++)
            {
                for(int x = 0; x < distance.GetLength(1); x++)
                {
                    distance[y, x] = -1;
                    if(map[y, x] == (int)ObjectCode.wall)
                    {
                        distance[y, x] = -2;
                    }
                }
            }

            distance[sPoint.Y, sPoint.X] = 0; // 방문한 것을 표시
            queue.Enqueue(sPoint);

            while (queue.Count != 0)
            {
                Point start = queue.Dequeue();
                // p에서 갈 수 있는 모든 경로 가져오기
                List<Point> posList = GetPointToGo(map, start);
                
                foreach (var end in posList)
                {
                    // 동선이 안겹칠 때 
                    if (distance[end.Y, end.X] == -1)
                    {
                        distance[end.Y, end.X] = distance[start.Y, start.X] + 1;
                        // 별을 먹었다면 
                        if (GetTargetPoint(map, start, end, (int)ObjectCode.star).Count > 0)
                        {
                            // 루트 저장
                            routes.AddRange(GetRouteByDistance(distance, end));
                            Console.WriteLine("별 먹음");
                        }
                        else
                        {
                            queue.Enqueue(end);
                        }
                    }
                    // 동선이 겹칠 때, 별이 바로 앞에 있을 때
                    else// if(distance[end.Y, end.X] == distance[start.Y, start.X])
                    {
                        if(GetTargetPoint(map, start, end, (int)ObjectCode.star).Count > 0)
                        {
                            //Console.WriteLine("동선 겹침, 이동 경로 :" + start + " ->" + end);
                            
                            // 경로 가져오기
                            var t_routes = GetRouteByDistance(distance, start);
                            // 맨 뒤에 end Point 추가
                            foreach(var r in t_routes)
                            {
                                r.Add(end);
                            }
                            // 별 먹은 루트 저장
                            routes.AddRange(t_routes);
                        }
                    }
                }
            }

            Console.WriteLine("BFS 경로");
            foreach (var r in routes)
            {
                foreach (var n in r)
                    Console.Write(n);
                Console.WriteLine();
            }

            Print2DArray(distance);
            Console.WriteLine();
            return routes;
        }

        // distance를 참조해서 start 위치에서 0 위치까지 경로들 구하는 함수
        List<List<Point>> GetRouteByDistance(int[,] distance, Point start)
        {
            List<List<Point>> completeRoutes = new List<List<Point>>();
            List<List<Point>> routes = new List<List<Point>>();
            routes.Add(new List<Point>());
            routes[0].Add(start);
            while(routes.Count != 0) {
                var route = routes[0];
                // 갈 수 있는 곳 가져오기
                List<Point> points = GetPointByDistance(distance, route[route.Count - 1]);
                if (points.Count == 1)
                {
                    route.Add(points[0]);
                }
                else if(points.Count > 1) // 여러 개일 때 루트 복제
                {
                    for (int i = 1; i < points.Count; i++)
                    {
                        //Console.WriteLine("루트 복제");
                        var t = route.ToList();
                        t.Add(points[i]);
                        routes.Add(t);
                    }
                    route.Add(points[0]);
                }
                else // 최종 목적지에 도달했을 경우
                {
                    route.Reverse();
                    completeRoutes.Add(route);
                    routes.Remove(route);
                }
            }

            return completeRoutes;
        }

        // distance 참조해서 start 위치에서 가야하는 위치들 반환
        List<Point> GetPointByDistance(int[,] distance, Point start)
        {
            List<Point> points = new List<Point>();
            int nowNum = distance[start.Y, start.X];
            if (nowNum <= 0) return points;
            // 위쪽 탐색
            for(int y = start.Y - 1; y >= 0; y--)
            {
                if (distance[y, start.X] == nowNum - 1)
                {
                    points.Add(new Point(start.X, y));
                    break;
                }
                else if (distance[y, start.X] == -2) break;
            }

            // 아래쪽 탐색
            for (int y = start.Y + 1; y < distance.GetLength(0); y++)
            {
                if (distance[y, start.X] == nowNum - 1)
                {
                    points.Add(new Point(start.X, y));
                    break;
                }
                else if (distance[y, start.X] == -2) break;
            }

            // 왼쪽 탐색
            for (int x = start.X - 1; x >= 0; x--)
            {
                if (distance[start.Y, x] == nowNum - 1)
                {
                    points.Add(new Point(x, start.Y));
                    break;
                }
                else if(distance[start.Y, x] == -2) break;
            }

            // 오른쪽 탐색
            for (int x = start.X + 1; x < distance.GetLength(1); x++)
            {
                if (distance[start.Y, x] == nowNum - 1)
                {
                    points.Add(new Point(x, start.Y));
                    break;
                }
                else if (distance[start.Y, x] == -2) break;
            }

            return points;
        } 
            
        // 맵에서 루트에 있는 별들 삭제
        int[,] RemoveStarByRoute(int[,] map, List<Point> route)
        {
            int[,] t_map = (int[,])map.Clone();
            for(int i = 0; i < route.Count-1; i++)
            {
                List<Point> stars = GetTargetPoint(t_map, route[i], route[i+1], (int)ObjectCode.star);
                foreach(var s in stars)
                {
                    t_map[s.Y, s.X] = (int)ObjectCode.blank;
                }
            }
            return t_map;
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

        // 맵에 존재하는 별의 개수를 반환
        int GetStarNumber(int[,] map)
        {
            int cnt = 0;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == (int)ObjectCode.star)
                        cnt++;
                }
            }
            return cnt;
        }

        // 맵이 정상적인지 확인
        public bool IsMapOK(int[,] map)
        {
            int starNum = GetStarNumber(map);
            int bfs_starNum = BFS_GetStarNumber(map);
            if (starNum != bfs_starNum)
                return false;
            else
                return true;
        }
        
        // BFS로 먹을 수 있는 별 전부 먹기
        int BFS_GetStarNumber(int[,] map)
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
                            }
                        }
                    }
                }
            }

            return starPoints.Count;
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
