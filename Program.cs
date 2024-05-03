using System.Collections.Generic;
using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace SnakeGame
{
    class Program
    {
        //BIẾN TOÀN CỤC

        //KHỞI TẠO RẮN
        struct Point
        {
            public int X;
            public int Y;
        }
        static List<Point> snake = new List<Point>();


        //HÀM DIRECTION 
        enum Direction
        {
            right,
            left,
            up,
            down
        }
        static Direction direction;

        static int width = 70;
        static int height = 22;

        static Point point = new Point();
        static bool inGame = true;
        static DateTime start = DateTime.Now;
        static Point food;
        static Point obstacle;
        static int score = 0;
        static int view;


        //VOID MAIN
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 28);
            Console.CursorVisible = false;
            drawLogo();
            RunMenu();
        }


        //HÀM VẼ LOGO
        static void drawLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n");
            Console.WriteLine("                            oo_                         ");
            Console.WriteLine("                           /  _)-<              _        ");
            Console.WriteLine("                           '-- `.              | |       ");
            Console.WriteLine("                              ` '. __ __   __ _| | _____ ");
            Console.WriteLine("                             _|  '| '_  | / _` | |/ / _ |");
            Console.WriteLine("                           ,-'  .'| | | | (_ | |   <  __/");
            Console.WriteLine("                         (_..--'  |_| |_| |__|_|_| ||___|");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n");
            Console.WriteLine("                                 P R E S E N T S");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                T H E   S N A K E");
            Console.ReadKey();
            Console.Clear();

        }

        static void DrawYouWin()
        {
            Console.SetCursorPosition(1, 8);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                    __");
            Console.WriteLine("                        __ __ __  __ __     __ __ __|__|_____");
            Console.WriteLine("                       |  |  | . |  |  |   |  |  |  |  |     |");
            Console.WriteLine("                       |__   |___|_____|   |________|__|__|__|");
            Console.WriteLine("                       |_____|");

        }

        //MENU
        static void RunMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(32, 8);
            Console.Write("1-Level EASY");

            Console.SetCursorPosition(32, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("2-Level HARD");

            Console.SetCursorPosition(32, 12);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("3-Level VERY HARD");

            Console.SetCursorPosition(32, 14);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("4-EXIT");

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Level_1();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Level_2();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Level_3();
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    RunMenu();
                    break;
            }
        }
        //HÀM LEVEL 1
        static void Level_1()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(28, 8);
            Console.Write("WELCOME TO LEVEL EASY");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(28, 10);
            Console.Write("PRESS ANY KEY TO START");
            Console.ReadKey();
            Console.Clear();

            int scoreTarget = 10;
            MakePlayground();
            Make();
            Food();
            Obstacle();
            AppearFood();

            while (inGame)
            {
                CheckForInput();
                ResetFood();
                CheckForSelfColision();
                Move();
                IsFoodEaten();
                IsHitingObstacle();
                Thread.Sleep(250);

                if (score >= scoreTarget)
                {
                    Console.Clear();
                    DrawYouWin();
                    Console.SetCursorPosition(37, 16);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"SCORE: {score}");
                    Console.ReadKey();
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(15, 12);
                    Console.Write("1 - GO TO THE NEXT LEVEL");

                    Console.SetCursorPosition(45, 12);
                    Console.Write("2 - RETURN TO MENU ");

                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            Level_2();
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            RunMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        //HÀM LEVEL 2
        static void Level_2()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(30, 8);
            Console.Write("WELCOME TO LEVEL HARD");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(30, 10);
            Console.Write("PRESS ANY KEY TO START");
            Console.ReadKey();
            Console.Clear();

            int scoreTarget = 20;
            MakePlayground();
            Make();
            Food();
            Obstacle();
            AppearFood();

            while (inGame)
            {
                CheckForInput();
                ResetFood();
                CheckForSelfColision();
                Move();
                IsFoodEaten();
                IsHitingObstacle();
                Thread.Sleep(150);

                if (score >= scoreTarget)
                {
                    Console.Clear();
                    DrawYouWin();
                    Console.SetCursorPosition(37, 16);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"SCORE: {score}");
                    Console.ReadKey();
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(15, 14);
                    Console.Write("1 - GO TO THE NEXT LEVEL");

                    Console.SetCursorPosition(45, 14);
                    Console.Write("2 - RETURN TO MENU ");

                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            Level_3();
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            RunMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        //HÀM LEVEL 3
        static void Level_3()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(30, 8);
            Console.Write("WELCOME TO LEVEL VERY HARD");
            Console.SetCursorPosition(30, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("PRESS ANY KEY TO START");
            Console.ReadKey();
            Console.Clear();

            int scoreTarget = 30;
            MakePlayground();
            Make();
            Food();
            Obstacle();
            AppearFood();

            while (inGame)
            {
                CheckForInput();
                ResetFood();
                CheckForSelfColision();
                Move();
                IsFoodEaten();
                IsHitingObstacle();
                Thread.Sleep(100);

                if (score >= scoreTarget)
                {

                    Console.Clear();
                    DrawYouWin();
                    Console.SetCursorPosition(32, 16);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"SCORE: {score}");
                    Console.ReadKey();
                    RunMenu();
                }
            }
        }

        //VẼ KHUNG
        static void MakePlayground()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int x = 1; x <= width; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.WriteLine("=");
                Console.SetCursorPosition(x, height +1);
                Console.WriteLine("=");
            }
            for (int i = 1; i <= height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.WriteLine("|");
                Console.SetCursorPosition(width +1, i);
                Console.WriteLine("|");
            }

            Console.SetCursorPosition(1, 24);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Score = {0}", score);
            Console.ForegroundColor = ConsoleColor.White;
        }


        //TẠO RẮN
        static void Make()
        {
            for (int i = 1; i < 6; i++)
            {
                point.X = i;
                point.Y = 5;
                snake.Add(point);

            }
            direction = Direction.right;//khởi tạo hướng đi ban đầu cho rắn

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write("O");

        }

        //RẮN DI CHUYỂN SANG PHẢI
        static void Right()
        {
            ClearPoint(snake[0]);
            snake.RemoveAt(0);
            point.Y = snake[snake.Count - 1].Y;
            point.X = snake[snake.Count - 1].X + 1;
            snake.Add(point);
            DrawPoint(point);
        }

        //RẮN DI CHUYỂN SANG TRÁI
        static void Left()
        {
            ClearPoint(snake[0]);
            snake.RemoveAt(0);
            point.Y = snake[snake.Count - 1].Y;
            point.X = snake[snake.Count - 1].X - 1;
            snake.Add(point);
            DrawPoint(point);
        }

        //RẮN DI CHUYỂN LÊN TRÊN
        static void Up()
        {
            ClearPoint(snake[0]);
            snake.RemoveAt(0);
            point.Y = snake[snake.Count - 1].Y - 1;
            point.X = snake[snake.Count - 1].X;
            snake.Add(point);
            DrawPoint(point);
        }

        //RẮN DI CHUYỂN XUỐNG DƯỚI
        static void Down()
        {
            ClearPoint(snake[0]);
            snake.RemoveAt(0);
            point.Y = snake[snake.Count - 1].Y + 1;
            point.X = snake[snake.Count - 1].X;
            snake.Add(point);
            DrawPoint(point);
        }

        //LỆNH NHẬN PHÍM ĐIỀU KHIỂN CỦA NGƯỜI CHƠI
        static void CheckForInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (direction != Direction.right)
                    {
                        direction = Direction.left;
                    }
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    if (direction != Direction.left)
                    {
                        direction = Direction.right;
                    }

                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (direction != Direction.down)
                    {
                        direction = Direction.up;
                    }

                }
                if (key.Key == ConsoleKey.DownArrow && direction != Direction.up)
                {
                    direction = Direction.down;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    inGame = false;
                }
            }
        }

        //KIỂM TRA RẮN CÓ DI CHUYỂN KHÔNG
        static void Move()
        {
            bool on = SnakeMove();
            if (!on)
            {
                GameOver();
            }
        }


        //HÀM HƯỚNG DI CHUYỂN VÀ CHECK ĐỤNG TƯỜNG
        static bool SnakeMove()
        {
            switch (direction)
            {
                case Direction.right:

                    if (snake[snake.Count - 1].X >= width)
                    {
                        return false;
                    }
                    Right();
                    break;
                case Direction.left:
                    if (snake[snake.Count - 1].X <= 1)
                    {
                        return false;
                    }
                    Left();
                    break;
                case Direction.up:
                    if (snake[snake.Count - 1].Y <= 1)
                    {
                        return false;
                    }
                    Up();
                    break;
                case Direction.down:
                    if (snake[snake.Count - 1].Y >= height)
                    {
                        return false;
                    }
                    Down();
                    break;
                default:
                    break;
            }
            return true;
        }


        //HÀM XÓA 1 ĐỐT
        static void ClearPoint(Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write(" ");
        }

        //HÀM VẼ THÊM ĐỐT
        static void DrawPoint(Point point)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write("O");
        }



        //KẾT THÚC GAME
        static void GameOver()
        {
            inGame = false;
            Console.Clear();

            Console.SetCursorPosition(35, 13);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("GAME OVER!");

            Console.SetCursorPosition(35, 15);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"SCORE: {score}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();

        }


        //HÀM CHECK ĐỤNG MÌNH
        static bool CheckForSelfColission()
        {
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[0].X == snake[i].X && snake[0].Y == snake[i].Y)
                {
                    return true;
                }
            }
            return false;
        }

        //KIỂM TRA XEM CÓ ĐỤNG MÌNH KHÔNG
        static void CheckForSelfColision()
        {
            if (CheckForSelfColission())
            {
                GameOver();
            }
        }

        //HÀM CHECK ĐỤNG FOOD/ OBSTACLE
        static bool CheckColission(int x, int y)
        {
            if (snake[snake.Count -1].X == x && snake[snake.Count - 1].Y == y)
            {
               return true;
            }
            else
               return false;
        }

        //KIỂM TRA XEM CÓ ĂN KHÔNG
        static void IsFoodEaten()
        {
            if (CheckColission(food.X, food.Y)) 
            {
                score += view;
                Grow();

                Console.SetCursorPosition(1, 24);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Score = {0}", score);
                Console.ForegroundColor = ConsoleColor.White;

                Food();
                AppearFood();
            }
        }

        //HÀM TĂNG CHIỀU DÀI RẮN KHI ĂN THÊM
        static void Grow()
        {
            for (int i = 1; i <= view; i++)
            {
                switch (direction)
                {
                    case Direction.right:
                        point.Y = snake[snake.Count - 1].Y;
                        point.X = snake[snake.Count - 1].X + 1;
                        break;
                    case Direction.left:
                        point.Y = snake[snake.Count - 1].Y;
                        point.X = snake[snake.Count - 1].X - 1;
                        break;
                    case Direction.up:
                        point.Y = snake[snake.Count - 1].Y - 1;
                        point.X = snake[snake.Count - 1].X;
                        break;
                    case Direction.down:
                        point.Y = snake[snake.Count - 1].Y + 1;
                        point.X = snake[snake.Count - 1].X;
                        break;
                    default:
                        break;
                }

                ClearPoint(snake[0]);
                snake.Add(point);
                DrawPoint(point);
            }
        }


        //CẬP NHẬT THỨC ĂN MỚI
        static void ResetFood()
        {
            if (start <= DateTime.Now.Subtract(TimeSpan.FromSeconds(10)))
            {
                DisappearFood();
                start = DateTime.Now;
                Food();
                AppearFood();
            }
        }


        //HÀM TẠO FOOD
        static void Food()
        {
            Random random = new Random();
            view = random.Next(1, 5);
            food.X = random.Next(1, width);
            food.Y = random.Next(1, height);
        }


        //HÀM CHO FOOD XUẤT HIỆN
        static void AppearFood()
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(view);
            Console.ForegroundColor = ConsoleColor.White;
        }


        //HÀM CHO FOOD BIẾN MẤT
        static void DisappearFood()
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.Write(" ");
        }


        //HÀM TẠO OBSTACLE
        static void Obstacle()
        {
            do
            {
                Random random = new Random();
                obstacle.X = random.Next(1, width);
                obstacle.Y = random.Next(1, height);
            }
            while (obstacle.X == food.X && obstacle.Y == food.Y);

            Console.SetCursorPosition(obstacle.X, obstacle.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("X");
            Console.ForegroundColor = ConsoleColor.White;
        }


        //KIỂM TRA XEM CÓ ĐỤNG OBSTACLE KHÔNG
        static void IsHitingObstacle()
        {
            if (CheckColission(obstacle.X, obstacle.Y))
            {
                GameOver();
            }
        }
    }
}

