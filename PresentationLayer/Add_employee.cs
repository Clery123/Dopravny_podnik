using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DTO.DTO;

namespace PresentationLayer
{
    public partial class Add_employee : Form
    {
        public Add_employee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zamestnanci zam = new Zamestnanci();
            this.Close();
            zam.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                return;
            }
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                return;
            }
            checkBox1.Enabled = true;
            checkBox3.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Enabled = false;
                checkBox1.Enabled = false;

                return;
            }
            checkBox2.Enabled = true;
            checkBox1.Enabled = true;
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
            button2.Enabled = false;
            //Console.WriteLine(DateTime.Now);
            
            /*if (ValidateTextBoxes())
            {
                 if (checkBox1.Checked) /// DRIVER CHECKED
                 {
                     label6.Text = "";
                     Employee emp = new Employee();
                     Driver driv = new Driver();
                     Collection<Employee> emps = EmployeeTable.Select();
                     int i = 1;
                     foreach (Employee em in emps)
                     {
                         if (em.eID == i)
                         {
                             i++;
                             continue;
                         }
                         else
                         {
                             break;
                         }
                     }
                     Collection<Driver> drvs = DriverTable.Select();
                     int k = 1;
                     foreach (Driver dr in drvs)
                     {
                         if (dr.dID == k)
                         {
                             k++;
                             continue;
                         }
                         else
                         {
                             break;
                         }
                     }
                     Console.WriteLine(i);
                     Console.WriteLine(k);
                     try
                     {
                         emp.eID = i;
                         emp.dID = k;
                         emp.First_name = textBox1.Text;
                         emp.Last_name = textBox2.Text;
                         emp.Birth_date = DateTime.Parse(textBox3.Text);
                         emp.Salary = Int32.Parse(textBox4.Text);
                         emp.Phone = "+" + textBox5.Text;
                         emp.Start_date = DateTime.Now;

                         driv.dID = k;
                         driv.eID = i;
                         driv.Hours_worked = Int32.Parse(textBox6.Text);
                     }
                     catch(Exception ex)
                     {
                         button2.Enabled = false;
                         label6.Text = "Chybne údaje";
                         return;
                     }
                     DriverTable.Insert(driv);

                     EmployeeTable.InsertD(emp);
                    EmployeeTable.LogE("Employee", "Insert");
                     return;
                 }
                if (checkBox2.Checked)  /////////DISPEČER CHECKED
                {
                    label6.Text = "";
                    Employee emp = new Employee();
                    Dispatcher driv = new Dispatcher();
                    Collection<Employee> emps = EmployeeTable.Select();
                    int i = 1;
                    foreach (Employee em in emps)
                    {
                        if (em.eID == i)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Collection<Dispatcher> drvs = DispatcherTable.Select();
                    int k = 1;
                    foreach (Dispatcher dr in drvs)
                    {
                        if (dr.disID == k)
                        {
                            k++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine(i);
                    Console.WriteLine(k);
                    try
                    {
                        emp.eID = i;
                        emp.disID = k;
                        emp.First_name = textBox1.Text;
                        emp.Last_name = textBox2.Text;
                        emp.Birth_date = DateTime.Parse(textBox3.Text);
                        emp.Salary = Int32.Parse(textBox4.Text);
                        emp.Phone = "+" + textBox5.Text;
                        emp.Start_date = DateTime.Now;

                        driv.disID = k;
                        driv.eID = i;
                        driv.Hours_worked = Int32.Parse(textBox6.Text);
                    }
                    catch(Exception ex)
                    {
                        button2.Enabled = false;
                        label6.Text = "Chybne údaje";
                        return;
                    }
                    DispatcherTable.Insert(driv);

                    EmployeeTable.InsertDis(emp);
                    return;
                }
                if (checkBox3.Checked) ////////CONTROLER CHECKED
                {
                    
                    label6.Text = "";
                
                    Employee emp = new Employee();
                    Controler driv = new Controler();
                    Collection<Employee> emps = EmployeeTable.Select();
                    int i = 1;
                    foreach (Employee em in emps)
                    {
                        if (em.eID == i)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Collection<Controler> drvs = ControlerTable.Select();
                    int k = 1;
                    foreach (Controler dr in drvs)
                    {
                        if (dr.cID == k)
                        {
                            k++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine(i);
                    Console.WriteLine(k);
                    try
                    {
                        emp.eID = i;
                        emp.cID = k;
                        emp.First_name = textBox1.Text;
                        emp.Last_name = textBox2.Text;
                        emp.Birth_date = DateTime.Parse(textBox3.Text);
                        emp.Salary = Int32.Parse(textBox4.Text);
                        emp.Phone = "+" + textBox5.Text;
                        emp.Start_date = DateTime.Now;
                        
                        driv.cID = k;
                        driv.eID = i;
                        driv.Number_of_fines = Int32.Parse(textBox6.Text);
                    }
                    catch (Exception ex)
                    {
                        button2.Enabled = false;
                        label6.Text = "Chybne údaje";
                        return;
                    }
                    ControlerTable.Insert(driv);
                    EmployeeTable.InsertC(emp);
                    return;
                }
            }
            else
            {
                button2.Enabled = false;
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            
        }
    }
}
