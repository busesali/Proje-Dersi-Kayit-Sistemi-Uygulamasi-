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
    public partial class Form7 : Form
    {
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }
        public string OgrenciNumarasi { get; set; }

        public Form7()
        {
            InitializeComponent();
            FillComboBox();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDers = comboBox1.SelectedItem.ToString();

            string prevSelectedHoca = comboBox2.SelectedItem as string;
            comboBox2.Items.Clear();
            comboBox2.Text = string.Empty;

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sorgu = "SELECT hocaad, hocasoyad, verdigidersler FROM ogretmenler";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string dersler = okuyucu.GetString(2);

                            string[] dersListesi = dersler.Split(',');

                            foreach (string ders in dersListesi)
                            {
                                if (ders.Trim() == selectedDers)
                                {
                                    string hocaAdi = okuyucu.GetString(0);
                                    string hocaSoyadi = okuyucu.GetString(1);

                                    string hocaAdSoyad = hocaAdi + " " + hocaSoyadi;
                                    comboBox2.Items.Add(hocaAdSoyad);
                                }
                            }
                        }
                    }
                }
            }
        }


        private void FillComboBox()
        {
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT dersadi FROM dersler";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string dersAdi = okuyucu.GetString(0);
                            comboBox1.Items.Add(dersAdi);
                        }
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string secilenDers = comboBox1.SelectedItem as string;
            string secilenOgretmen = comboBox2.SelectedItem as string;

            if (!string.IsNullOrEmpty(secilenDers) && !string.IsNullOrEmpty(secilenOgretmen))
            {
                int maxHocaSecimSayisi = GetMaxHocaSecimSayisi(secilenDers);

                int existingHocaTalepleri = GetExistingHocaTalepleri(secilenDers, OgrenciAdi, OgrenciSoyadi);

                if (existingHocaTalepleri < maxHocaSecimSayisi)
                {
                    string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        string checkDersTalebiSorgusu = "SELECT COUNT(*) FROM derstalepleri " +
                            "WHERE dersadi = @dersadi AND hocaadi = @hocaadi AND hocasoyadi = @hocasoyadi " +
                            "AND ogrenciadi = @ogrenciadi AND ogrencisoyadi = @ogrencisoyadi";

                        using (NpgsqlCommand checkDersTalebiKomut = new NpgsqlCommand(checkDersTalebiSorgusu, connection))
                        {
                            checkDersTalebiKomut.Parameters.AddWithValue("@dersadi", secilenDers);
                            string[] hocaAdSoyad = secilenOgretmen.Split(' ');
                            checkDersTalebiKomut.Parameters.AddWithValue("@hocaadi", hocaAdSoyad[0]);
                            checkDersTalebiKomut.Parameters.AddWithValue("@hocasoyadi", hocaAdSoyad[1]);
                            checkDersTalebiKomut.Parameters.AddWithValue("@ogrenciadi", OgrenciAdi);
                            checkDersTalebiKomut.Parameters.AddWithValue("@ogrencisoyadi", OgrenciSoyadi);

                            int existingRequests = Convert.ToInt32(checkDersTalebiKomut.ExecuteScalar());

                            if (existingRequests > 0)
                            {
                                MessageBox.Show("Bu dersi bu öğretmenden alma talebinde zaten bulundunuz. Farklı bir öğretmen seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                string talepEkleSorgusu = "INSERT INTO derstalepleri (dersadi, hocaadi, hocasoyadi, ogrenciadi, ogrencisoyadi, talepdurumu) " +
                                    "VALUES (@dersadi, @hocaadi, @hocasoyadi, @ogrenciadi, @ogrencisoyadi, @talepdurumu)";

                                using (NpgsqlCommand talepEkleKomut = new NpgsqlCommand(talepEkleSorgusu, connection))
                                {
                                    talepEkleKomut.Parameters.AddWithValue("@dersadi", secilenDers);
                                    talepEkleKomut.Parameters.AddWithValue("@hocaadi", hocaAdSoyad[0]);
                                    talepEkleKomut.Parameters.AddWithValue("@hocasoyadi", hocaAdSoyad[1]);
                                    talepEkleKomut.Parameters.AddWithValue("@ogrenciadi", OgrenciAdi);
                                    talepEkleKomut.Parameters.AddWithValue("@ogrencisoyadi", OgrenciSoyadi);
                                    talepEkleKomut.Parameters.AddWithValue("@talepdurumu", "onay bekleniyor");

                                    try
                                    {
                                        int etkilenenSatirSayisi = talepEkleKomut.ExecuteNonQuery();

                                        if (etkilenenSatirSayisi > 0)
                                        {
                                            MessageBox.Show("Ders talebiniz başarılı bir şekilde ilgili hocaya iletilmiştir.", "Talep İletildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ders talebi gönderilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Hata: " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bu dersi daha fazla hocadan talep edemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen ders ve öğretmen seçimini doğru şekilde gerçekleştiriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private int GetExistingHocaTalepleri(string secilenDers, string ogrenciAdi, string ogrenciSoyadi)
        {
            int existingHocaTalepleri = 0;

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sorgu = "SELECT COUNT(*) FROM derstalepleri WHERE dersadi = @dersadi AND ogrenciadi = @ogrenciadi AND ogrencisoyadi = @ogrencisoyadi";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@dersadi", secilenDers);
                    komut.Parameters.AddWithValue("@ogrenciadi", ogrenciAdi);
                    komut.Parameters.AddWithValue("@ogrencisoyadi", ogrenciSoyadi);

                    existingHocaTalepleri = Convert.ToInt32(komut.ExecuteScalar());
                }
            }

            return existingHocaTalepleri;
        }


        private int GetMaxHocaSecimSayisi(string secilenDers)
        {
            int maxHocaSecimSayisi = 0;

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sorgu = "SELECT hocasecimsayisi FROM yonetici";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {


                    object result = komut.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        maxHocaSecimSayisi = Convert.ToInt32(result);
                    }
                }
            }

            return maxHocaSecimSayisi;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sorgu = "SELECT dersadi, hocaadi, hocasoyadi, talepdurumu FROM derstalepleri " +
                    "WHERE ogrenciadi = @ogrenciadi AND ogrencisoyadi = @ogrencisoyadi";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@ogrenciadi", OgrenciAdi);
                    komut.Parameters.AddWithValue("@ogrencisoyadi", OgrenciSoyadi);

                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string dersAdi = okuyucu.GetString(0);
                            string hocaAdi = okuyucu.GetString(1);
                            string hocaSoyadi = okuyucu.GetString(2);
                            string talepDurumu = okuyucu.GetString(3);

                            form8.AddSelectedCourse(dersAdi, hocaAdi, hocaSoyadi, talepDurumu);
                        }
                    }
                }
            }

            form8.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ogrenciIlgiAlanlari = GetStudentInterestAreas(OgrenciNumarasi);

            if (string.IsNullOrEmpty(ogrenciIlgiAlanlari))
            {
                MessageBox.Show("Öğrencinin ilgi alanları bulunmamaktadır.");
                return;
            }

            Form11 form11 = new Form11(ogrenciIlgiAlanlari);
            form11.OgrenciAdi = OgrenciAdi;
            form11.OgrenciSoyadi = OgrenciSoyadi;
            form11.Show();
        }



        private string GetStudentInterestAreas(string ogrenciNumarasi)
        {
            string ilgiAlanlari = "";

            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT ogrenciilgialanlari FROM ogrenciler WHERE ogrencinumarasi = @ogrencinumarasi";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@ogrencinumarasi", ogrenciNumarasi);

                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        if (okuyucu.Read())
                        {
                            ilgiAlanlari = okuyucu.GetString(0);
                        }
                    }
                }
            }

            return ilgiAlanlari;
        }



        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}



