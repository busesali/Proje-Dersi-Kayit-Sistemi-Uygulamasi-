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
    public partial class Form13 : Form
    {
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }

        int karakterSiniri;
        private const string karakterSiniriText = "En fazla {0} karakterlik bir mesaj gönderebilirsiniz.";
        public Form13()
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

            string selectedTeacher = comboBox1.SelectedItem as string;

            string[] parts = selectedTeacher.Split(' ');
            string selectedTeacherAd = parts[0];
            string selectedTeacherSoyad = parts[1];

        }

        private void Form13_Load(object sender, EventArgs e)
        {
            FillTeacherComboBox();
        }
        private void FillTeacherComboBox()
        {


            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT hocaad, hocasoyad FROM ogretmenler";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string hocaAdi = okuyucu.GetString(okuyucu.GetOrdinal("hocaad"));
                            string hocaSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("hocasoyad"));

                            comboBox1.Items.Add($"{hocaAdi} {hocaSoyadi}");
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
                MessageBox.Show("Lütfen öğretmen seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                string sorgu = "INSERT INTO mesaj (gonderenogrenciad, gonderenogrencisoyad, mesaj, gonderilenhocaad, gonderilenhocasoyad) " +
                    "VALUES (@gonderenogrenciad, @gonderenogrencisoyad, @mesaj, @gonderilenhocaad, @gonderilenhocasoyad)";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@gonderenogrenciad", OgrenciAdi);
                    komut.Parameters.AddWithValue("@gonderenogrencisoyad", OgrenciSoyadi);
                    komut.Parameters.AddWithValue("@mesaj", message);
                    komut.Parameters.AddWithValue("@gonderilenhocaad", selectedTeacherAd);
                    komut.Parameters.AddWithValue("@gonderilenhocasoyad", selectedTeacherSoyad);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
