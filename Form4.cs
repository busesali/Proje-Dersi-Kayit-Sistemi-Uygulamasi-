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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234");

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string yoneticisicilno = textBox1.Text;
            string yoneticisifre = textBox2.Text;

            string sorgu = "SELECT * FROM yonetici WHERE yoneticisicilno = @yoneticisicilno AND yoneticisifre = @yoneticisifre";

            using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti))
            {
                komut.Parameters.AddWithValue("@yoneticisicilno", yoneticisicilno);
                komut.Parameters.AddWithValue("@yoneticisifre", yoneticisifre);

                try
                {
                    baglanti.Open();
                    NpgsqlDataReader okuyucu = komut.ExecuteReader();

                    if (okuyucu.HasRows)
                    {
                        Form12 form12 = new Form12();
                        form12.Show();

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

            if (baglanti.State == ConnectionState.Open)
            {
                baglanti.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
