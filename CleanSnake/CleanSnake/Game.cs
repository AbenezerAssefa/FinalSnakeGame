using System;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace SnakeGame
{
    class Game
    {
        private int screenWidth = Console.WindowWidth;
        private int screenHeight = Console.WindowHeight;
        private Random randomGenerator = new Random();
        private Snake snake;
        private int score = 5;
        private int berryX;
        private int berryY;
        private bool isGameOver;

        public void Start()
        {
            InitializeGame();
            while (!isGameOver)
            {
                PlayTurn();
            }
            DisplayGameOver();
        }

        private void InitializeGame()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            snake = new Snake(screenWidth / 2, screenHeight / 2);
            berryX = randomGenerator.Next(1, screenWidth - 2);
            berryY = randomGenerator.Next(1, screenHeight - 2);

            isGameOver = false;
        }

        private void PlayTurn()
        {
            Console.Clear();
            DrawBorders();
            HandleBerryCollision();
            DrawSnake();
            UpdateSnakeMovement();
            HandleInput();
            Thread.Sleep(500); // Pause for a bit before next move
        }

        private void DrawBorders()
        {
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
        }

        private void HandleBerryCollision()
        {
            if (berryX == snake.HeadX && berryY == snake.HeadY)
            {
                score++;
                berryX = randomGenerator.Next(1, screenWidth - 2);
                berryY = randomGenerator.Next(1, screenHeight - 2);
            }
            Console.SetCursorPosition(berryX, berryY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }

        private void DrawSnake()
        {
            foreach (var segment in snake.Body)
            {
                Console.SetCursorPosition(segment.X, segment.Y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("■");
            }
            Console.SetCursorPosition(snake.HeadX, snake.HeadY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
        }

        private void UpdateSnakeMovement()
        {
            snake.Move();
            if (snake.CollidesWithSelf() || snake.CollidesWithWall(screenWidth, screenHeight))
            {
                isGameOver = true;
            }
        }

        private void HandleInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                snake.ChangeDirection(keyPress.Key);
            }
        }

        private void DisplayGameOver()
        {
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }
    }
}
