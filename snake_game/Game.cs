using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game {
    public class Game {
        private readonly int width;
        private readonly int height;

        private readonly Snake snake;
        private readonly Food food;
        private readonly Random random;

        public Game(int _width, int _height) {
            width = _width;
            height = _height;

            snake = new Snake(Brushes.DarkGreen);
            random = new Random();
            food = new Food(0, 0, Brushes.IndianRed);
            Restart();
        }

        public void Restart() {
            snake.Clear();
            CreateFood();
        }

        public void Draw(Graphics graphics) {
            snake.Draw(graphics);
            food.Draw(graphics);
        }

        public int GetPoints() {
            return snake.Points;
        }

        public bool SnakeRising() {
            switch (snake.direction) {
                case Direction.Down:
                    return TryEat(0, 1);
                case Direction.Up:
                    return TryEat(0, -1);
                case Direction.Right:
                    return TryEat(1, 0);
                case Direction.Left:
                    return TryEat(-1, 0);
            }
            return false;
        }

        public bool GameOver() {
            return snake.HeadX > width || snake.HeadX < 0 || snake.HeadY > height || snake.HeadY < 0 || snake.EatSnake();
        }

        public void ChangeDirection(Direction direction) {
            snake.direction = direction;
        }

        public bool TryEat(int x, int y) {
            if (snake.CanEat(x, y, food)) {
                snake.Eat(food);
                CreateFood();
                return true;
            }

            snake.Move(x, y);
            return false;
        }

        private void CreateFood() {
            var x = random.Next(0, width);
            var y = random.Next(0, height);

            if (snake.Contains(x, y)) {
                CreateFood();
            }

            food.X = x;
            food.Y = y;
        }
    }
}
