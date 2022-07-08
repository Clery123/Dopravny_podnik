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
    public partial class Zoznam_tras : Form
    {
        private Collection<Schedule> schedules = Schedule_Service.getAll();
        public Zoznam_tras()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Date";
            dataGridView1.Columns[2].Name = "Line Number";
            foreach (Schedule sch in schedules)
            {

                this.dataGridView1.Rows.Add(sch.sID, sch.Date.Day+"."+sch.Date.Month+" "+sch.Date.Hour+":"+sch.Date.Minute, sch.Number);
            }

        }

        private void Zoznam_tras_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = schedules[dataGridView1.CurrentCell.RowIndex].sID - 1;
            DetailyJazdy dj = new DetailyJazdy();
            dj.Show();
            dj.get(index);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddSchedule ads = new AddSchedule();
            ads.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
