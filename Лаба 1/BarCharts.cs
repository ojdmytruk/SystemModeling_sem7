using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSlab1
{
    public partial class BarCharts : Form
    {
        public BarCharts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formula1 f1 = new Formula1();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Formula2 f2 = new Formula2();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Formula3 f3 = new Formula3();
            f3.Show();
        }
    }
}
