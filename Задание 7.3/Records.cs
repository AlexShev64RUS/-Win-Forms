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

namespace Задание_7._3
{
    public partial class Recods : Form
    {

        private Record[] records = new Record[10];
        private Menu menu;
        public Recods(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Width) / 2, pictureBox1.Location.Y);
            panel1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - panel1.Width) / 2, panel1.Location.Y);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Label[] labelsScopes = { labelScopes1, labelScopes2, labelScopes3, labelScopes4, labelScopes5, labelScopes6, labelScopes7, labelScopes8, labelScopes9, labelScopes10 };
            Label[] labelsNames = { labelName1, labelName2, labelName3, labelName4, labelName5, labelName6, labelName7, labelName8, labelName9, labelName10 };
            StreamReader f;
            try
            {
                f = new StreamReader("Records.txt");
            }
            catch
            {
                createRecordsFile();
                f = new StreamReader("Records.txt");
            }
            string str;
            string[] buf;
            for (int i = 0; i < 10; i++)
            {
                str = f.ReadLine();
                buf = str.Split(';');
                labelsNames[i].Text = buf[0];
                labelsScopes[i].Text = Int32.Parse(buf[1]).ToString();
            }
            f.Close();
        }

        public static void createRecordsFile()
        {
            StreamWriter f = new StreamWriter("Records.txt");
            for (int i = 0; i < 10; i++)
                f.WriteLine("{0};{1}", "-", 0);
            f.Close();
        }

        private void labelScopes10_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
            menu.Visible = true;
            menu.Enabled = true;
        }

        private void buttonBack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                buttonBack_Click(null, null);
        }
    }
}
