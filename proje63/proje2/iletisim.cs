using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace proje2
{
    public partial class iletisim : Form
    {
        public iletisim()
        {
            InitializeComponent();
        }

        public Form1 formIns1;

        private void button5_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("E-mail alanı boş kalamaz!");
            }
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("gaziprojecoin@gmail.com", "gazicoin123");
            istemci.Port = 587;
            istemci.Host = "smtp.gmail.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(label13.Text);
            mesajim.From = new MailAddress("gaziprojecoin@gmail.com");
            mesajim.Subject = textBox5.Text;
            mesajim.Body = " Adı Soyadı: " + textBox3.Text + " " + textBox4.Text + Environment.NewLine + " Mail Adresi :" + textBox5.Text + Environment.NewLine + "Telefon numarası: " + textBox6.Text + " " + Environment.NewLine + "İçerik:" + Environment.NewLine + textBox7.Text;
            istemci.Send(mesajim);


            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            MessageBox.Show("Mailiniz başarı ile gönderildi");

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
            formIns1.ShowAlSatScreen();
            this.Hide();
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
            //Close();
        }

        private void iletisim_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Başarıyla çıkış yaptınız");

            formIns1.Close();
        }
    }
}
