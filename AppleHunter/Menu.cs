using System;
using System.Drawing;
using System.Windows.Forms;


namespace AppleHunter
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            pictureBox1.Location = new Point((Screen.PrimaryScreen.Bounds.Width - pictureBox1.Width) / 2, pictureBox1.Location.Y);
            buttonPlay.Left = buttonRecords.Left = buttonClose.Left = (Screen.PrimaryScreen.Bounds.Width - buttonPlay.Width) / 2;
            
        }

        private void buttonRecords_Click(object sender, EventArgs e)
        {
            new Records(this).Show();
            Enabled = false;
            Visible = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            new Game(this).Show();
            Enabled = false;
            Visible = false;
        }

        private void menu_OnEscape(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
