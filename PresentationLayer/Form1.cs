using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO.DTO;

using System.Runtime.InteropServices;

namespace PresentationLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vozidla voz = new Vozidla();
            voz.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zamestnanci zam = new Zamestnanci();
            zam.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Zoznam_tras zt = new Zoznam_tras();
            zt.Show();
        }
    }
}
