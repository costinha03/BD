using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroManager
{
    class Func
    {
        private string _Nome;
        private string _ID;
        private string _PCidade;
        private string _PID;
        private string _salario;
        private string _email;
        private string _contacto;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string PCidade
        {
            get { return _PCidade; }
            set { _PCidade = value; }
        }

        public string Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }

        public string PID
        {
            get { return _PID; }
            set { _PID = value; }
        }

        public string Salario
        {
            get { return _salario; }
            set { _salario = value; }
        }



        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public override string ToString()
        {
            return $" {ID.PadLeft(3, '0')}          {Nome}";
        }

        public Func() : base()
        {
        }
    }
}
