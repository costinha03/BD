using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroManager
{
    class Posto2
    {
        private string _ID;
        private string _cidade;
        private string _contacto;
        private string _horaAbertura;
        private string _horaFecho;
        private string _MgrID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        public string Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }

        public string HoraAbertura
        {
            get { return _horaAbertura; }
            set { _horaAbertura = value; }
        }

        public string HoraFecho
        {
            get { return _horaFecho; }
            set { _horaFecho = value; }
        }

        public string MgrID
        {
            get { return _MgrID; }
            set { _MgrID = value; }
        }

       
        public Posto2() : base()
        {
        }
    }
}
