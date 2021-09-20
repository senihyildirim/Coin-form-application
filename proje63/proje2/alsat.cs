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
    public partial class alsat : Form
    {
        public alsat()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("provider= microsoft.jet.oledb.4.0; data source= vtt3.mdb");
        public Form1 formIns1;

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void alsat_Load(object sender, EventArgs e)
        {
            string[] birimler = { "TL", "Dolar", "Euro", "BTC" };
            comboBox1.Items.AddRange(birimler);
            comboBox2.Items.AddRange(birimler);

            UpdateBalance();
            UpdateCardComboBox();

            /*
            textBox1.Text = formIns1.userData.tl;
            textBox2.Text = formIns1.userData.dolar;
            textBox3.Text = formIns1.userData.euro;
            textBox4.Text = formIns1.userData.btc;
            */

        }

        public void UpdateBalance()
        {
            textBox1.Text = formIns1.userBalance.tl.ToString("F2");
            textBox2.Text = formIns1.userBalance.usd.ToString("F2");
            textBox3.Text = formIns1.userBalance.eur.ToString("F2");
            textBox4.Text = formIns1.userBalance.btc.ToString("F2");

            /*
            OleDbCommand komut = new OleDbCommand("SELECT * FROM Bakiye WHERE ID=@id", baglanti);
            komut.Parameters.AddWithValue("@id", formIns1.userData.id);
            baglanti.Open();
            OleDbDataReader dr = komut.ExecuteReader();
            if (dr.HasRows && dr.Read())
            {
                textBox1.Text = dr.GetValue(1).ToString();
                textBox2.Text = dr.GetValue(2).ToString();
                textBox3.Text = dr.GetValue(3).ToString();
                textBox4.Text = dr.GetValue(4).ToString();
            }                                
            dr.Close();
            baglanti.Close();
            */
        }

        public void UpdateCardComboBox()
        {
            comboBox3.Items.Clear();

            if (formIns1.userCardList != null)
            {
                for (int i = 0; i < formIns1.userCardList.Count; i++)
                    comboBox3.Items.Add(formIns1.userCardList[i].kartNo);

                if (formIns1.userCardList.Count > 0)
                    comboBox3.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //hesap hsp = new hesap();
            //hsp.Show();
            formIns1.ShowHesapScreen();
            this.Hide();
            //Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //alsat alst = new alsat();
            //alst.Show();

            //Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //borsa brs = new borsa();
            //brs.Show();
            formIns1.ShowBorsaScreen();
            this.Hide();
            //Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //iletisim ilt = new iletisim();
            //ilt.Show();
            formIns1.ShowIletisimScreen();
            this.Hide();
            //Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
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

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Başarıyla çıkış yaptınız");

            formIns1.Close();
        }

        private void tlAddButton_Click(object sender, EventArgs e)
        {
            int miktar = 0;
            if(int.TryParse(textBox6.Text, out miktar))
            {
                formIns1.AddBalance(miktar, 0);
            }
        }

        private void usdAddButton_Click(object sender, EventArgs e)
        {
            int miktar = 0;
            if (int.TryParse(textBox6.Text, out miktar))
            {
                formIns1.AddBalance(miktar, 1);
            }
        }

        private void eurAddButton_Click(object sender, EventArgs e)
        {
            int miktar = 0;
            if (int.TryParse(textBox6.Text, out miktar))
            {
                formIns1.AddBalance(miktar, 2);
            }
        }

        private void btcAddButton_Click(object sender, EventArgs e)
        {
            int miktar = 0;
            if (int.TryParse(textBox6.Text, out miktar))
            {
                formIns1.AddBalance(miktar, 3);
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
                return;

            if (comboBox1.SelectedIndex == comboBox2.SelectedIndex)
                return;

            double miktar = 0;
            if (double.TryParse(textBox5.Text, out miktar))
            {
                formIns1.ConvertCurrency(miktar, comboBox1.SelectedIndex, comboBox2.SelectedIndex);
            }
        }
    }
}
