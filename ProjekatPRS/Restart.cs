using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjekatPRS
{
    public partial class Restart : Form
    {
        public Restart()
        {
            InitializeComponent();
            txtRezultat.Text = "Vaš rezultat je:" + Form1.number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            Form1 sistem = new Form1();
            sistem.ShowDialog();
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void Restart_Load(object sender, EventArgs e)
        {

        }
    }
}