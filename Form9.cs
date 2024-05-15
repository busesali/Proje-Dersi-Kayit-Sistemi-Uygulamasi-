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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();

        }
        public string HocaAdi { get; set; }
        public string HocaSoyadi { get; set; }
        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            using (NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234"))
            {
                connection.Open();

                string sorgu = "SELECT ogrenciadi, ogrencisoyadi, dersadi FROM derstalepleri " +
                    "WHERE hocaadi = @hocaadi AND hocasoyadi = @hocasoyadi AND talepdurumu = 'onay bekleniyor'";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@hocaadi", HocaAdi);
                    komut.Parameters.AddWithValue("@hocasoyadi", HocaSoyadi);

                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string ogrenciAdi = okuyucu.GetString(okuyucu.GetOrdinal("ogrenciadi"));
                            string ogrenciSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("ogrencisoyadi"));
                            string dersAdi = okuyucu.GetString(okuyucu.GetOrdinal("dersadi"));

                            form10.AddSelectedCourse(ogrenciAdi, ogrenciSoyadi, dersAdi);
                        }
                    }
                }
            }
            form10.HocaAdi = HocaAdi;
            form10.HocaSoyadi = HocaSoyadi;
            form10.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form14 form14 = new Form14();
            form14.HocaAdi = HocaAdi;
            form14.HocaSoyadi = HocaSoyadi;
            form14.Show();


        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ilgiAlanlari = textBox1.Text;

            using (NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234"))
            {
                connection.Open();

                string sorgu = "UPDATE ogretmenler SET hocailgialanlari = @hocailgialanlari WHERE hocaad = @hocaad AND hocasoyad = @hocasoyad";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@hocailgialanlari", ilgiAlanlari);
                    komut.Parameters.AddWithValue("@hocaad", HocaAdi);
                    komut.Parameters.AddWithValue("@hocasoyad", HocaSoyadi);

                    komut.ExecuteNonQuery();
                }

                MessageBox.Show("İlgi alanları kaydedildi.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form15 form15 = new Form15();

            using (NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234"))
            {
                connection.Open();

                string sorgu = "SELECT gonderenogrenciad, gonderenogrencisoyad, mesaj FROM mesaj " +
                    "WHERE gonderilenhocaad = @hocaad AND gonderilenhocasoyad = @hocasoyad";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@hocaad", HocaAdi);
                    komut.Parameters.AddWithValue("@hocasoyad", HocaSoyadi);

                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string gonderenOgrenciAdi = okuyucu.GetString(okuyucu.GetOrdinal("gonderenogrenciad"));
                            string gonderenOgrenciSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("gonderenogrencisoyad"));
                            string mesaj = okuyucu.GetString(okuyucu.GetOrdinal("mesaj"));
                            form15.AddSelectedCourse(gonderenOgrenciAdi, gonderenOgrenciSoyadi, mesaj);
                        }
                    }
                }
            }

            form15.HocaAdi = HocaAdi;
            form15.HocaSoyadi = HocaSoyadi;
            form15.Show();
        }
    }
}
