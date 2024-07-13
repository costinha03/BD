using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroManager
{
     class Transacao
    {
        private string _transacaoID;
        private string _data;
        private string _combustivelID;
        private string _quantidade;
        private string _precoCombustivel;
        private string _totalCompra;
        private string _Nome;
        private string _Contacto;
        private string _NIF;
        private string _fNome;
        private string _funcID;

        public string TransacaoID
        {
            get { return _transacaoID; }
            set { _transacaoID = value; }
        }

        public string funcID
        {
            get { return _funcID; }
            set { _funcID = value; }
        }

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string CombustivelID
        {
            get { return _combustivelID; }
            set { _combustivelID = value; }
        }

        public string Quantidade
        {
            get { return _quantidade; }
            set { _quantidade = value; }
        }

        public string PrecoCombustivel
        {
            get { return _precoCombustivel; }
            set { _precoCombustivel = value; }
        }

        public string TotalCompra
        {
            get { return _totalCompra; }
            set { _totalCompra = value; }
        }

        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        public string Contacto
        {
            get { return _Contacto; }
            set { _Contacto = value; }
        }

        public string NIF
        {
            get { return _NIF; }
            set { _NIF = value; }
        }

        public string FuncionarioNome
        {
            get { return _fNome; }
            set { _fNome = value; }
        }

        public override string ToString()
        {
            return $"{TransacaoID.PadLeft(3, '0')}          {NIF}           {TotalCompra} €";
        }

        public Transacao() : base()
        {
        }
    }


}
