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
    public partial class SetRecord : Form
    {
        private Game form1;
        public SetRecord(Game form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text))
                form1.setName(textBox1.Text.ToUpper());
            else
                form1.setName("-");
            Close();
        }
    }
}
