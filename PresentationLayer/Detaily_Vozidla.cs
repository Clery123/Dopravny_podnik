using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using BussinessLayer.Services;
using DTO.DTO;
namespace PresentationLayer
{
    public partial class Detaily_Vozidla : System.Windows.Forms.Form
    {
        private int Id;
        private String Name;
        private String Type;
        private int Capacity;
        private String Condition;
        private String License_plate;
        private int Yr;
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
        public Detaily_Vozidla()
        {
            InitializeComponent();
        }

        public void get(int id, String name, String type, int capacity, String condition, String license_plate, int yr)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Capacity = capacity;
            this.Condition = condition;
            this.License_plate = license_plate;
            this.Yr = yr;
            Console.WriteLine(this.Id);
            int rok = DateTime.Now.Year - this.Yr;
            textBox1.Text = this.Name.ToString();
            textBox2.Text = this.Capacity.ToString();
            textBox4.Text = this.Type.ToString();
            textBox3.Text = this.Condition.ToString();
            textBox5.Text = this.License_plate.ToString();
            textBox6.Text = rok.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {
                try
                {
                    Vehicle veh = new Vehicle();
                    veh.Name = textBox1.Text;
                    veh.Capacity = Int32.Parse(textBox2.Text);
                    veh.Condition = (textBox3.Text);
                    veh.Type = textBox4.Text;
                    veh.License_plate = textBox5.Text;
                    veh.Year_of_manufacture = (DateTime.Now.Year - Int32.Parse(textBox6.Text));

                    Console.WriteLine(this.Id + 1);
                    Console.WriteLine("this");
                    Vehicle_Service.Update(veh, this.Id);
                    
                }
                catch(Exception ex)
                {
                    label9.Text = "Nespravny format";
                    button2.Enabled = false;
                }
            }
            else
            {
                label9.Text = "Prazdny textbox";
                button2.Enabled = false;
            }
        }

        private void Detaily_Vozidla_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label9.Text = "";
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vozidla vo = new Vozidla();
            this.Close();
            vo.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            
            label9.Text = Vehicle_Service.Del(this.Id).ToString();
            button3.Enabled = false;
            return;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label9.Text = "";
            button2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label9.Text = "";
            button2.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label9.Text = "";
            button2.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label9.Text = "";
            button2.Enabled = true;
        }
    }
}
