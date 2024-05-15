using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WinFormsApp5
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
        public void AddSelectedCourse(string dersAdi, string hocaAdi, string hocaSoyadi, string talepDurumu)
        {
            
            if (dataGridView1.Columns.Count == 0)
            {
              

                dataGridView1.Columns.Add("DersAdi", "Ders Adı");
                dataGridView1.Columns["DersAdi"].Width = 200;
                dataGridView1.Columns.Add("HocaAdi", "Hoca Adı");
                dataGridView1.Columns["HocaAdi"].Width = 130;
                dataGridView1.Columns.Add("HocaSoyadi", "Hoca Soyadı");
                dataGridView1.Columns["HocaSoyadi"].Width = 130;
                dataGridView1.Columns.Add("TalepDurumu", "Talep Durumu");
                dataGridView1.Columns["TalepDurumu"].Width = 130;

                DataGridViewButtonColumn talebiGeriCekButton = new DataGridViewButtonColumn();
                talebiGeriCekButton.Name = "Talep Silme";
                talebiGeriCekButton.Text = "Talebi Geri Çek";
                talebiGeriCekButton.UseColumnTextForButtonValue = true; 
                dataGridView1.Columns.Add(talebiGeriCekButton);
            }

            DataGridViewRow row = new DataGridViewRow();
            row.Cells.Add(new DataGridViewTextBoxCell { Value = dersAdi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = hocaAdi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = hocaSoyadi });
            row.Cells.Add(new DataGridViewTextBoxCell { Value = talepDurumu });

            if (!(talepDurumu == "onay bekleniyor"))
            {
                row.Cells.Add(new DataGridViewButtonCell()); 
            }

            dataGridView1.Rows.Add(row);
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Talep Silme"].Index && e.RowIndex >= 0)
            {              
                DialogResult result = MessageBox.Show("Ders talebini geri çekmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string dersAdi = dataGridView1.Rows[e.RowIndex].Cells["DersAdi"].Value.ToString();
                    string hocaAdi = dataGridView1.Rows[e.RowIndex].Cells["HocaAdi"].Value.ToString();
                    string hocaSoyadi = dataGridView1.Rows[e.RowIndex].Cells["HocaSoyadi"].Value.ToString();

                    string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM derstalepleri WHERE dersadi = @dersadi AND hocaadi = @hocaadi AND hocasoyadi = @hocasoyadi";
                        using (NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@dersadi", dersAdi);
                            deleteCommand.Parameters.AddWithValue("@hocaadi", hocaAdi);
                            deleteCommand.Parameters.AddWithValue("@hocasoyadi", hocaSoyadi);

                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                            }
                            else
                            {
                                MessageBox.Show("Talep silinirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
