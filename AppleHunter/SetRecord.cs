using System;
using System.Windows.Forms;

namespace AppleHunter
{
    public partial class SetRecord : Form
    {
        private Game form1;
        public SetRecord(Game form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void buttonSaveRecord_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
                form1.SetName(textBox1.Text.ToUpper());
            else
                form1.SetName("-");
            Close();
        }
    }
}
