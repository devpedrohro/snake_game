using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_game {
    public class Food {
        public static int sides = 25;
        private readonly Brush brush;

        public int X { get; set; }
        public int Y { get; set; }

        public Food(int _X, int _Y, Brush _brush) {
            X = _X;
            Y = _Y;
            brush = _brush;
        }

        public void Draw(Graphics graphics) {
            graphics.FillRectangle(brush, new Rectangle(X * sides, Y * sides, sides, sides));
        }
    }
}
