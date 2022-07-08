using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BussinessLayer.Services;
using DTO.DTO;
namespace PresentationLayer
{
    public partial class Add_Vehicle : Form
    {
        public Add_Vehicle()
        {
            InitializeComponent();
        }
        private Boolean ValidateTextBoxes()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (string.IsNullOrEmpty(c.Text))
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {

                Collection<Vehicle> vehs = Vehicle_Service.getAll();
                int i = 1;
                foreach (Vehicle veh in vehs)
                {
                    if (veh.Id == i)
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine(i);
                Vehicle ve = new Vehicle();
                ve.Id = i;
                ve.Name = textBox1.Text;
                ve.Capacity = Int32.Parse(textBox2.Text);
                ve.Condition = textBox3.Text;
                ve.Type = textBox4.Text;
                ve.License_plate = textBox5.Text;
                ve.Year_of_manufacture = DateTime.Now.Year - Int32.Parse(textBox6.Text);
                Vehicle_Service.Insert(ve);
                button2.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Vozidla v = new Vozidla();
            v.Show();
        }

        private void Add_Vehicle_Load(object sender, EventArgs e)
        {

        }
    }
}
