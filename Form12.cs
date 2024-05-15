using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp5
{
    public partial class Form12 : Form
    {
        public Form12()
        {


            InitializeComponent();
            SetupStudentListView();
            PopulateStudentListView();
            SetupTeacherListView();
            PopulateTeacherListView();
            SetupTalepListView();
            PopulateTalepListView();
        }

        private void SetupTeacherListView()
        {
            listView2.View = View.Details;
            listView2.GridLines = true;

            listView2.Columns.Add("Öğretmen No", 150);
            listView2.Columns.Add("Öğretmen Adı", 150);
            listView2.Columns.Add("Soyadı", 150);
            listView2.Columns.Add("İlgi Alanları", 300);
            listView2.Columns.Add("Kontenjan Bilgisi", 100);
            listView2.Columns.Add("Şifre", 150);
        }

        private void PopulateTeacherListView()
        {
            listView2.Items.Clear();
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT hocasicilnumarasi, hocaad, hocasoyad, hocailgialanlari, kontenjanbilgisi, hocasifre FROM ogretmenler";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader[0].ToString());
                            item.SubItems.Add(reader[1].ToString());
                            item.SubItems.Add(reader[2].ToString());
                            item.SubItems.Add(reader[3].ToString());
                            item.SubItems.Add(reader[4].ToString());
                            item.SubItems.Add(reader[5].ToString());
                            listView2.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void SetupStudentListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;

            listView1.Columns.Add("Öğrenci No", 150);
            listView1.Columns.Add("Öğrenci Adı", 150);
            listView1.Columns.Add("Soyadı", 150);
            listView1.Columns.Add("Şifre", 150);
            listView1.Columns.Add("İlgi Alanları", 300);
        }

        private void PopulateStudentListView()
        {
            listView1.Items.Clear();
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ogrencinumarasi, ogrenciadi, ogrencisoyadi, ogrencisifre, ogrenciilgialanlari FROM ogrenciler";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader[0].ToString());
                            item.SubItems.Add(reader[1].ToString());
                            item.SubItems.Add(reader[2].ToString());
                            item.SubItems.Add(reader[3].ToString());
                            item.SubItems.Add(reader[4].ToString());
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void SetupTalepListView()
        {
            listView3.View = View.Details;
            listView3.GridLines = true;

            listView3.Columns.Add("Öğrenci Adı", 150);
            listView3.Columns.Add("Öğrenci Soyadı", 150);
            listView3.Columns.Add("Ders Adı", 150);
            listView3.Columns.Add("Talep Durumu", 150);
            listView3.Columns.Add("Hoca Adı", 150);
            listView3.Columns.Add("Hoca Soyadı", 150);
        }

        private void PopulateTalepListView()
        {
            listView3.Items.Clear();
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ogrenciadi, ogrencisoyadi, dersadi, talepdurumu, hocaadi, hocasoyadi FROM derstalepleri";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader[0].ToString());
                            item.SubItems.Add(reader[1].ToString());
                            item.SubItems.Add(reader[2].ToString());
                            item.SubItems.Add(reader[3].ToString());
                            item.SubItems.Add(reader[4].ToString());
                            item.SubItems.Add(reader[5].ToString());
                            listView3.Items.Add(item);
                        }
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int karakterSayisi;
            if (int.TryParse(textBox1.Text, out karakterSayisi))
            {
                string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sorgu = "UPDATE yonetici SET karaktersayisi = @karaktersayisi";

                    using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@karaktersayisi", karakterSayisi);

                        int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                        if (etkilenenSatirSayisi > 0)
                        {
                            MessageBox.Show("Karakter sayısı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Karakter sayısı güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir karakter sayısı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int hocasecimsayisi;
            if (int.TryParse(textBox2.Text, out hocasecimsayisi))
            {
                string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sorgu = "UPDATE yonetici SET hocasecimsayisi = @hocasecimsayisi";

                    using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                    {
                        komut.Parameters.AddWithValue("@hocasecimsayisi", hocasecimsayisi);

                        int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                        if (etkilenenSatirSayisi > 0)
                        {
                            MessageBox.Show("Hoca seçim sayısı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hoca seçim sayısı güncellenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Öğrenciyi silmek istediğinize emin misiniz?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string studentNum = listView1.SelectedItems[0].Text;
                    string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string deleteQuery = "DELETE FROM ogrenciler WHERE ogrencinumarasi = @studentNum";
                        using (NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@studentNum", studentNum);
                            deleteCommand.ExecuteNonQuery();
                        }
                        listView1.Items.Remove(listView1.SelectedItems[0]);
                    }
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Öğretmeni silmek istediğinize emin misiniz?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string teacherNum = listView2.SelectedItems[0].Text;
                    string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string deleteQuery = "DELETE FROM ogretmenler WHERE hocasicilnumarasi = @teacherNum";
                        using (NpgsqlCommand deleteCommand = new NpgsqlCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@teacherNum", teacherNum);
                            deleteCommand.ExecuteNonQuery();
                        }
                        listView2.Items.Remove(listView2.SelectedItems[0]);
                    }
                }
            }
        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string studentName = textBox3.Text;
            string studentLastName = textBox4.Text;
            string studentNumber = textBox5.Text;
            string studentkey = textBox6.Text;

            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(studentLastName) || string.IsNullOrWhiteSpace(studentNumber) || string.IsNullOrWhiteSpace(studentkey))
            {
                MessageBox.Show("Lütfen boş alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO ogrenciler (ogrenciadi, ogrencisoyadi, ogrencinumarasi, ogrencisifre) VALUES (@studentName, @studentLastName, @studentNumber, @studentkey)";
                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@studentName", studentName);
                    insertCommand.Parameters.AddWithValue("@studentLastName", studentLastName);
                    insertCommand.Parameters.AddWithValue("@studentNumber", studentNumber);
                    insertCommand.Parameters.AddWithValue("@studentkey", studentkey);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Öğrenci başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Öğrenci eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

            PopulateStudentListView();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string hocaad = textBox10.Text;
            string hocasoyad = textBox9.Text;
            string sicilno = textBox8.Text;
            string sifre = textBox7.Text;

            if (string.IsNullOrWhiteSpace(hocaad) || string.IsNullOrWhiteSpace(hocasoyad) || string.IsNullOrWhiteSpace(sicilno) || string.IsNullOrWhiteSpace(sifre))
            {
                MessageBox.Show("Lütfen boş alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO ogretmenler (hocaad, hocasoyad, hocasicilnumarasi, hocasifre) VALUES (@hocaad, @hocasoyad, @sicilno, @sifre)";
                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@hocaad", hocaad);
                    insertCommand.Parameters.AddWithValue("@hocasoyad", hocasoyad);
                    insertCommand.Parameters.AddWithValue("@sicilno", sicilno);
                    insertCommand.Parameters.AddWithValue("@sifre", sifre);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Öğretmen başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Öğretmen eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();

            PopulateTeacherListView();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
