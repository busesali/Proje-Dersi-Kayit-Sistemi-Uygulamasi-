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
    public partial class Form14 : Form
    {
        public string HocaAdi { get; set; }
        public string HocaSoyadi { get; set; }

        int karakterSiniri;
        private const string karakterSiniriText = "En fazla {0} karakterlik bir mesaj gönderebilirsiniz.";

        public Form14()
        {
            InitializeComponent();
            karakterSiniri = GetCharacterLimitFromDatabase();
            textBox1.MaxLength = karakterSiniri;

            textBox1.Text = string.Format(karakterSiniriText, karakterSiniri);

            textBox1.Click += (sender, e) =>
            {
                if (textBox1.Text == string.Format(karakterSiniriText, karakterSiniri))
                {
                    textBox1.Text = string.Empty;
                }
            };

            textBox1.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    textBox1.Text = string.Format(karakterSiniriText, karakterSiniri);
                }
            };
        }

        private int GetCharacterLimitFromDatabase()
        {
            int karakterSiniri = 0;

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT karaktersayisi FROM yonetici";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    karakterSiniri = Convert.ToInt32(komut.ExecuteScalar());
                }
            }

            return karakterSiniri;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedTeacher = comboBox1.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedTeacher))
            {
                MessageBox.Show("Lütfen öğrenci seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

            }

            string message = textBox1.Text;

            string[] parts = selectedTeacher.Split(' ');
            string selectedTeacherAd = parts[0];
            string selectedTeacherSoyad = parts[1];

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "INSERT INTO mesaj2 (gonderenhocaad, gonderenhocasoyad, mesaj, gonderilenogrenciad, gonderilenogrencisoyad) " +
                    "VALUES (@gonderenhocaad, @gonderenhocasoyad, @mesaj, @gonderilenogrenciad, @gonderilenogrencisoyad)";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@gonderenhocaad", HocaAdi);
                    komut.Parameters.AddWithValue("@gonderenhocasoyad", HocaSoyadi);
                    komut.Parameters.AddWithValue("@mesaj", message);
                    komut.Parameters.AddWithValue("@gonderilenogrenciad", selectedTeacherAd);
                    komut.Parameters.AddWithValue("@gonderilenogrencisoyad", selectedTeacherSoyad);

                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Mesajınız başarıyla gönderildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Mesaj gönderilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            FillTeacherComboBox();
        }

        private void FillTeacherComboBox()
        {


            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT ogrenciadi, ogrencisoyadi FROM ogrenciler";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string ogrenciAdi = okuyucu.GetString(okuyucu.GetOrdinal("ogrenciadi"));
                            string ogrenciSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("ogrencisoyadi"));

                            comboBox1.Items.Add($"{ogrenciAdi} {ogrenciSoyadi}");
                        }
                    }
                }
            }
        }
    }
}
