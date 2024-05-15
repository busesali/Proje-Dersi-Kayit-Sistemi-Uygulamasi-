using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp5
{
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
        }

        public void AddSelectedCourse(string gonderenHocaAdi, string gonderenHocaSoyadi, string mesaj)
        {

            if (dataGridView1.Columns.Count == 0)
            {


                dataGridView1.Columns.Add("gonderenHocaAdi", "Öğretmen Adı");
                dataGridView1.Columns["gonderenHocaAdi"].Width = 200;
                dataGridView1.Columns.Add("gonderenHocaSoyadi", "Öğretmen Soyadı");
                dataGridView1.Columns["gonderenHocaSoyadi"].Width = 250;
                dataGridView1.Columns.Add("mesaj", "Mesaj");
                dataGridView1.Columns["mesaj"].Width = 250;
            }
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = gonderenHocaAdi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = gonderenHocaSoyadi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = mesaj });


            dataGridView1.Rows.Add(row); 
        }

        private void Form16_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //github 
    }
}
