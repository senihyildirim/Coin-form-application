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
using System.Net;
using System.IO;

namespace proje2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        OleDbConnection baglanti = new OleDbConnection("provider= microsoft.jet.oledb.4.0; data source= vtt3.mdb");

        public UserData userData;
        public List<UserCards> userCardList;
        public UserBalance userBalance;

        public double usdAlis, usdSatis;
        public double eurAlis, eurSatis;
        public double btcAlis, btcSatis;

        public string coinJson, currencyJson;

        private hesap hsp;
        private alsat alst;
        private borsa brs;
        private iletisim ilts;
        private kartekle krt;
        private kayitol kayit;

        private void button1_Click(object sender, EventArgs e)
        { 
            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM Kisi WHERE kullaniciadi=@kullaniciadi and sifre=@sifre", baglanti);
            sorgu.Parameters.AddWithValue("@kullaniciadi", textBox1.Text);
            sorgu.Parameters.AddWithValue("@sifre", textBox2.Text);
            baglanti.Open();

            OleDbDataReader dr = sorgu.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                userData = new UserData(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetString(6));
                /*
                userData = new UserData((int)dr.GetValue(0), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString(), dr.GetValue(5).ToString(), dr.GetValue(6).ToString(),
                    dr.GetValue(12).ToString(), dr.GetValue(13).ToString(), dr.GetValue(14).ToString(), dr.GetValue(15).ToString(), dr.GetValue(4).ToString(), dr.GetValue(7).ToString(), dr.GetValue(8).ToString(), dr.GetValue(9).ToString(), dr.GetValue(10).ToString());
                */
                //MessageBox.Show(dr.GetValue(0).ToString(), "UYARI");

                //hesap hsp = new hesap();
                /*
                if (hsp == null)
                    hsp = new hesap();

                hsp.formIns1 = this;

                hsp.Show();
                this.Hide();
                */

                baglanti.Close();

                UpdateCards();
                UpdateBalance();
                ShowHesapScreen();
            
            }
            else
            {
                MessageBox.Show("Gidiğiniz veriler sistemdeki kayıtlarla uyuşmuyor. Kontrol ediniz!!");
                baglanti.Close();
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("Kullanıcı adı boş bırakılamaz!");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Şifre boş bırakılamaz!");
            }

            baglanti.Close();
        }

        public void UpdateCards()
        {
            userCardList = new List<UserCards>();
            baglanti.Open();
            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM Kart WHERE userID=@id", baglanti);
            sorgu.Parameters.AddWithValue("@id", userData.id);
            
            OleDbDataReader dr = sorgu.ExecuteReader();

            while(dr.Read())
            {
                UserCards uc = new UserCards(dr.GetInt32(0), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                userCardList.Add(uc);
            }

            dr.Close();
            baglanti.Close();

            if(hsp != null)
                hsp.UpdateCardComboBox();

        }

        public void UpdateBalance()
        {
            userBalance = new UserBalance(userData.id, 0, 0, 0, 0);

            OleDbCommand sorgu = new OleDbCommand("SELECT * FROM Bakiye WHERE ID=@id", baglanti);
            sorgu.Parameters.AddWithValue("@id", userData.id);
            baglanti.Open();

            OleDbDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                userBalance.tl = dr.GetDouble(1);
                userBalance.usd = dr.GetDouble(2);
                userBalance.eur = dr.GetDouble(3);
                userBalance.btc = dr.GetDouble(4);
            }

            dr.Close();
            baglanti.Close();

            if (alst != null)
                alst.UpdateBalance();
        }

        public void AddBalance(int miktar, int paraCesidi)
        {
            string txt = "";
            if(paraCesidi == 0) // TL
            {
                userBalance.tl += miktar;
                txt = "TL";
            }
            else if(paraCesidi == 1) // USD
            {
                userBalance.usd += miktar;
                txt = "$";
            }
            else if (paraCesidi == 2) // EUR
            {
                userBalance.eur += miktar;
                txt = "€";
            }
            else if (paraCesidi == 3) // BTC
            {
                userBalance.btc += miktar;
                txt = "BTC";
            }

            OleDbCommand komut = new OleDbCommand("UPDATE Bakiye SET tl=@tl, usd=@usd, eur=@eur, btc=@btc WHERE ID=@id", baglanti);
            komut.Parameters.AddWithValue("@tl", userBalance.tl);
            komut.Parameters.AddWithValue("@usd", userBalance.usd);
            komut.Parameters.AddWithValue("@eur", userBalance.eur);
            komut.Parameters.AddWithValue("@btc", userBalance.btc);
            komut.Parameters.AddWithValue("@id", userData.id);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            alst.UpdateBalance();

            MessageBox.Show(miktar + txt + " başarıyla eklendi!", "TEBRİKLER");
        }

        public void ConvertCurrency(double istenenMiktar, int alinacak, int odemeTuru)
        {
            double parite = 0;
            double dusulecekMiktar = 0;

            string fromSymbol = "";
            string toSymbol = "";

            // TL
            if (odemeTuru == 0 && alinacak == 1) // TL -> USD
            {
                parite = usdSatis;
                dusulecekMiktar = parite * istenenMiktar;

                if(dusulecekMiktar > userBalance.tl)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!","UYARI");
                    return;
                }

                userBalance.tl -= dusulecekMiktar;
                userBalance.usd += istenenMiktar;

                fromSymbol = "TL";
                toSymbol = "$";
            }
            else if (odemeTuru == 0 && alinacak == 2) // TL -> EUR
            {
                parite = eurSatis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.tl)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.tl -= dusulecekMiktar;
                userBalance.eur += istenenMiktar;

                fromSymbol = "TL";
                toSymbol = "€";
            }
            else if (odemeTuru == 0 && alinacak == 3) // TL -> BTC
            {
                parite = btcSatis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.tl)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.tl -= dusulecekMiktar;
                userBalance.btc += istenenMiktar;

                fromSymbol = "TL";
                toSymbol = "BTC";
            }

            //USD
            else if (odemeTuru == 1 && alinacak == 0) // USD -> TL
            {
                parite = usdAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.usd)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.usd -= dusulecekMiktar;
                userBalance.tl += istenenMiktar;

                fromSymbol = "$";
                toSymbol = "TL";
            }
            else if (odemeTuru == 1 && alinacak == 2) // USD -> EUR
            {
                parite = eurSatis / usdAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.usd)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.usd -= dusulecekMiktar;
                userBalance.eur += istenenMiktar;

                fromSymbol = "$";
                toSymbol = "€";
            }
            else if (odemeTuru == 1 && alinacak == 3) // USD -> BTC
            {
                parite = btcSatis / usdAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.usd)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.usd -= dusulecekMiktar;
                userBalance.btc += istenenMiktar;

                fromSymbol = "$";
                toSymbol = "BTC";
            }

            //EUR
            else if (odemeTuru == 2 && alinacak == 0) // EUR -> TL
            {
                parite = eurAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.eur)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.eur -= dusulecekMiktar;
                userBalance.tl += istenenMiktar;

                fromSymbol = "€";
                toSymbol = "TL";
            }
            else if (odemeTuru == 2 && alinacak == 1) // EUR -> USD
            {
                parite = usdSatis / eurAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.eur)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.eur -= dusulecekMiktar;
                userBalance.usd += istenenMiktar;

                fromSymbol = "€";
                toSymbol = "$";
            }
            else if (odemeTuru == 2 && alinacak == 3) // EUR -> BTC
            {
                parite = btcSatis / eurAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.eur)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.eur -= dusulecekMiktar;
                userBalance.btc += istenenMiktar;

                fromSymbol = "€";
                toSymbol = "BTC";
            }

            //BTC
            else if (odemeTuru == 3 && alinacak == 0) // BTC -> TL
            {
                parite = btcAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.btc)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.btc -= dusulecekMiktar;
                userBalance.tl += istenenMiktar;

                fromSymbol = "BTC";
                toSymbol = "TL";
            }
            else if (odemeTuru == 3 && alinacak == 1) // BTC -> USD
            {
                parite = usdSatis / btcAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.btc)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.btc -= dusulecekMiktar;
                userBalance.usd += istenenMiktar;

                fromSymbol = "BTC";
                toSymbol = "$";
            }
            else if (odemeTuru == 3 && alinacak == 2) // BTC -> EUR
            {
                parite = eurSatis / btcAlis;
                dusulecekMiktar = parite * istenenMiktar;

                if (dusulecekMiktar > userBalance.btc)
                {
                    MessageBox.Show("Yeterli bakiyeniz bulunmamaktadır!", "UYARI");
                    return;
                }

                userBalance.btc -= dusulecekMiktar;
                userBalance.eur += istenenMiktar;

                fromSymbol = "BTC";
                toSymbol = "€";
            }

            OleDbCommand komut = new OleDbCommand("UPDATE Bakiye SET tl=@tl, usd=@usd, eur=@eur, btc=@btc WHERE ID=@id", baglanti);
            komut.Parameters.AddWithValue("@tl", userBalance.tl);
            komut.Parameters.AddWithValue("@usd", userBalance.usd);
            komut.Parameters.AddWithValue("@eur", userBalance.eur);
            komut.Parameters.AddWithValue("@btc", userBalance.btc);
            komut.Parameters.AddWithValue("@id", userData.id);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            alst.UpdateBalance();

            MessageBox.Show(dusulecekMiktar + fromSymbol + " karşılık " + istenenMiktar + toSymbol + " başarıyla alınmıştır!", "TEBRİKLER");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //kayitol kayit = new kayitol();
            if (kayit == null)
                kayit = new kayitol();

            kayit.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AcceptButton = button1;
           

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://static.coinpaper.io/api/coins.json");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            coinJson = new StreamReader(response.GetResponseStream()).ReadToEnd();



            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"https://api.genelpara.com/embed/doviz.json");

            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            currencyJson = new StreamReader(response2.GetResponseStream()).ReadToEnd();


            if (brs == null)
            {
                brs = new borsa();
                brs.formIns1 = this;

                brs.GetCoinList();
                brs.GetCurrencyList();
            }
        }

        public void ShowHesapScreen()
        {
            if (hsp == null)
                hsp = new hesap();

            hsp.formIns1 = this;

            hsp.Show();
            this.Hide();
        }

        public void ShowAlSatScreen()
        {
            if (alst == null)
                alst = new alsat();

            alst.formIns1 = this;

            alst.Show();
            this.Hide();
        }

        public void ShowBorsaScreen()
        {
            if (brs == null)
                brs = new borsa();

            brs.formIns1 = this;

            brs.Show();
            this.Hide();
        }

        public void ShowIletisimScreen()
        {
            if (ilts == null)
                ilts = new iletisim();

            ilts.formIns1 = this;

            ilts.Show();
            this.Hide();
        }

        public void ShowKartEkleScreen()
        {
            if (krt == null)
                krt = new kartekle();

            krt.formIns1 = this;

            krt.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Başarıyla çıkış yaptınız");

            Close();
        }
    }
}
