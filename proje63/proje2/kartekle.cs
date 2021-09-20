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
    public partial class kartekle : Form
    {
        public kartekle()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("provider= microsoft.jet.oledb.4.0; data source= vtt3.mdb");
        public Form1 formIns1;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label7.Text = textBox1.Text;
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Text = comboBox1.Text + "/" + comboBox2.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Text = comboBox1.Text + "/" + comboBox2.Text;
        }


        private void kartekle_Load(object sender, EventArgs e)
        {
            int ay;
            int yil;
            for (ay = 1; ay < 13; ay++)
            {
                comboBox1.Items.Add(ay);
            }
            for (yil = 21; yil < 30; yil++)
            {
                comboBox2.Items.Add(yil);
            }
        }

        private void maskedTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            label6.Text = maskedTextBox1.Text;
        }

        private void maskedTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            label9.Text = maskedTextBox2.Text;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            label7.Text = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Ad Soyad boş bırakılamaz!", "UYARI");
            }
            else if (string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                MessageBox.Show("Kart numarası hatalı!", "UYARI");
            }
            else if(comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Ay ve Yıl seçiniz!", "UYARI");
            }
            else if(string.IsNullOrEmpty(maskedTextBox2.Text))
            {
                MessageBox.Show("CVV numarası hatalı!", "UYARI");
            }
            else
            {
                OleDbConnection baglanti = new OleDbConnection("provider= microsoft.jet.oledb.4.0; data source= vtt3.mdb");
                baglanti.Open();

                OleDbCommand komut = new OleDbCommand("SELECT * FROM Kart WHERE kartno=@kartno", baglanti);
                komut.Parameters.AddWithValue("@kartno", maskedTextBox1.Text);

                OleDbDataReader dr = komut.ExecuteReader();
        
                if (dr.HasRows)
                {
                    baglanti.Close();
                    MessageBox.Show("Kart numarası önceden eklenmiş!", "UYARI");
                    return;
                }

                komut = new OleDbCommand("INSERT INTO Kart(userID,kartno,ay,yil,cvv) VALUES(@userID,@kartno,@ay,@yil,@cvv)", baglanti);

                komut.Parameters.AddWithValue("@userID", formIns1.userData.id);
                komut.Parameters.AddWithValue("@kartno", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@ay", comboBox1.Items[comboBox1.SelectedIndex].ToString());
                komut.Parameters.AddWithValue("@yil", comboBox2.Items[comboBox2.SelectedIndex].ToString());
                komut.Parameters.AddWithValue("@cvv", maskedTextBox2.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                formIns1.UpdateCards();

                /*
                OleDbCommand kartisim = new OleDbCommand("update giris set isim = @isim where ID = @id", baglanti);
                kartisim.Parameters.AddWithValue("@isim",textBox1.Text);
                kartisim.Parameters.AddWithValue("@id", formIns1.userData.id);
                kartisim.ExecuteNonQuery();
                baglanti.Close();

                OleDbCommand kartno = new OleDbCommand("update giris set kartno = @kartno where ID = @id", baglanti);
                kartno.Parameters.AddWithValue("@kartno", maskedTextBox1.Text);
                kartno.Parameters.AddWithValue("@id", formIns1.userData.id);
                baglanti.Open();
                kartno.ExecuteNonQuery();
                baglanti.Close();

                OleDbCommand kartay = new OleDbCommand("update giris set ay = @ay where ID = @id", baglanti);
                kartay.Parameters.AddWithValue("@ay", comboBox1.Text);
                kartay.Parameters.AddWithValue("@id", formIns1.userData.id);
                baglanti.Open();
                kartay.ExecuteNonQuery();
                baglanti.Close();


                OleDbCommand kartyil = new OleDbCommand("update giris set yil = @yil where ID = @id", baglanti);
                kartyil.Parameters.AddWithValue("@yil", comboBox2.Text);
                kartyil.Parameters.AddWithValue("@id", formIns1.userData.id);
                baglanti.Open();
                kartyil.ExecuteNonQuery();
                baglanti.Close();


                OleDbCommand kartcvv = new OleDbCommand("update giris set cvv = @cvv where ID = @id", baglanti);
                kartcvv.Parameters.AddWithValue("@cvv", maskedTextBox2.Text);
                kartcvv.Parameters.AddWithValue("@id", formIns1.userData.id);
                baglanti.Open();
                kartcvv.ExecuteNonQuery();
                baglanti.Close();
                */

                MessageBox.Show("Kartınız başarıyla eklendi", "TEBRİKLER");



                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kart Eklemeden Çıkış Yaptınız!!!");
            Close();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
    
}
