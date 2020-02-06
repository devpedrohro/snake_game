using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game {
    public partial class GameScreen : Form {
        private const int width = 25;
        private const int height = 25;
        private const string pointsString = "Points: {0}";

        private readonly Color backgroundColor;
        private readonly Game game;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public GameScreen() {
            InitializeComponent();

            backgroundColor = Color.White;
            bitmap = new Bitmap(width * Food.sides, height * Food.sides);
            graphics = Graphics.FromImage(bitmap);
            graphics.PageUnit = GraphicsUnit.Pixel;
            ClientSize = new Size(bitmap.Width, bitmap.Height + lblRestart.Height);
            game = new Game(width, height);
            tmrTimer.Start();
        }

        private void UpdatePoints() {
            lblPoints.Text = string.Format(pointsString, game.GetPoints());
        }

        private void GameScreen_Load(object sender, EventArgs e) {
            tmrTimer.Enabled = true;
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    game.ChangeDirection(Direction.Left);
                    break;
                case Keys.Right:
                    game.ChangeDirection(Direction.Right);
                    break;
                case Keys.Up:
                    game.ChangeDirection(Direction.Up);
                    break;
                case Keys.Down:
                    game.ChangeDirection(Direction.Down);
                    break;
            }
        }

        private void tmrTimer_Tick(object sender, EventArgs e) {
            if (game.SnakeRising())
                UpdatePoints();

            if (game.GameOver()) {
                tmrTimer.Stop();
                lblRestart.Enabled = true;
                lblGameOver.Visible = true;
            }
            Invalidate();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e) {
            graphics.Clear(backgroundColor);
            game.Draw(graphics);
            e.Graphics.DrawImage(bitmap, 0, lblRestart.Height);
        }

        private void lblRestart_Click(object sender, EventArgs e) {
            lblGameOver.Visible = false;
            lblRestart.Enabled = false;
            game.Restart();
            UpdatePoints();
            tmrTimer.Start();
        }
    }
}
