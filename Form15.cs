using System;
using System.Windows.Forms;
using iTextSharp.text;
using Npgsql;

namespace WinFormsApp5
{
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
        }

        public string HocaAdi { get; set; }
        public string HocaSoyadi { get; set; }

        private void Form15_Load(object sender, EventArgs e)
        {

        }

        public void AddSelectedCourse(string gonderenOgrenciAdi, string gonderenOgrenciSoyadi, string mesaj)
        {

            if (dataGridView1.Columns.Count == 0)
            {


                dataGridView1.Columns.Add("gonderenOgrenciAdi", "Öğrenci Adı");
                dataGridView1.Columns["gonderenOgrenciAdi"].Width = 200;
                dataGridView1.Columns.Add("gonderenOgrenciSoyadi", "Öğrenci Soyadı");
                dataGridView1.Columns["gonderenOgrenciSoyadi"].Width = 250;
                dataGridView1.Columns.Add("mesaj", "Mesaj");
                dataGridView1.Columns["mesaj"].Width = 250;
            }
            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = gonderenOgrenciAdi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = gonderenOgrenciSoyadi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = mesaj });


            dataGridView1.Rows.Add(row); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }
    }
}


