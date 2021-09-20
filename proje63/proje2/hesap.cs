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
    public partial class hesap : Form
    {
        public hesap()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("provider= microsoft.jet.oledb.4.0; data source= vtt3.mdb");
        public Form1 formIns1;

        private void button4_Click(object sender, EventArgs e)
        {
            //hesap hsp = new hesap();
            //hsp.Show();
            //formIns1.ShowHesapScreen();
            //Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //alsat alst = new alsat();
            //alst.Show();
            formIns1.ShowAlSatScreen();
            this.Hide();
            //Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            //borsa brs = new borsa();
            //brs.Show();
            formIns1.ShowBorsaScreen();
            this.Hide();
            //Close();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {

            //iletisim ilt = new iletisim();
            //ilt.Show();
            formIns1.ShowIletisimScreen();
            this.Hide();
            //Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void hesap_Load(object sender, EventArgs e)
        {
            /*
            baglanti.Open();
            OleDbCommand kom = new OleDbCommand();
            kom.CommandText = "SELECT * FROM Kisi";
            kom.Connection = baglanti;
            kom.CommandType = CommandType.Text;
            OleDbDataReader drr;
            drr = kom.ExecuteReader();
            
            while(drr.Read())
            {
                comboBox1.ValueMember = "id";
                comboBox1.DisplayMember = formIns1.userData.kartno;

            }
            
            baglanti.Close();
            */

            UpdateCardComboBox();

            textBox1.Text = formIns1.userData.adSoyad;
            textBox2.Text = formIns1.userData.tcno;
            textBox3.Text = formIns1.userData.eposta;
            textBox4.Text = formIns1.userData.telno;

        }

        public void UpdateCardComboBox()
        {
            comboBox1.Items.Clear();

            if (formIns1.userCardList != null)
            {
                for (int i = 0; i < formIns1.userCardList.Count; i++)
                    comboBox1.Items.Add(formIns1.userCardList[i].kartNo);

                if(formIns1.userCardList.Count > 0)
                    comboBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

       


        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Şifre alanları boş kalamaz!", "UYARI");
            }
            else if(!textBox5.Text.Equals(formIns1.userData.kullSifre))
            {
                MessageBox.Show("Lütfen şifrenizi doğru giriniz!", "UYARI");
            }
            else if(textBox5.Text.Equals(textBox6.Text) || textBox5.Text.Equals(textBox7.Text))
            {
                MessageBox.Show("Lütfen farklı bir şifre giriniz!", "UYARI");
            }
            else if(!textBox6.Text.Equals(textBox7.Text))
            {
                MessageBox.Show("Lütfen yeni şifreyi doğru giriniz!", "UYARI");
            }
            else
            {
                
                OleDbCommand komut = new OleDbCommand("UPDATE Kisi SET sifre = @sifre WHERE ID = @id", baglanti);
                komut.Parameters.AddWithValue("@sifre", textBox6.Text);
                komut.Parameters.AddWithValue("@id", formIns1.userData.id);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();

                formIns1.userData.kullSifre = textBox6.Text;

                MessageBox.Show("Şifreniz başarıyla değiştirildi!", "TEBRİKLER");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            formIns1.ShowKartEkleScreen();



            //kartekle krt1 = new kartekle();
            //krt1.formIns1 = this;
            //krt1.ShowDialog();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Başarıyla çıkış yaptınız");

            formIns1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
                return;

            OleDbCommand komut = new OleDbCommand("DELETE FROM Kart WHERE ID=@id", baglanti);
            komut.Parameters.AddWithValue("@id", formIns1.userCardList[comboBox1.SelectedIndex].id);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            
            formIns1.UpdateCards();

            MessageBox.Show("Kart başarıyla silindi!","TEBRİKLER");

        }
    }
}
