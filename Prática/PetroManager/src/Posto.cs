using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PetroManager
{   public class Posto
    {
        private string _ID;
        private string _cidade;
        private string _contacto; 
        private string _horaAbertura; 
        private string _horaFecho;
        private string _MgrNome;
        private string _MgrEmail;
        private string _MgrContacto;
        private string _MgrID;

        public string MgrID
        {
            get { return _MgrID; }
            set { _MgrID = value; }
        }
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

        public string MgrNome
        {
            get { return _MgrNome; }
            set { _MgrNome = value; }
        }

        public string MgrEmail
        {
            get { return _MgrEmail; }
            set { _MgrEmail = value; }
        }

        public string MgrContacto
        {
            get { return _MgrContacto; }
            set { _MgrContacto = value; }
        }

        public override string ToString()
        {
            return $" {ID.PadLeft(3, '0')}          {Cidade}";
        }

        public Posto() : base()
        {
        }
            
       
    }
}
