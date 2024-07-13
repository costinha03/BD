using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PetroManager
{
    public class Manager
    {
        private string _Nome;
        private string _Email;
        private string _Contacto;


        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }



        public override string ToString()
        {
            return $"{Nome}  {Email}  {Contacto}";
        }

        public Manager() : base()
        {
        }


    }
}
