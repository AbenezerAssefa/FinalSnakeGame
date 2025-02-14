using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame
{
    class Snake
    {
        private string currentDirection = "RIGHT";
        private string lastDirection = "RIGHT";
        private List<Pixel> body;

        public int HeadX => body.Last().X;
        public int HeadY => body.Last().Y;
        public List<Pixel> Body => body;

        public Snake(int startX, int startY)
        {
            body = new List<Pixel> { new Pixel(startX, startY) };
        }

        public void Move()
        {
            Pixel newHead = new Pixel(body.Last().X, body.Last().Y);

            switch (currentDirection)
            {
                case "UP":
                    newHead.Y--;
                    break;
                case "DOWN":
                    newHead.Y++;
                    break;
                case "LEFT":
                    newHead.X--;
                    break;
                case "RIGHT":
                    newHead.X++;
                    break;
            }

            body.Add(newHead);
            if (body.Count > 5) // 5 is the initial score
            {
                body.RemoveAt(0);
            }
        }

        public void ChangeDirection(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow && lastDirection != "DOWN")
                currentDirection = "UP";
            if (key == ConsoleKey.DownArrow && lastDirection != "UP")
                currentDirection = "DOWN";
            if (key == ConsoleKey.LeftArrow && lastDirection != "RIGHT")
                currentDirection = "LEFT";
            if (key == ConsoleKey.RightArrow && lastDirection != "LEFT")
                currentDirection = "RIGHT";

            lastDirection = currentDirection;
        }

        public bool CollidesWithSelf()
        {
            for (int i = 0; i < body.Count - 1; i++)
            {
                if (body[i].X == HeadX && body[i].Y == HeadY)
                    return true;
            }
            return false;
        }

        public bool CollidesWithWall(int width, int height)
        {
            return HeadX == 0 || HeadX == width - 1 || HeadY == 0 || HeadY == height - 1;
        }
    }
}
