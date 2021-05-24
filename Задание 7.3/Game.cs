using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppleHunter.Properties;

namespace Задание_7._3
{
    public partial class Game : Form
    {
        private static Image[,] images = new Image[,]
        {
            { Resources.boy1u1, Resources.boy1u2, Resources.boy1u3, Resources.boy1u4 },
            { Resources.boy1l1, Resources.boy1l2, Resources.boy1l3, Resources.boy1l4 },
            { Resources.boy1d1, Resources.boy1d2, Resources.boy1d3, Resources.boy1d4 },
            { Resources.boy1r1, Resources.boy1r2, Resources.boy1r3, Resources.boy1r4 }
        };
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        private Random rnd = new Random();
        private int direction = 0;
        private int step = 4;
        private int frame = 0;
        private bool isAbleToRun = true;
        private bool isPressedShift = false;
        private int stamina = 6000;
        private Menu menu;
        private Record[] records = new Record[11];
        private String name;


        public Game(Menu menu)
        {
            InitializeComponent();
            MessageBox.Show("Правила: соберите как можно больше яблок.");
            pictureBox1.Image = images[2, 0];
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - pictureBox1.Height) / 2);
            timer2.Enabled = true;
            this.menu = menu;
            StreamReader f;
            try
            {
                f = new StreamReader("Records.txt");
            }
            catch
            {
                Recods.createRecordsFile();
                f = new StreamReader("Records.txt");
            }
            string str;
            string[] buf;
            for (int i = 0; i < 10; i++)
            {
                str = f.ReadLine();
                buf = str.Split(';');
                records[i] = new Record(buf[0], Int32.Parse(buf[1]));
            }
            f.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (e.Shift && isAbleToRun)
            {
                step = 16;
                isPressedShift = true;
            }
            else
            {
                step = 4;
                isPressedShift = false;
            }
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = 0;
                    break;
                case Keys.Left:
                    direction = 1;
                    break;
                case Keys.Down:
                    direction = 2;
                    break;
                case Keys.Right:
                    direction = 3;
                    break;
                case Keys.Escape:
                    exitGame();
                    break;
                default:
                    {

                    }
                    break;
            }
        }
        private void horisontalMoving()
        {
            int reverse;
            if (direction == 3)
                reverse = 1;
            else
                reverse = -1;
            if ((panelLeft.Right <= pictureBox1.Left && reverse == -1) || (panelRight.Left >= pictureBox1.Right && reverse == 1))
                pictureBox1.Left += (step * reverse);
        }
        private void verticalMoving()
        {
            int reverse;
            if (direction == 2)
                reverse = 1;
            else
                reverse = -1;
            if ((panelTop.Bottom <= pictureBox1.Top && reverse == -1) || (panelBottom.Top >= pictureBox1.Bottom && reverse == 1))
                pictureBox1.Top += (step * reverse);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rectangle r1 = new Rectangle(pictureBox1.Location, pictureBox1.Size);
            PictureBox p = new PictureBox();
            foreach (PictureBox pb in pictureBoxes)
            {
                Rectangle r2 = new Rectangle(pb.Location, pb.Size);
                if (r1.IntersectsWith(r2))
                {
                    scopes.Text = (Int32.Parse(scopes.Text) + 100).ToString();
                    Controls.Remove(pb);
                    p = pb;
                }
            }
            pictureBoxes.Remove(p);
            pictureBox1.Image = images[direction, frame];
            frame++;
            if (frame == 3)
                frame = 0;
            if (direction == 0 || direction == 2)
                verticalMoving();
            else
                horisontalMoving();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int estTime;
            time.Text = (estTime = Int32.Parse(time.Text) - 1).ToString();
            if (estTime == 10)
                time.ForeColor = Color.Red;
            if (estTime == 0)
            {
                timer2.Enabled = false;
                MessageBox.Show("Время вышло");
                new SetRecord(this).ShowDialog();
                records[10] = new Record(name, Int32.Parse(scopes.Text));
                Array.Sort(records);
                StreamWriter f = new StreamWriter("Records.txt");
                for (int i = 0; i < 10; i++)
                    f.WriteLine("{0};{1}", records[i].getName(), records[i].getScopes());
                f.Close();
                exitGame();
                return;
            }
            if (estTime % 2 == 1)
            {
                PictureBox pb = new PictureBox();
                pb.Image = Resources.apple;
                pb.Size = new Size(24, 24);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.BackColor = Color.Transparent;
                pb.Location = new Point(rnd.Next(panelLeft.Width, ClientSize.Width - pb.Width - panelRight.Width), rnd.Next(panelTop.Height, ClientSize.Height - pb.Height - panelBottom.Height));
                Controls.Add(pb);
                pictureBoxes.Add(pb);
            }
        }

        private void exitGame()
        {
            Close();
            menu.Visible = true;
            menu.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = stamina / 60;
            if (isPressedShift && stamina > 0)
            {
                stamina -= 30;
                if (stamina <= 0)
                    isAbleToRun = false;
            }
            else
            {
                if (stamina != 6000)
                {
                    stamina += 15;
                }
                if (stamina >= 1200)
                    isAbleToRun = true;
            }
        }

        public void setName(String name)
        {
            this.name = name;
        }
    }
}
