using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace PetroManager
{
    class Deposito
    {
        private string _CapacidadeAtual;
        

        public string CapacidadeAtual
        {
            get { return _CapacidadeAtual; }
            set { _CapacidadeAtual = value; }
        }

       


        public override string ToString()
        {
            return $"{_CapacidadeAtual}";
        }

        public Deposito() : base()
        {
        }
    }
}
