using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game {
    public enum Direction {
        Down,
        Up,
        Right,
        Left
    }

    public class Snake {
        public int HeadX => foods.Last().X;
        public int HeadY => foods.Last().Y;
        public int Points => foods.Count - 2;
        public Direction direction { get; set; }

        private readonly Queue<Food> foods;
        private readonly Brush brush;

        public Snake(Brush _brush) {
            brush = _brush;
            foods = new Queue<Food>();
        }

        public void Draw(Graphics graphics) {
            foreach (var food in foods) {
                food.Draw(graphics);
            }
        }

        public bool CanEat(int x, int y, Food food) {
            return food.X == HeadX + x && food.Y == HeadY + y;
        }

        public bool EatSnake() {
            var i = 0;
            return foods.Any(food => i++ != foods.Count - 1 && HeadY == food.Y && HeadX == food.X);
        }

        public bool Contains(int x, int y) {
            return foods.Any(food => food.X == x && food.Y == y);
        }

        public void Eat(Food food) {
            foods.Enqueue(new Food(food.X, food.Y, brush));
        }

        public void Clear() {
            foods.Clear();
            foods.Enqueue(new Food(0, 0, brush));
            foods.Enqueue(new Food(0, 1, brush));
            direction = Direction.Down;
        }

        public void Move(int x, int y) {
            foods.Enqueue(new Food(HeadX + x, HeadY + y, brush));
            foods.Dequeue();
        }
    }
}
