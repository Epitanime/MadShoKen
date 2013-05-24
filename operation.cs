using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TimingMadShoken
{
    public partial class operation : Form
    {
        public operation()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dec = 0;
            if (int.TryParse(this.decallageTextBox1.Text,out dec))
                ((mainForm)Owner).decallageFrames(dec);
            else
                ((mainForm)Owner).decallageFrames(0);
        }

        private void operation_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Enabled = true;
        }

        private void operation_Load(object sender, EventArgs e)
        {

        }
    }
}