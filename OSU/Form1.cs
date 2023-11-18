using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSU
{
    public partial class OSU : Form
    {
        public Bitmap HandlerTexture = Resource1.Handler,
                      TargetTexture = Resource1.Target;
        private Point _TagretPosition = new Point(300, 300);
        private Point _direction = Point.Empty;
        private int _score = 0;
        public OSU()
        {
            InitializeComponent();


            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);

            UpdateStyles();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            timer1.Interval = r.Next(25, 1000);
            _direction.X = r.Next(-1, 2);
            _direction.Y = r.Next(-1, 2);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            var LocalPosition = this.PointToClient(Cursor.Position);

            _TagretPosition.X += _direction.X * 10;
            _TagretPosition.Y += _direction.Y * 10;

            if (_TagretPosition.X < 0 || _TagretPosition.X > 500)
            {
                _direction.X *= -1;
            }
            if (_TagretPosition.Y < 0 || _TagretPosition.Y > 500)
            {
                _direction.Y *= -1;
            }

            Point between = new Point(LocalPosition.X - _TagretPosition.X, LocalPosition.Y - _TagretPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y));

            if (distance < 20)
            {
                AddScore(1);
            }


            var handlerRect = new Rectangle(LocalPosition.X - 50, LocalPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_TagretPosition.X - 50, _TagretPosition.Y - 50, 100, 100);


            g.DrawImage(HandlerTexture, handlerRect);
            g.DrawImage(TargetTexture, targetRect);
        }   
        private void AddScore(int score)
        {
            _score += score;
            scorelabl.Text = _score.ToString();
        }
    }
}
