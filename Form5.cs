using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Npgsql;
using NpgsqlTypes;

namespace WinFormsApp5
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5436; Database=proje; user Id=postgres; password=1234");

        public string OgrenciNumarasi { get; set; }


        private void button1_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
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

                    form6.OgrenciAdi = ogrenciAdi;
                    form6.OgrenciSoyadi = ogrenciSoyadi;

                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "PDF Dosyaları|*.pdf"; 
            openFileDialog.Title = "PDF Dosyası Seç";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string secilenDosyaYolu = openFileDialog.FileName;
                string pdfMetin = ExtractTextFromPdf(secilenDosyaYolu);


                string patternAdi = @"[A-Z]{3}\d{3}[^Z]*";
                string patternNotu = @"[A-Z]{3}\d{3}.*(AA|BA|BB|CB|CC|DC|DD|FD|FF)";

                Regex regexAdi = new Regex(patternAdi);
                Regex regexNotu = new Regex(patternNotu);

                MatchCollection matchesAdi = regexAdi.Matches(pdfMetin);
                MatchCollection matchesNotu = regexNotu.Matches(pdfMetin);

                string connectionString = "Host=localhost;Port=5436;Database=proje;Username=postgres;Password=1234;";

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    for (int i = 0; i < Math.Min(matchesAdi.Count, matchesNotu.Count); i++)
                    {
                        string courseName = matchesAdi[i].Value.Substring(6); 
                        string courseGrade = matchesNotu[i].Groups[1].Value; 

                        using (NpgsqlCommand insertCommand = new NpgsqlCommand("INSERT INTO pdften_okutulan_dersler (pdfdersadi, pdfdersnotu, ogrencinumarasi) VALUES (@courseName, @courseGrade, @ogrencinumarasi)", connection))
                        {
                            insertCommand.Parameters.AddWithValue("@courseName", courseName);
                            insertCommand.Parameters.AddWithValue("@courseGrade", courseGrade);
                            insertCommand.Parameters.AddWithValue("@ogrencinumarasi", OgrenciNumarasi); 

                            try
                            {
                                insertCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Hata Oluştu: " + ex.Message);
                            }
                        }
                    }
                }

                MessageBox.Show("Ders adları ve notları PostgreSQL veritabanına başarıyla eklendi.");

                
                form6.OgrenciNumarasi = OgrenciNumarasi;
                form6.Show();

               

            }
        }



        private string GetCourseGrade(string courseName)
        {
            string[] validGrades = { "AA", "BA", "BB", "CB", "CC", "DC", "DD", "FD", "FF" };

            foreach (string grade in validGrades)
            {
                if (courseName.Contains(grade))
                {
                    return grade;
                }
            }

            return null; 
        }

        private string ExtractTextFromPdf(string pdfFilePath)
        {
            StringBuilder text = new StringBuilder();
            using (PdfReader pdfReader = new PdfReader(pdfFilePath))
            {
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(pdfReader, page));
                }
            }
            return text.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}