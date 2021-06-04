using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AppleHunter.Properties;

namespace AppleHunter
{
    public partial class Game : Form
    {
        private static readonly Image[,] Images = new Image[,]
        {
            {Resources.boy1u1, Resources.boy1u2, Resources.boy1u3, Resources.boy1u4},
            {Resources.boy1l1, Resources.boy1l2, Resources.boy1l3, Resources.boy1l4},
            {Resources.boy1d1, Resources.boy1d2, Resources.boy1d3, Resources.boy1d4},
            {Resources.boy1r1, Resources.boy1r2, Resources.boy1r3, Resources.boy1r4}
        };

        private readonly List<PictureBox> _pictureBoxes = new List<PictureBox>();
        
        private readonly Random _rnd = new Random();

        private enum Direction
        {
            Up,
            Left,
            Down,
            Right
        }
        private Direction _direction;
        private int _step = 4;
        private int _frame;
        
        private bool _isAbleToRun = true;
        private bool _isPressedShift;
        
        private int _stamina = 6000;
        
        private readonly Menu _menu;
        private readonly Record[] _records = new Record[11];
        private string _name;


        public Game(Menu menu)
        {
            InitializeComponent();
            MessageBox.Show("Правила: соберите как можно больше яблок.");
            pictureBox1.Image = Images[2, 0];
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Height) / 2);
            timer2.Enabled = true;
            _menu = menu;
            StreamReader f;
            try
            {
                f = new StreamReader("Records.txt");
            }
            catch
            {
                Records.CreateRecordsFile();
                f = new StreamReader("Records.txt");
            }

            for (var i = 0; i < 10; i++)
            {
                var str = f.ReadLine();
                if (str != null)
                {
                    var buf = str.Split(';');
                    _records[i] = new Record(buf[0], int.Parse(buf[1]));
                }
            }

            f.Close();
        }

        private void Game_OnInteract(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (e.Shift && _isAbleToRun)
            {
                _step = 16;
                _isPressedShift = true;
            }
            else
            {
                _step = 4;
                _isPressedShift = false;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    _direction = Direction.Up;
                    break;
                case Keys.Left:
                case Keys.A:
                    _direction = Direction.Left;
                    break;
                case Keys.Down:
                case Keys.S:
                    _direction = Direction.Down;
                    break;
                case Keys.Right:
                case Keys.D:
                    _direction = Direction.Right;
                    break;
                case Keys.Escape:
                    ExitGame();
                    break;
                default:
                {
                }
                    break;
            }
        }

        private void HorisontalMoving()
        {
            int reverse;
            if (_direction == Direction.Right)
                reverse = 1;
            else
                reverse = -1;
            if (panelLeft.Right <= pictureBox1.Left && reverse == -1 ||
                panelRight.Left >= pictureBox1.Right && reverse == 1)
                pictureBox1.Left += _step * reverse;
        }

        private void VerticalMoving()
        {
            int reverse;
            if (_direction == Direction.Down)
                reverse = 1;
            else
                reverse = -1;
            if (panelTop.Bottom <= pictureBox1.Top && reverse == -1 ||
                panelBottom.Top >= pictureBox1.Bottom && reverse == 1)
                pictureBox1.Top += _step * reverse;
        }

        private void Menu_StopInteract(object sender, KeyEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rectangle r1 = new Rectangle(pictureBox1.Location, pictureBox1.Size);
            PictureBox p = new PictureBox();
            foreach (PictureBox pb in _pictureBoxes)
            {
                Rectangle r2 = new Rectangle(pb.Location, pb.Size);
                if (r1.IntersectsWith(r2))
                {
                    scopes.Text = (int.Parse(scopes.Text) + 100).ToString();
                    Controls.Remove(pb);
                    p = pb;
                }
            }

            _pictureBoxes.Remove(p);
            pictureBox1.Image = Images[(int) _direction, _frame];
            _frame++;
            if (_frame == 3)
                _frame = 0;
            if (_direction == Direction.Up || _direction == Direction.Down)
                VerticalMoving();
            else
                HorisontalMoving();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int estTime;
            time.Text = (estTime = int.Parse(time.Text) - 1).ToString();
            if (estTime == 10)
                time.ForeColor = Color.Red;
            if (estTime == 0)
            {
                timer2.Enabled = false;
                MessageBox.Show("Время вышло");
                new SetRecord(this).ShowDialog();
                _records[10] = new Record(_name, int.Parse(scopes.Text));
                Array.Sort(_records);
                StreamWriter f = new StreamWriter("Records.txt");
                for (int i = 0; i < 10; i++)
                    f.WriteLine("{0};{1}", _records[i].GetName(), _records[i].GetScopes());
                f.Close();
                ExitGame();
                return;
            }

            if (estTime % 2 == 1)
            {
                PictureBox pb = new PictureBox
                {
                    Image = Resources.apple,
                    Size = new Size(24, 24),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent
                };
                pb.Location = new Point(_rnd.Next(panelLeft.Width, ClientSize.Width - pb.Width - panelRight.Width),
                    _rnd.Next(panelTop.Height, ClientSize.Height - pb.Height - panelBottom.Height));
                Controls.Add(pb);
                _pictureBoxes.Add(pb);
            }
        }

        private void ExitGame()
        {
            Close();
            _menu.Visible = true;
            _menu.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = _stamina / 60;
            if (_isPressedShift && _stamina > 0)
            {
                _stamina -= 30;
                if (_stamina <= 0)
                    _isAbleToRun = false;
            }
            else
            {
                if (_stamina != 6000)
                {
                    _stamina += 15;
                }

                if (_stamina >= 1200)
                    _isAbleToRun = true;
            }
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }
}