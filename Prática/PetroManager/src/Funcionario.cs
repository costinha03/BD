using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroManager
{
    class Funcionario
    {
        private string _Nome;
        private string _Email;
        private string _Contacto;
        private string _Salario;
        private string _Cidade;


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

        public string Salario
        {
            get { return _Salario; }
            set { _Salario = value; }
        }

        public string Cidade
        {
            get { return _Contacto; }
            set { _Cidade = value; }
        }



        public override string ToString()
        {
            return $"{Nome}  {Email}  {Contacto}";
        }

        public Funcionario() : base()
        {
        }
    }
}
