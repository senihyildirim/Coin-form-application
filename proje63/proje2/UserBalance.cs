using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class UserBalance
    {
        public int id;
        public double tl;
        public double usd;
        public double eur;
        public double btc;

        public UserBalance()
        {

        }

        public UserBalance(int _id, double _tl, double _usd, double _eur, double _btc)
        {
            id = _id;
            tl = _tl;
            usd = _usd;
            eur = _eur;
            btc = _btc;
        }
    }
}
