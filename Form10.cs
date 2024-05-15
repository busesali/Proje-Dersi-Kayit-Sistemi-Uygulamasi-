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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        public string HocaAdi { get; set; }
        public string HocaSoyadi { get; set; }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("OgrenciAdi", "Öğrenci Adı");
            dataGridView1.Columns["OgrenciAdi"].Width = 150;
            dataGridView1.Columns.Add("OgrenciSoyadi", "Öğrenci Soyadı");
            dataGridView1.Columns["OgrenciSoyadi"].Width = 150;
            dataGridView1.Columns.Add("DersAdi", "Ders Adı");
            dataGridView1.Columns["DersAdi"].Width = 280;

            DataGridViewButtonColumn onaylaButton = new DataGridViewButtonColumn();
            onaylaButton.Name = "OnaylaColumn";
            onaylaButton.Text = "Onayla";
            onaylaButton.UseColumnTextForButtonValue = true; 
            onaylaButton.DefaultCellStyle.BackColor = Color.Green;
            dataGridView1.Columns.Add(onaylaButton);

            DataGridViewButtonColumn reddetButton = new DataGridViewButtonColumn();
            reddetButton.Name = "ReddetColumn";
            reddetButton.Text = "Reddet";
            reddetButton.UseColumnTextForButtonValue = true; 
            reddetButton.DefaultCellStyle.BackColor = Color.Red;
            dataGridView1.Columns.Add(reddetButton);

        }

        public void AddSelectedCourse(string ogrenciAdi, string ogrenciSoyadi, string dersAdi)
        {
            dataGridView1.Rows.Add(ogrenciAdi, ogrenciSoyadi, dersAdi);

        }


        private void Form10_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "OnaylaColumn")
                {
                    string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE derstalepleri SET talepdurumu = 'onaylandı' WHERE hocaadi = @hocaadi AND hocasoyadi = @hocasoyadi";

                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@hocaadi", HocaAdi);
                            updateCommand.Parameters.AddWithValue("@hocasoyadi", HocaSoyadi);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Talep onaylandı.");
                                dataGridView1.Rows.RemoveAt(e.RowIndex);

                            }
                            else
                            {
                                MessageBox.Show("Talep onaylanırken hata oluştu.");
                            }
                        }
                    }

                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "ReddetColumn")
                {
                    string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";

                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE derstalepleri SET talepdurumu = 'reddedildi' WHERE hocaadi = @hocaadi AND hocasoyadi = @hocasoyadi";

                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@hocaadi", HocaAdi);
                            updateCommand.Parameters.AddWithValue("@hocasoyadi", HocaSoyadi);

                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Talep reddedildi.");
                                dataGridView1.Rows.RemoveAt(e.RowIndex);

                            }
                            else
                            {
                                MessageBox.Show("Talep reddedilirken hata oluştu.");
                            }
                        }
                    }

                }
            }
        }

    }

}  