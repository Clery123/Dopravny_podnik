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
    public partial class Vozidla : Form
    {

        private Collection<Vehicle> vehicles;
        public Vozidla()
        {
            InitializeComponent();
            Vehicle_Service.Vehicle_Check();
            // VehicleTable.VehicleCheck();        
             vehicles = Vehicle_Service.getAll();
             dataGridView1.ColumnCount = 3;
             dataGridView1.Columns[0].Name = "Vehicle ID";
             dataGridView1.Columns[1].Name = "Name";
             dataGridView1.Columns[2].Name = "Condition";

            /*string[] row = new string[] { "1", "Product 1", "1000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "2", "Product 2", "2000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "3", "Product 3", "3000" };
            dataGridView1.Rows.Add(row);
            row = new string[] { "4", "Product 4", "4000" };
            dataGridView1.Rows.Add(row);*/
             foreach (Vehicle veh in vehicles)
             {

                 this.dataGridView1.Rows.Add(veh.Id, veh.Name, veh.Condition);
             }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Detaily_Vozidla det = new Detaily_Vozidla();
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            string cellValue = Convert.ToString(selectedRow.Cells["Vehicle ID"].Value);
            int index = Int32.Parse(cellValue);
            Vehicle vv = Vehicle_Service.getVehicle(index);
            det.get(index, vv.Name, vv.Type, vv.Capacity, vv.Condition, vv.License_plate,vv.Year_of_manufacture);
            //Console.WriteLine(dataGridView1.CurrentCell.RowIndex);
            det.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Vehicle av = new Add_Vehicle();
            this.Close();
            av.Show();
        }

        private void Vozidla_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}
