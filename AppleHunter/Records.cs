using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AppleHunter
{
    public partial class Records : Form
    {
        private readonly Menu _menu;
        public Records(Menu menu)
        {
            InitializeComponent();
            _menu = menu;
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Width) / 2, pictureBox1.Location.Y);
            panel1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - panel1.Width) / 2, panel1.Location.Y);
        }

        private void Records_Load(object sender, EventArgs e)
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
                CreateRecordsFile();
                f = new StreamReader("Records.txt");
            }

            var buf = new string[] { };
            for (var i = 0; i < 10; i++)
            {
                var str = f.ReadLine();
                if (str != null) buf = str.Split(';');
                labelsNames[i].Text = buf[0];
                labelsScopes[i].Text = int.Parse(buf[1]).ToString();
            }
            f.Close();
        }

        public static void CreateRecordsFile()
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
            _menu.Visible = true;
            _menu.Enabled = true;
        }

        private void buttonBack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                buttonBack_Click(null, null);
        }
    }
}
