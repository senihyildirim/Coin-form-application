using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace proje2
{
    public partial class borsa : Form
    {
        public borsa()
        {
            InitializeComponent();
        }

        public Form1 formIns1;

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

        private void borsa_Load(object sender, EventArgs e)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://static.coinpaper.io/api/coins.json");

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            /*
            string content = formIns1.coinJson;

            var coinList = JsonConvert.DeserializeObject<List<BorsaVeri>>(content);

            var dt = coinList.Take(22).ToList().ToDataTable<BorsaVeri>();

         
            coinGrid.DataSource = dt;

            coinGrid.Columns["image"].Visible = false;

            formIns1.btcAlis = double.Parse(coinGrid.Rows[0].Cells[5].Value.ToString());
            formIns1.btcSatis = double.Parse(coinGrid.Rows[0].Cells[5].Value.ToString());
            */

            //GetCoinList();
            //GetCurrencyList();

        }

        public void GetCoinList()
        {
            string content = formIns1.coinJson;

            var coinList = JsonConvert.DeserializeObject<List<BorsaVeri>>(content);

            var dt = coinList.Take(22).ToList().ToDataTable<BorsaVeri>();


            coinGrid.DataSource = dt;

            coinGrid.Columns["image"].Visible = false;

            formIns1.btcAlis = double.Parse(coinGrid.Rows[0].Cells[5].Value.ToString());
            formIns1.btcSatis = double.Parse(coinGrid.Rows[0].Cells[5].Value.ToString());
        }

        private void coinGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {/*
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
               if (coinGrid.Columns[e.ColumnIndex].Name.Contains("Sembol"))
                {
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(coinGrid.Rows[e.RowIndex].Cells["image"].Value.ToString());
                    myRequest.Method = "GET";
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(myResponse.GetResponseStream());
                    myResponse.Close();

                    Image imgForGridCell = (Image)bmp;

                    if (imgForGridCell != null)
                    {
                        SolidBrush gridBrush = new SolidBrush(coinGrid.GridColor);
                        Pen gridLinePen = new Pen(gridBrush);
                        SolidBrush backColorBrush = new SolidBrush(e.CellStyle.BackColor);
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        // Draw lines over cell  
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        // Draw the image over cell at specific location.  
                        e.Graphics.DrawImage(imgForGridCell, e.CellBounds.Location);
                        coinGrid.Rows[e.RowIndex].Cells["Sembol"].ReadOnly = true; // make cell readonly so below text will not dispaly on double click over cell.  
                    }
                    e.Handled = true;
                }
                
            }
            */
        }

        public void GetCurrencyList()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.genelpara.com/embed/doviz.json");

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

            string content = formIns1.currencyJson;

            JObject  currencyList = JsonConvert.DeserializeObject<JObject>(content);

            formIns1.usdAlis = currencyList["USD"]["alis"].Value<double>();
            formIns1.usdSatis = currencyList["USD"]["satis"].Value<double>();

            formIns1.eurAlis = currencyList["EUR"]["alis"].Value<double>();
            formIns1.eurSatis = currencyList["EUR"]["satis"].Value<double>();

            usdAlisLabel.Text = formIns1.usdAlis + "TL";
            usdSatisLabel.Text = formIns1.usdSatis + "TL";

            eurAlisLabel.Text = formIns1.eurAlis + "TL";
            eurSatisLabel.Text = formIns1.eurSatis + "TL";

            formIns1.btcAlis *= formIns1.usdAlis;
            formIns1.btcSatis *= formIns1.usdSatis;

            btcAlisLabel.Text = formIns1.btcAlis + "TL";
            btcSatisLabel.Text = formIns1.btcSatis + "TL";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Başarıyla çıkış yaptınız");

            formIns1.Close();
        }
    }

}
