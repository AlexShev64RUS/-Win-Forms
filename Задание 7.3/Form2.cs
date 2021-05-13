using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Задание_7._3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Width) / 2, pictureBox1.Location.Y);
            buttonPlay.Left = buttonRecords.Left = buttonClose.Left = (Screen.PrimaryScreen.Bounds.Width - buttonPlay.Width) / 2;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form3(this).Show();
            Enabled = false;
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            new Form1(this).Show();
            Enabled = false;
            Visible = false;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
