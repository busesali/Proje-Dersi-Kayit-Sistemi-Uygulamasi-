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
    public partial class Form6 : Form
    {
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }
        public Form6()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234");

        private void Form6_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)

        {
            NpgsqlConnection baglanti2 = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234");
            string adSoyadSorgusu = "SELECT ogrenciadi, ogrencisoyadi FROM ogrenciler WHERE ogrencinumarasi = @ogrencinumarasi";
            using (NpgsqlCommand adSoyadKomut = new NpgsqlCommand(adSoyadSorgusu, baglanti2))
            {
                adSoyadKomut.Parameters.AddWithValue("@ogrencinumarasi", OgrenciNumarasi);
                
                baglanti2.Open();
                NpgsqlDataReader adSoyadOkuyucu = adSoyadKomut.ExecuteReader();
                if (adSoyadOkuyucu.Read())
                {
                    string ogrenciAdi = adSoyadOkuyucu.GetString(0);
                    string ogrenciSoyadi = adSoyadOkuyucu.GetString(1);

                    Form7 form7 = new Form7();
                    form7.OgrenciNumarasi = OgrenciNumarasi;
                    form7.OgrenciAdi = ogrenciAdi;
                    form7.OgrenciSoyadi = ogrenciSoyadi;
                    form7.Show();

                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string OgrenciNumarasi { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sorgu = "SELECT pdfdersadi, pdfdersnotu FROM pdften_okutulan_dersler WHERE ogrencinumarasi = @ogrencinumarasi";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    NpgsqlParameter param = new NpgsqlParameter();
                    param.ParameterName = "@ogrencinumarasi";
                    param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;
                    param.Value = OgrenciNumarasi;
                    komut.Parameters.Add(param);

                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string adSoyadSorgusu = "SELECT ogrenciadi, ogrencisoyadi FROM ogrenciler WHERE ogrencinumarasi = @ogrencinumarasi";
            using (NpgsqlCommand adSoyadKomut = new NpgsqlCommand(adSoyadSorgusu, baglanti))
            {
                adSoyadKomut.Parameters.AddWithValue("@ogrencinumarasi", OgrenciNumarasi);
                baglanti.Open();
                NpgsqlDataReader adSoyadOkuyucu = adSoyadKomut.ExecuteReader();
                if (adSoyadOkuyucu.Read())
                {
                    string ogrenciAdi = adSoyadOkuyucu.GetString(0);
                    string ogrenciSoyadi = adSoyadOkuyucu.GetString(1);

                    Form13 form13 = new Form13();
                    form13.OgrenciAdi = ogrenciAdi;
                    form13.OgrenciSoyadi = ogrenciSoyadi;
                    form13.Show();

                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
                    Form16 form16 = new Form16();

            using (NpgsqlConnection connection = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234"))
            {
                connection.Open();

                string sorgu = "SELECT gonderenhocaad, gonderenhocasoyad, mesaj FROM mesaj2 " +
                    "WHERE gonderilenogrenciad = @ogrenciadi AND gonderilenogrencisoyad= @ogrencisoyadi";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@ogrenciadi", OgrenciAdi);
                    komut.Parameters.AddWithValue("@ogrencisoyadi", OgrenciSoyadi);

                    using (NpgsqlDataReader okuyucu = komut.ExecuteReader())
                    {
                        while (okuyucu.Read())
                        {
                            string gonderenHocaAdi = okuyucu.GetString(okuyucu.GetOrdinal("gonderenhocaad"));
                            string gonderenHocaSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("gonderenhocasoyad"));
                            string mesaj = okuyucu.GetString(okuyucu.GetOrdinal("mesaj"));
                            form16.AddSelectedCourse(gonderenHocaAdi, gonderenHocaSoyadi, mesaj);
                        }
                    }
                }
            }

            
            form16.Show();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}






