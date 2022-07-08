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
    public partial class AddSchedule : Form
    {
        Collection<Employee> emp = Employee_Service.getAll();
        Collection<Vehicle> veh = new Collection<Vehicle>();
        Collection<Path> pth = Path_Service.getAll();
        Collection<Controler> cnt = Controler_Service.getAll();
        private int drivID;
        private int vehID;
        private int cID;
        private int pID;
        public AddSchedule()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (Path p in pth) {
                comboBox3.Items.Add(p.pID+" Od: "+p.From+" Do: "+p.To);
            }
            foreach(Controler c in cnt)
            {
                comboBox4.Items.Add(c.cID + " " + Employee_Service.getByID(c.eID).First_name);
            }
            veh = Vehicle_Service.getFree();
            foreach (Vehicle vh in veh)
            {
                    comboBox2.Items.Add(vh.Id+" "+vh.Name);
            }
            foreach (Employee e in emp)
            {
                if (e.dID != 0)
                {
                    comboBox1.Items.Add(e.dID+" "+e.First_name);
                    //comboBox1.SelectedIndex = comboBox1.FindStringExact(Employee_Service.getByID(Driver_Service.getByID(Schedule_Service.getByID(this.id).dID).eID).First_name);
                }
            }
        }

        private void AddSchedule_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label9.Text = "";
            Collection<Schedule> sch = Schedule_Service.getAll();
            int i = 1;
            foreach (Schedule sc in sch)
            {
                if (sc.sID == i)
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
            Schedule s = new Schedule();
            try
            {
                s.sID = i;
                s.pID = this.pID;
                s.vID = this.vehID;
                s.dID = this.drivID;
                s.cID = this.cID;
                s.Date = DateTime.Parse(textBox3.Text);
                s.Number = Int32.Parse(textBox4.Text);
            }
            catch(Exception ex)
            {
                button2.Enabled = false;
                label9.Text = "Nespravne udaje";
                return;
            }
            Console.WriteLine(s.Date);
            Schedule_Service.Insert(s);
            
            button2.Enabled = false;
            Vehicle_Service.Vehicle_Check();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            String text = comboBox1.Text;
            int index = text.IndexOf(" ");
            text = text.Substring(0, index);
            this.drivID = Int32.Parse(text);
            Console.WriteLine(this.drivID);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            String text = comboBox2.Text;
            int index = text.IndexOf(" ");
            text = text.Substring(0, index);
            this.vehID = Int32.Parse(text);
            Console.WriteLine(this.vehID);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            String text = comboBox3.Text;
            int index = text.IndexOf(" ");
            text = text.Substring(0, index);
            this.pID = Int32.Parse(text);
            Console.WriteLine(this.pID);
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            String text = comboBox4.Text;
            int index = text.IndexOf(" ");
            text = text.Substring(0, index);
            this.cID = Int32.Parse(text);
            Console.WriteLine(this.cID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Zoznam_tras zt = new Zoznam_tras();
            zt.Show();
        }
    }
}
