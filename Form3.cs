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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("Server=localHost; Port=5436; Database=proje; User Id=postgres; Password=1234");

        private void button1_Click_1(object sender, EventArgs e)
        {
            string hocasicilnumarasi = textBox1.Text;
            string hocasifre = textBox2.Text;

            string sorgu = "SELECT * FROM ogretmenler WHERE hocasicilnumarasi = @hocasicilnumarasi AND hocasifre = @hocasifre";

            using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@hocasicilnumarasi", hocasicilnumarasi);
                komut.Parameters.AddWithValue("@hocasifre", hocasifre);

                try
                {
                    baglanti.Open();
                    NpgsqlDataReader okuyucu = komut.ExecuteReader();

                    if (okuyucu.HasRows)
                    {
                        okuyucu.Read();
                        string hocaAdi = okuyucu.GetString(okuyucu.GetOrdinal("hocaad"));
                        string hocaSoyadi = okuyucu.GetString(okuyucu.GetOrdinal("hocasoyad"));

                        Form9 form9 = new Form9();
                        form9.HocaAdi = hocaAdi;
                        form9.HocaSoyadi = hocaSoyadi;

                        form9.Show();

                    }
                    else
                    {
                        MessageBox.Show("Giriş bilgileri hatalı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    baglanti.Close();
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
