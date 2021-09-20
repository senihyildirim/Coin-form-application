using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class UserData
    {
        public int id;
        public string kullAdi;
        public string kullSifre;
        public string eposta;
        public string tcno;
        public string telno;
        public string adSoyad;
        /*
        public string tl;
        public string dolar;
        public string euro;
        public string btc;
        public string isim;
        public string kartno;
        public string ay;
        public string yil;
        public string cvv;
        */
        public UserData()
        {
   
        }

        public UserData(int _id, string _kullAdi, string _kullSifre, string _eposta, string _tcno, string _telno, string _adSoyad)
        {
            id = _id;
            kullAdi = _kullAdi;
            kullSifre = _kullSifre;
            eposta = _eposta;
            tcno = _tcno;
            telno = _telno;
            adSoyad = _adSoyad;
        }
        /*
        public UserData(int _id, string _kullAdi, string _kullSifre, string _eposta, string _tcno, string _telno, string _adSoyad, string _tl, string _dolar, string _euro, string _btc, string _isim, string _kartno, string _ay, string _yil, string _cvv)
        {
            id = _id;
            kullAdi = _kullAdi;
            kullSifre = _kullSifre;
            eposta = _eposta;
            tcno = _tcno;
            telno = _telno;
            adSoyad = _adSoyad;
            tl = _tl;
            dolar = _dolar;
            euro = _euro;
            btc = _btc;
            isim = _isim;
            kartno = _kartno;
            ay = _ay;
            yil = _yil;
            cvv = _cvv;




        }
        */
        
    }
}
