using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DTO.DTO;
using BussinessLayer.Services;
namespace PresentationLayer
{
    public partial class zamestnanec : Form
    {
        private int id;
        private String meno;
        private String priezvisko;
        private DateTime datum_nar;
        private int plat;
        private String tel_c;
        private DateTime datum_nas;
        private String rola;

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
        public void get(int id,String meno,String priezvisko,DateTime datum1, int plat, String tel_c, DateTime datum2)
        {
            this.id = id;
            Console.WriteLine(this.id);
            this.meno = meno;
            this.priezvisko = priezvisko;
            this.datum_nar = datum1;
            this.plat = plat;
            this.tel_c = tel_c;
            this.datum_nas = datum2;
            textBox1.Text = this.meno.ToString();
            textBox2.Text = this.priezvisko.ToString();
            textBox3.Text = this.datum_nar.ToString();
            textBox4.Text = this.plat.ToString();
            textBox5.Text = this.tel_c.ToString();
            textBox6.Text = this.datum_nas.ToString();
            try
            {
                int? idckoV = Employee_Service.getByID(this.id).dID;
                int? idckoD = Employee_Service.getByID(this.id).disID;
                if (idckoV.Value != 0)
                {
                    this.rola = "driver";
                    int? hr = Driver_Service.getByID(idckoV.Value).Hours_worked;
                    label7.Text = "Odpracované hodiny: " + hr.Value.ToString();
                    label8.Text = "Najazdených kilometrov: " + Driver_Service.Daily(idckoV.Value);
                    Collection<Vehicle> veh = Driver_Service.Added_vehicles(idckoV.Value);
                    String st = "";
                    foreach (Vehicle v in veh)
                    {
                        st += v.Name + " " + v.License_plate + "\n";
                    }
                    label10.Text = "Pridelené vozidlá: " + veh.Count + "\n" + st;
                }
                else if (idckoD.Value != 0)
                {
                    this.rola = "disp";
                    int? hr1 = Dispatcher_Service.getByID(idckoD.Value).Hours_worked;
                    label7.Text = "Odpracované hodiny: " + hr1.Value.ToString();

                }
                else
                {
                    this.rola = "kont";
                    label7.Text = "Počet pokút: " + Controler_Service.getByID(Employee_Service.getByID(this.id).cID).Number_of_fines;
                }
            }
            catch (NullReferenceException e)
            {
                label7.Text = "0";
            }
            //label7.Text = hr.ToString();
            //label7.Text = "Odpracované hodiny: " + hr;
            //label7.Text = "Odpracované hodiny: " + DispatcherTable.SelectID(EmployeeTable.SelectID(this.id + 1).disID).Hours_worked;

        }
        public zamestnanec()
        {
            InitializeComponent();
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Close();
            Zamestnanci zam = new Zamestnanci();
            zam.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateTextBoxes())
            {
                Employee emp = new Employee();
                try
                {
                    emp.First_name = textBox1.Text;
                    emp.Last_name = textBox2.Text;
                    emp.Birth_date = DateTime.Parse(textBox3.Text);
                    emp.Salary = Int32.Parse(textBox4.Text);
                    emp.Phone = textBox5.Text;
                    emp.Start_date = DateTime.Parse(textBox6.Text);
                }
                catch (Exception ex)
                {
                    label9.Text = "Nesprávne údaje";
                    return;
                }
                Employee_Service.Update(emp, this.id);
                
            }
            else
            {
                label9.Text = "Prazdny textbox";
                button2.Enabled = false;
            }
            }

            private void button3_Click(object sender, EventArgs e)
            {
                int id;
                if (this.rola == "driver")
                {
                    id = Employee_Service.getByID(this.id).dID;
                    label9.Text = Driver_Service.Del(id).ToString();
                    button3.Enabled = false;
                    return;
                }
                if (this.rola == "disp")
                {
                    id = Employee_Service.getByID(this.id).disID;
                    Employee_Service.Del(this.id);
                    Dispatcher_Service.Del(id);
                    button3.Enabled = false;
                    return;
                }
                id = Employee_Service.getByID(this.id).cID;
                Employee_Service.Del(this.id);
                Controler_Service.Del(id);
                button3.Enabled = false;
            
            }

            private void textBox6_TextChanged(object sender, EventArgs e)
            {
            button2.Enabled = true;
            }

            private void zamestnanec_Load(object sender, EventArgs e)
            {

            }

            private void zamestnanec_Load_1(object sender, EventArgs e)
            {

            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
