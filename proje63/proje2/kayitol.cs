using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace proje2
{
    public partial class kayitol : Form
    {
        public kayitol()
        {
            InitializeComponent();
        }

        public Form1 formIns1;

        private void button2_Click(object sender, EventArgs e)
        {
            
            string kul_adi = textBox1.Text;
            string sif_re = textBox2.Text;
            string tc_no = textBox4.Text;
            string e_posta = textBox5.Text;
            string tel_no = textBox6.Text;
            string adsoyad = textBox7.Text;

            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Şifreler Eşleşmiyor.");
            }
            else if (tc_no.Length != 11)
            {
                MessageBox.Show("Yanlış Tc kimlik numarası girdiniz.");
            }
            else if (kul_adi == "")
                MessageBox.Show("Kullanıcı adı girilmelidir.");
            else if (sif_re == "") 
                MessageBox.Show("Sifre adı girilmelidir.");
            else if (tc_no == "")
                MessageBox.Show("TC kimlik numarası Girilmelidir.");
            else if (e_posta == "")
                MessageBox.Show("E-posta adresi girilmelidir.");
            else if (tel_no == "")
                MessageBox.Show("Telefon numarası girilmelidir.");
            else if (textBox2.Text.Length < 8)
                MessageBox.Show("Şifreniz çok kısa");
            else if (textBox2.Text.Length > 14)
                MessageBox.Show("Şifreniz çok uzun");
            else
            {
                OleDbConnection baglanti = new OleDbConnection("provider= microsoft.jet.oledb.4.0; data source= vtt3.mdb");
                baglanti.Open();

                OleDbCommand komut = new OleDbCommand("INSERT INTO Kisi(kullaniciadi,sifre,eposta,tcno,telno,ad_soyad) VALUES(@kullaniciadi,@sifre,@eposta,@tcno,@telno,@ad_soyad)", baglanti);

                komut.Parameters.AddWithValue("@kullaniciadi", textBox1.Text);
                komut.Parameters.AddWithValue("@sifre", textBox2.Text);
                komut.Parameters.AddWithValue("@eposta", textBox5.Text);
                komut.Parameters.AddWithValue("@tcno", textBox4.Text);
                komut.Parameters.AddWithValue("@telno", textBox6.Text);
                komut.Parameters.AddWithValue("@ad_soyad", textBox7.Text);

                komut.ExecuteNonQuery();

                komut.CommandText = "SELECT @@Identity";
                int ID = (int)komut.ExecuteScalar();

                komut = new OleDbCommand("INSERT INTO Bakiye(ID) VALUES (@id)", baglanti);
                komut.Parameters.AddWithValue("@kullaniciadi", ID);
                komut.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show("Kaydınız başarı ile oluşturuldu");
            }
            
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kayitol_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 100; i++)
            {
                comboBox1.Items.Add("+" + i);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kayıt olmadan çıkış yaptınız.");

            Close();
        }
    }
}
