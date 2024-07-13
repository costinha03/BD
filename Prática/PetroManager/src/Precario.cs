using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroManager
{
    public class Precario
    {
        private int _combustivelID;
        private float _preco;
        private DateTime _dataInicio;
        private DateTime _dataFim;

        public int CombustivelID
        {
            get { return _combustivelID; }
            set { _combustivelID = value; }
        }

        public float Preco
        {
            get { return _preco; }
            set { _preco = value; }
        }

        public DateTime DataInicio
        {
            get { return _dataInicio; }
            set { _dataInicio = value; }
        }

        public DateTime DataFim
        {
            get { return _dataFim; }
            set { _dataFim = value; }
        }

        public override string ToString()
        {
            return $"CombustivelID: {CombustivelID}, Preco: {Preco}, DataInicio: {DataInicio.ToShortDateString()}, DataFim: {DataFim.ToShortDateString()}";
        }

        public Precario() : base()
        {
        }
    }
}
