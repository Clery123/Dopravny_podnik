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
            //Employee emp = EmployeeTable.SelectID(5);
           // Schedule veh = ScheduleTable.SelectID(1);

            //Driver driver = DriverTable.SelectID(emp.dID);
            //Console.WriteLine(driver.Hours_worked.ToString());
           // Console.WriteLine ("Denny nájazd KM: "+DriverTable.Daily(driver.dID).ToString());
           // Collection<Vehicle> vehi = DriverTable.Added_vehicles(1);
            //String s = "";
            /*foreach (Vehicle ve in vehi)
            {
                s = s + ve.Name+"\n";
               
            }*/
           // Console.WriteLine("Pridelene vozidla: \n" + s);
            //Console.WriteLine(VehicleTable.VehicleCheck());


           /* Vehicle veh = VehicleTable.Select()[0];
            Collection<Vehicle> vehicles = new Collection<Vehicle>();
            vehicles=VehicleTable.Select(db);*/
            
            InitializeComponent();
            
           // db.Close();
            /*if (con == true)
            {
                label1.Text = "Con.";
                foreach (Vehicle v in vehicles)
                {
                    label2.Text += v.Type.ToString();
                }
            }*/
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
            //EmployeeTable.Bonus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Zoznam_tras zt = new Zoznam_tras();
            zt.Show();
        }
    }
}
