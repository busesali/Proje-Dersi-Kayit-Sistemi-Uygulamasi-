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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234");

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ogrencinumarasi = textBox1.Text;
            string ogrencisifre = textBox2.Text;

            string sorgu = "SELECT * FROM ogrenciler WHERE ogrencinumarasi = @ogrencinumarasi AND ogrencisifre = @ogrencisifre";

                using (NpgsqlCommand komut = new NpgsqlCommand(sorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@ogrencinumarasi", ogrencinumarasi);
                    komut.Parameters.AddWithValue("@ogrencisifre", ogrencisifre);

                    try
                    {
                        baglanti.Open();
                        NpgsqlDataReader okuyucu = komut.ExecuteReader();

                        if (okuyucu.HasRows)
                        {
                            Form5 form5 = new Form5();
                            form5.OgrenciNumarasi = ogrencinumarasi;
                            form5.Show();

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
            private void label2_Click(object sender, EventArgs e)
            {
            }

            private void label1_Click(object sender, EventArgs e)
            {
            }

            private void textBox2_TextChanged(object sender, EventArgs e)
            {
            }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {
            }

            private void Form2_Load(object sender, EventArgs e)
            {
            textBox2.PasswordChar = '*';
        }
        }
    }


