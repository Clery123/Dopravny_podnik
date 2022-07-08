using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using DTO.DTO;
using BussinessLayer.Services;
namespace PresentationLayer
{
    public partial class DetailyJazdy : Form
    {
        private int id;
        Collection<Employee> emp = Employee_Service.getAll();
        Collection<Vehicle> veh = new Collection<Vehicle>();
        Collection<Schedule> sch = Schedule_Service.getAll();
        Collection<Path> pth = Path_Service.getAll();
        public void get(int id)
        {
            this.id = id+1;
            foreach (Employee e in emp)
            {
                if (e.dID != 0) { 
                    comboBox1.Items.Add(e.First_name);
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(Employee_Service.getByID(Driver_Service.getByID(Schedule_Service.getByID(this.id).dID).eID).First_name);
                }
            }
            veh = Vehicle_Service.getFree();
            foreach (Vehicle vh in veh)
            {
                    comboBox2.Items.Add(vh.Name);
                    comboBox2.Text = Vehicle_Service.getVehicle(Schedule_Service.getByID(this.id).vID).Name;
            }
            textBox3.Text = Schedule_Service.getByID(this.id).Date.ToString();
            textBox4.Text = Schedule_Service.getByID(this.id).Number.ToString();
            foreach (Path ph in pth)
            {
                comboBox3.Items.Add(ph.pID);
                comboBox3.Text = Schedule_Service.getByID(this.id).pID.ToString();
            }
        }
        public DetailyJazdy()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Text= Schedule_Service.Del(this.id);
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Zoznam_tras zt = new Zoznam_tras();
            zt.Show();
        }

        private void DetailyJazdy_Load(object sender, EventArgs e)
        {

        }
    }
}
