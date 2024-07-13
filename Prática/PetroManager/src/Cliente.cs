using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroManager
{
    class Cliente
    {
        private string _Nome;
        private string _NIF;
        private string _Contacto;
        private string _Idade;

        public string Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public string NIF
        {
            get { return _NIF; }
            set { _NIF = value; }
        }

        public string Idade
        {
            get { return _Idade; }
            set { _Idade = value; }
        }

        public override string ToString()
        {
            return $" {NIF}         {Nome}";
        }

        public Cliente() : base()
        {
        }
    }
}
