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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp5
{
    public partial class Form11 : Form
    {
        private string ogrenciIlgiAlanlari;

        public Form11(string ogrenciIlgiAlanlari)
        {
            InitializeComponent();
            this.ogrenciIlgiAlanlari = ogrenciIlgiAlanlari;
            FillTeacherComboBox();


        }
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }
        private void FillTeacherComboBox()
        {
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT hocaad, hocasoyad, verdigidersler, hocailgialanlari FROM ogretmenler";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string hocaAdi = okuyucu.GetString(okuyucu.GetOrdinal("hocaad"));
                            string hocaSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("hocasoyad"));
                            string dersler = okuyucu.GetString(okuyucu.GetOrdinal("verdigidersler"));
                            string hocaIlgiAlanlari = okuyucu.GetString(okuyucu.GetOrdinal("hocailgialanlari"));

                            if (HasMatchingInterestAreas(ogrenciIlgiAlanlari, hocaIlgiAlanlari))
                            {
                                string teacherInfo = hocaAdi + " " + hocaSoyadi + " (" + hocaIlgiAlanlari + ")";
                                comboBox1.Items.Add(teacherInfo);
                            }
                        }
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTeacherInfo = comboBox1.SelectedItem as string;

            string[] parts = selectedTeacherInfo.Split(new[] { " (" }, StringSplitOptions.None);
            string selectedTeacherName = parts[0];

            string selectedTeacherCourses = GetTeacherCourses(selectedTeacherName);

            string[] coursesArray = selectedTeacherCourses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            comboBox2.Items.Clear();
            comboBox2.Text = string.Empty;
            comboBox2.Items.AddRange(coursesArray);
        }

        private bool HasMatchingInterestAreas(string ogrenciIlgiAlanlari, string hocaIlgiAlanlari)
        {
            string[] ogrenciIlgiAlanlariList = ogrenciIlgiAlanlari.Split(',');
            string[] hocaIlgiAlanlariList = hocaIlgiAlanlari.Split(',');

            foreach (string ogrenciIlgiAlani in ogrenciIlgiAlanlariList)
            {
                foreach (string hocaIlgiAlani in hocaIlgiAlanlariList)
                {
                    if (ogrenciIlgiAlani.Trim() == hocaIlgiAlani.Trim())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetTeacherCourses(string teacherName)
        {
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT verdigidersler FROM ogretmenler " +
                    "WHERE hocaad || ' ' || hocasoyad = @teacherName";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@teacherName", teacherName);

                    string teacherCourses = komut.ExecuteScalar() as string;

                    return teacherCourses;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string secilenDers = comboBox2.SelectedItem as string;
            string secilenOgretmen = comboBox1.SelectedItem as string;

            if (!string.IsNullOrEmpty(secilenDers) && !string.IsNullOrEmpty(secilenOgretmen))
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
                MessageBox.Show("Lütfen ders ve öğretmen seçimini doğru şekilde gerçekleştiriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
