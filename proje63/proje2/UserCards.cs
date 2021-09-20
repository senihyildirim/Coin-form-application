using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class UserCards
    {
        public int id;
        public string kartNo;
        public string ay;
        public string yil;
        public string cvv;

        public UserCards()
        {

        }

        public UserCards(int _id, string _kartNo, string _ay, string _yil, string _cvv)
        {
            id = _id;
            kartNo = _kartNo;
            ay = _ay;
            yil = _yil;
            cvv = _cvv;
        }
    }
}
