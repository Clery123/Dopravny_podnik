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
    public partial class Zamestnanci : Form
    {
        private Collection<Employee> employees = Employee_Service.getAll();
        private void AddRow(DataTable table)
        {
            DataRow newRow = table.NewRow();
            table.Rows.Add(newRow);
        }
        public Zamestnanci()
        {
            InitializeComponent();
            int i = 0;
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Employee ID";
            dataGridView1.Columns[1].Name = "First Name";
            dataGridView1.Columns[2].Name = "Last Name";
            dataGridView1.Columns[3].Name = "Salary";

           /* string[] row = new string[] { "1", "Product 1", "1000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "2", "Product 2", "2000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "3", "Product 3", "3000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "4", "Product 4", "4000" };
            dataGridView1.Rows.Add(row);*/
            foreach (Employee emp in employees)
            {

                 this.dataGridView1.Rows.Add(emp.eID,emp.First_name,emp.Last_name,emp.Salary);
            }
        }

        private void Zamestnanci_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            zamestnanec zam = new zamestnanec();
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string cellValue = Convert.ToString(selectedRow.Cells["Employee ID"].Value);
            int index = Int32.Parse(cellValue); //employees[dataGridView1.CurrentCell.RowIndex].eID -1;
            Employee ee = Employee_Service.getByID(index);
            zam.get(index,ee.First_name,ee.Last_name,ee.Birth_date,ee.Salary,ee.Phone,ee.Start_date);
            //Console.WriteLine(dataGridView1.CurrentCell.RowIndex);
            zam.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_employee add = new Add_employee();
            this.Close();
            add.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Use Case
            Employee_Service.Bonus();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Employee ID";
            dataGridView1.Columns[1].Name = "First Name";
            dataGridView1.Columns[2].Name = "Last Name";
            dataGridView1.Columns[3].Name = "Salary";
            employees = Employee_Service.getAll();

          /*  string[] row = new string[] { "1", "Product 1", "1000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "2", "Product 2", "2000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "3", "Product 3", "3000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "4", "Product 4", "4000" };
            dataGridView1.Rows.Add(row);*/
            foreach (Employee emp in employees)
            {

                this.dataGridView1.Rows.Add(emp.eID, emp.First_name, emp.Last_name, emp.Salary);
            }
        }
    }
}
